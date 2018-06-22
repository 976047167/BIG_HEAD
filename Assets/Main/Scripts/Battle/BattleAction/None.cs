using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class None : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.None; } }
        public override void Excute()
        {

        }

        public override int Excute(int damage)
        {
            return 0;
        }
    }
}
