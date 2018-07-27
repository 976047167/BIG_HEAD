//generate by code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;

public class LCLogoutHandler : BasePacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)NetworkMessageId.LCLogout;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        throw new System.NotImplementedException(GetType().ToString());
    }
}
