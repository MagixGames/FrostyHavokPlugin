using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleRayCastWheelCollide : hknpVehicleWheelCollide, IEquatable<hknpVehicleRayCastWheelCollide?>
{
    public override uint Signature => 0;
    public uint _wheelCollisionFilterInfo;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _wheelCollisionFilterInfo = br.ReadUInt32();
        br.Position += 4; // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_wheelCollisionFilterInfo), _wheelCollisionFilterInfo);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleRayCastWheelCollide);
    }
    public bool Equals(hknpVehicleRayCastWheelCollide? other)
    {
        return other is not null && _wheelCollisionFilterInfo.Equals(other._wheelCollisionFilterInfo) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_wheelCollisionFilterInfo);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
