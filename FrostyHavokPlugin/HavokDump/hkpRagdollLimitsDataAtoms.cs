using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpRagdollLimitsDataAtoms : IHavokObject, IEquatable<hkpRagdollLimitsDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalRotationsConstraintAtom _rotations;
    public hkpTwistLimitConstraintAtom _twistLimit;
    public hkpConeLimitConstraintAtom _coneLimit;
    public hkpConeLimitConstraintAtom _planesLimit;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _rotations = new hkpSetLocalRotationsConstraintAtom();
        _rotations.Read(des, br);
        _twistLimit = new hkpTwistLimitConstraintAtom();
        _twistLimit.Read(des, br);
        _coneLimit = new hkpConeLimitConstraintAtom();
        _coneLimit.Read(des, br);
        _planesLimit = new hkpConeLimitConstraintAtom();
        _planesLimit.Read(des, br);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_rotations), _rotations);
        xs.WriteClass(xe, nameof(_twistLimit), _twistLimit);
        xs.WriteClass(xe, nameof(_coneLimit), _coneLimit);
        xs.WriteClass(xe, nameof(_planesLimit), _planesLimit);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpRagdollLimitsDataAtoms);
    }
    public bool Equals(hkpRagdollLimitsDataAtoms? other)
    {
        return other is not null && _rotations.Equals(other._rotations) && _twistLimit.Equals(other._twistLimit) && _coneLimit.Equals(other._coneLimit) && _planesLimit.Equals(other._planesLimit) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_rotations);
        code.Add(_twistLimit);
        code.Add(_coneLimit);
        code.Add(_planesLimit);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
