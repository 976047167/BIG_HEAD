using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

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

    static Dictionary<BattleActionType, Type> dicActiuonType = new Dictionary<BattleActionType, Type>();
    public static BattleActionBase Create(BattleActionType actionType, int actionArg,int actionArg2,BattleCardData cardData,BattlePlayer owner,BattlePlayer target)
    {
        BattleActionBase battleAction = Activator.CreateInstance(dicActiuonType[actionType]) as BattleActionBase;
        battleAction.ActionArg = actionArg;
        battleAction.ActionArg2 = actionArg2;
        battleAction.CardData = cardData;
        battleAction.Owner = owner;
        battleAction.Target = target;
        return battleAction;
    }
    /// <summary>
    /// 效果的实现
    /// </summary>
    /// <param name="num"></param>
    public abstract void Excute();
    
}
public class CardActionAttack : BattleActionBase
{
    public CardActionAttack()
    {
    }

    public BattleActionType ActionType { get { return BattleActionType.None; } }


    public override void Excute()
    {
        throw new System.NotImplementedException();
    }
}