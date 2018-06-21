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
            bool skipDamage = false;
            //�ȼ������
            for (int i = 0; i < owner.Data.BuffList.Count; i++)
            {
                BattleBuffData buff = owner.Data.BuffList[i];
                for (int j = 0; j < buff.Data.ActionTypes.Count; j++)
                {
                    switch ((BattleActionType)buff.Data.ActionTypes[j])
                    {
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
            for (int i = 0; i < target.Data.EquipList.Count; i++)
            {
                BattleEquipData equip = target.Data.EquipList[i];
                for (int j = 0; j < equip.Data.ActionTypes.Count; j++)
                {
                    switch ((BattleActionType)equip.Data.ActionTypes[j])
                    {
                        default:
                            break;
                    }
                }
            }
            //û���˺�����������Ч
            if (finalDamage == 0)
            {
                return;
            }
            //�������
            for (int i = 0; i < target.Data.BuffList.Count; i++)
            {
                BattleBuffData buff = target.Data.BuffList[i];
                for (int j = 0; j < buff.Data.ActionTypes.Count; j++)
                {
                    if (skipDamage)
                    {
                        break;
                    }
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
                        case BattleActionType.WithstandOneAttack:
                            buff.Layer--;
                            if (buff.Layer == 0)
                            {
                                target.Data.BuffList.RemoveAt(i);
                            }
                            skipDamage = true;
                            break;
                        case BattleActionType.DodgeDamage:
                            //�������ܶ���
                            buff.Layer--;
                            if (buff.Layer == 0)
                            {
                                target.Data.BuffList.RemoveAt(i);
                            }
                            battleMgr.AddUIAction(new UIAction.UIDodgeDamage());
                            skipDamage = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            for (int i = 0; i < target.Data.EquipList.Count; i++)
            {
                BattleEquipData equip = target.Data.EquipList[i];
                for (int j = 0; j < equip.Data.ActionTypes.Count; j++)
                {
                    if (skipDamage)
                    {
                        break;
                    }
                    switch ((BattleActionType)equip.Data.ActionTypes[j])
                    {
                        case BattleActionType.DefenseDamage:
                            finalDamage = finalDamage - equip.Data.ActionPrarms[j];
                            break;
                        case BattleActionType.WithstandOneAttack:
                            skipDamage = true;
                            break;
                        case BattleActionType.DodgeDamage:
                            //�������ܶ���
                            skipDamage = true;
                            battleMgr.AddUIAction(new UIAction.UIDodgeDamage());
                            break;
                        default:
                            break;
                    }
                }
            }
            //Ӧ���˺�
            if (skipDamage)
            {
                target.Data.HP -= finalDamage;
                battleMgr.AddUIAction(new UIAction.UIHpDamage(target, finalDamage));
                for (int i = 0; i < target.Data.BuffList.Count; i++)
                {
                    BattleBuffData buff = target.Data.BuffList[i];
                    for (int j = 0; j < buff.Data.ActionTypes.Count; j++)
                    {
                        switch ((BattleActionType)buff.Data.ActionTypes[j])
                        {
                            case BattleActionType.ReflectionDamage:
                                if (depth > 1)
                                {
                                    break;
                                }
                                buff.Layer--;
                                if (buff.Layer == 0)
                                {
                                    target.Data.BuffList.RemoveAt(i);
                                }
                                if (actionArg > UnityEngine.Random.Range(0, 100))
                                {
                                    Create(BattleActionType.Attack, finalDamage, 0, buff.CardData, target, owner);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                for (int i = 0; i < target.Data.EquipList.Count; i++)
                {
                    BattleEquipData equip = target.Data.EquipList[i];
                    for (int j = 0; j < equip.Data.ActionTypes.Count; j++)
                    {
                        switch ((BattleActionType)equip.Data.ActionTypes[j])
                        {
                            case BattleActionType.ReflectionDamage:
                                if (depth > 1)
                                {
                                    break;
                                }
                                if (actionArg > UnityEngine.Random.Range(0, 100))
                                {
                                    Create(BattleActionType.Attack, finalDamage, 0, equip.CardData, target, owner);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            //���㸽���˺�
            int extraDamage = 0;
            skipDamage = false;
            for (int i = 0; i < owner.Data.BuffList.Count; i++)
            {
                BattleBuffData buff = owner.Data.BuffList[i];
                for (int j = 0; j < buff.Data.ActionTypes.Count; j++)
                {
                    switch ((BattleActionType)buff.Data.ActionTypes[j])
                    {
                        case BattleActionType.ExtraPercentDamage:
                            extraDamage += Mathf.RoundToInt((float)finalDamage * ((float)buff.Data.ActionPrarms[j] / 100f));
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
            for (int i = 0; i < target.Data.EquipList.Count; i++)
            {
                BattleEquipData equip = target.Data.EquipList[i];
                for (int j = 0; j < equip.Data.ActionTypes.Count; j++)
                {
                    switch ((BattleActionType)equip.Data.ActionTypes[j])
                    {
                        case BattleActionType.ExtraPercentDamage:
                            extraDamage += Mathf.RoundToInt((float)finalDamage * ((float)equip.Data.ActionPrarms[j] / 100f));
                            break;
                        default:
                            break;
                    }
                }
            }
            //û���˺�����������Ч
            if (extraDamage == 0)
            {
                return;
            }
            //�����˺��ļ���
            for (int i = 0; i < target.Data.BuffList.Count; i++)
            {
                BattleBuffData buff = target.Data.BuffList[i];
                for (int j = 0; j < buff.Data.ActionTypes.Count; j++)
                {
                    if (skipDamage)
                    {
                        break;
                    }
                    switch ((BattleActionType)buff.Data.ActionTypes[j])
                    {
                        case BattleActionType.DefenseDamage:
                            extraDamage = extraDamage - buff.Data.ActionPrarms[j];
                            buff.Layer--;
                            if (buff.Layer == 0)
                            {
                                target.Data.BuffList.RemoveAt(i);
                            }
                            break;
                        case BattleActionType.WithstandOneAttack:
                            buff.Layer--;
                            if (buff.Layer == 0)
                            {
                                target.Data.BuffList.RemoveAt(i);
                            }
                            skipDamage = true;
                            break;
                        case BattleActionType.DodgeDamage:
                            //�������ܶ���
                            buff.Layer--;
                            if (buff.Layer == 0)
                            {
                                target.Data.BuffList.RemoveAt(i);
                            }
                            battleMgr.AddUIAction(new UIAction.UIDodgeDamage());
                            skipDamage = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            for (int i = 0; i < target.Data.EquipList.Count; i++)
            {
                BattleEquipData equip = target.Data.EquipList[i];
                for (int j = 0; j < equip.Data.ActionTypes.Count; j++)
                {
                    if (skipDamage)
                    {
                        break;
                    }
                    switch ((BattleActionType)equip.Data.ActionTypes[j])
                    {
                        case BattleActionType.DefenseDamage:
                            extraDamage = extraDamage - equip.Data.ActionPrarms[j];
                            break;
                        case BattleActionType.WithstandOneAttack:
                            skipDamage = true;
                            break;
                        case BattleActionType.DodgeDamage:
                            //�������ܶ���
                            skipDamage = true;
                            battleMgr.AddUIAction(new UIAction.UIDodgeDamage());
                            break;
                        default:
                            break;
                    }
                }
            }
            //Ӧ�ø����˺�
            if (skipDamage)
            {
                target.Data.HP -= extraDamage;
                battleMgr.AddUIAction(new UIAction.UIHpDamage(target, extraDamage));
                for (int i = 0; i < target.Data.BuffList.Count; i++)
                {
                    BattleBuffData buff = target.Data.BuffList[i];
                    for (int j = 0; j < buff.Data.ActionTypes.Count; j++)
                    {
                        switch ((BattleActionType)buff.Data.ActionTypes[j])
                        {
                            case BattleActionType.ReflectionDamage:
                                if (depth > 1)
                                {
                                    break;
                                }
                                buff.Layer--;
                                if (buff.Layer == 0)
                                {
                                    target.Data.BuffList.RemoveAt(i);
                                }
                                if (actionArg > UnityEngine.Random.Range(0, 100))
                                {
                                    Create(BattleActionType.Attack, extraDamage, 0, buff.CardData, target, owner);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                for (int i = 0; i < target.Data.EquipList.Count; i++)
                {
                    BattleEquipData equip = target.Data.EquipList[i];
                    for (int j = 0; j < equip.Data.ActionTypes.Count; j++)
                    {
                        switch ((BattleActionType)equip.Data.ActionTypes[j])
                        {
                            case BattleActionType.ReflectionDamage:
                                if (depth > 1)
                                {
                                    break;
                                }
                                if (actionArg > UnityEngine.Random.Range(0, 100))
                                {
                                    Create(BattleActionType.Attack, extraDamage, 0, equip.CardData, target, owner);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}


