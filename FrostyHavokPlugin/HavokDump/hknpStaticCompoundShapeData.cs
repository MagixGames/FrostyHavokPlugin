using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpStaticCompoundShapeData : hkReferencedObject, IEquatable<hknpStaticCompoundShapeData?>
{
    public override uint Signature => 0;
    public hknpStaticCompoundShapeTree _aabbTree;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _aabbTree = new hknpStaticCompoundShapeTree();
        _aabbTree.Read(des, br);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_aabbTree), _aabbTree);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpStaticCompoundShapeData);
    }
    public bool Equals(hknpStaticCompoundShapeData? other)
    {
        return other is not null && _aabbTree.Equals(other._aabbTree) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_aabbTree);
        code.Add(Signature);
        return code.ToHashCode();
    }
}