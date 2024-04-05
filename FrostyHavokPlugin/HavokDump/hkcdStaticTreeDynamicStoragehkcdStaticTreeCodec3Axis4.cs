using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticTreeDynamicStoragehkcdStaticTreeCodec3Axis4 : IHavokObject, IEquatable<hkcdStaticTreeDynamicStoragehkcdStaticTreeCodec3Axis4?>
{
    public virtual uint Signature => 0;
    public List<hkcdStaticTreeCodec3Axis4> _nodes;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _nodes = des.ReadClassArray<hkcdStaticTreeCodec3Axis4>(br);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray<hkcdStaticTreeCodec3Axis4>(xe, nameof(_nodes), _nodes);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdStaticTreeDynamicStoragehkcdStaticTreeCodec3Axis4);
    }
    public bool Equals(hkcdStaticTreeDynamicStoragehkcdStaticTreeCodec3Axis4? other)
    {
        return other is not null && _nodes.Equals(other._nodes) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_nodes);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
