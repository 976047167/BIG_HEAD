//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class CGEnterInstanceHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CGEnterInstance;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CGEnterInstance data = packet as CGEnterInstance;
        //处理完数据和逻辑后,发送消息通知客户端
        throw new System.NotImplementedException(GetType().ToString());
    }
}
