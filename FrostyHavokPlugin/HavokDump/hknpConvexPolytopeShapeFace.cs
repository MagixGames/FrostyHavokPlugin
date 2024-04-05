using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpConvexPolytopeShapeFace : IHavokObject, IEquatable<hknpConvexPolytopeShapeFace?>
{
    public virtual uint Signature => 0;
    public byte _minHalfAngle;
    public byte _numIndices;
    public ushort _firstIndex;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _minHalfAngle = br.ReadByte();
        _numIndices = br.ReadByte();
        _firstIndex = br.ReadUInt16();
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_minHalfAngle), _minHalfAngle);
        xs.WriteNumber(xe, nameof(_numIndices), _numIndices);
        xs.WriteNumber(xe, nameof(_firstIndex), _firstIndex);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpConvexPolytopeShapeFace);
    }
    public bool Equals(hknpConvexPolytopeShapeFace? other)
    {
        return other is not null && _minHalfAngle.Equals(other._minHalfAngle) && _numIndices.Equals(other._numIndices) && _firstIndex.Equals(other._firstIndex) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_minHalfAngle);
        code.Add(_numIndices);
        code.Add(_firstIndex);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
