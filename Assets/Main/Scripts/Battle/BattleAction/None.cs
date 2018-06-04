using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class None : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.None; } }
        public override void Excute()
        {
            
        }
    }
}
