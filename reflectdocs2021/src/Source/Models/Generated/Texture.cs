// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Texture.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Unity.Reflect.Model {

  /// <summary>Holder for reflection information generated from Texture.proto</summary>
  public static partial class TextureReflection {

    #region Descriptor
    /// <summary>File descriptor for Texture.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static TextureReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg1UZXh0dXJlLnByb3RvEgdSZWZsZWN0IkcKC1N5bmNUZXh0dXJlEgwKBG5h",
            "bWUYASABKAkSDgoGc291cmNlGAIgASgMEhoKEmNvbnZlcnRUb05vcm1hbE1h",
            "cBgDIAEoCEIWqgITVW5pdHkuUmVmbGVjdC5Nb2RlbGIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Unity.Reflect.Model.SyncTexture), global::Unity.Reflect.Model.SyncTexture.Parser, new[]{ "Name", "Source", "ConvertToNormalMap" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class SyncTexture : pb::IMessage<SyncTexture> {
    private static readonly pb::MessageParser<SyncTexture> _parser = new pb::MessageParser<SyncTexture>(() => new SyncTexture());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SyncTexture> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Unity.Reflect.Model.TextureReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SyncTexture() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SyncTexture(SyncTexture other) : this() {
      name_ = other.name_;
      source_ = other.source_;
      convertToNormalMap_ = other.convertToNormalMap_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SyncTexture Clone() {
      return new SyncTexture(this);
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "source" field.</summary>
    public const int SourceFieldNumber = 2;
    private pb::ByteString source_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Source {
      get { return source_; }
      set {
        source_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "convertToNormalMap" field.</summary>
    public const int ConvertToNormalMapFieldNumber = 3;
    private bool convertToNormalMap_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool ConvertToNormalMap {
      get { return convertToNormalMap_; }
      set {
        convertToNormalMap_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SyncTexture);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SyncTexture other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if (Source != other.Source) return false;
      if (ConvertToNormalMap != other.ConvertToNormalMap) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Source.Length != 0) hash ^= Source.GetHashCode();
      if (ConvertToNormalMap != false) hash ^= ConvertToNormalMap.GetHashCode();
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
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (Source.Length != 0) {
        output.WriteRawTag(18);
        output.WriteBytes(Source);
      }
      if (ConvertToNormalMap != false) {
        output.WriteRawTag(24);
        output.WriteBool(ConvertToNormalMap);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Source.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Source);
      }
      if (ConvertToNormalMap != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SyncTexture other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Source.Length != 0) {
        Source = other.Source;
      }
      if (other.ConvertToNormalMap != false) {
        ConvertToNormalMap = other.ConvertToNormalMap;
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
            Name = input.ReadString();
            break;
          }
          case 18: {
            Source = input.ReadBytes();
            break;
          }
          case 24: {
            ConvertToNormalMap = input.ReadBool();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code