using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpPositionConstraintMotor : hkpLimitedForceConstraintMotor, IEquatable<hkpPositionConstraintMotor?>
{
    public override uint Signature => 0;
    public float _tau;
    public float _damping;
    public float _proportionalRecoveryVelocity;
    public float _constantRecoveryVelocity;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _tau = br.ReadSingle();
        _damping = br.ReadSingle();
        _proportionalRecoveryVelocity = br.ReadSingle();
        _constantRecoveryVelocity = br.ReadSingle();
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_tau), _tau);
        xs.WriteFloat(xe, nameof(_damping), _damping);
        xs.WriteFloat(xe, nameof(_proportionalRecoveryVelocity), _proportionalRecoveryVelocity);
        xs.WriteFloat(xe, nameof(_constantRecoveryVelocity), _constantRecoveryVelocity);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpPositionConstraintMotor);
    }
    public bool Equals(hkpPositionConstraintMotor? other)
    {
        return other is not null && _tau.Equals(other._tau) && _damping.Equals(other._damping) && _proportionalRecoveryVelocity.Equals(other._proportionalRecoveryVelocity) && _constantRecoveryVelocity.Equals(other._constantRecoveryVelocity) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_tau);
        code.Add(_damping);
        code.Add(_proportionalRecoveryVelocity);
        code.Add(_constantRecoveryVelocity);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
