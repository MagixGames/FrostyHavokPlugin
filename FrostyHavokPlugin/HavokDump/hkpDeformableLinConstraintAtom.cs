using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpDeformableLinConstraintAtom : hkpConstraintAtom, IEquatable<hkpDeformableLinConstraintAtom?>
{
    public override uint Signature => 0;
    public Vector4 _offset;
    public Vector4 _yieldStrengthDiag;
    public Vector4 _yieldStrengthOffDiag;
    public Vector4 _ultimateStrengthDiag;
    public Vector4 _ultimateStrengthOffDiag;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 14; // padding
        _offset = des.ReadVector4(br);
        _yieldStrengthDiag = des.ReadVector4(br);
        _yieldStrengthOffDiag = des.ReadVector4(br);
        _ultimateStrengthDiag = des.ReadVector4(br);
        _ultimateStrengthOffDiag = des.ReadVector4(br);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteVector4(xe, nameof(_offset), _offset);
        xs.WriteVector4(xe, nameof(_yieldStrengthDiag), _yieldStrengthDiag);
        xs.WriteVector4(xe, nameof(_yieldStrengthOffDiag), _yieldStrengthOffDiag);
        xs.WriteVector4(xe, nameof(_ultimateStrengthDiag), _ultimateStrengthDiag);
        xs.WriteVector4(xe, nameof(_ultimateStrengthOffDiag), _ultimateStrengthOffDiag);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpDeformableLinConstraintAtom);
    }
    public bool Equals(hkpDeformableLinConstraintAtom? other)
    {
        return other is not null && _offset.Equals(other._offset) && _yieldStrengthDiag.Equals(other._yieldStrengthDiag) && _yieldStrengthOffDiag.Equals(other._yieldStrengthOffDiag) && _ultimateStrengthDiag.Equals(other._ultimateStrengthDiag) && _ultimateStrengthOffDiag.Equals(other._ultimateStrengthOffDiag) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_offset);
        code.Add(_yieldStrengthDiag);
        code.Add(_yieldStrengthOffDiag);
        code.Add(_ultimateStrengthDiag);
        code.Add(_ultimateStrengthOffDiag);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
