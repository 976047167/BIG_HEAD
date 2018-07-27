using Google.Protobuf;

namespace BigHead.Net
{
    public abstract class BasePacketHandler
    {
        public abstract ushort OpCode { get; }

        public virtual void Handle(object sender, IMessage packet)
        {
//            PacketType packetType;
//            int packetActionId;

//            NetworkHelper.ParseOpCode(packet.Id, out packetType, out packetActionId);
//            if (NetworkHelper.NeedWaiting(packetType, packetActionId))
//            {
//                GameEntry.Waiting.StopWaiting(WaitingType.Network, packetActionId.ToString());
//            }
//#if CSF
//            NetLog.LogPackage(packet);
//#endif
        }
    }
}
