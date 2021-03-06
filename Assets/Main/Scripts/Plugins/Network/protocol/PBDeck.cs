// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: PBDeck.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace BigHead.protocol {

  /// <summary>Holder for reflection information generated from PBDeck.proto</summary>
  public static partial class PBDeckReflection {

    #region Descriptor
    /// <summary>File descriptor for PBDeck.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PBDeckReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgxQQkRlY2sucHJvdG8iRwoGUEJEZWNrEg0KBWluZGV4GAEgASgFEhEKCW1h",
            "eF9jb3VudBgCIAEoBRIMCgRuYW1lGAMgASgJEg0KBWNhcmRzGAQgAygFQkIK",
            "HWNvbS53aGFsZWlzbGFuZC5nYW1lLnByb3RvY29sQg5QQkRlY2tQcm90b2Nv",
            "bKoCEEJpZ0hlYWQucHJvdG9jb2xiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::BigHead.protocol.PBDeck), global::BigHead.protocol.PBDeck.Parser, new[]{ "Index", "MaxCount", "Name", "Cards" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// deck
  /// </summary>
  public sealed partial class PBDeck : pb::IMessage<PBDeck> {
    private static readonly pb::MessageParser<PBDeck> _parser = new pb::MessageParser<PBDeck>(() => new PBDeck());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PBDeck> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::BigHead.protocol.PBDeckReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PBDeck() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PBDeck(PBDeck other) : this() {
      index_ = other.index_;
      maxCount_ = other.maxCount_;
      name_ = other.name_;
      cards_ = other.cards_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PBDeck Clone() {
      return new PBDeck(this);
    }

    /// <summary>Field number for the "index" field.</summary>
    public const int IndexFieldNumber = 1;
    private int index_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Index {
      get { return index_; }
      set {
        index_ = value;
      }
    }

    /// <summary>Field number for the "max_count" field.</summary>
    public const int MaxCountFieldNumber = 2;
    private int maxCount_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int MaxCount {
      get { return maxCount_; }
      set {
        maxCount_ = value;
      }
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 3;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "cards" field.</summary>
    public const int CardsFieldNumber = 4;
    private static readonly pb::FieldCodec<int> _repeated_cards_codec
        = pb::FieldCodec.ForInt32(34);
    private readonly pbc::RepeatedField<int> cards_ = new pbc::RepeatedField<int>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<int> Cards {
      get { return cards_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PBDeck);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PBDeck other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Index != other.Index) return false;
      if (MaxCount != other.MaxCount) return false;
      if (Name != other.Name) return false;
      if(!cards_.Equals(other.cards_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Index != 0) hash ^= Index.GetHashCode();
      if (MaxCount != 0) hash ^= MaxCount.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      hash ^= cards_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Index != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Index);
      }
      if (MaxCount != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(MaxCount);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Name);
      }
      cards_.WriteTo(output, _repeated_cards_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Index != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Index);
      }
      if (MaxCount != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(MaxCount);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      size += cards_.CalculateSize(_repeated_cards_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PBDeck other) {
      if (other == null) {
        return;
      }
      if (other.Index != 0) {
        Index = other.Index;
      }
      if (other.MaxCount != 0) {
        MaxCount = other.MaxCount;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      cards_.Add(other.cards_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Index = input.ReadInt32();
            break;
          }
          case 16: {
            MaxCount = input.ReadInt32();
            break;
          }
          case 26: {
            Name = input.ReadString();
            break;
          }
          case 34:
          case 32: {
            cards_.AddEntriesFrom(input, _repeated_cards_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
