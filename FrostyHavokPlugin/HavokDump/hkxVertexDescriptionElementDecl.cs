using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxVertexDescriptionElementDecl : IHavokObject, IEquatable<hkxVertexDescriptionElementDecl?>
{
    public virtual uint Signature => 0;
    public uint _byteOffset;
    public hkxVertexDescription_DataType _type;
    public hkxVertexDescription_DataUsage _usage;
    public uint _byteStride;
    public byte _numElements;
    public hkxVertexDescription_DataHint _hint;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _byteOffset = br.ReadUInt32();
        _type = (hkxVertexDescription_DataType)br.ReadUInt16();
        _usage = (hkxVertexDescription_DataUsage)br.ReadUInt16();
        _byteStride = br.ReadUInt32();
        _numElements = br.ReadByte();
        br.Position += 1; // padding
        _hint = (hkxVertexDescription_DataHint)br.ReadUInt16();
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_byteOffset), _byteOffset);
        xs.WriteEnum<hkxVertexDescription_DataType, ushort>(xe, nameof(_type), (ushort)_type);
        xs.WriteEnum<hkxVertexDescription_DataUsage, ushort>(xe, nameof(_usage), (ushort)_usage);
        xs.WriteNumber(xe, nameof(_byteStride), _byteStride);
        xs.WriteNumber(xe, nameof(_numElements), _numElements);
        xs.WriteEnum<hkxVertexDescription_DataHint, ushort>(xe, nameof(_hint), (ushort)_hint);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxVertexDescriptionElementDecl);
    }
    public bool Equals(hkxVertexDescriptionElementDecl? other)
    {
        return other is not null && _byteOffset.Equals(other._byteOffset) && _type.Equals(other._type) && _usage.Equals(other._usage) && _byteStride.Equals(other._byteStride) && _numElements.Equals(other._numElements) && _hint.Equals(other._hint) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_byteOffset);
        code.Add(_type);
        code.Add(_usage);
        code.Add(_byteStride);
        code.Add(_numElements);
        code.Add(_hint);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
