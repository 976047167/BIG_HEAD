//generate by code
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf;
using BigHead.protocol;
namespace BigHead.Net
{
    public class DicParser
    {
        public static Dictionary<ushort, MessageParser> Dic = new Dictionary<ushort, MessageParser>();
        public static void Register()
        {
            Dic.Clear();
            
            Dic.Add((ushort)MessageId_Receive.LCEnterBattle, LCEnterBattle.Parser);
            Dic.Add((ushort)MessageId_Receive.LCGetUserData, LCGetUserData.Parser);
            Dic.Add((ushort)MessageId_Receive.LCLogin, LCLogin.Parser);
            Dic.Add((ushort)MessageId_Receive.LCLogout, LCLogout.Parser);
        }
    }
}
