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

    protected int actionArg;
    protected int actionArg2;
    protected BattleCardData cardData;
    protected BattlePlayer owner;
    protected BattlePlayer target;
    protected BattleMgr battleMgr;
    /// <summary>
    /// 执行深度,预防无限递归
    /// </summary>
    protected int depth;
    const int MAX_EXCUTE_DEPTH = 10;
    /// <summary>
    /// 主动效果的实现
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

    public static BattleAction CreateNew(BattleActionType actionType, int actionArg, int actionArg2, BattleCardData cardData, BattlePlayer owner, BattlePlayer target)
    {
        if (dicActionType == null)
        {
            Init();
        }
        BattleAction battleAction = Activator.CreateInstance(dicActionType[actionType]) as BattleAction;
        battleAction.actionArg = actionArg;
        battleAction.actionArg2 = actionArg2;
        battleAction.cardData = cardData;
        battleAction.owner = owner;
        battleAction.target = target;
        battleAction.battleMgr = Game.BattleManager;
        //StackTrace st = new StackTrace();
        //执行深度
        battleAction.depth = 1;
        return battleAction;
    }
    protected BattleAction Create(BattleActionType actionType, int actionArg, int actionArg2, BattleCardData cardData, BattlePlayer owner, BattlePlayer target)
    {
        if (depth > MAX_EXCUTE_DEPTH)
        {
            return Create(BattleActionType.None, 0, 0, null, null, null);
        }
        BattleAction battleAction = Create(actionType, actionArg, actionArg2, cardData, owner, target);
        //StackTrace st = new StackTrace();
        //执行深度
        battleAction.depth = depth + 1;
        return battleAction;

    }
    #endregion
}
