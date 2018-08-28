// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: 3001_GCExitBattle.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace BigHead.protocol {

  /// <summary>Holder for reflection information generated from 3001_GCExitBattle.proto</summary>
  public static partial class GCExitBattleReflection {

    #region Descriptor
    /// <summary>File descriptor for 3001_GCExitBattle.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GCExitBattleReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChczMDAxX0dDRXhpdEJhdHRsZS5wcm90bxoSUEJQbGF5ZXJEYXRhLnByb3Rv",
            "IjIKDEdDRXhpdEJhdHRsZRISCgptb25zdGVyX2lkGAEgASgFEg4KBnJlYXNv",
            "bhgCIAEoBUJICh1jb20ud2hhbGVpc2xhbmQuZ2FtZS5wcm90b2NvbEIUQ0dF",
            "eGl0QmF0dGxlUHJvdG9jb2yqAhBCaWdIZWFkLnByb3RvY29sYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::BigHead.protocol.PBPlayerDataReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::BigHead.protocol.GCExitBattle), global::BigHead.protocol.GCExitBattle.Parser, new[]{ "MonsterId", "Reason" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// 进入战斗请求
  /// </summary>
  public sealed partial class GCExitBattle : pb::IMessage<GCExitBattle> {
    private static readonly pb::MessageParser<GCExitBattle> _parser = new pb::MessageParser<GCExitBattle>(() => new GCExitBattle());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GCExitBattle> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::BigHead.protocol.GCExitBattleReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GCExitBattle() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GCExitBattle(GCExitBattle other) : this() {
      monsterId_ = other.monsterId_;
      reason_ = other.reason_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GCExitBattle Clone() {
      return new GCExitBattle(this);
    }

    /// <summary>Field number for the "monster_id" field.</summary>
    public const int MonsterIdFieldNumber = 1;
    private int monsterId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int MonsterId {
      get { return monsterId_; }
      set {
        monsterId_ = value;
      }
    }

    /// <summary>Field number for the "reason" field.</summary>
    public const int ReasonFieldNumber = 2;
    private int reason_;
    /// <summary>
    ///0Win 1Failure 2Me Escape 3Monster Escape
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Reason {
      get { return reason_; }
      set {
        reason_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as GCExitBattle);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GCExitBattle other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (MonsterId != other.MonsterId) return false;
      if (Reason != other.Reason) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (MonsterId != 0) hash ^= MonsterId.GetHashCode();
      if (Reason != 0) hash ^= Reason.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (MonsterId != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(MonsterId);
      }
      if (Reason != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Reason);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (MonsterId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(MonsterId);
      }
      if (Reason != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Reason);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(GCExitBattle other) {
      if (other == null) {
        return;
      }
      if (other.MonsterId != 0) {
        MonsterId = other.MonsterId;
      }
      if (other.Reason != 0) {
        Reason = other.Reason;
      }
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
            MonsterId = input.ReadInt32();
            break;
          }
          case 16: {
            Reason = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
