using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveAllBuff : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.RemoveAllBuff; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
