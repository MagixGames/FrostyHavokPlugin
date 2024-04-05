using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMemoryMeshMaterial : hkMeshMaterial, IEquatable<hkMemoryMeshMaterial?>
{
    public override uint Signature => 0;
    public string _materialName;
    public List<hkMeshTexture> _textures;
    public Vector4 _diffuseColor;
    public Vector4 _ambientColor;
    public Vector4 _specularColor;
    public Vector4 _emissiveColor;
    public ulong _userData;
    public float _tesselationFactor;
    public float _displacementAmount;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _materialName = des.ReadStringPointer(br);
        _textures = des.ReadClassPointerArray<hkMeshTexture>(br);
        br.Position += 8; // padding
        _diffuseColor = des.ReadVector4(br);
        _ambientColor = des.ReadVector4(br);
        _specularColor = des.ReadVector4(br);
        _emissiveColor = des.ReadVector4(br);
        _userData = br.ReadUInt64();
        _tesselationFactor = br.ReadSingle();
        _displacementAmount = br.ReadSingle();
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteString(xe, nameof(_materialName), _materialName);
        xs.WriteClassPointerArray<hkMeshTexture>(xe, nameof(_textures), _textures);
        xs.WriteVector4(xe, nameof(_diffuseColor), _diffuseColor);
        xs.WriteVector4(xe, nameof(_ambientColor), _ambientColor);
        xs.WriteVector4(xe, nameof(_specularColor), _specularColor);
        xs.WriteVector4(xe, nameof(_emissiveColor), _emissiveColor);
        xs.WriteNumber(xe, nameof(_userData), _userData);
        xs.WriteFloat(xe, nameof(_tesselationFactor), _tesselationFactor);
        xs.WriteFloat(xe, nameof(_displacementAmount), _displacementAmount);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkMemoryMeshMaterial);
    }
    public bool Equals(hkMemoryMeshMaterial? other)
    {
        return other is not null && _materialName.Equals(other._materialName) && _textures.Equals(other._textures) && _diffuseColor.Equals(other._diffuseColor) && _ambientColor.Equals(other._ambientColor) && _specularColor.Equals(other._specularColor) && _emissiveColor.Equals(other._emissiveColor) && _userData.Equals(other._userData) && _tesselationFactor.Equals(other._tesselationFactor) && _displacementAmount.Equals(other._displacementAmount) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_materialName);
        code.Add(_textures);
        code.Add(_diffuseColor);
        code.Add(_ambientColor);
        code.Add(_specularColor);
        code.Add(_emissiveColor);
        code.Add(_userData);
        code.Add(_tesselationFactor);
        code.Add(_displacementAmount);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
