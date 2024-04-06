using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkUiAttribute : IHavokObject, IEquatable<hkUiAttribute?>
{
    public virtual uint Signature => 0;
    public bool _visible;
    public bool _editable;
    public hkUiAttribute_HideInModeler _hideInModeler;
    public string _label;
    public string _group;
    public string _hideBaseClassMembers;
    public bool _endGroup;
    public bool _endGroup2;
    public bool _advanced;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _visible = br.ReadBoolean();
        _editable = br.ReadBoolean();
        _hideInModeler = (hkUiAttribute_HideInModeler)br.ReadSByte();
        br.Position += 5; // padding
        _label = des.ReadStringPointer(br);
        _group = des.ReadStringPointer(br);
        _hideBaseClassMembers = des.ReadStringPointer(br);
        _endGroup = br.ReadBoolean();
        _endGroup2 = br.ReadBoolean();
        _advanced = br.ReadBoolean();
        br.Position += 5; // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteBoolean(xe, nameof(_visible), _visible);
        xs.WriteBoolean(xe, nameof(_editable), _editable);
        xs.WriteEnum<hkUiAttribute_HideInModeler, sbyte>(xe, nameof(_hideInModeler), (sbyte)_hideInModeler);
        xs.WriteString(xe, nameof(_label), _label);
        xs.WriteString(xe, nameof(_group), _group);
        xs.WriteString(xe, nameof(_hideBaseClassMembers), _hideBaseClassMembers);
        xs.WriteBoolean(xe, nameof(_endGroup), _endGroup);
        xs.WriteBoolean(xe, nameof(_endGroup2), _endGroup2);
        xs.WriteBoolean(xe, nameof(_advanced), _advanced);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkUiAttribute);
    }
    public bool Equals(hkUiAttribute? other)
    {
        return other is not null && _visible.Equals(other._visible) && _editable.Equals(other._editable) && _hideInModeler.Equals(other._hideInModeler) && _label.Equals(other._label) && _group.Equals(other._group) && _hideBaseClassMembers.Equals(other._hideBaseClassMembers) && _endGroup.Equals(other._endGroup) && _endGroup2.Equals(other._endGroup2) && _advanced.Equals(other._advanced) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_visible);
        code.Add(_editable);
        code.Add(_hideInModeler);
        code.Add(_label);
        code.Add(_group);
        code.Add(_hideBaseClassMembers);
        code.Add(_endGroup);
        code.Add(_endGroup2);
        code.Add(_advanced);
        code.Add(Signature);
        return code.ToHashCode();
    }
}