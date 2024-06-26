using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpVehicleFrictionDescription : hkReferencedObject
{
    public override uint Signature => 0;
    public float _wheelDistance;
    public float _chassisMassInv;
    public hkpVehicleFrictionDescriptionAxisDescription?[] _axleDescr = new hkpVehicleFrictionDescriptionAxisDescription?[2];
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _wheelDistance = br.ReadSingle();
        _chassisMassInv = br.ReadSingle();
        _axleDescr = des.ReadStructCStyleArray<hkpVehicleFrictionDescriptionAxisDescription>(br, 2);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_wheelDistance);
        bw.WriteSingle(_chassisMassInv);
        s.WriteStructCStyleArray<hkpVehicleFrictionDescriptionAxisDescription>(bw, _axleDescr);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_wheelDistance), _wheelDistance);
        xs.WriteFloat(xe, nameof(_chassisMassInv), _chassisMassInv);
        xs.WriteClassArray(xe, nameof(_axleDescr), _axleDescr);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpVehicleFrictionDescription other && base.Equals(other) && _wheelDistance == other._wheelDistance && _chassisMassInv == other._chassisMassInv && _axleDescr == other._axleDescr && Signature == other.Signature;
    }
    public static bool operator ==(hkpVehicleFrictionDescription? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpVehicleFrictionDescription? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_wheelDistance);
        code.Add(_chassisMassInv);
        code.Add(_axleDescr);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
