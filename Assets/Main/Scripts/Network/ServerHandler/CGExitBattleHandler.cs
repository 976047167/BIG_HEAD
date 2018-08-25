//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class CGExitBattleHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CGExitBattle;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CGExitBattle data = packet as CGExitBattle;
        //处理完数据和逻辑后,发送消息通知客户端
        throw new System.NotImplementedException(GetType().ToString());
    }
}
