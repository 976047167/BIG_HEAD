using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIUseCard : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.UseCard; } }

        public BattleCardData CardData { get; private set; }
        public UIUseCard(BattleCardData cardData) : base()
        {
            CardData = cardData;
        }

        public override IEnumerator Excute()
        {
            UIBattleCard battleCard = BattleForm.GetUIBattleCard(CardData);
            if (battleCard == null)
            {
                yield return null;
            }
            battleCard.UseCard();
            Vector3 cachePos = battleCard.cacheChildCardTrans.position;
            battleCard.transform.SetParent(BattleForm.UsedCardsGrid.transform, false);
            BattleForm.UsedCardsGrid.Reposition();
            BattleForm.MyCardsGrid.Reposition();

            battleCard.cacheChildCardTrans.position = cachePos;
            yield return null;
            TweenPosition.Begin(battleCard.cacheChildCardTrans.gameObject, 0.5f, Vector3.zero, false);
            PlayerInfoView playerInfoView = BattleForm.GetPlayerInfoViewByPlayer(CardData.Owner);
            playerInfoView.PlayerInfo.CemeteryCount++;
            //playerInfoView.PlayerInfo.AP -= battleCard.CardData.Data.Spending;
            yield return new WaitForSeconds(0.5f);
            BattleForm.OppCardsGrid.Reposition();
            battleCard.RefreshDepth();
            yield return null;
        }
    }
}
