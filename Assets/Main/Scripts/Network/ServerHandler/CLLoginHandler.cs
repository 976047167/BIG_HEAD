//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class CLLoginHandler : BasePacketHandler
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
        //处理完数据和逻辑后,发送消息通知客户端
        LCLogin login = new LCLogin();
        login.Result = 0;
        Game.NetworkManager.Session.OnMessage((ushort)MessageId_Receive.LCLogin, login);
    }
}
