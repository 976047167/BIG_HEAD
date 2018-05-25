using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏里面的每一种效果
/// </summary>
public abstract class BattleActionBase
{
    public BattleActionType ActionType { get { return BattleActionType.None; } }
    public int ActionArg;
    public int ActionArg2;
    public BattleCardData CardData;
    public BattlePlayerData Owner;
    public BattlePlayerData Target;
    /// <summary>
    /// 效果的实现
    /// </summary>
    /// <param name="num"></param>
    public abstract void GameAction(int num);
    
}
public class CardActionAttack : BattleActionBase
{
    public static BattleActionType ActionId()
    {
        return BattleActionType.Attack;
    }

    public override void GameAction(int num)
    {
        throw new System.NotImplementedException();
    }
}