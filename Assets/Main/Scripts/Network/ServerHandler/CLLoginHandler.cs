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
        //SaveData("login", data);
        //Debug.LogError(data.UserId.ToString());
        //CLLogin read = GetSavedData<CLLogin>("login");
        //if (read != null)
        //{
        //    Debug.LogError(read.UserId.ToString());
        //}

        LCLogin login = new LCLogin();
        login.Result = 0;
        SendToClient(MessageId_Receive.LCLogin, login);
    }
}
