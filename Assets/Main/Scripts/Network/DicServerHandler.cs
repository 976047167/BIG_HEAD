//generate by code
using System.Collections.Generic;
namespace BigHead.Net
{
    public class DicServerHandler
    {
        public static Dictionary<ushort, BasePacketHandler> Dic = new Dictionary<ushort, BasePacketHandler>();
        public static void Register()
        {
            Dic.Clear();
            
            Dic.Add((ushort)MessageId_Send.CLLogin, new CLLoginHandler());
            Dic.Add((ushort)MessageId_Send.CLGetUserData, new CLGetUserDataHandler());
            Dic.Add((ushort)MessageId_Send.CLCreatePlayer, new CLCreatePlayerHandler());
            Dic.Add((ushort)MessageId_Send.CLEnterInstance, new CLEnterInstanceHandler());
            Dic.Add((ushort)MessageId_Send.CLEnterBattle, new CLEnterBattleHandler());
        }
    }
}
