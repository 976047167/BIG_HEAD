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
