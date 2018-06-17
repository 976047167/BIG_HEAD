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

            target.Data.HP -= actionArg;
            battleMgr.AddUIAction(new UIAction.UIHpDamage(target, actionArg));
            int finalDamage = actionArg;

            //先计算加伤
            for (int i = 0; i < owner.Data.BuffList.Count; i++)
            {
                BattleBuffData buff = owner.Data.BuffList[i];
                for (int j = 0; j < buff.Data.ActionTypes.Count; j++)
                {
                    switch ((BattleActionType)buff.Data.ActionTypes[j])
                    {
                        case BattleActionType.ExtraPercentDamage:
                            finalDamage = Mathf.RoundToInt((float)actionArg * ((100f + (float)buff.Data.ActionPrarms[j]) / 100f));
                            break;
                        default:
                            break;
                    }
                }

            }

            //计算附加伤害
            for (int i = 0; i < owner.Data.BuffList.Count; i++)
            {
                BattleBuffData buff = owner.Data.BuffList[i];
                for (int j = 0; j < buff.Data.ActionTypes.Count; j++)
                {
                    switch ((BattleActionType)buff.Data.ActionTypes[j])
                    {

                        default:
                            break;
                    }
                }
            }

            //计算减伤
            for (int i = 0; i < target.Data.BuffList.Count; i++)
            {
                BattleBuffData buff = target.Data.BuffList[i];
                for (int j = 0; j < buff.Data.ActionTypes.Count; j++)
                {
                    switch ((BattleActionType)buff.Data.ActionTypes[j])
                    {
                        case BattleActionType.DefenseDamage:
                            finalDamage = finalDamage - buff.Data.ActionPrarms[j];
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
