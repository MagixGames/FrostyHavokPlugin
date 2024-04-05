using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxMaterialTextureStage : IHavokObject, IEquatable<hkxMaterialTextureStage?>
{
    public virtual uint Signature => 0;
    public hkReferencedObject _texture;
    public hkxMaterial_TextureType _usageHint;
    public int _tcoordChannel;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _texture = des.ReadClassPointer<hkReferencedObject>(br);
        _usageHint = (hkxMaterial_TextureType)br.ReadInt32();
        _tcoordChannel = br.ReadInt32();
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassPointer(xe, nameof(_texture), _texture);
        xs.WriteEnum<hkxMaterial_TextureType, int>(xe, nameof(_usageHint), (int)_usageHint);
        xs.WriteNumber(xe, nameof(_tcoordChannel), _tcoordChannel);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxMaterialTextureStage);
    }
    public bool Equals(hkxMaterialTextureStage? other)
    {
        return other is not null && _texture.Equals(other._texture) && _usageHint.Equals(other._usageHint) && _tcoordChannel.Equals(other._tcoordChannel) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_texture);
        code.Add(_usageHint);
        code.Add(_tcoordChannel);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
