using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkSkinnedMeshShapeBoneSection : IHavokObject, IEquatable<hkSkinnedMeshShapeBoneSection?>
{
    public virtual uint Signature => 0;
    public hkMeshShape _meshBuffer;
    public ushort _startBoneSetId;
    public short _numBoneSets;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _meshBuffer = des.ReadClassPointer<hkMeshShape>(br);
        _startBoneSetId = br.ReadUInt16();
        _numBoneSets = br.ReadInt16();
        br.Position += 4; // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassPointer(xe, nameof(_meshBuffer), _meshBuffer);
        xs.WriteNumber(xe, nameof(_startBoneSetId), _startBoneSetId);
        xs.WriteNumber(xe, nameof(_numBoneSets), _numBoneSets);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkSkinnedMeshShapeBoneSection);
    }
    public bool Equals(hkSkinnedMeshShapeBoneSection? other)
    {
        return other is not null && _meshBuffer.Equals(other._meshBuffer) && _startBoneSetId.Equals(other._startBoneSetId) && _numBoneSets.Equals(other._numBoneSets) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_meshBuffer);
        code.Add(_startBoneSetId);
        code.Add(_numBoneSets);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
