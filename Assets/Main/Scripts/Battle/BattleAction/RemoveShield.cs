using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveShield : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.RemoveShield; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
