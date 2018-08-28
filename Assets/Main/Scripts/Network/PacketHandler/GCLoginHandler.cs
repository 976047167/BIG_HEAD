//generate by code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class GCLoginHandler : BasePacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Receive.GCLogin;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        //处理完数据和逻辑后,发送消息通知其他模块,绝对不可以直接操作UI等Unity主线程的东西!
        GCLogin login = packet as GCLogin;
        if (login == null)
        {
            Debug.LogError("消息错误!");
        }
        Game.DataManager.InitAccount(login.AccountData);
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
            CGSignIn data = new CGSignIn();
            data.UserId = 1;
            Game.NetworkManager.SendToLobby(MessageId_Send.CGSignIn, data);
        }

    }
}
