//generate by code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class LCLoginHandler : BasePacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Receive.LCLogin;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        //处理完数据和逻辑后,发送消息通知其他模块,绝对不可以直接操作UI等Unity主线程的东西!
        LCLogin login = packet as LCLogin;
        if (login == null)
        {
            Debug.LogError("消息错误!");
        }
        if (login.Result < 0)
        {
            Messenger.Broadcast(MessageId.UI_LOGIN_FAILED);
        }
        if (login.Result == 0)
        {
            Messenger.Broadcast(MessageId.UI_GAME_CREATE_CHARACTER);
        }
        if (login.Result == 1)
        {
            CLGetUserData data = new CLGetUserData();
            data.UserId = 1;
            Game.NetworkManager.Send(MessageId_Send.CLGetUserData, data);
        }

    }
}
