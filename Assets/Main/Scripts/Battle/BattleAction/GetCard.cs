using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class GetCard : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.GetCard; } }
        public override void Excute()
        {
            for (int i = 0; i < actionArg; i++)
            {
                if (owner.Data.HandCardList.Count >= BattleMgr.MAX_HAND_CARD_COUNT)
                {
                    return;
                }
                BattleCardData cardData = new BattleCardData(actionArg2, owner);
                owner.Data.HandCardList.Add(cardData);
                battleMgr.AddUIAction(new UIAction.UIGetCard(cardData));
            }
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
