using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkSimpleProperty : IHavokObject
{
    public virtual uint Signature => 0;
    public uint _key;
    // TYPE_UINT32 TYPE_VOID _alignmentPadding
    public hkSimplePropertyValue? _value;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _key = br.ReadUInt32();
        br.Position += 4; // padding
        _value = new hkSimplePropertyValue();
        _value.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt32(_key);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        _value.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_key), _key);
        xs.WriteClass(xe, nameof(_value), _value);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkSimpleProperty other && _key == other._key && _value == other._value && Signature == other.Signature;
    }
    public static bool operator ==(hkSimpleProperty? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkSimpleProperty? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_key);
        code.Add(_value);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
