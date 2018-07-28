using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;
/// <summary>
/// 离线模式逻辑，按照联网游戏的逻辑，写他妈的一个服务器
/// </summary>
public sealed class OfflineNetworkSession : INetworkSession
{
    string name = "";
    public OfflineNetworkSession(string name)
    {
        this.name = name;
    }

    public void Connect(string ip, int port)
    {
        throw new System.NotImplementedException();
    }

    public void Send(MessageId_Send msgId, IMessage msg)
    {
        switch (msgId)
        {
            case MessageId_Send.None:
                break;
            case MessageId_Send.CLLogin:

                break;
            case MessageId_Send.MAX:
                break;
            default:
                break;
        }
    }
    void OnMessage(MessageId_Send msgId, IMessage data)
    {
        DicHandler.Dic[(ushort)msgId].Handle(this, data);
    }
    public void Close()
    {
        throw new System.NotImplementedException();
    }
}
