using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkRefCountedPropertiesEntry : IHavokObject, IEquatable<hkRefCountedPropertiesEntry?>
{
    public virtual uint Signature => 0;
    public hkReferencedObject _object;
    public ushort _key;
    public ushort _flags;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _object = des.ReadClassPointer<hkReferencedObject>(br);
        _key = br.ReadUInt16();
        _flags = br.ReadUInt16();
        br.Position += 4; // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassPointer(xe, nameof(_object), _object);
        xs.WriteNumber(xe, nameof(_key), _key);
        xs.WriteNumber(xe, nameof(_flags), _flags);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkRefCountedPropertiesEntry);
    }
    public bool Equals(hkRefCountedPropertiesEntry? other)
    {
        return other is not null && _object.Equals(other._object) && _key.Equals(other._key) && _flags.Equals(other._flags) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_object);
        code.Add(_key);
        code.Add(_flags);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
