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

            //�ȼ������
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
            //TODO: װ��Ч��

            //���㸽���˺�
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

            //�������
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
                            //�������ܶ���
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
            //Ӧ���˺�
            target.Data.HP -= finalDamage;
            battleMgr.AddUIAction(new UIAction.UIHpDamage(target, finalDamage));
        }
    }
}
