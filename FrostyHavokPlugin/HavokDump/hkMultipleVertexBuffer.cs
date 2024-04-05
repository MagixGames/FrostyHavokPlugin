using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMultipleVertexBuffer : hkMeshVertexBuffer, IEquatable<hkMultipleVertexBuffer?>
{
    public override uint Signature => 0;
    public hkVertexFormat _vertexFormat;
    public List<hkMultipleVertexBufferLockedElement> _lockedElements;
    public hkMemoryMeshVertexBuffer _lockedBuffer;
    public List<hkMultipleVertexBufferElementInfo> _elementInfos;
    public List<hkMultipleVertexBufferVertexBufferInfo> _vertexBufferInfos;
    public int _numVertices;
    public bool _isLocked;
    public uint _updateCount;
    public bool _writeLock;
    public bool _isSharable;
    public bool _constructionComplete;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _vertexFormat = new hkVertexFormat();
        _vertexFormat.Read(des, br);
        br.Position += 4; // padding
        _lockedElements = des.ReadClassArray<hkMultipleVertexBufferLockedElement>(br);
        _lockedBuffer = des.ReadClassPointer<hkMemoryMeshVertexBuffer>(br);
        _elementInfos = des.ReadClassArray<hkMultipleVertexBufferElementInfo>(br);
        _vertexBufferInfos = des.ReadClassArray<hkMultipleVertexBufferVertexBufferInfo>(br);
        _numVertices = br.ReadInt32();
        _isLocked = br.ReadBoolean();
        br.Position += 3; // padding
        _updateCount = br.ReadUInt32();
        _writeLock = br.ReadBoolean();
        _isSharable = br.ReadBoolean();
        _constructionComplete = br.ReadBoolean();
        br.Position += 1; // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_vertexFormat), _vertexFormat);
        xs.WriteClassArray<hkMultipleVertexBufferLockedElement>(xe, nameof(_lockedElements), _lockedElements);
        xs.WriteClassPointer(xe, nameof(_lockedBuffer), _lockedBuffer);
        xs.WriteClassArray<hkMultipleVertexBufferElementInfo>(xe, nameof(_elementInfos), _elementInfos);
        xs.WriteClassArray<hkMultipleVertexBufferVertexBufferInfo>(xe, nameof(_vertexBufferInfos), _vertexBufferInfos);
        xs.WriteNumber(xe, nameof(_numVertices), _numVertices);
        xs.WriteBoolean(xe, nameof(_isLocked), _isLocked);
        xs.WriteNumber(xe, nameof(_updateCount), _updateCount);
        xs.WriteBoolean(xe, nameof(_writeLock), _writeLock);
        xs.WriteBoolean(xe, nameof(_isSharable), _isSharable);
        xs.WriteBoolean(xe, nameof(_constructionComplete), _constructionComplete);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkMultipleVertexBuffer);
    }
    public bool Equals(hkMultipleVertexBuffer? other)
    {
        return other is not null && _vertexFormat.Equals(other._vertexFormat) && _lockedElements.Equals(other._lockedElements) && _lockedBuffer.Equals(other._lockedBuffer) && _elementInfos.Equals(other._elementInfos) && _vertexBufferInfos.Equals(other._vertexBufferInfos) && _numVertices.Equals(other._numVertices) && _isLocked.Equals(other._isLocked) && _updateCount.Equals(other._updateCount) && _writeLock.Equals(other._writeLock) && _isSharable.Equals(other._isSharable) && _constructionComplete.Equals(other._constructionComplete) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_vertexFormat);
        code.Add(_lockedElements);
        code.Add(_lockedBuffer);
        code.Add(_elementInfos);
        code.Add(_vertexBufferInfos);
        code.Add(_numVertices);
        code.Add(_isLocked);
        code.Add(_updateCount);
        code.Add(_writeLock);
        code.Add(_isSharable);
        code.Add(_constructionComplete);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
