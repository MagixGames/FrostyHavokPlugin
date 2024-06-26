using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpSetLocalTranslationsConstraintAtom : hkpConstraintAtom
{
    public override uint Signature => 0;
    public Vector4 _translationA;
    public Vector4 _translationB;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 14; // padding
        _translationA = des.ReadVector4(br);
        _translationB = des.ReadVector4(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 14; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _translationA);
        s.WriteVector4(bw, _translationB);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteVector4(xe, nameof(_translationA), _translationA);
        xs.WriteVector4(xe, nameof(_translationB), _translationB);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpSetLocalTranslationsConstraintAtom other && base.Equals(other) && _translationA == other._translationA && _translationB == other._translationB && Signature == other.Signature;
    }
    public static bool operator ==(hkpSetLocalTranslationsConstraintAtom? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpSetLocalTranslationsConstraintAtom? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_translationA);
        code.Add(_translationB);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
