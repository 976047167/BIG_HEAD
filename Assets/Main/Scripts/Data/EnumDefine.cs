using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanguageType
{
    None = 0,
    ChineseSimplified = 1,
    ChineseTraditional,
    English,
}
/// <summary>
/// 物品类型(卡牌0,装备1,技能2,消耗品3)
/// </summary>
public enum ItemType
{
    /// <summary>
    /// 卡牌
    /// </summary>
    Card = 0,
    /// <summary>
    /// 装备
    /// </summary>
    Equip,
    /// <summary>
    /// 技能
    /// </summary>
    Skill,
    /// <summary>
    /// 消耗品
    /// </summary>
    Consumable,

}
/// <summary>
/// 卡牌类型(攻击0,装备1,法术2,消耗品3) 注意和ItemType不是一个东西
/// </summary>
public enum CardType
{
    /// <summary>
    /// 攻击0
    /// </summary>
    Attack = 0,
    /// <summary>
    /// 装备1
    /// </summary>
    Equip,
    /// <summary>
    /// 法术2
    /// </summary>
    Magic,
    /// <summary>
    /// 消耗品3
    /// </summary>
    Consumable,
}
/// <summary>
/// 怪物稀有度 0普通1小头目2舵主3护法4教主
/// </summary>
public enum MonsterRarity
{
    /// <summary>
    /// 普通
    /// </summary>
    Ordinary = 0,
    /// <summary>
    /// 小头目
    /// </summary>
    SmallHead,
    /// <summary>
    /// 舵主
    /// </summary>
    Duozhu,
    /// <summary>
    /// 护法
    /// </summary>
    Custodian,
    /// <summary>
    /// 教主
    /// </summary>
    Leader,
}
public enum MessageBoxType
{
    Yes = 0,
    YesNo = 1,
    YesNoCancel = 2,
}
public enum MessageBoxReturnType
{
    Cancel = 0,
    Yes = 1,
    No = 2,
}
