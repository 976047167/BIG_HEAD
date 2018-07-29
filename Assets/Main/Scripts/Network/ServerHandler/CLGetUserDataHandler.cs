//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class CLGetUserDataHandler : BasePacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CLGetUserData;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        //处理完数据和逻辑后,发送消息通知客户端
        CLGetUserData data = packet as CLGetUserData;
        LCGetUserData userData = new LCGetUserData();
        userData.CharacterId = data.UserId;
        Game.NetworkManager.Session.OnMessage((ushort)MessageId_Receive.LCGetUserData, userData);
    }
}
