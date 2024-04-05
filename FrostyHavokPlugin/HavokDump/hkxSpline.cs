using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxSpline : hkReferencedObject, IEquatable<hkxSpline?>
{
    public override uint Signature => 0;
    public List<hkxSplineControlPoint> _controlPoints;
    public bool _isClosed;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _controlPoints = des.ReadClassArray<hkxSplineControlPoint>(br);
        _isClosed = br.ReadBoolean();
        br.Position += 7; // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hkxSplineControlPoint>(xe, nameof(_controlPoints), _controlPoints);
        xs.WriteBoolean(xe, nameof(_isClosed), _isClosed);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxSpline);
    }
    public bool Equals(hkxSpline? other)
    {
        return other is not null && _controlPoints.Equals(other._controlPoints) && _isClosed.Equals(other._isClosed) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_controlPoints);
        code.Add(_isClosed);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
