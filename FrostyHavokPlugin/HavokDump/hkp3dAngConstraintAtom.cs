using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkp3dAngConstraintAtom : hkpConstraintAtom
{
    public override uint Signature => 0;
    // TYPE_UINT8 TYPE_VOID _padding
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 14; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 14; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkp3dAngConstraintAtom other && base.Equals(other) && Signature == other.Signature;
    }
    public static bool operator ==(hkp3dAngConstraintAtom? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkp3dAngConstraintAtom? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(Signature);
        return code.ToHashCode();
    }
}
