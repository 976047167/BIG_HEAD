using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class GetOppCardMaxCost : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.GetOppCardMaxCost; } }
        public override void Excute()
        {
            List<BattleCardData> handList = target.Data.HandCardList;
            List<BattleCardData> sortList = new List<BattleCardData>(handList.Count);
            sortList.AddRange(handList);
            sortList.Sort((c1, c2) => c2.Data.Spending.CompareTo(c1.Data.Spending));
            if (actionArg > sortList.Count)
            {
                actionArg = sortList.Count;
            }
            for (int i = 0; i < actionArg; i++)
            {
                if (owner.Data.HandCardList.Count >= BattleMgr.MAX_HAND_CARD_COUNT)
                {
                    return;
                }
                BattleCardData cardData = new BattleCardData(sortList[i].CardId, owner);
                owner.Data.HandCardList.Add(cardData);
                handList.Remove(sortList[i]);
                battleMgr.AddUIAction(new UIAction.UIGetOppCard(sortList[i], cardData));
            }

        }
    }
}
