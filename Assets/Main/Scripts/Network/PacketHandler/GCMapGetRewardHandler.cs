//generate by code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class GCMapGetRewardHandler : BasePacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Receive.GCMapGetReward;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        GCMapGetReward data = packet as GCMapGetReward;
        //处理完数据和逻辑后,发送消息通知其他模块,绝对不可以直接操作UI等Unity主线程的东西!
        List<int> items = new List<int>();
        items.AddRange(data.Cards);
        items.AddRange(data.Equips);
        RewardData rewardData = new RewardData(data.Gold, data.Diamonds, data.OldLevel, data.OldExp, data.AddedExp, data.Food, items.ToArray());


        Messenger.Broadcast(MessageId.MAP_GET_REWARD, rewardData);
    }
}
