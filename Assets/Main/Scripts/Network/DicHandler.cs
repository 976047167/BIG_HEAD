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
            
            Dic.Add((ushort)MessageId_Receive.LCLogin, new LCLoginHandler());
            Dic.Add((ushort)MessageId_Receive.LCLogout, new LCLogoutHandler());
        }
    }
}
