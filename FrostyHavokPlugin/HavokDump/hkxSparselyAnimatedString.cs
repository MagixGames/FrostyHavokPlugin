using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxSparselyAnimatedString : hkReferencedObject, IEquatable<hkxSparselyAnimatedString?>
{
    public override uint Signature => 0;
    public List<string> _strings;
    public List<float> _times;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _strings = des.ReadStringPointerArray(br);
        _times = des.ReadSingleArray(br);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteStringArray(xe, nameof(_strings), _strings);
        xs.WriteFloatArray(xe, nameof(_times), _times);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxSparselyAnimatedString);
    }
    public bool Equals(hkxSparselyAnimatedString? other)
    {
        return other is not null && _strings.Equals(other._strings) && _times.Equals(other._times) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_strings);
        code.Add(_times);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
