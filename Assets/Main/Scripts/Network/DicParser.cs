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
            Dic.Add((ushort)MessageId_Receive.GCSignIn, GCSignIn.Parser);
            Dic.Add((ushort)MessageId_Receive.GCUpdatePlayerData, GCUpdatePlayerData.Parser);
            Dic.Add((ushort)MessageId_Receive.GCEnterInstance, GCEnterInstance.Parser);
            Dic.Add((ushort)MessageId_Receive.GCGetMapLayerData, GCGetMapLayerData.Parser);
            Dic.Add((ushort)MessageId_Receive.GCMapGetReward, GCMapGetReward.Parser);
            Dic.Add((ushort)MessageId_Receive.GCExitInstance, GCExitInstance.Parser);
            Dic.Add((ushort)MessageId_Receive.GCMapCardInteraction, GCMapCardInteraction.Parser);
            Dic.Add((ushort)MessageId_Receive.GCMapPlayerMove, GCMapPlayerMove.Parser);
            Dic.Add((ushort)MessageId_Receive.GCMapBuyItem, GCMapBuyItem.Parser);
            Dic.Add((ushort)MessageId_Receive.GCMapChangeDeck, GCMapChangeDeck.Parser);
            Dic.Add((ushort)MessageId_Receive.GCMapUseItem, GCMapUseItem.Parser);
            Dic.Add((ushort)MessageId_Receive.GCUpdateMapPlayerData, GCUpdateMapPlayerData.Parser);
            Dic.Add((ushort)MessageId_Receive.GCEnterBattle, GCEnterBattle.Parser);
            Dic.Add((ushort)MessageId_Receive.GCExitBattle, GCExitBattle.Parser);
        }
    }
}
