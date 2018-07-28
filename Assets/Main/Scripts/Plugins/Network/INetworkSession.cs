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
    }
}
