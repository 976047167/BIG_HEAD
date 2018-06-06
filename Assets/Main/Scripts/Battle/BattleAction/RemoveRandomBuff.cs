using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveRandomBuff : BattleActionBase
    {
        public static BattleActionType ActionType { get { return BattleActionType.RemoveRandomBuff; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
