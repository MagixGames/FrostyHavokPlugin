using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticTreeDynamicStoragehkcdStaticTreeCodec3Axis5 : IHavokObject
{
    public virtual uint Signature => 0;
    public List<hkcdStaticTreeCodec3Axis5?> _nodes = new();
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _nodes = des.ReadClassArray<hkcdStaticTreeCodec3Axis5>(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteClassArray<hkcdStaticTreeCodec3Axis5>(bw, _nodes);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray<hkcdStaticTreeCodec3Axis5>(xe, nameof(_nodes), _nodes);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkcdStaticTreeDynamicStoragehkcdStaticTreeCodec3Axis5 other && _nodes.SequenceEqual(other._nodes) && Signature == other.Signature;
    }
    public static bool operator ==(hkcdStaticTreeDynamicStoragehkcdStaticTreeCodec3Axis5? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdStaticTreeDynamicStoragehkcdStaticTreeCodec3Axis5? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_nodes);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
