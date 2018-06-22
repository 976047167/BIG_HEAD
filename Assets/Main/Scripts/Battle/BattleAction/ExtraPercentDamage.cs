using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class ExtraPercentDamage : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.ExtraPercentDamage; } }
        public override void Excute()
        {
            Debug.LogError("this is a buff!");
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
