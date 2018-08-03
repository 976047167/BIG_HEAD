//generate by code
using System.Collections.Generic;
namespace BigHead.Net
{
    public class DicHandler
    {
        public static Dictionary<ushort, BasePacketHandler> Dic = new Dictionary<ushort, BasePacketHandler>();
        public static void Register()
        {
            Dic.Clear();
            
            Dic.Add((ushort)MessageId_Receive.GCLogin, new GCLoginHandler());
            Dic.Add((ushort)MessageId_Receive.GCGetUserData, new GCGetUserDataHandler());
            Dic.Add((ushort)MessageId_Receive.GCEnterInstance, new GCEnterInstanceHandler());
            Dic.Add((ushort)MessageId_Receive.GCEnterBattle, new GCEnterBattleHandler());
        }
    }
}
