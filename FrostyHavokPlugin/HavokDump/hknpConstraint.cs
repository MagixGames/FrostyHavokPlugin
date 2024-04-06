using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpConstraint : IHavokObject, IEquatable<hknpConstraint?>
{
    public virtual uint Signature => 0;
    public uint _bodyIdA;
    public uint _bodyIdB;
    public hkpConstraintData _data;
    public uint _id;
    // TYPE_UINT16 TYPE_VOID _immediateId
    public hknpConstraint_FlagsEnum _flags;
    public hkpConstraintData_ConstraintType _type;
    // TYPE_POINTER TYPE_VOID _atoms
    public ushort _sizeOfAtoms;
    public ushort _sizeOfSchemas;
    public byte _numSolverResults;
    public byte _numSolverElemTemps;
    public ushort _runtimeSize;
    // TYPE_POINTER TYPE_VOID _runtime
    public ulong _userData;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _bodyIdA = br.ReadUInt32();
        _bodyIdB = br.ReadUInt32();
        _data = des.ReadClassPointer<hkpConstraintData>(br);
        _id = br.ReadUInt32();
        br.Position += 2; // padding
        _flags = (hknpConstraint_FlagsEnum)br.ReadByte();
        _type = (hkpConstraintData_ConstraintType)br.ReadByte();
        br.Position += 8; // padding
        _sizeOfAtoms = br.ReadUInt16();
        _sizeOfSchemas = br.ReadUInt16();
        _numSolverResults = br.ReadByte();
        _numSolverElemTemps = br.ReadByte();
        _runtimeSize = br.ReadUInt16();
        br.Position += 8; // padding
        _userData = br.ReadUInt64();
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_bodyIdA), _bodyIdA);
        xs.WriteNumber(xe, nameof(_bodyIdB), _bodyIdB);
        xs.WriteClassPointer(xe, nameof(_data), _data);
        xs.WriteNumber(xe, nameof(_id), _id);
        xs.WriteFlag<hknpConstraint_FlagsEnum, byte>(xe, nameof(_flags), (byte)_flags);
        xs.WriteEnum<hkpConstraintData_ConstraintType, byte>(xe, nameof(_type), (byte)_type);
        xs.WriteNumber(xe, nameof(_sizeOfAtoms), _sizeOfAtoms);
        xs.WriteNumber(xe, nameof(_sizeOfSchemas), _sizeOfSchemas);
        xs.WriteNumber(xe, nameof(_numSolverResults), _numSolverResults);
        xs.WriteNumber(xe, nameof(_numSolverElemTemps), _numSolverElemTemps);
        xs.WriteNumber(xe, nameof(_runtimeSize), _runtimeSize);
        xs.WriteNumber(xe, nameof(_userData), _userData);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpConstraint);
    }
    public bool Equals(hknpConstraint? other)
    {
        return other is not null && _bodyIdA.Equals(other._bodyIdA) && _bodyIdB.Equals(other._bodyIdB) && _data.Equals(other._data) && _id.Equals(other._id) && _flags.Equals(other._flags) && _type.Equals(other._type) && _sizeOfAtoms.Equals(other._sizeOfAtoms) && _sizeOfSchemas.Equals(other._sizeOfSchemas) && _numSolverResults.Equals(other._numSolverResults) && _numSolverElemTemps.Equals(other._numSolverElemTemps) && _runtimeSize.Equals(other._runtimeSize) && _userData.Equals(other._userData) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_bodyIdA);
        code.Add(_bodyIdB);
        code.Add(_data);
        code.Add(_id);
        code.Add(_flags);
        code.Add(_type);
        code.Add(_sizeOfAtoms);
        code.Add(_sizeOfSchemas);
        code.Add(_numSolverResults);
        code.Add(_numSolverElemTemps);
        code.Add(_runtimeSize);
        code.Add(_userData);
        code.Add(Signature);
        return code.ToHashCode();
    }
}