using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class DefenseDamage : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.DefenseDamage; } }
        public override void Excute()
        {
            Debug.LogError("这是一个被动buff");
        }
    }
}
