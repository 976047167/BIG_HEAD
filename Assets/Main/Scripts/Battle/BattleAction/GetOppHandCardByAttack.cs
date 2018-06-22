using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class GetOppHandCardByAttack : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.GetOppHandCardByAttack; } }
        public override void Excute()
        {
            //与伤害无关的buff，通过通用触发
            List<BattleCardData> handList = target.Data.HandCardList;
            if (UnityEngine.Random.Range(0, 100) < actionArg)
            {
                if (owner.Data.HandCardList.Count >= BattleMgr.MAX_HAND_CARD_COUNT)
                {
                    return;
                }
                int cardIndex = UnityEngine.Random.Range(0, handList.Count);
                BattleCardData cardData = new BattleCardData(handList[cardIndex].CardId, owner);
                owner.Data.HandCardList.Add(cardData);
                battleMgr.AddUIAction(new UIAction.UIGetOppCard(handList[cardIndex], cardData));
                handList.Remove(handList[cardIndex]);
            }
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
