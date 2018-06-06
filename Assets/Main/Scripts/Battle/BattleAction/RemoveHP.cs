using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveHP : BattleActionBase
    {
        public static BattleActionType ActionType { get { return BattleActionType.RemoveHP; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
