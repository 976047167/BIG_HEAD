using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏里面的每一种效果
/// </summary>
public abstract class BattleActionBase
{
    
    private int ActionArg;
    private int ActionArg2;
    private BattleCardData CardData;
    private BattlePlayer Owner;
    private BattlePlayer Target;
    public BattleActionBase(int actionArg,int actionArg2,BattleCardData cardData,BattlePlayer owner,BattlePlayer target)
    {
        this.ActionArg = actionArg;
        this.ActionArg2 = actionArg2;
        this.CardData = cardData;
        this.Owner = owner;
        this.Target = target;
    }
    /// <summary>
    /// 效果的实现
    /// </summary>
    /// <param name="num"></param>
    public abstract void Excute();
    
}
public class CardActionAttack : BattleActionBase
{
    public CardActionAttack(int actionArg, int actionArg2, BattleCardData cardData, BattlePlayer owner, BattlePlayer target) : base(actionArg, actionArg2, cardData, owner, target)
    {
    }

    public BattleActionType ActionType { get { return BattleActionType.None; } }


    public override void Excute()
    {
        throw new System.NotImplementedException();
    }
}