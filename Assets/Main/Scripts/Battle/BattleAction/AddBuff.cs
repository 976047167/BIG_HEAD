using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using AppSettings;

public partial class BattleAction
{
    public class AddBuff : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.AddBuff; } }
        public override void Excute()
        {
            Debug.LogError((owner.IsMe ? "[我]" : "[怪]") + "添加了一个buff-> " + actionArg + "[" + actionArg2 + "]");
            bool added = false;
            for (int i = 0; i < owner.Data.BuffList.Count; i++)
            {
                if (actionArg == owner.Data.BuffList[i].BuffId)
                {
                    added = true;
                    //刷新buff时间，不叠加
                    BattleBuffTableSetting buffData = BattleBuffTableSettings.Get(owner.Data.BuffList[i].BuffId);
                    if (buffData.IsOverlay == false)
                    {
                        owner.Data.BuffList[i].Time = buffData.Time;
                    }
                    else
                    {
                        owner.Data.BuffList[i].Time = owner.Data.BuffList[i].Time + buffData.Time;
                        if (owner.Data.BuffList[i].Time > buffData.MaxLayer)
                        {
                            owner.Data.BuffList[i].Time = buffData.MaxLayer;
                        }
                    }
                    break;
                }
            }
            if (added == false)
            {
                BattleBuffData buffData = new BattleBuffData(actionArg, actionArg2, sourceData, owner, owner);
                owner.Data.BuffList.Add(buffData);
                //UI用独立的buff数据，和血量什么的一样
                battleMgr.AddUIAction(new UIAction.UIAddBuff(buffData));
                //battleMgr.AddUIAction(new UIAction.UIAddBuff(new BattleBuffData(actionArg, actionArg2, sourceData, owner, owner)));
            }
        }
        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
