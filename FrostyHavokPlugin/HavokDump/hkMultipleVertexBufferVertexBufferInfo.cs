using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMultipleVertexBufferVertexBufferInfo : IHavokObject, IEquatable<hkMultipleVertexBufferVertexBufferInfo?>
{
    public virtual uint Signature => 0;
    public hkMeshVertexBuffer _vertexBuffer;
    // TYPE_POINTER TYPE_VOID _lockedVertices
    public bool _isLocked;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _vertexBuffer = des.ReadClassPointer<hkMeshVertexBuffer>(br);
        br.Position += 8; // padding
        _isLocked = br.ReadBoolean();
        br.Position += 7; // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassPointer(xe, nameof(_vertexBuffer), _vertexBuffer);
        xs.WriteBoolean(xe, nameof(_isLocked), _isLocked);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkMultipleVertexBufferVertexBufferInfo);
    }
    public bool Equals(hkMultipleVertexBufferVertexBufferInfo? other)
    {
        return other is not null && _vertexBuffer.Equals(other._vertexBuffer) && _isLocked.Equals(other._isLocked) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_vertexBuffer);
        code.Add(_isLocked);
        code.Add(Signature);
        return code.ToHashCode();
    }
}