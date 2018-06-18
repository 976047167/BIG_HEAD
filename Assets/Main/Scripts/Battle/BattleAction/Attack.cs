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
                            finalDamage = Mathf.RoundToInt((float)finalDamage * ((100f + (float)buff.Data.ActionPrarms[j]) / 100f));
                            buff.Layer--;
                            if (buff.Layer == 0)
                            {
                                target.Data.BuffList.RemoveAt(i);
                            }
                            break;
                        case BattleActionType.BuffLayerDamage:
                            finalDamage = finalDamage + buff.Layer * buff.Data.ActionPrarms[j];
                            buff.Layer--;
                            if (buff.Layer == 0)
                            {
                                target.Data.BuffList.RemoveAt(i);
                            }
                            break;
                        default:
                            break;
                    }
                }

            }
            //TODO: 装备效果

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
                            buff.Layer--;
                            if (buff.Layer == 0)
                            {
                                target.Data.BuffList.RemoveAt(i);
                            }
                            break;
                        case BattleActionType.DodgeDamage:
                            //播放闪避动画
                            buff.Layer--;
                            if (buff.Layer == 0)
                            {
                                target.Data.BuffList.RemoveAt(i);
                            }
                            battleMgr.AddUIAction(new UIAction.UIDodgeDamage());
                            return;
                        default:
                            break;
                    }
                }
            }
            //应用伤害
            target.Data.HP -= finalDamage;
            battleMgr.AddUIAction(new UIAction.UIHpDamage(target, finalDamage));
        }
    }
}
