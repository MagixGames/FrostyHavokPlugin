using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpShapeSignals : IHavokObject
{
    public virtual uint Signature => 0;
    // TYPE_POINTER TYPE_VOID _shapeMutated
    // TYPE_POINTER TYPE_VOID _shapeDestroyed
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        br.Position += 16; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        for (int i = 0; i < 16; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpShapeSignals other && Signature == other.Signature;
    }
    public static bool operator ==(hknpShapeSignals? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpShapeSignals? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(Signature);
        return code.ToHashCode();
    }
}
