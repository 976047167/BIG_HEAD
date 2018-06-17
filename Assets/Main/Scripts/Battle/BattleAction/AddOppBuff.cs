using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using AppSettings;

public partial class BattleAction
{
    public class AddOppBuff : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.AddOppBuff; } }
        public override void Excute()
        {
            bool added = false;
            for (int i = 0; i < target.Data.BuffList.Count; i++)
            {
                if (actionArg == target.Data.BuffList[i].BuffId)
                {
                    added = true;
                    //刷新buff时间，不叠加
                    BattleBuffTableSetting buffData = BattleBuffTableSettings.Get(target.Data.BuffList[i].BuffId);
                    if (buffData.IsSuperposition == false)
                    {
                        target.Data.BuffList[i].Time = buffData.Time;
                    }
                    else
                    {
                        target.Data.BuffList[i].Time = target.Data.BuffList[i].Time + buffData.Time;
                        if (target.Data.BuffList[i].Time > buffData.MaxFloor)
                        {
                            target.Data.BuffList[i].Time = buffData.MaxFloor;
                        }
                    }
                    break;
                }
            }
            if (added == false)
            {
                BattleBuffData buffData = new BattleBuffData(actionArg, 0, cardData, owner, target);
                target.Data.BuffList.Add(buffData);
                battleMgr.AddUIAction(new UIAction.UIAddBuff(buffData));
            }
        }
    }
}
