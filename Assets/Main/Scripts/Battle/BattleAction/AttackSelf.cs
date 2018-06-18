using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class AttackSelf : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.AttackSelf; } }
        public override void Excute()
        {
            Create(BattleActionType.Attack, actionArg, actionArg2, cardData, owner, owner).Excute();
        }
    }
}
