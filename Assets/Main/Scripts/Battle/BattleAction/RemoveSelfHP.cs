using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveSelfHP : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.RemoveSelfHP; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
