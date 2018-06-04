using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveHP : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.Attack; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}