using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpSetLocalTransformsConstraintAtom : hkpConstraintAtom, IEquatable<hkpSetLocalTransformsConstraintAtom?>
{
    public override uint Signature => 0;
    public Matrix4 _transformA;
    public Matrix4 _transformB;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 14; // padding
        _transformA = des.ReadTransform(br);
        _transformB = des.ReadTransform(br);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteTransform(xe, nameof(_transformA), _transformA);
        xs.WriteTransform(xe, nameof(_transformB), _transformB);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpSetLocalTransformsConstraintAtom);
    }
    public bool Equals(hkpSetLocalTransformsConstraintAtom? other)
    {
        return other is not null && _transformA.Equals(other._transformA) && _transformB.Equals(other._transformB) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transformA);
        code.Add(_transformB);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
