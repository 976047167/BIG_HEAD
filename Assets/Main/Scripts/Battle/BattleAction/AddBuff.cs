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
            bool added = false;
            for (int i = 0; i < owner.Data.BuffList.Count; i++)
            {
                if (actionArg == owner.Data.BuffList[i].BuffId)
                {
                    added = true;
                    //Ë¢ÐÂbuffÊ±¼ä£¬²»µþ¼Ó
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
                BattleBuffData buffData = new BattleBuffData(actionArg, 0, cardData, owner, owner);
                owner.Data.BuffList.Add(buffData);
                battleMgr.AddUIAction(new UIAction.UIAddBuff(buffData));
            }
        }
    }
}
