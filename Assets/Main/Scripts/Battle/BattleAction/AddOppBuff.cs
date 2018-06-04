using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class AddOppBuff : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.AddOppBuff; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
