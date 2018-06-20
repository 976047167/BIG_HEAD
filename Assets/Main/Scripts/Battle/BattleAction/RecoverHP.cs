using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RecoverHP : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.RecoverHP; } }
        public override void Excute()
        {
            owner.Data.HP += actionArg;
            if (owner.Data.HP>owner.Data.MaxHP)
            {
                owner.Data.HP = owner.Data.MaxHP;
            }
        }
    }
}
