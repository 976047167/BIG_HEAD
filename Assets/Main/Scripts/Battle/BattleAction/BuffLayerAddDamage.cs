using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class BuffLayerAddDamage : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.BuffLayerDamage; } }
        public override void Excute()
        {
            Debug.LogError("����һ������Ч��");
        }
    }
}
