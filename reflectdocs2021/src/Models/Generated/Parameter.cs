// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Parameter.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Unity.Reflect.Model {

  /// <summary>Holder for reflection information generated from Parameter.proto</summary>
  public static partial class ParameterReflection {

    #region Descriptor
    /// <summary>File descriptor for Parameter.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ParameterReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg9QYXJhbWV0ZXIucHJvdG8SB1JlZmxlY3QiRwoNU3luY1BhcmFtZXRlchIN",
            "CgV2YWx1ZRgBIAEoCRIWCg5wYXJhbWV0ZXJHcm91cBgCIAEoCRIPCgd2aXNp",
            "YmxlGAMgASgIQhaqAhNVbml0eS5SZWZsZWN0Lk1vZGVsYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Unity.Reflect.Model.SyncParameter), global::Unity.Reflect.Model.SyncParameter.Parser, new[]{ "Value", "ParameterGroup", "Visible" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class SyncParameter : pb::IMessage<SyncParameter> {
    private static readonly pb::MessageParser<SyncParameter> _parser = new pb::MessageParser<SyncParameter>(() => new SyncParameter());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SyncParameter> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Unity.Reflect.Model.ParameterReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SyncParameter() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SyncParameter(SyncParameter other) : this() {
      value_ = other.value_;
      parameterGroup_ = other.parameterGroup_;
      visible_ = other.visible_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SyncParameter Clone() {
      return new SyncParameter(this);
    }

    /// <summary>Field number for the "value" field.</summary>
    public const int ValueFieldNumber = 1;
    private string value_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Value {
      get { return value_; }
      set {
        value_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "parameterGroup" field.</summary>
    public const int ParameterGroupFieldNumber = 2;
    private string parameterGroup_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string ParameterGroup {
      get { return parameterGroup_; }
      set {
        parameterGroup_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "visible" field.</summary>
    public const int VisibleFieldNumber = 3;
    private bool visible_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Visible {
      get { return visible_; }
      set {
        visible_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SyncParameter);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SyncParameter other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Value != other.Value) return false;
      if (ParameterGroup != other.ParameterGroup) return false;
      if (Visible != other.Visible) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Value.Length != 0) hash ^= Value.GetHashCode();
      if (ParameterGroup.Length != 0) hash ^= ParameterGroup.GetHashCode();
      if (Visible != false) hash ^= Visible.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Value.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Value);
      }
      if (ParameterGroup.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(ParameterGroup);
      }
      if (Visible != false) {
        output.WriteRawTag(24);
        output.WriteBool(Visible);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Value.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Value);
      }
      if (ParameterGroup.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ParameterGroup);
      }
      if (Visible != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SyncParameter other) {
      if (other == null) {
        return;
      }
      if (other.Value.Length != 0) {
        Value = other.Value;
      }
      if (other.ParameterGroup.Length != 0) {
        ParameterGroup = other.ParameterGroup;
      }
      if (other.Visible != false) {
        Visible = other.Visible;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Value = input.ReadString();
            break;
          }
          case 18: {
            ParameterGroup = input.ReadString();
            break;
          }
          case 24: {
            Visible = input.ReadBool();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code