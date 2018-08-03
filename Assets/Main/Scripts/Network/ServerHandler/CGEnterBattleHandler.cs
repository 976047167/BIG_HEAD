//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class CGEnterBattleHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CGEnterBattle;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CGEnterBattle data = packet as CGEnterBattle;
        //处理完数据和逻辑后,发送消息通知客户端
        GCEnterBattle enterBattle = new GCEnterBattle();
        enterBattle.MonsterId = data.MonsterId;
        SendToClient(MessageId_Receive.GCEnterBattle, enterBattle);
    }
}
