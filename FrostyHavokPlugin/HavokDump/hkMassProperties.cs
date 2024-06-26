using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMassProperties : IHavokObject
{
    public virtual uint Signature => 0;
    public float _volume;
    public float _mass;
    public Vector4 _centerOfMass;
    public Matrix3x4 _inertiaTensor;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _volume = br.ReadSingle();
        _mass = br.ReadSingle();
        br.Position += 8; // padding
        _centerOfMass = des.ReadVector4(br);
        _inertiaTensor = des.ReadMatrix3(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteSingle(_volume);
        bw.WriteSingle(_mass);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _centerOfMass);
        s.WriteMatrix3(bw, _inertiaTensor);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteFloat(xe, nameof(_volume), _volume);
        xs.WriteFloat(xe, nameof(_mass), _mass);
        xs.WriteVector4(xe, nameof(_centerOfMass), _centerOfMass);
        xs.WriteMatrix3(xe, nameof(_inertiaTensor), _inertiaTensor);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkMassProperties other && _volume == other._volume && _mass == other._mass && _centerOfMass == other._centerOfMass && _inertiaTensor == other._inertiaTensor && Signature == other.Signature;
    }
    public static bool operator ==(hkMassProperties? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkMassProperties? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_volume);
        code.Add(_mass);
        code.Add(_centerOfMass);
        code.Add(_inertiaTensor);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
