using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkPackedVector3 : IHavokObject
{
    public virtual uint Signature => 0;
    public short[] _values = new short[4];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _values = des.ReadInt16CStyleArray(br, 4);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteInt16CStyleArray(bw, _values);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_values), _values);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkPackedVector3 other && _values == other._values && Signature == other.Signature;
    }
    public static bool operator ==(hkPackedVector3? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkPackedVector3? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_values);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
