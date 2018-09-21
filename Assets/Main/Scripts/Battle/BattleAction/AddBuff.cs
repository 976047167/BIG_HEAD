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
            Debug.LogError((owner.IsMe ? "[我]" : "[怪]") + "添加了一个buff-> " + actionArg + 
                "[L:" + actionArg2 + "][T:" + BattleBuffTableSettings.Get(actionArg).Time + "]");
            //buff已经存在的标识
            bool added = false;
            for (int i = 0; i < owner.Data.BuffList.Count; i++)
            {
                BattleBuffData buff = owner.Data.BuffList[i];
                if (actionArg == buff.BuffId)
                {
                    added = true;
                    //刷新buff时间，不叠加
                    BattleBuffTableSetting buffData = BattleBuffTableSettings.Get(buff.BuffId);
                    if (buffData.IsOverlay)
                    {
                        buff.Layer = Mathf.Max(buff.Layer + buffData.DefaultLayer, buffData.MaxLayer);
                    }
                    //有持续时间的
                    if (buff.Time >= 0)
                    {
                        //buff.Time = Mathf.Max(buff.Time + buffData.Time, buffData.MaxLayer);
                        buff.Time = buff.Time + buffData.Time;
                    }
                    break;
                }
            }
            if (added == false)
            {
                BattleBuffData buffData = null;
                if (userdata != null && (userdata is BattleBuffData))
                {
                    //转移buff
                    BattleBuffData sourceBuff = (userdata as BattleBuffData);
                    buffData = new BattleBuffData(actionArg, sourceBuff.Time, sourceBuff.Layer, sourceData, owner, owner);
                }
                else
                {
                    //新增buff
                    buffData = new BattleBuffData(actionArg, 0, actionArg2, sourceData, owner, owner);
                }
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
