using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxSkinBinding : hkReferencedObject
{
    public override uint Signature => 0;
    public hkxMesh? _mesh;
    public List<string> _nodeNames = new();
    public List<Matrix4> _bindPose = new();
    public Matrix4 _initSkinTransform;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _mesh = des.ReadClassPointer<hkxMesh>(br);
        _nodeNames = des.ReadStringPointerArray(br);
        _bindPose = des.ReadMatrix4Array(br);
        br.Position += 8; // padding
        _initSkinTransform = des.ReadMatrix4(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointer<hkxMesh>(bw, _mesh);
        s.WriteStringPointerArray(bw, _nodeNames);
        s.WriteMatrix4Array(bw, _bindPose);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        s.WriteMatrix4(bw, _initSkinTransform);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_mesh), _mesh);
        xs.WriteStringArray(xe, nameof(_nodeNames), _nodeNames);
        xs.WriteMatrix4Array(xe, nameof(_bindPose), _bindPose);
        xs.WriteMatrix4(xe, nameof(_initSkinTransform), _initSkinTransform);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxSkinBinding other && base.Equals(other) && _mesh == other._mesh && _nodeNames.SequenceEqual(other._nodeNames) && _bindPose.SequenceEqual(other._bindPose) && _initSkinTransform == other._initSkinTransform && Signature == other.Signature;
    }
    public static bool operator ==(hkxSkinBinding? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxSkinBinding? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_mesh);
        code.Add(_nodeNames);
        code.Add(_bindPose);
        code.Add(_initSkinTransform);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
