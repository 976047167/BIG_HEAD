using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;
/// <summary>
/// 离线模式逻辑，按照联网游戏的逻辑，写他妈的一个服务器
/// </summary>
public sealed class OfflineNetworkSession : INetworkSession
{
    string name = "";
    NetState netState = NetState.None;
    public OfflineNetworkSession(string name)
    {
        this.name = name;
    }

    public void Connect(string ip, int port)
    {
        netState = NetState.Connected;
        Messenger.BroadcastAsync(MessageId.NetworkConnect, name);
    }

    public void Send(MessageId_Send msgId, IMessage msg)
    {
        Debug.Log("Send -> " + msgId.ToString());
        DicServerHandler.Dic[(ushort)msgId].Handle(this, msg);
    }
    public void OnMessage(ushort msgId, IMessage data)
    {
        DicHandler.Dic[(ushort)msgId].Handle(this, data);
    }
    public void Close()
    {
        throw new System.NotImplementedException();
    }
}
