using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkGeometryTriangle : IHavokObject
{
    public virtual uint Signature => 0;
    public int _a;
    public int _b;
    public int _c;
    public int _material;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _a = br.ReadInt32();
        _b = br.ReadInt32();
        _c = br.ReadInt32();
        _material = br.ReadInt32();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteInt32(_a);
        bw.WriteInt32(_b);
        bw.WriteInt32(_c);
        bw.WriteInt32(_material);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_a), _a);
        xs.WriteNumber(xe, nameof(_b), _b);
        xs.WriteNumber(xe, nameof(_c), _c);
        xs.WriteNumber(xe, nameof(_material), _material);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkGeometryTriangle other && _a == other._a && _b == other._b && _c == other._c && _material == other._material && Signature == other.Signature;
    }
    public static bool operator ==(hkGeometryTriangle? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkGeometryTriangle? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_a);
        code.Add(_b);
        code.Add(_c);
        code.Add(_material);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
