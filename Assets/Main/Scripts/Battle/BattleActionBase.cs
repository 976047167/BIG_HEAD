using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

/// <summary>
/// 游戏里面的每一种效果
/// </summary>
public abstract partial class BattleAction
{

    protected int ActionArg;
    protected int ActionArg2;
    protected BattleCardData CardData;
    protected BattlePlayer Owner;
    protected BattlePlayer Target;

    
    /// <summary>
    /// 效果的实现
    /// </summary>
    /// <param name="num"></param>
    public abstract void Excute();

    #region Static Method
    static Dictionary<BattleActionType, Type> dicActionType = null;

    static void Init()
    {
        Type baseType = typeof(BattleAction);
        Type[] types = Assembly.GetExecutingAssembly().GetExportedTypes();
        dicActionType = new Dictionary<BattleActionType, Type>(Enum.GetNames(typeof(BattleActionType)).Length);
        List<string> tableNames = new List<string>();
        Type type = null;
        for (int i = 0; i < types.Length; i++)
        {
            type = types[i];
            if (baseType != type && baseType.IsAssignableFrom(type))
            {

                dicActionType.Add((BattleActionType)type.GetProperty("ActionType", BindingFlags.Static | BindingFlags.Public).GetValue(null, null), type);
            }
        }
    }

    public static BattleAction Create(BattleActionType actionType, int actionArg, int actionArg2, BattleCardData cardData, BattlePlayer owner, BattlePlayer target)
    {
        if (dicActionType == null)
        {
            Init();
        }
        BattleAction battleAction = Activator.CreateInstance(dicActionType[actionType]) as BattleAction;
        battleAction.ActionArg = actionArg;
        battleAction.ActionArg2 = actionArg2;
        battleAction.CardData = cardData;
        battleAction.Owner = owner;
        battleAction.Target = target;
        return battleAction;
    }
    #endregion
}
