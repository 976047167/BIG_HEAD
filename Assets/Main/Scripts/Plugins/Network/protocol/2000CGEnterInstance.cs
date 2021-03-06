// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: 2000_CGEnterInstance.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace BigHead.protocol {

  /// <summary>Holder for reflection information generated from 2000_CGEnterInstance.proto</summary>
  public static partial class CGEnterInstanceReflection {

    #region Descriptor
    /// <summary>File descriptor for 2000_CGEnterInstance.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CGEnterInstanceReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChoyMDAwX0NHRW50ZXJJbnN0YW5jZS5wcm90bxoMUEJEZWNrLnByb3RvIoYB",
            "Cg9DR0VudGVySW5zdGFuY2USEgoKaW5zdGFuY2VJZBgBIAEoBRIVCgRkZWNr",
            "GAIgASgLMgcuUEJEZWNrEgwKBGdvbGQYAyABKAUSDAoEZm9vZBgEIAEoBRIN",
            "CgVpdGVtcxgFIAMoBRIOCgZlcXVpcHMYBiADKAUSDQoFYnVmZnMYByADKAVC",
            "SwodY29tLndoYWxlaXNsYW5kLmdhbWUucHJvdG9jb2xCF0NHRW50ZXJJbnN0",
            "YW5jZVByb3RvY29sqgIQQmlnSGVhZC5wcm90b2NvbGIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::BigHead.protocol.PBDeckReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::BigHead.protocol.CGEnterInstance), global::BigHead.protocol.CGEnterInstance.Parser, new[]{ "InstanceId", "Deck", "Gold", "Food", "Items", "Equips", "Buffs" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// Request to Enter the instance
  /// </summary>
  public sealed partial class CGEnterInstance : pb::IMessage<CGEnterInstance> {
    private static readonly pb::MessageParser<CGEnterInstance> _parser = new pb::MessageParser<CGEnterInstance>(() => new CGEnterInstance());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CGEnterInstance> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::BigHead.protocol.CGEnterInstanceReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CGEnterInstance() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CGEnterInstance(CGEnterInstance other) : this() {
      instanceId_ = other.instanceId_;
      Deck = other.deck_ != null ? other.Deck.Clone() : null;
      gold_ = other.gold_;
      food_ = other.food_;
      items_ = other.items_.Clone();
      equips_ = other.equips_.Clone();
      buffs_ = other.buffs_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CGEnterInstance Clone() {
      return new CGEnterInstance(this);
    }

    /// <summary>Field number for the "instanceId" field.</summary>
    public const int InstanceIdFieldNumber = 1;
    private int instanceId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int InstanceId {
      get { return instanceId_; }
      set {
        instanceId_ = value;
      }
    }

    /// <summary>Field number for the "deck" field.</summary>
    public const int DeckFieldNumber = 2;
    private global::BigHead.protocol.PBDeck deck_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::BigHead.protocol.PBDeck Deck {
      get { return deck_; }
      set {
        deck_ = value;
      }
    }

    /// <summary>Field number for the "gold" field.</summary>
    public const int GoldFieldNumber = 3;
    private int gold_;
    /// <summary>
    ///带进去的金币
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Gold {
      get { return gold_; }
      set {
        gold_ = value;
      }
    }

    /// <summary>Field number for the "food" field.</summary>
    public const int FoodFieldNumber = 4;
    private int food_;
    /// <summary>
    ///带进去的食物
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Food {
      get { return food_; }
      set {
        food_ = value;
      }
    }

    /// <summary>Field number for the "items" field.</summary>
    public const int ItemsFieldNumber = 5;
    private static readonly pb::FieldCodec<int> _repeated_items_codec
        = pb::FieldCodec.ForInt32(42);
    private readonly pbc::RepeatedField<int> items_ = new pbc::RepeatedField<int>();
    /// <summary>
    ///携带的消耗品
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<int> Items {
      get { return items_; }
    }

    /// <summary>Field number for the "equips" field.</summary>
    public const int EquipsFieldNumber = 6;
    private static readonly pb::FieldCodec<int> _repeated_equips_codec
        = pb::FieldCodec.ForInt32(50);
    private readonly pbc::RepeatedField<int> equips_ = new pbc::RepeatedField<int>();
    /// <summary>
    ///装备在身上的装备
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<int> Equips {
      get { return equips_; }
    }

    /// <summary>Field number for the "buffs" field.</summary>
    public const int BuffsFieldNumber = 7;
    private static readonly pb::FieldCodec<int> _repeated_buffs_codec
        = pb::FieldCodec.ForInt32(58);
    private readonly pbc::RepeatedField<int> buffs_ = new pbc::RepeatedField<int>();
    /// <summary>
    ///备用
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<int> Buffs {
      get { return buffs_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as CGEnterInstance);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(CGEnterInstance other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (InstanceId != other.InstanceId) return false;
      if (!object.Equals(Deck, other.Deck)) return false;
      if (Gold != other.Gold) return false;
      if (Food != other.Food) return false;
      if(!items_.Equals(other.items_)) return false;
      if(!equips_.Equals(other.equips_)) return false;
      if(!buffs_.Equals(other.buffs_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (InstanceId != 0) hash ^= InstanceId.GetHashCode();
      if (deck_ != null) hash ^= Deck.GetHashCode();
      if (Gold != 0) hash ^= Gold.GetHashCode();
      if (Food != 0) hash ^= Food.GetHashCode();
      hash ^= items_.GetHashCode();
      hash ^= equips_.GetHashCode();
      hash ^= buffs_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (InstanceId != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(InstanceId);
      }
      if (deck_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Deck);
      }
      if (Gold != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Gold);
      }
      if (Food != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(Food);
      }
      items_.WriteTo(output, _repeated_items_codec);
      equips_.WriteTo(output, _repeated_equips_codec);
      buffs_.WriteTo(output, _repeated_buffs_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (InstanceId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(InstanceId);
      }
      if (deck_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Deck);
      }
      if (Gold != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Gold);
      }
      if (Food != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Food);
      }
      size += items_.CalculateSize(_repeated_items_codec);
      size += equips_.CalculateSize(_repeated_equips_codec);
      size += buffs_.CalculateSize(_repeated_buffs_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(CGEnterInstance other) {
      if (other == null) {
        return;
      }
      if (other.InstanceId != 0) {
        InstanceId = other.InstanceId;
      }
      if (other.deck_ != null) {
        if (deck_ == null) {
          deck_ = new global::BigHead.protocol.PBDeck();
        }
        Deck.MergeFrom(other.Deck);
      }
      if (other.Gold != 0) {
        Gold = other.Gold;
      }
      if (other.Food != 0) {
        Food = other.Food;
      }
      items_.Add(other.items_);
      equips_.Add(other.equips_);
      buffs_.Add(other.buffs_);
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
            InstanceId = input.ReadInt32();
            break;
          }
          case 18: {
            if (deck_ == null) {
              deck_ = new global::BigHead.protocol.PBDeck();
            }
            input.ReadMessage(deck_);
            break;
          }
          case 24: {
            Gold = input.ReadInt32();
            break;
          }
          case 32: {
            Food = input.ReadInt32();
            break;
          }
          case 42:
          case 40: {
            items_.AddEntriesFrom(input, _repeated_items_codec);
            break;
          }
          case 50:
          case 48: {
            equips_.AddEntriesFrom(input, _repeated_equips_codec);
            break;
          }
          case 58:
          case 56: {
            buffs_.AddEntriesFrom(input, _repeated_buffs_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
