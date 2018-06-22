using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class DrawCard : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.DrawCard; } }
        public override void Excute()
        {
            int count = actionArg;
            for (int i = 0; i < count; i++)
            {
                if (owner.Data.HandCardList.Count >= BattleMgr.MAX_HAND_CARD_COUNT)
                {
                    return;
                }
                if (owner.Data.CurrentCardList.Count <= 0)
                {
                    for (int j = 0; j < owner.Data.CardList.Count; j++)
                    {
                        owner.Data.CurrentCardList.Add(new BattleCardData(owner.Data.CardList[j].Data.Id, owner.Data.CardList[j].Owner));
                    }
                    //playerData.CurrentCardList = new List<BattleCardData>(playerData.CardList);
                }
                BattleCardData card = owner.Data.CurrentCardList[UnityEngine.Random.Range(0, owner.Data.CurrentCardList.Count)];
                owner.Data.CurrentCardList.Remove(card);
                owner.Data.HandCardList.Add(card);
                battleMgr.AddUIAction(new UIAction.UIDrawCard(card));
            }
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
