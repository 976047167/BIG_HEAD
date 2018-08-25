//generate by code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class GCGetRewardHandler : BasePacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Receive.GCGetReward;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        GCGetReward data = packet as GCGetReward;
        //处理完数据和逻辑后,发送消息通知其他模块,绝对不可以直接操作UI等Unity主线程的东西!
        throw new System.NotImplementedException(GetType().ToString());
    }
}
