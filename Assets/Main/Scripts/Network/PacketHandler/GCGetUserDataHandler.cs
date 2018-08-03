//generate by code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class GCGetUserDataHandler : BasePacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Receive.GCGetUserData;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        GCGetUserData data = packet as GCGetUserData;
        //处理完数据和逻辑后,发送消息通知其他模块,绝对不可以直接操作UI等Unity主线程的东西!

        Game.DataManager.InitAccount(data.AccountData);
        //目前没有皮肤了，暂时默认皮肤了
        Game.DataManager.InitPlayer(data.PlayerData, data.PlayerDetailData);

        Messenger.Broadcast(MessageId.UI_GAME_START);
    }
}
