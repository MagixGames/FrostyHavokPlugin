using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkBitFieldBasehkBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator : IHavokObject
{
    public virtual uint Signature => 0;
    public hkBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator? _storage;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _storage = new hkBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator();
        _storage.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _storage.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_storage), _storage);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkBitFieldBasehkBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator other && _storage == other._storage && Signature == other.Signature;
    }
    public static bool operator ==(hkBitFieldBasehkBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkBitFieldBasehkBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_storage);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
