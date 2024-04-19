using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicTreeDynamicStorage0hkcdDynamicTreeAnisotropicMetrichkcdDynamicTreeCodecRawUint : hkcdDynamicTreeAnisotropicMetric
{
    public override uint Signature => 0;
    public List<hkcdDynamicTreeCodecRawUint?> _nodes = new();
    public uint _firstFree;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _nodes = des.ReadClassArray<hkcdDynamicTreeCodecRawUint>(br);
        _firstFree = br.ReadUInt32();
        br.Position += 3; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassArray<hkcdDynamicTreeCodecRawUint>(bw, _nodes);
        bw.WriteUInt32(_firstFree);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hkcdDynamicTreeCodecRawUint>(xe, nameof(_nodes), _nodes);
        xs.WriteNumber(xe, nameof(_firstFree), _firstFree);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkcdDynamicTreeDynamicStorage0hkcdDynamicTreeAnisotropicMetrichkcdDynamicTreeCodecRawUint other && base.Equals(other) && _nodes.SequenceEqual(other._nodes) && _firstFree == other._firstFree && Signature == other.Signature;
    }
    public static bool operator ==(hkcdDynamicTreeDynamicStorage0hkcdDynamicTreeAnisotropicMetrichkcdDynamicTreeCodecRawUint? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdDynamicTreeDynamicStorage0hkcdDynamicTreeAnisotropicMetrichkcdDynamicTreeCodecRawUint? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_nodes);
        code.Add(_firstFree);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
