using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class GroupOverlayDamage : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.GroupOverlayDamage; } }
        public override void Excute()
        {
            Debug.LogError("This is a Buff!");
        }

        public override int Excute(int damage)
        {
            if (sourceData.ItemType == BattleEffectItemType.Card)
            {
                BattleCardData cardData = sourceData as BattleCardData;
                battleMgr.SetRoundCounter("CardGroup" + cardData.Data.GroupId, 1);
                int count = battleMgr.GetBattleCounter("CardGroup" + cardData.Data.GroupId);
                return damage + count * actionArg;
            }
            Debug.LogError("不支持非卡牌用此效果!");
            return damage;
        }
    }
}
