using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
namespace BigHead.Net
{
    public abstract class BaseServerPacketHandler : BasePacketHandler
    {
        protected void SendToClient(MessageId_Receive messageId, IMessage message)
        {
            Game.NetworkManager.Session.OnMessage((ushort)messageId, message);
        }

    }
}
