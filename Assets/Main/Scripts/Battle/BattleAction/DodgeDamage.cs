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
            throw new System.NotImplementedException();
        }
    }
}
