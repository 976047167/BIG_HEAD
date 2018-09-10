//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class CGMapApplyEffectHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CGMapApplyEffect;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CGMapApplyEffect data = packet as CGMapApplyEffect;
        //处理完数据和逻辑后,发送消息通知客户端
        GCMapApplyEffect applyEffect = new GCMapApplyEffect();

        //applyEffect.PlayerId
    }
}
