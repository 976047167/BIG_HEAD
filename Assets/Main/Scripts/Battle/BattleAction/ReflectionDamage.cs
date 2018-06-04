using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class ReflectionDamage : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.ReflectionDamage; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
