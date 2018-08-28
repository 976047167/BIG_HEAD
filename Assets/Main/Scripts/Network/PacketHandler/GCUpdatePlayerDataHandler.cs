//generate by code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class GCUpdatePlayerDataHandler : BasePacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Receive.GCUpdatePlayerData;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        GCUpdatePlayerData data = packet as GCUpdatePlayerData;
        //处理完数据和逻辑后,发送消息通知其他模块,绝对不可以直接操作UI等Unity主线程的东西!
        //此处发送消息不允许使用Messenger.BroadcastSync同步通知UI
        Game.DataManager.UpdateAccount(data.AccountData);
        //目前没有皮肤了，暂时默认皮肤了
        Game.DataManager.UpdatePlayer(data.PlayerData, data.PlayerDetailData);

        Messenger.BroadcastAsync(MessageId.GAME_UPDATE_PLAYER_INFO);
    }
}
