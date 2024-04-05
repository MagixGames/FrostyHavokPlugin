using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxAnimatedQuaternion : hkReferencedObject, IEquatable<hkxAnimatedQuaternion?>
{
    public override uint Signature => 0;
    public List<float> _quaternions;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _quaternions = des.ReadSingleArray(br);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloatArray(xe, nameof(_quaternions), _quaternions);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxAnimatedQuaternion);
    }
    public bool Equals(hkxAnimatedQuaternion? other)
    {
        return other is not null && _quaternions.Equals(other._quaternions) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_quaternions);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
