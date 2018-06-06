using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class DefenseDamage : BattleActionBase
    {
        public static BattleActionType ActionType { get { return BattleActionType.DefenseDamage; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
