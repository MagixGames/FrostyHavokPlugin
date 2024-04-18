using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.CommonTypes;
using hk;
using System.Numerics;

using Box3 = OpenTK.Mathematics.Box3;

namespace HavokFb;

internal class Program
{
    static void Main(string[] args)
    {
#if DEBUG
        // Dump

        //HavokTypeDumper.Dump(51128);

        //return;

        // Testing serialization

        string path = @"E:\_Downloads\test.obj";
        //HavokToObj(path);

        // ObjToHavok_1Shape(path);
        TestSerialize(path);
        /*
        string tempHavokPath = path + ".temp_havok";
        string tempMetaPath = path + ".temp_resmeta";

        HavokPhysicsData havok = new HavokPhysicsData();
        using (DataStream stream = new DataStream(File.OpenRead(tempHavokPath)))
        {
            havok.Deserialize(stream, new ReadOnlySpan<byte>(File.ReadAllBytes(tempMetaPath)));
        }

        havok.WriteToOBJ(path);
        */
        /*
        using (DataStream stream = new DataStream(File.OpenWrite(tempHavokPath + ".out")))
        {
            havok.Serialize(stream, new Span<byte>(File.ReadAllBytes(tempMetaPath)));
        }
        */


        return;
#endif
        switch (args[0])
        {
            case "--toObj":
                {
                    HavokToObj(args[1]);
                    break;
                }
            case "--toHavok":
                {
                    ObjToHavok_1Shape(args[1]);
                    break;
                }
            case "--testserialize":
                {
                    TestSerialize(args[1]); 
                    break;
                }
        }
    }



    public static void HavokToObj(string path)
    {
        // Temporary paths
        string tempHavokPath = path + ".temp_havok";
        string tempMetaPath = path + ".temp_resmeta";

        // Write obj
        HavokPhysicsData havok = new HavokPhysicsData();
        using (DataStream stream = new DataStream(File.OpenRead(tempHavokPath)))
        {
            havok.Deserialize(stream, new ReadOnlySpan<byte>(File.ReadAllBytes(tempMetaPath)));
        }
        havok.WriteToOBJ(path);

        // Make a combined version
        List<WavefrontObj> objs = WavefrontObj.Read(File.ReadAllText(path));
        File.WriteAllText(path.Replace(".obj", "") + ".combined.obj", WavefrontObj.Combine(objs).Build());
        File.WriteAllText(path.Replace(".obj", "") + ".combined_trid.obj", objs.Build(true));


        // Cleanup
        try
        {
            File.Delete(tempHavokPath);
            File.Delete(tempMetaPath);
        }
        catch { }
    }

    public static void TestSerialize(string path)
    {
        HavokPhysicsData asset = new HavokPhysicsData();

        string tempHavokPath = path + ".temp_havok";
        string tempMetaPath = path + ".temp_resmeta";
        string tempOutHavokPath = path + ".temp_outhavok";
        string tempOutMetaPath = path + ".temp_outresmeta";

        // Deserialize
        Span<byte> inResMeta = new Span<byte>(File.ReadAllBytes(tempMetaPath));
        using DataStream stream = new DataStream(File.OpenRead(tempHavokPath));
        asset.Deserialize(stream, inResMeta);
        asset.WriteToOBJ(path);

        // Serialize to file
        Span<byte> outResMeta = new Span<byte>(new byte[16]);
        using DataStream outStream = new DataStream(File.Create(tempOutHavokPath));
        asset.Serialize(outStream, outResMeta);
        File.WriteAllBytes(path + ".firstpackfile.bin", asset.m_firstPackFile!.ToSpan().ToArray());
        File.WriteAllBytes(tempOutMetaPath, outResMeta.ToArray());
    }


    public static HKXHeader CreateDefaultHeader()
    {
        return new HKXHeader()
        {
            BaseClass = 1,
            ContentsClassNameSectionIndex = 0,
            ContentsClassNameSectionOffset = 75,
            ContentsSectionIndex = 2,
            ContentsSectionOffset = 0,
            ContentsVersionString = "hk_2013.3.0-r1",
            Endian = 1,
            FileVersion = 11,
            Flags = 0,
            Magic0 = 1474355287,
            Magic1 = 281067536,
            MaxPredicate = 21,
            PaddingOption = 0,
            PointerSize = 8,
            SectionCount = 3,
            SectionOffset = 16,
            Unk40 = 20,
            Unk42 = 0,
            Unk44 = 0,
            Unk48 = 0,
            Unk4C = 0,
            UserTag = 0
        };
    }


    public static void ObjToHavok_1Shape(string path)
    {
        // Only read the first shape
        WavefrontObj objFile = WavefrontObj.ReadFromFile(path).First();

        // For now, enforce tris for the point of vector math normals
        // objFile.ForceTris();

        HavokPhysicsData asset = new HavokPhysicsData();
        asset.m_header = CreateDefaultHeader();
        asset.m_obj = new hkRootLevelContainer();
        asset.GetObject<hkRootLevelContainer>()._namedVariants = new List<hk.hkRootLevelContainerNamedVariant>() { new hk.hkRootLevelContainerNamedVariant() 
        { 
            _name = "PhysicsContainer",
            _className = "HavokPhysicsContainer",
            _variant = new hk.HavokPhysicsContainer(),
        } };

        var container = asset.GetObject<hkRootLevelContainer>()._namedVariants[0]._variant as hk.HavokPhysicsContainer;
        container._shapes = new List<hknpShape>();

        // Build StaticCompoundShape
        // ^ For now, we will just have a convex mesh as a result of not having the tech
        // to build proper compound shapes
        hknpConvexPolytopeShape shape = BuildConvexShape(objFile);
        container._shapes.Add(shape);
        asset.LocalAabbs.Add(GenAABB(shape._vertices));
        asset.PartCount = 1;
        asset.m_firstPackFile = new Frosty.Sdk.Utils.Block<byte>(16);
        BlockStream ts = new BlockStream(asset.m_firstPackFile);
        ts.Write([0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0]); // 16
        string fpfp = @"E:\_Downloads\test.obj.firstpackfile.bin";
        asset.m_firstPackFile = new Frosty.Sdk.Utils.Block<byte>(new Span<byte>(File.ReadAllBytes(fpfp)));

        asset.SerializeXML(path + ".dump.xml");

        Span<byte> outResMeta = new Span<byte>(new byte[16]);

        using DataStream outStream = new DataStream(File.Create(path + ".temp_out"));
        asset.Serialize(outStream, outResMeta);
        File.WriteAllBytes(path + ".temp_resmeta", outResMeta.ToArray());
    }

    public static hknpConvexPolytopeShape BuildConvexShape(WavefrontObj objFile)
    {
        hknpConvexPolytopeShape shape = new hknpConvexPolytopeShape();

        // Flags i found on multiple so ill just copy
        shape._flags = hknpShape_FlagsEnum.IS_CONVEX_SHAPE | hknpShape_FlagsEnum.IS_CONVEX_POLYTOPE_SHAPE | hknpShape_FlagsEnum.USE_SMALL_FACE_INDICES | hknpShape_FlagsEnum.USE_NORMAL_TO_FIND_SUPPORT_PLANE;

        shape._dispatchType = hknpCollisionDispatchType_Enum.CONVEX;
        shape._userData = 0; // or 0
        // Hopefully aren't nessessary

        shape._planes = new List<OpenTK.Mathematics.Vector4>();
        shape._faces = new List<hknpConvexPolytopeShapeFace>();
        shape._indices = new List<byte>();

        // Build planes
        foreach (List<Vector4> vertices in objFile.GetFaces())
        {
            Vector4 plane = GetNormal(vertices);

            float distance = 0.0f;
            for (int i = 0; i < vertices.Count; i++)
            {
                Vector3 vertex = vertices[i].ToVec3();

                distance += -Vector3.Dot(vertex, plane.ToVec3());

            }
            distance /= objFile._faces.Count;

            plane.W = distance;

            shape._planes.Add(plane.ToOpenTK());
        }

        // Make `verticesPerFace` & `indices` all for calculating the minAngle, of which
        // I still have no idea how it works!

        List<int> verticesPerFace = new List<int>();
        List<int> indices = new List<int>();
        foreach (WavefrontObj.Face f in objFile._faces)
        {
            int startIndex = indices.Count;
            int length = f._indices.Count;
            verticesPerFace.Add(length);
            indices.AddRange(f._indices);
        }

        // Build vertices
        // Notes: vert.W has index info for whatever reason
        shape._vertices = new List<OpenTK.Mathematics.Vector4>();
        for (int i = 0; i < objFile._vertices.Count; i++)
        {
            shape._vertices.Add(new Vector4(objFile._vertices[i].ToVec3(), BitConverter.ToSingle(BitConverter.GetBytes(i | 0x3f000000))).ToOpenTK());
        }

        int maxNumEdgesPerFaces = 0;
        int indicesOffset = 0;

        // Indices, vertices, indices
        for (int i = 0; i < objFile._faces.Count; i++)
        {
            WavefrontObj.Face f = objFile._faces[i];
            
            // Add indices
            foreach (int indice in f.Get())
            {
                shape._indices.Add((byte)indice);
            }

            int startIndex = indicesOffset;
            int length = f._indices.Count;

            maxNumEdgesPerFaces = Math.Max(maxNumEdgesPerFaces, length);

            // Build face
            hknpConvexPolytopeShapeFace face = new hknpConvexPolytopeShapeFace();
            face._firstIndex = (ushort)startIndex;
            face._numIndices = (byte)length;

            // Build Min Edge Angle for face

            // Truthfully, I have no fucking idea what is going on here!
            
            float maxCosAngle = 0f;
            for (int j = length - 1, k = 0; k < length; j=k++) // meant to be j < length
            {
                try
                {
                    int fIndex = hknpConvexPolytopeShape__findFace(indices[startIndex + k], indices[startIndex + j], verticesPerFace, indices);
                    maxCosAngle = Math.Max(maxCosAngle, Vector3.Dot(shape._planes[i].ToNumerics().ToVec3(), shape._planes[fIndex].ToNumerics().ToVec3()));
                }
                catch
                {

                }
            }
            face._minHalfAngle = (byte)hknpMotionUtil__convertAngleToAngularTIM((float)(Math.Acos(maxCosAngle) * 0.5));
            
            // face._minHalfAngle = 1;

            shape._faces.Add(face);
            indicesOffset += length;
        }

        // Enables bplane collisions, only works with 4 edges
        if (maxNumEdgesPerFaces == 4)
        {
            //shape._flags |= hknpShape_FlagsEnum.SUPPORTS_BPLANE_COLLISIONS;
            if (maxNumEdgesPerFaces == 3)
            {
                // shape._flags |= hknpShape_FlagsEnum.CONTAINS_ONLY_TRIANGLES;
            }
        }

        // Apply padding

        // Pad vertices
        while ((shape._vertices.Count & 3) != 0)
        {
            shape._vertices.Add(shape._vertices.Last());
        }

        // Pad planes
        int numFaces = verticesPerFace.Count;
        int numFacesMin4 = Math.Min(numFaces, 4);
        while ((verticesPerFace.Count & 3) != 0)
        {
            shape._planes.Add(new OpenTK.Mathematics.Vector4(0, 0, 0, float.MinValue));
            verticesPerFace.Add(0);
        }

        // Build Mass Properties !!!!

        // Test putting properties from a square :troll:
        {
            shape._properties = new hkRefCountedProperties();
            shape._properties._entries = new List<hkRefCountedPropertiesEntry>();
            hknpShapeMassProperties massProperties = new hknpShapeMassProperties();
            shape._properties._entries.Add(new hkRefCountedPropertiesEntry() { _object = massProperties, _flags = 0, _key = 61698 });
            massProperties._compressedMassProperties = new hkCompressedMassProperties()
            {
                _centerOfMass = [0, -1102, 18183, 12800],
                _inertia = [1969, 24249, 22797, 13440],
                _majorAxisSpace = [-32768, -32768, -32768, -2768],
                _mass = 28.836905f,
                _volume = 28.836905f,
            };
        }

        // Actual building using AABB method from source
        /*
        {
            Box3 aabb = GenAABB(shape._vertices);

            Vector4 halfExtents;
            {
                //halfExtents = aabb.Max - aabb.Min;
            }

            shape._properties = new hkRefCountedProperties();
            shape._properties._entries = new List<hkRefCountedPropertiesEntry>();
            hknpShapeMassProperties massProperties = new hknpShapeMassProperties();
            shape._properties._entries.Add(new hkRefCountedPropertiesEntry() { _object = massProperties, _flags = 0, _key = 61698 });
            massProperties._compressedMassProperties = new hkCompressedMassProperties()
            {
                _centerOfMass = [0, -1102, 18183, 12800],
                _inertia = [1969, 24249, 22797, 13440],
                _majorAxisSpace = [-32768, -32768, -32768, -2768],
                _mass = 28.836905f,
                _volume = 28.836905f,
            };
        }*/



        return shape;
    }

    public static Vector3 GetNormal(Vector3 v0, Vector3 v1, Vector3 v2)
    {
        return Vector3.Cross(v0 - v1, v2 - v0);
    }

    public static Vector3 GetQuadNormal(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
    {
        return Vector3.Normalize((GetNormal(v0, v1, v2) + GetNormal(v1, v2, v3)) / 2);
    }

    public static Box3 GenAABB(List<Vector3> vertices)
    {
        Vector3 min = Vector3.Zero;
        Vector3 max = Vector3.Zero;

        foreach (Vector3 vec in vertices)
        {
            min = Vector3.Min(vec, min);
            max = Vector3.Max(vec, max);
        }

        return new Box3(min.ToOpenTK(), max.ToOpenTK());
    }

    public static Box3 GenAABB(List<Vector4> vertices)
    {
        Vector3 min = Vector3.Zero;
        Vector3 max = Vector3.Zero;

        foreach (Vector4 vec in vertices)
        {
            min = Vector3.Min(vec.ToVec3(), min);
            max = Vector3.Max(vec.ToVec3(), max);
        }

        return new Box3(min.ToOpenTK(), max.ToOpenTK());
    }

    public static Box3 GenAABB(List<OpenTK.Mathematics.Vector4> vertices)
    {
        Vector3 min = Vector3.Zero;
        Vector3 max = Vector3.Zero;

        foreach (OpenTK.Mathematics.Vector4 vec in vertices)
        {
            min = Vector3.Min(vec.ToNumerics().ToVec3(), min);
            max = Vector3.Max(vec.ToNumerics().ToVec3(), max);
        }

        return new Box3(min.ToOpenTK(), max.ToOpenTK());
    }

    public static Vector3 GetNormal(List<Vector3> vects)
    {
        if (vects.Count < 3)
        {
            throw new InvalidDataException("Too little vectors");
        }
        if (vects.Count == 3)
        {
            // Tri, just do normal vect math
            return GetNormal(vects[0], vects[1], vects[2]);
        }
        Vector3 normal = new Vector3();

        for (int i = 1; i < vects.Count - 1; i++)
        {
            normal += GetNormal(vects.First(), vects[i], vects[i + 1]);
        }

        return Vector3.Normalize(normal / (vects.Count - 2));
    }

    public static Vector4 GetNormal(List<Vector4> vects)
    {
        List<Vector3> n = new List<Vector3>();
        vects.ForEach(vect => n.Add(vect.ToVec3()));
        return GetNormal(n).ToVec4(-1f);
    }


    #region Ports

    // Generates a complete mesh from just vertices !!
    // Its >1000 lines of c++ so i dont wanna deal with it
    internal hknpConvexPolytopeShape hknpConvexPolytopeShape_createFromVerticesInternal(List<float> verticesIn, float radius, BuildConfig buildConfig)
    {
        if ((verticesIn.Count <= 0) || (radius < 0.0f))
        {
            throw new ArgumentException();
        }

        return new hknpConvexPolytopeShape();
    }


    internal struct BuildConfig
    {
        /// <summary>
        /// If true, try lower dimensional convex hull if 3d fails (default: false).
        /// </summary>
        public bool m_allowLowerDimensions;

        /// <summary>
        /// If true, attempt to compute a projection plane for 3d convex hull (default: false).
        /// </summary>
        public bool m_alwaysComputeProjectionPlane;

        /// <summary>
        /// If the dot product between two neighboring planes is greater than this value, the planes are merged (default: HKGPCONVEXHULL_DEFAULT_MIN_COS_ANGLE).
        /// </summary>
        public float m_minCosAngle;

        /// <summary>
        /// Set source vertex indices using hkVector4::setInt24W (default: false).
        /// </summary>
        public bool m_setSourceIndices;

        /// <summary>
        /// Automatically call buildIndices using m_minCosAngle (default: true).
        /// </summary>
        public bool m_buildIndices;

        /// <summary>
        /// Automatically call buildMassProperties (default: false).
        /// </summary>
        public bool m_buildMassProperties;

        /// <summary>
        /// Sort inputs to improve performance (in most cases, pathological cases like cylinders can become slower) (default: true).
        /// </summary>
        public bool m_sortInputs;

        /// <summary>
        /// Do not process inputs in any way, so that SOURCE_VERTICES == INTERNAL_VERTICES hold true (default: false).
        /// </summary>
        public bool m_internalInputs;

        /// <summary>
        /// Check for degenerated mass properties (default: true if HK_DEBUG is defined else false).
        /// </summary>
        public bool m_checkForDegeneratedMassProperties;

        /// <summary>
        /// Internally, the algorithm might modify input coordinates to ensure numerically robust results.
        /// As a result, the plane equations might not exactly confine the input points set.
        /// Setting this value to true will make sure the plane equations enclose both sources and internals points representations. (default: false).
        /// </summary>
        public bool m_ensurePlaneEnclosing;
    }

    public static int hknpConvexPolytopeShape__findFace(int a, int b, List<int> vpf, List<int> indices)
    {
        int indicesOffset = 0;
        for (int i = 0; i < vpf.Count; ++i)
        {
            int numIndices = vpf[i];
            for (int j = numIndices - 1, k = 0; k < numIndices; j = k++)
            {
                if ((indices[indicesOffset + j] == a && indices[indicesOffset + k] == b))
                {
                    return i;
                }
            }
            indicesOffset += numIndices;
        }
        throw new Exception("Fucked up finding face");
        return -1;
    }

    public static int hknpMotionUtil__convertAngleToAngularTIM(float angle)
    {
        return Math.Clamp((int)(angle * 510 / Math.PI + 0.5f), 0, 255);
    }


    #endregion
}

public static class VectorExtensions
{
    public static Vector3 ToVec3(this Vector4 v) => new Vector3(v.X, v.Y, v.Z);
    public static Vector4 ToVec4(this Vector3 v, float w) => new Vector4(v.X, v.Y, v.Z, w);

    public static OpenTK.Mathematics.Vector4 ToOpenTK(this System.Numerics.Vector4 vec) => new OpenTK.Mathematics.Vector4(vec.X, vec.Y, vec.Z, vec.W);
    public static OpenTK.Mathematics.Vector3 ToOpenTK(this System.Numerics.Vector3 vec) => new OpenTK.Mathematics.Vector3(vec.X, vec.Y, vec.Z);

    public static System.Numerics.Vector4 ToNumerics(this OpenTK.Mathematics.Vector4 vec) => new System.Numerics.Vector4(vec.X, vec.Y, vec.Z, vec.W);
    public static System.Numerics.Vector3 ToOpenTK(this OpenTK.Mathematics.Vector3 vec) => new System.Numerics.Vector3(vec.X, vec.Y, vec.Z);

}