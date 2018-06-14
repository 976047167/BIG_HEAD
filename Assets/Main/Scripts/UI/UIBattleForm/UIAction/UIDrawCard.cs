using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIDrawCard : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.DrawCard; } }

        public BattleCardData CardData { get; private set; }

        public UIDrawCard(BattleCardData data) : base()
        {
            CardData = data;
        }

        public override IEnumerator Excute()
        {
            yield return null;
            if (CardData.Owner == Game.BattleManager.MyPlayer)
            {
                BattleForm.CreateBattleCard(CardData, BattleForm.MyCardsGrid);
            }
            else
            {
                BattleForm.CreateBattleCard(CardData, BattleForm.OppCardsGrid);
            }
            //BattleForm.GetPlayerInfoViewByPlayer(CardData.Owner).DrawCard();
            PlayerInfoView playerInfoView = BattleForm.GetPlayerInfoViewByPlayer(CardData.Owner);
            playerInfoView.PlayerInfo.CardCount--;
            if (playerInfoView.PlayerInfo.CardCount <= 0)
            {
                playerInfoView.PlayerInfo.CardCount = playerInfoView.BindPlayerData.CurrentCardList.Count;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
