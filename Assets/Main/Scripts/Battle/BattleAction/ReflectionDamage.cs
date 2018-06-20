using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class ReflectionDamage : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.ReflectionDamage; } }
        public override void Excute()
        {
            Debug.LogError("this is a buff");
        }
    }
}
