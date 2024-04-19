using System.Buffers.Binary;
using System.Diagnostics;
using Frosty.Sdk;
using Frosty.Sdk.IO;
using Frosty.Sdk.Utils;
using FrostyHavokPlugin.CommonTypes;
using FrostyHavokPlugin.Interfaces;
using FrostyHavokPlugin.Utils;
using hk;
using OpenTK.Mathematics;

namespace FrostyHavokPlugin;

public struct MaterialDecl
{
    public uint Packed;

    public static implicit operator uint(MaterialDecl value) => value.Packed;

    public static implicit operator MaterialDecl(uint value) => new() { Packed = value };

    public ushort PhysicsPropertyIndex => (ushort)((Packed >> 20) & 0xFF);
    public ushort PhysicsMaterialIndex => (ushort)((Packed >> 12) & 0xFF);
    public ushort Flags => (ushort)((Packed >> 4) & 0xFF);
}

public class HavokPhysicsData
{

    public static float FrostbiteVersion = 1002017;
    public int PartCount { get; private set; }
    public List<Vector3> PartTranslations { get; set; } = new();
    public List<Box3> LocalAabbs { get; set; } = new();
    public List<byte> MaterialIndices { get; set; } = new(); // map PartIndex to MaterialIndex
    public List<MaterialDecl> MaterialFlagsAndIndices { get; set; } = new(); // map MaterialIndex to MaterialFlag(MaterialDecl) -> MaterialCount = MaterialFlagsAndIndices.Count - 1
    public List<ushort> DetailResourceIndices { get; set; } = new();
    public byte MaterialCountUsed { get; set; }
    public byte HighestMaterialIndex { get; set; }


    public Block<byte>? m_firstPackFile;
    public HKXHeader? m_header;
    public IHavokObject? m_obj;

    public T GetObject<T>() where T : IHavokObject, new() => (T) m_obj!;

    public void Deserialize(DataStream inStream, ReadOnlySpan<byte> inResMeta)
    {
        uint packFilesOffset = BinaryPrimitives.ReadUInt16LittleEndian(inResMeta.Slice(0, 2));
        uint firstPackFileSize = BinaryPrimitives.ReadUInt32LittleEndian(inResMeta.Slice(4, 4));
        uint secondPackFileSize = BinaryPrimitives.ReadUInt32LittleEndian(inResMeta.Slice(8, 4));
        uint fixupTablesSize = BinaryPrimitives.ReadUInt32LittleEndian(inResMeta.Slice(12, 4));

        PartCount = inStream.ReadInt32();

        int count = inStream.ReadInt32();
        PartTranslations.EnsureCapacity(count);
        inStream.ReadRelocPtr(stream =>
        {
            for (int i = 0; i < count; i++)
            {
                PartTranslations.Add(stream.ReadVector3());
            }
        });


        count = inStream.ReadInt32();
        LocalAabbs.EnsureCapacity(count);
        inStream.ReadRelocPtr(stream =>
        {
            for (int i = 0; i < count; i++)
            {
                LocalAabbs.Add(stream.ReadAabb());
            }
        });

        count = inStream.ReadInt32();
        MaterialIndices.EnsureCapacity(count);
        inStream.ReadRelocPtr(stream =>
        {
            for (int i = 0; i < count; i++)
            {
                MaterialIndices.Add(stream.ReadByte());
            }
        });

        count = inStream.ReadInt32();
        MaterialFlagsAndIndices.EnsureCapacity(count);
        inStream.ReadRelocPtr(stream =>
        {
            uint flags = 0;
            for (int i = 0; i < count; i++)
            {
                MaterialFlagsAndIndices.Add(stream.ReadUInt32());
                if (i != count - 1)
                {
                    flags |= MaterialFlagsAndIndices.Last().Flags;
                }
                else
                {
                    Debug.Assert(flags == MaterialFlagsAndIndices.Last().Flags);
                }
            }
        });

        // TODO: check when they added this
        if (FrostbiteVersion > 2016)
        {
            count = inStream.ReadInt32();
            DetailResourceIndices.EnsureCapacity(count);
            inStream.ReadRelocPtr(stream =>
            {
                for (int i = 0; i < count; i++)
                {
                    DetailResourceIndices.Add(stream.ReadUInt16());
                }
            });
        }

        MaterialCountUsed = inStream.ReadByte();
        HighestMaterialIndex = inStream.ReadByte();

        inStream.Position = packFilesOffset + firstPackFileSize + secondPackFileSize;
        int firstFixupTableSize = inStream.ReadInt32();
        int secondFixupTableSize = inStream.ReadInt32();
        uint fixupTablesOffset = (uint)inStream.Position;

        // reloc table after fixup table

        inStream.Position = packFilesOffset + firstPackFileSize;

        PackFileDeserializer deserializer = new();

        m_obj = deserializer.Deserialize(inStream, (uint)(fixupTablesOffset + firstFixupTableSize));
        m_header = deserializer.Header;
    }

    public void WriteToOBJ(string filePath)
    {
         hkRootLevelContainer? root = m_obj as hkRootLevelContainer;
         /*hkRootLevelContainer? root = m_obj as hkRootLevelContainer;

         HavokPhysicsContainer? container = root!._namedVariants[0]._variant as HavokPhysicsContainer;

         int current = 0;

         ObjWriter writer = new();
         foreach (hknpShape shape in container!._shapes)
         {
             shape.Export(writer, $"{current++}");
         }

        writer.WriteToFile(filePath);
         current = 0;
         foreach (Box3 aabb in LocalAabbs)
         {
             aabb.CreateAabb($"localaabb-{current++}", writer);
         }

        writer.WriteToFile("/home/jona/havok.obj");*/

    }

    public void Serialize(DataStream inStream, Span<byte> inResMeta)
    {
        // write frostbite stuff
        inStream.WriteInt32(PartCount);

        inStream.WriteInt32(PartTranslations.Count);
        inStream.WriteRelocPtr(nameof(PartTranslations));
        inStream.WriteInt32(LocalAabbs.Count);
        inStream.WriteRelocPtr(nameof(LocalAabbs));
        inStream.WriteInt32(MaterialIndices.Count);
        inStream.WriteRelocPtr(nameof(MaterialIndices));
        inStream.WriteInt32(MaterialFlagsAndIndices.Count);
        inStream.WriteRelocPtr(nameof(MaterialFlagsAndIndices));

        if (FrostbiteVersion > 2016)
        {
            inStream.WriteInt32(DetailResourceIndices.Count);
            inStream.WriteRelocPtr(nameof(DetailResourceIndices));
        }

        inStream.WriteByte(MaterialCountUsed);
        inStream.WriteByte(HighestMaterialIndex);

        inStream.Pad(16);

        inStream.AddRelocData(nameof(PartTranslations));
        foreach (Vector3 translation in PartTranslations)
        {
            inStream.WriteVector3(translation);
        }

        inStream.AddRelocData(nameof(LocalAabbs));
        foreach (Box3 aabb in LocalAabbs)
        {
            inStream.WriteAabb(aabb);
        }

        inStream.AddRelocData(nameof(MaterialIndices));
        foreach (byte index in MaterialIndices)
        {
            inStream.WriteByte(index);
        }
        inStream.Pad(16);

        inStream.AddRelocData(nameof(MaterialFlagsAndIndices));
        foreach (uint value in MaterialFlagsAndIndices)
        {
            inStream.WriteUInt32(value);
        }
        inStream.Pad(16);

        if (FrostbiteVersion > 2016)
        {
            inStream.AddRelocData(nameof(DetailResourceIndices));
            foreach (ushort index in DetailResourceIndices)
            {
                inStream.WriteUInt16(index);
            }
            inStream.Pad(16);
        }

        // write packfilesOffset to meta
        BinaryPrimitives.WriteUInt16LittleEndian(inResMeta.Slice(0, 2), (ushort)inStream.Position);

        PackFileSerializer serializer = new();
        using Block<byte> data = new(0);
        using Block<byte> fixupTable = new(0);
        using (BlockStream fixupTableStream = new(fixupTable, true))
        using (BlockStream stream = new(data, true))
        {
            serializer.Serialize(m_obj!, stream, fixupTableStream, m_header!);
        }

        // we are just writing the 64 bit one twice
        inStream.Write(data);
        BinaryPrimitives.WriteInt32LittleEndian(inResMeta.Slice(4, 4), data.Size);

        inStream.Write(data);
        BinaryPrimitives.WriteInt32LittleEndian(inResMeta.Slice(8, 4), data.Size);

        long fixupOffset = inStream.Position;
        inStream.WriteInt32(fixupTable.Size);
        inStream.WriteInt32(fixupTable.Size);
        inStream.Write(fixupTable);
        inStream.Write(fixupTable);

        inStream.WriteRelocTable();
        BinaryPrimitives.WriteUInt32LittleEndian(inResMeta.Slice(12, 4), (uint)(inStream.Position - fixupOffset));

        // set packfile types
        inResMeta[2] = 1;
        inResMeta[3] = 1;
    }

    public void SerializeXML(string path)
    {
        XmlSerializer serializer = new();
        using FileStream stream = File.Create(path);
        serializer.Serialize(m_obj!, m_header!, stream);
    }
}