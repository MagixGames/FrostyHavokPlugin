using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpCircularSurfaceVelocity : hknpSurfaceVelocity, IEquatable<hknpCircularSurfaceVelocity?>
{
    public override uint Signature => 0;
    public bool _velocityIsLocalSpace;
    public Vector4 _pivot;
    public Vector4 _angularVelocity;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _velocityIsLocalSpace = br.ReadBoolean();
        br.Position += 15; // padding
        _pivot = des.ReadVector4(br);
        _angularVelocity = des.ReadVector4(br);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteBoolean(xe, nameof(_velocityIsLocalSpace), _velocityIsLocalSpace);
        xs.WriteVector4(xe, nameof(_pivot), _pivot);
        xs.WriteVector4(xe, nameof(_angularVelocity), _angularVelocity);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpCircularSurfaceVelocity);
    }
    public bool Equals(hknpCircularSurfaceVelocity? other)
    {
        return other is not null && _velocityIsLocalSpace.Equals(other._velocityIsLocalSpace) && _pivot.Equals(other._pivot) && _angularVelocity.Equals(other._angularVelocity) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_velocityIsLocalSpace);
        code.Add(_pivot);
        code.Add(_angularVelocity);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
