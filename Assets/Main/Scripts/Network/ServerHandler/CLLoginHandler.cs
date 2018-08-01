//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;
using UnityEngine;

public class CLLoginHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CLLogin;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CLLogin data = packet as CLLogin;
        //处理完数据和逻辑后,发送消息通知客户端
        LCLogin login = new LCLogin();
        CLLogin read = GetSavedData<CLLogin>("login");
        if (read != null)
        {
            if (read.UserId != data.UserId)
            {
                //新玩家
                login.Result = 0;
            }
            else
            {
                login.Result = 1;
            }
        }
        else
        {
            SaveData("login", data);
            login.Result = 0;
        }
        //Debug.LogError(data.UserId.ToString());


        SendToClient(MessageId_Receive.LCLogin, login);
    }
}
