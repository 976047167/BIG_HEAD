using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveBuff : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.RemoveBuff; } }
        public override void Excute()
        {
            for (int i = 0; i < target.Data.BuffList.Count; i++)
            {
                if (actionArg == target.Data.BuffList[i].BuffId)
                {
                    target.Data.BuffList.RemoveAt(i);
                    battleMgr.AddUIAction(new UIAction.UIRemoveBuff(target, actionArg));
                    break;
                }
            }
        }
    }
}
