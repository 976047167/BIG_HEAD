using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIActionType
{
    None = 0,
    DrawCard,
    UseCard,
    HpDamage,
    MpSpend,
    HpRecover,
    MPRecover,
    AddBuff,
    RoundStart,
    RoundEnd,
    AddEquip,
    RemoveBuff,
    /// <summary>
    /// 闪避伤害动画
    /// </summary>
    DodgeDamage,
}
