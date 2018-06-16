using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 触发buff的时机  1游戏开始,2回合开始,3回合结束,4受到伤害,5发起伤害,6使用卡牌,7抽卡
/// </summary>
public enum BattleActionTime : int
{
    None=0,
    BattleStart,
    RoundStart,
    RoundEnd,
    Injured,
    Attack,
    UseCard,
    DrawCard,
}
