using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpWorldSnapshot : hkReferencedObject
{
    public override uint Signature => 0;
    public hknpWorldCinfo? _worldCinfo;
    public List<hknpBody?> _bodies = new();
    public List<string> _bodyNames = new();
    public List<hknpMotion?> _motions = new();
    public List<hknpConstraintCinfo?> _constraints = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _worldCinfo = new hknpWorldCinfo();
        _worldCinfo.Read(des, br);
        _bodies = des.ReadClassArray<hknpBody>(br);
        _bodyNames = des.ReadStringPointerArray(br);
        _motions = des.ReadClassArray<hknpMotion>(br);
        _constraints = des.ReadClassArray<hknpConstraintCinfo>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _worldCinfo.Write(s, bw);
        s.WriteClassArray<hknpBody>(bw, _bodies);
        s.WriteStringPointerArray(bw, _bodyNames);
        s.WriteClassArray<hknpMotion>(bw, _motions);
        s.WriteClassArray<hknpConstraintCinfo>(bw, _constraints);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_worldCinfo), _worldCinfo);
        xs.WriteClassArray<hknpBody>(xe, nameof(_bodies), _bodies);
        xs.WriteStringArray(xe, nameof(_bodyNames), _bodyNames);
        xs.WriteClassArray<hknpMotion>(xe, nameof(_motions), _motions);
        xs.WriteClassArray<hknpConstraintCinfo>(xe, nameof(_constraints), _constraints);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpWorldSnapshot other && base.Equals(other) && _worldCinfo == other._worldCinfo && _bodies.SequenceEqual(other._bodies) && _bodyNames.SequenceEqual(other._bodyNames) && _motions.SequenceEqual(other._motions) && _constraints.SequenceEqual(other._constraints) && Signature == other.Signature;
    }
    public static bool operator ==(hknpWorldSnapshot? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpWorldSnapshot? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_worldCinfo);
        code.Add(_bodies);
        code.Add(_bodyNames);
        code.Add(_motions);
        code.Add(_constraints);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
