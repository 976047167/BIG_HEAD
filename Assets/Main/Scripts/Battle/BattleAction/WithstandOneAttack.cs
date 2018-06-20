using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class WithstandOneAttack : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.WithstandOneAttack; } }
        public override void Excute()
        {
            Debug.LogError("这是一个buff");
        }
    }
}
