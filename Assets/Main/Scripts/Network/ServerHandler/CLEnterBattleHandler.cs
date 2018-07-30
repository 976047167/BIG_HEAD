//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class CLEnterBattleHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CLEnterBattle;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CLEnterBattle data = packet as CLEnterBattle;
        //处理完数据和逻辑后,发送消息通知客户端
        LCEnterBattle enterBattle = new LCEnterBattle();
        enterBattle.MonsterId = data.MonsterId;
        SendToClient(MessageId_Receive.LCEnterBattle, enterBattle);
    }
}
