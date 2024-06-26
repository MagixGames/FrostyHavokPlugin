using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkContactPointMaterial : IHavokObject
{
    public virtual uint Signature => 0;
    public ulong _userData;
    public hkUFloat8? _friction;
    public byte _restitution;
    public hkUFloat8? _maxImpulse;
    public byte _flags;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _userData = br.ReadUInt64();
        _friction = new hkUFloat8();
        _friction.Read(des, br);
        _restitution = br.ReadByte();
        _maxImpulse = new hkUFloat8();
        _maxImpulse.Read(des, br);
        _flags = br.ReadByte();
        br.Position += 4; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt64(_userData);
        _friction.Write(s, bw);
        bw.WriteByte(_restitution);
        _maxImpulse.Write(s, bw);
        bw.WriteByte(_flags);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_userData), _userData);
        xs.WriteClass(xe, nameof(_friction), _friction);
        xs.WriteNumber(xe, nameof(_restitution), _restitution);
        xs.WriteClass(xe, nameof(_maxImpulse), _maxImpulse);
        xs.WriteNumber(xe, nameof(_flags), _flags);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkContactPointMaterial other && _userData == other._userData && _friction == other._friction && _restitution == other._restitution && _maxImpulse == other._maxImpulse && _flags == other._flags && Signature == other.Signature;
    }
    public static bool operator ==(hkContactPointMaterial? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkContactPointMaterial? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_userData);
        code.Add(_friction);
        code.Add(_restitution);
        code.Add(_maxImpulse);
        code.Add(_flags);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
