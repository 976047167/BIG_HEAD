using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;

public class LCLoginHandler : BasePacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)NetworkMessageId.LCLogin;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        throw new System.NotImplementedException(GetType().ToString());
    }
}
