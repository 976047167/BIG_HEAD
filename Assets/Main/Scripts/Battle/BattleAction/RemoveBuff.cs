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

                    battleMgr.AddUIAction(new UIAction.UIRemoveBuff(target, actionArg, -1));
                    target.Data.BuffList.RemoveAt(i);
                    break;
                }
            }
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
