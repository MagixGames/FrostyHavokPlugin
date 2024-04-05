using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpAngLimitConstraintAtom : hkpConstraintAtom, IEquatable<hkpAngLimitConstraintAtom?>
{
    public override uint Signature => 0;
    public byte _isEnabled;
    public byte _limitAxis;
    public float _minAngle;
    public float _maxAngle;
    public float _angularLimitsTauFactor;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _isEnabled = br.ReadByte();
        _limitAxis = br.ReadByte();
        _minAngle = br.ReadSingle();
        _maxAngle = br.ReadSingle();
        _angularLimitsTauFactor = br.ReadSingle();
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_isEnabled), _isEnabled);
        xs.WriteNumber(xe, nameof(_limitAxis), _limitAxis);
        xs.WriteFloat(xe, nameof(_minAngle), _minAngle);
        xs.WriteFloat(xe, nameof(_maxAngle), _maxAngle);
        xs.WriteFloat(xe, nameof(_angularLimitsTauFactor), _angularLimitsTauFactor);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpAngLimitConstraintAtom);
    }
    public bool Equals(hkpAngLimitConstraintAtom? other)
    {
        return other is not null && _isEnabled.Equals(other._isEnabled) && _limitAxis.Equals(other._limitAxis) && _minAngle.Equals(other._minAngle) && _maxAngle.Equals(other._maxAngle) && _angularLimitsTauFactor.Equals(other._angularLimitsTauFactor) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_isEnabled);
        code.Add(_limitAxis);
        code.Add(_minAngle);
        code.Add(_maxAngle);
        code.Add(_angularLimitsTauFactor);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
