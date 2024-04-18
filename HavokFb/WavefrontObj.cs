using hk;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HavokFb;

internal class WavefrontObj
{
    public struct Face
    {
        public List<int> _indices;


        public Face(params int[] indices) 
        {
            _indices = indices.ToList();
        }

        public void Add(int _vert)
        {
            _indices.Add(_vert);
        }
        public void Set(List<int> list)
        {
            _indices = list;
        }
        public void Offset(int amt)
        {
            if (amt == 0)
            {
                return;
            }
            for (int i = 0; i < _indices.Count; i++)
            {
                _indices[i] += amt;
            }
        }

        public List<Face> AsTris()
        {
            return new List<Face>();
        }

        public string Build()
        {
            string s = "";
            foreach (int i in _indices)
            {
                s += $"{i} ";
            }
            return s;
        }
        public IEnumerable<int> Get()
        {
            foreach (int i in _indices)
            {
                yield return i;
            }
        }

    }

    public List<Vector4> _vertices;
    public List<Face> _faces;

    public WavefrontObj()
    {
        _vertices = new List<Vector4>();
        _faces = new List<Face>();
    }

    public static List<WavefrontObj> ReadFromFile(string file) => Read(File.ReadAllText(file));
    public static List<WavefrontObj> Read(string text)
    {
        string[] parse = text.Split('\n');
        int pos = 0;
        bool alreadyHitObj = false;
        List<WavefrontObj> objects = new List<WavefrontObj>();
        WavefrontObj current = new WavefrontObj();

        while (pos < parse.Length)
        {
            string line = parse[pos++];
            string[] spaced = line.Split(' ');
            if (line.Length <= 2)
            {
                pos++;
                continue;
            }
            if (spaced[0].Length > 1)
            {
                continue;
            }
            switch (line[0])
            {
                case 'v':
                    {
                        float x = Convert.ToSingle(spaced[1]); 
                        float y = Convert.ToSingle(spaced[2]);
                        float z = Convert.ToSingle(spaced[3]);
                        float w = 0.5f;

                        // is Vec4
                        if (spaced.Length >= 5)
                        {
                            w = Convert.ToSingle(spaced[4]);
                        }
                        

                        current._vertices.Add(new Vector4(x, y, z, w));
                        break;
                    }
                case 'f':
                    {
                        string buff = line.Substring(2);

                        List<int> list = buff.Replace('/', ' ').Split(' ').Select((s) => Convert.ToInt32(s)).ToList();
                        Face face = new Face();
                        face.Set(list);
                        current._faces.Add(face);

                        break;
                    }
                case 'o':
                    {
                        // First object in .obj
                        if (!alreadyHitObj)
                        {
                            alreadyHitObj = true;
                            break;
                        }

                        objects.Add(current);

                        current = new WavefrontObj();
                        break;
                    }
                default:
                    pos++;
                    break;

            }
        }
        objects.Add(current);

        return objects;
    }

    public IEnumerable<List<Vector4>> GetFaces()
    {
        foreach (Face face in _faces)
        {
            List<Vector4> points = new List<Vector4>();
            foreach (int indice in face.Get())
            {
                points.Add(_vertices[indice - 1]);
            }
            yield return points;
        }
    }


    public static WavefrontObj Combine(List<WavefrontObj> list)
    {
        WavefrontObj obj = new WavefrontObj();

        foreach (WavefrontObj item in list)
        {
            int verticesCount = obj._vertices.Count;
            int facesCount = obj._faces.Count;

            obj._vertices.AddRange(item._vertices);
            obj._faces.AddRange(item._faces);
            //foreach (Face face in item._faces)
            //{
            //    //face.Offset(verticesCount);
            //    obj._faces.Add(face);
            //}
        }

        return obj;
    }

    public void ForceTris()
    {
        List<Face> newFaces = new List<Face>();

        foreach (Face face in _faces)
        {
            // More than 3 vertices
            if (face._indices.Count > 3)
            {
                List<Face> split = new List<Face>();

                for (int i = 1; i < face._indices.Count - 1; i++)
                {
                    split.Add(new Face(face._indices.First(), face._indices[i], face._indices[i + 1]));
                }
                newFaces.AddRange(split);

                continue;
            }

            // Is a tri, add as it is
            newFaces.Add(face);
        }
        _faces.Clear();
        _faces = newFaces;
    }

    public string Build(bool addTag = true)
    {
        StringBuilder sb = new StringBuilder();
        if (addTag)
        {
            sb.AppendLine("# AutoGenerated by Magix1484's ObjToHavok");
        }
        foreach (Vector4 v in _vertices)
        {
            sb.AppendLine($"v {v.X} {v.Y} {v.Z} {v.W}");
        }
        foreach (Face f in _faces)
        {
            sb.AppendLine($"f {f.Build()}");
        }
        return sb.ToString();
    }
}

internal static class WavefrontObjExtensions
{
    public static string Build(this List<WavefrontObj> objs, bool forceTris = false)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("# AutoGenerated by Magix1484's ObjToHavok");
        for (int i = 0; i < objs.Count; i++)
        {
            WavefrontObj obj = objs[i];
            sb.AppendLine($"o shape{i}");
            if (forceTris)
            {
                obj.ForceTris();
            }
            sb.AppendLine(obj.Build(false));
        }
        return sb.ToString();
    }
}
