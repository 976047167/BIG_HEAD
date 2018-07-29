using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BigHead.Net
{
    public interface INetworkSession
    {
        void Connect(string ip, int port);
        void Send(MessageId_Send msgId, IMessage msg);
        void Close();
        void OnMessage(ushort msgId, IMessage data);
    }
}
public enum NetState
{
    None = 0,
    Inited,
    Connecting,
    Connected,
    Closed,
}
