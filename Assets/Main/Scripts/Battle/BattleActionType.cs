using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleActionType : int
{
    None = 0,
    /// <summary>
    /// 添加一个buff
    /// </summary>
    AddBuff = 1,
    //////////////////////////////////////各种攻击，包括属性攻击100-199
    /// <summary>
    /// 普通攻击
    /// </summary>
    Attack = 100,



    /////////////////////////////////////各种回血，200-299
    /// <summary>
    /// 回血
    /// </summary>
    RecoverHP = 2,
    /////////////////////////////////////各种回蓝，300-399
    /// <summary>
    /// 回蓝
    /// </summary>
    RecoverMP = 3,
    /// <summary>
    /// 抽卡
    /// </summary>
    DrawCard = 4,
    /// <summary>
    /// 回血buff
    /// </summary>
    BuffRecoverHP,
    /// <summary>
    /// 持续伤害buff
    /// </summary>
    BuffDamage,



}
