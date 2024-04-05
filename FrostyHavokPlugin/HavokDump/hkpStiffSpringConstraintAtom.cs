using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpStiffSpringConstraintAtom : hkpConstraintAtom, IEquatable<hkpStiffSpringConstraintAtom?>
{
    public override uint Signature => 0;
    public float _length;
    public float _maxLength;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 2; // padding
        _length = br.ReadSingle();
        _maxLength = br.ReadSingle();
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_length), _length);
        xs.WriteFloat(xe, nameof(_maxLength), _maxLength);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpStiffSpringConstraintAtom);
    }
    public bool Equals(hkpStiffSpringConstraintAtom? other)
    {
        return other is not null && _length.Equals(other._length) && _maxLength.Equals(other._maxLength) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_length);
        code.Add(_maxLength);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
