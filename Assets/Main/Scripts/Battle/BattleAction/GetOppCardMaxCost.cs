using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class GetOppCardMaxCost : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.GetOppCardMaxCost; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
