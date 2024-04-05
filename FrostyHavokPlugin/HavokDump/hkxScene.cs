using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxScene : hkReferencedObject, IEquatable<hkxScene?>
{
    public override uint Signature => 0;
    public string _modeller;
    public string _asset;
    public float _sceneLength;
    public uint _numFrames;
    public hkxNode _rootNode;
    public List<hkxNodeSelectionSet> _selectionSets;
    public List<hkxCamera> _cameras;
    public List<hkxLight> _lights;
    public List<hkxMesh> _meshes;
    public List<hkxMaterial> _materials;
    public List<hkxTextureInplace> _inplaceTextures;
    public List<hkxTextureFile> _externalTextures;
    public List<hkxSkinBinding> _skinBindings;
    public List<hkxSpline> _splines;
    public Matrix4 _appliedTransform;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _modeller = des.ReadStringPointer(br);
        _asset = des.ReadStringPointer(br);
        _sceneLength = br.ReadSingle();
        _numFrames = br.ReadUInt32();
        _rootNode = des.ReadClassPointer<hkxNode>(br);
        _selectionSets = des.ReadClassPointerArray<hkxNodeSelectionSet>(br);
        _cameras = des.ReadClassPointerArray<hkxCamera>(br);
        _lights = des.ReadClassPointerArray<hkxLight>(br);
        _meshes = des.ReadClassPointerArray<hkxMesh>(br);
        _materials = des.ReadClassPointerArray<hkxMaterial>(br);
        _inplaceTextures = des.ReadClassPointerArray<hkxTextureInplace>(br);
        _externalTextures = des.ReadClassPointerArray<hkxTextureFile>(br);
        _skinBindings = des.ReadClassPointerArray<hkxSkinBinding>(br);
        _splines = des.ReadClassPointerArray<hkxSpline>(br);
        _appliedTransform = des.ReadMatrix3(br);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteString(xe, nameof(_modeller), _modeller);
        xs.WriteString(xe, nameof(_asset), _asset);
        xs.WriteFloat(xe, nameof(_sceneLength), _sceneLength);
        xs.WriteNumber(xe, nameof(_numFrames), _numFrames);
        xs.WriteClassPointer(xe, nameof(_rootNode), _rootNode);
        xs.WriteClassPointerArray<hkxNodeSelectionSet>(xe, nameof(_selectionSets), _selectionSets);
        xs.WriteClassPointerArray<hkxCamera>(xe, nameof(_cameras), _cameras);
        xs.WriteClassPointerArray<hkxLight>(xe, nameof(_lights), _lights);
        xs.WriteClassPointerArray<hkxMesh>(xe, nameof(_meshes), _meshes);
        xs.WriteClassPointerArray<hkxMaterial>(xe, nameof(_materials), _materials);
        xs.WriteClassPointerArray<hkxTextureInplace>(xe, nameof(_inplaceTextures), _inplaceTextures);
        xs.WriteClassPointerArray<hkxTextureFile>(xe, nameof(_externalTextures), _externalTextures);
        xs.WriteClassPointerArray<hkxSkinBinding>(xe, nameof(_skinBindings), _skinBindings);
        xs.WriteClassPointerArray<hkxSpline>(xe, nameof(_splines), _splines);
        xs.WriteMatrix3(xe, nameof(_appliedTransform), _appliedTransform);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxScene);
    }
    public bool Equals(hkxScene? other)
    {
        return other is not null && _modeller.Equals(other._modeller) && _asset.Equals(other._asset) && _sceneLength.Equals(other._sceneLength) && _numFrames.Equals(other._numFrames) && _rootNode.Equals(other._rootNode) && _selectionSets.Equals(other._selectionSets) && _cameras.Equals(other._cameras) && _lights.Equals(other._lights) && _meshes.Equals(other._meshes) && _materials.Equals(other._materials) && _inplaceTextures.Equals(other._inplaceTextures) && _externalTextures.Equals(other._externalTextures) && _skinBindings.Equals(other._skinBindings) && _splines.Equals(other._splines) && _appliedTransform.Equals(other._appliedTransform) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_modeller);
        code.Add(_asset);
        code.Add(_sceneLength);
        code.Add(_numFrames);
        code.Add(_rootNode);
        code.Add(_selectionSets);
        code.Add(_cameras);
        code.Add(_lights);
        code.Add(_meshes);
        code.Add(_materials);
        code.Add(_inplaceTextures);
        code.Add(_externalTextures);
        code.Add(_skinBindings);
        code.Add(_splines);
        code.Add(_appliedTransform);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
