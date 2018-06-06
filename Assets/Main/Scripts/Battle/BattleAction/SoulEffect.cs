using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class SoulEffect : BattleActionBase
    {
        public static BattleActionType ActionType { get { return BattleActionType.SoulEffect; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
