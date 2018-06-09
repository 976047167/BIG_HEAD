using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveBuff : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.RemoveBuff; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
