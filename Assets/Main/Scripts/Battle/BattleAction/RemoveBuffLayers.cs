using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveBuffLayers : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.RemoveBuffLayers; } }
        public override void Excute()
        {
            if (sourceData.ItemType == BattleEffectItemType.Buff)
            {
                BattleBuffData buff = sourceData as BattleBuffData;
                buff.Layer -= actionArg;
                if (buff.Layer <= 0)
                {
                    sourceData.Owner.Data.BuffList.Remove(buff);
                }
                battleMgr.AddUIAction(new UIAction.UIRemoveBuff(target, buff.BuffId, actionArg));
                return;
            }
            else
                Debug.LogError("这是BUFF专用效果!");
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
