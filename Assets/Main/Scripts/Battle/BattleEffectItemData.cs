using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 在本局对战中，起效果的物体数据类，包括卡牌、buff、装备、技能
/// </summary>
public abstract class BattleEffectItemData
{
    public BattleEffectItemType ItemType { get; protected set; }
    public BattlePlayer Owner { get; protected set; }
}
///// <summary>
///// 本局对战中的每个被动效果的数据类，主动的直接执行了
///// </summary>
//public class BattleEffectData
//{
//    BattleActionType effectType = BattleActionType.None;
//    int priority = 0;
//    SourceType sourceType = SourceType.None;
//    BattleEquipData equipData = null;
//    BattleBuffData buffData = null;
//    BattleCardData cardData = null;


//}
public enum BattleEffectItemType
{
    None = 0,
    Card = 1,
    Buff = 2,
    Equip = 3,
    Skill = 4,
}