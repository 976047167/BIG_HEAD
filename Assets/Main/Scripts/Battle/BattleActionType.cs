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
    /// 抽卡，参数为抽卡数量
    /// </summary>
    DrawCard = 5,
    /// <summary>
    /// 使用装备，参数为装备ID
    /// </summary>
    AddEuipment = 6,
    /// <summary>
    /// 移除随机buff，参数为数量
    /// </summary>
    RemoveRandomBuff = 7,
    /// <summary>
    /// 无视防御造成伤害，参数为伤害量
    /// </summary>
    AttackIgnoreDefense = 8,
    /// <summary>
    /// 抵挡伤害，参数为抵挡次数
    /// </summary>
    AttackDefense = 9,
    /// <summary>
    /// 本回合中使用同一套牌的伤害叠加，参数为一张套牌的伤害递增数
    /// </summary>
    AttackGroupCard = 10,

}
