using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class Attack : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.Attack; } }
        public override void Excute()
        {
            if (owner.IsMe)
            {
                target.Data.HP -= actionArg;
                battleMgr.AddUIAction(new UIAction.UIHpDamage(target, actionArg));
            }
            else if (owner == target)
            {
                owner.Data.HP -= actionArg;
                battleMgr.AddUIAction(new UIAction.UIHpDamage(owner, actionArg));
            }
        }
    }
}
