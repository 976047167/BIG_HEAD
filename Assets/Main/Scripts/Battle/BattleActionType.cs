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
    /// <summary>
    /// 普通攻击
    /// </summary>
    Attack = 2,
    /// <summary>
    /// 回血
    /// </summary>
    RecoverHP = 3,
    /// <summary>
    /// 回蓝
    /// </summary>
    RecoverMP = 4,
    /// <summary>
    /// 抽卡
    /// </summary>
    DrawCard = 5,



}
