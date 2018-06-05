using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RecoverHP : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.RecoverHP; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
