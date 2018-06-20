using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class DodgeDamage : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.DodgeDamage; } }
        public override void Excute()
        {
            Debug.LogError("这是一个buff");
        }
    }
}
