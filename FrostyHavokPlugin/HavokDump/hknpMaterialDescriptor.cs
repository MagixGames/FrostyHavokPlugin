using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMaterialDescriptor : IHavokObject, IEquatable<hknpMaterialDescriptor?>
{
    public virtual uint Signature => 0;
    public string _name;
    public hknpRefMaterial _material;
    public ushort _materialId;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _name = des.ReadStringPointer(br);
        _material = des.ReadClassPointer<hknpRefMaterial>(br);
        _materialId = br.ReadUInt16();
        br.Position += 6; // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteClassPointer(xe, nameof(_material), _material);
        xs.WriteNumber(xe, nameof(_materialId), _materialId);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpMaterialDescriptor);
    }
    public bool Equals(hknpMaterialDescriptor? other)
    {
        return other is not null && _name.Equals(other._name) && _material.Equals(other._material) && _materialId.Equals(other._materialId) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_material);
        code.Add(_materialId);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
