using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicTreeDynamicStorage16 : hkcdDynamicTreeDefaultDynamicStoragehkcdDynamicTreeCodec32, IEquatable<hkcdDynamicTreeDynamicStorage16?>
{
    public override uint Signature => 0;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdDynamicTreeDynamicStorage16);
    }
    public bool Equals(hkcdDynamicTreeDynamicStorage16? other)
    {
        return other is not null && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(Signature);
        return code.ToHashCode();
    }
}
