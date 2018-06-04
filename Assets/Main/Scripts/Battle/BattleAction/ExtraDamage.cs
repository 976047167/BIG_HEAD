using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class ExtraDamage : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.ExtraDamage; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
