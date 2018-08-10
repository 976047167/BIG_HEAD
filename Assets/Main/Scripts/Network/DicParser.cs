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
            
            Dic.Add((ushort)MessageId_Receive.GCLogin, GCLogin.Parser);
            Dic.Add((ushort)MessageId_Receive.GCGetUserData, GCGetUserData.Parser);
            Dic.Add((ushort)MessageId_Receive.GCEnterInstance, GCEnterInstance.Parser);
            Dic.Add((ushort)MessageId_Receive.GCGetMapLayerData, GCGetMapLayerData.Parser);
            Dic.Add((ushort)MessageId_Receive.GCEnterBattle, GCEnterBattle.Parser);
        }
    }
}
