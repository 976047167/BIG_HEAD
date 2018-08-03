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
            
            Dic.Add((ushort)MessageId_Send.CGLogin, new CGLoginHandler());
            Dic.Add((ushort)MessageId_Send.CGLogout, new CGLogoutHandler());
            Dic.Add((ushort)MessageId_Send.CGGetUserData, new CGGetUserDataHandler());
            Dic.Add((ushort)MessageId_Send.CGCreatePlayer, new CGCreatePlayerHandler());
            Dic.Add((ushort)MessageId_Send.CGEnterInstance, new CGEnterInstanceHandler());
            Dic.Add((ushort)MessageId_Send.CGEnterBattle, new CGEnterBattleHandler());
        }
    }
}
