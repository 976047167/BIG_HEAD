using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveMP : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.RemoveMP; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
