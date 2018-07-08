using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 触发buff的时机  1游戏开始,2回合开始,3回合结束,4受到伤害,5发起伤害，6发起伤害后,7使用卡牌,8抽卡
/// </summary>
public enum BattleActionTime : int
{
    None = 0,
    /// <summary>
    ///  1游戏开始
    /// </summary>
    BattleStart,
    /// <summary>
    /// 2回合开始
    /// </summary>
    RoundStart,
    /// <summary>
    /// 3回合结束
    /// </summary>
    RoundEnd,
    /// <summary>
    /// 4受到伤害
    /// </summary>
    Injured,
    /// <summary>
    /// 5发起伤害
    /// </summary>
    Attack,
    /// <summary>
    /// 6发起伤害后
    /// </summary>
    AfterAttack,
    /// <summary>
    /// 7使用卡牌
    /// </summary>
    UseCard,
    /// <summary>
    /// 8抽卡
    /// </summary>
    DrawCard,
}
