using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleActionType : int
{
    None = 0,

    /// <summary>
    /// 普通攻击,造成对方P点伤害
    /// </summary>
    Attack = 1,
    /// <summary>
    /// 扣除HP，无视护甲
    /// </summary>
    RemoveHP = 2,
    /// <summary>
    /// 回血
    /// </summary>
    RecoverHP = 3,
    /// <summary>
    /// 回蓝
    /// </summary>
    RecoverMP = 4,
    /// <summary>
    /// 扣除对方P点魔量
    /// </summary>
    RemoveMP = 5,
    /// <summary>
    /// 给自己添加一个ID为P的BUFF，P2为层数
    /// </summary>
    AddBuff = 6,
    /// <summary>
    /// 给对方添加一个ID为P的BUFF，P2为层数
    /// </summary>
    AddOppBuff = 7,
    /// <summary>
    /// 抽P张卡
    /// </summary>
    DrawCard = 8,
    /// <summary>
    /// 对自身造成P点伤害
    /// </summary>
    AttackSelf = 9,
    /// <summary>
    /// 扣除自身P点血量
    /// </summary>
    RemoveSelfHP = 10,
    /// <summary>
    /// 添加ID为P的装备
    /// </summary>
    AddEuipment = 11,
    /// <summary>
    /// 驱散P个随机BUFF，驱散等级为P2
    /// </summary>
    RemoveRandomBuff = 12,
    /// <summary>
    /// 无视护盾造成伤害P
    /// </summary>
    AttackIgnoreDefense = 13,
    /// <summary>
    /// 抵挡1次伤害
    /// </summary>
    OffsetAttack = 14,
    /// <summary>
    /// 减少对方P点护盾
    /// </summary>
    RemoveShield = 15,
    /// <summary>
    /// 获取P张ID为P2的卡牌
    /// </summary>
    GetCard = 16,
    /// <summary>
    /// 对方造成伤害时，P概率回应同等伤害
    /// </summary>
    ReflectionDamage = 17,
    /// <summary>
    /// 本回合中使用同一套牌的伤害叠加，P为一张套牌的伤害递增数
    /// </summary>
    GroupOverlayDamage = 18,
    /// <summary>
    /// 额外造成伤害，P为原伤害的百分比，向下取整
    /// </summary>
    ExtraPercentDamage = 19,
    /// <summary>
    /// 移除所有BUFF，P为0是自己，1是对方，驱散等级为P2
    /// </summary>
    RemoveAllBuff = 20,
    /// <summary>
    /// 获取对方卡组最上面的P张卡牌
    /// </summary>
    GetOppTopCards = 21,
    /// <summary>
    /// 攻击P百分比概率获取对方随机一张手牌
    /// </summary>
    GetOppHandCardByAttack = 22,
    /// <summary>
    /// 获取对方手牌中消耗最大的P张牌
    /// </summary>
    GetOppCardMaxCost = 23,
    /// <summary>
    /// 将对方随机P张手牌移入墓地
    /// </summary>
    ThrowOppHandCard = 24,
    /// <summary>
    /// 造成自身损失血量值的P百分比伤害，向下取整
    /// </summary>
    LostSelfPercentHP = 25,
    /// <summary>
    /// 将自身的所有BUFF转移到对方身上去，驱散等级为P
    /// </summary>
    TansferBuffs = 26,
    /// <summary>
    /// （装备专用）装备使用次数减1，次数为0时，将原装备移入墓地，替换ID为P的装备,P为0时不替换
    /// </summary>
    EquipEndReplace = 27,
    /// <summary>
    /// （BUFF专用）抵挡伤害，每点伤害扣除P层BUFF
    /// </summary>
    DefenseDamage = 28,
    /// <summary>
    /// （BUFF专用）扣除P层BUFF
    /// </summary>
    RemoveBuffLayers = 29,
    /// <summary>
    /// 移除ID为P的BUFF
    /// </summary>
    RemoveBuff = 30,
    /// <summary>
    /// （不可复用）当灵体(一个buff)小于3个时可增加5点法力值；灵体大于等于2个时则增加5点法力值同时造成一次1点伤害   P为buffID
    /// </summary>
    SoulEffect = 31,
    /// <summary>
    /// P为BUFF的ID，根据BUFF层数来获取对应层数的魔量，并移除该BUFF
    /// </summary>
    BuffLayerEffect = 32,
    /// <summary>
    /// 闪避对方的攻击卡
    /// </summary>
    DodgeDamage = 33,
    /// <summary>
    /// 自己打出攻击卡时，每层BUFF增加P点伤害
    /// </summary>
    BuffLayerDamage = 34,


}
