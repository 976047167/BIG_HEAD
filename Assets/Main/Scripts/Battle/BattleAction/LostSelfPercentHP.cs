using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class LostSelfPercentHP : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.LostSelfPercentHP; } }
        public override void Excute()
        {
            int damage = Mathf.RoundToInt(owner.Data.HP * (actionArg / 100f));
            Create(BattleActionType.Attack, damage, 0, sourceData, owner, owner).Excute();
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
