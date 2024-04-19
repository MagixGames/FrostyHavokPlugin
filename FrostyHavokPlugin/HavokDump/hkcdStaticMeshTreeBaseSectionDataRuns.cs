using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticMeshTreeBaseSectionDataRuns : IHavokObject
{
    public virtual uint Signature => 0;
    public uint _data;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _data = br.ReadUInt32();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt32(_data);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_data), _data);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkcdStaticMeshTreeBaseSectionDataRuns other && _data == other._data && Signature == other.Signature;
    }
    public static bool operator ==(hkcdStaticMeshTreeBaseSectionDataRuns? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdStaticMeshTreeBaseSectionDataRuns? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_data);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
