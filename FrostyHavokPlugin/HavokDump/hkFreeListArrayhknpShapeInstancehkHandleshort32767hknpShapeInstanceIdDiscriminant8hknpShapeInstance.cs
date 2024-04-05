using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkFreeListArrayhknpShapeInstancehkHandleshort32767hknpShapeInstanceIdDiscriminant8hknpShapeInstance : IHavokObject, IEquatable<hkFreeListArrayhknpShapeInstancehkHandleshort32767hknpShapeInstanceIdDiscriminant8hknpShapeInstance?>
{
    public virtual uint Signature => 0;
    public List<hknpShapeInstance> _elements;
    public int _firstFree;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _elements = des.ReadClassArray<hknpShapeInstance>(br);
        _firstFree = br.ReadInt32();
        br.Position += 4; // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray<hknpShapeInstance>(xe, nameof(_elements), _elements);
        xs.WriteNumber(xe, nameof(_firstFree), _firstFree);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkFreeListArrayhknpShapeInstancehkHandleshort32767hknpShapeInstanceIdDiscriminant8hknpShapeInstance);
    }
    public bool Equals(hkFreeListArrayhknpShapeInstancehkHandleshort32767hknpShapeInstanceIdDiscriminant8hknpShapeInstance? other)
    {
        return other is not null && _elements.Equals(other._elements) && _firstFree.Equals(other._firstFree) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_elements);
        code.Add(_firstFree);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
