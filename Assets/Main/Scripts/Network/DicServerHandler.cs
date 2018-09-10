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
            Dic.Add((ushort)MessageId_Send.CGSignIn, new CGSignInHandler());
            Dic.Add((ushort)MessageId_Send.CGCreatePlayer, new CGCreatePlayerHandler());
            Dic.Add((ushort)MessageId_Send.CGEnterInstance, new CGEnterInstanceHandler());
            Dic.Add((ushort)MessageId_Send.CGGetMapLayerData, new CGGetMapLayerDataHandler());
            Dic.Add((ushort)MessageId_Send.CGExitInstance, new CGExitInstanceHandler());
            Dic.Add((ushort)MessageId_Send.CGMapCardInteraction, new CGMapCardInteractionHandler());
            Dic.Add((ushort)MessageId_Send.CGMapPlayerMove, new CGMapPlayerMoveHandler());
            Dic.Add((ushort)MessageId_Send.CGMapBuyItem, new CGMapBuyItemHandler());
            Dic.Add((ushort)MessageId_Send.CGMapChangeDeck, new CGMapChangeDeckHandler());
            Dic.Add((ushort)MessageId_Send.CGMapUseItem, new CGMapUseItemHandler());
            Dic.Add((ushort)MessageId_Send.CGMapApplyEffect, new CGMapApplyEffectHandler());
            Dic.Add((ushort)MessageId_Send.CGEnterBattle, new CGEnterBattleHandler());
            Dic.Add((ushort)MessageId_Send.CGExitBattle, new CGExitBattleHandler());
        }
    }
}
