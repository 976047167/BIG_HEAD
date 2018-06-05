using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class AttackSelf : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.AttackSelf; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
