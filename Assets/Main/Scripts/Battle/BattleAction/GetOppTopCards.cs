using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class GetOppTopCards : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.GetOppTopCards; } }
        public override void Excute()
        {
            int count = actionArg;
            for (int i = 0; i < count; i++)
            {
                if (target.Data.HandCardList.Count >= BattleMgr.MAX_HAND_CARD_COUNT)
                {
                    return;
                }
                if (target.Data.CurrentCardList.Count <= 0)
                {
                    for (int j = 0; j < target.Data.CardList.Count; j++)
                    {
                        target.Data.CurrentCardList.Add(new BattleCardData(target.Data.CardList[j].Data.Id, target.Data.CardList[j].Owner));
                    }
                    //playerData.CurrentCardList = new List<BattleCardData>(playerData.CardList);
                }
                BattleCardData card = target.Data.CurrentCardList[target.Data.CurrentCardList.Count - 1];
                target.Data.CurrentCardList.Remove(card);
                BattleCardData drawedCard = new BattleCardData(card.CardId, owner);
                owner.Data.HandCardList.Add(drawedCard);
                battleMgr.AddUIAction(new UIAction.UIDrawOppCard(drawedCard));
            }
        }
    }
}
