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
            Dic.Add((ushort)MessageId_Receive.GCSignIn, new GCSignInHandler());
            Dic.Add((ushort)MessageId_Receive.GCUpdatePlayerData, new GCUpdatePlayerDataHandler());
            Dic.Add((ushort)MessageId_Receive.GCEnterInstance, new GCEnterInstanceHandler());
            Dic.Add((ushort)MessageId_Receive.GCGetMapLayerData, new GCGetMapLayerDataHandler());
            Dic.Add((ushort)MessageId_Receive.GCMapGetReward, new GCMapGetRewardHandler());
            Dic.Add((ushort)MessageId_Receive.GCExitInstance, new GCExitInstanceHandler());
            Dic.Add((ushort)MessageId_Receive.GCMapCardInteraction, new GCMapCardInteractionHandler());
            Dic.Add((ushort)MessageId_Receive.GCMapPlayerMove, new GCMapPlayerMoveHandler());
            Dic.Add((ushort)MessageId_Receive.GCMapBuyItem, new GCMapBuyItemHandler());
            Dic.Add((ushort)MessageId_Receive.GCMapChangeDeck, new GCMapChangeDeckHandler());
            Dic.Add((ushort)MessageId_Receive.GCMapUseItem, new GCMapUseItemHandler());
            Dic.Add((ushort)MessageId_Receive.GCUpdateMapPlayerData, new GCUpdateMapPlayerDataHandler());
            Dic.Add((ushort)MessageId_Receive.GCMapApplyEffect, new GCMapApplyEffectHandler());
            Dic.Add((ushort)MessageId_Receive.GCEnterBattle, new GCEnterBattleHandler());
            Dic.Add((ushort)MessageId_Receive.GCExitBattle, new GCExitBattleHandler());
        }
    }
}
