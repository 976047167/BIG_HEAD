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
    protected BattleEffectItemData sourceData;
    protected BattlePlayer owner;
    protected BattlePlayer target;
    protected object userdata;
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
    /// <summary>
    /// 被动导致伤害变动的触发
    /// </summary>
    /// <param name="damage">初始伤害</param>
    /// <returns></returns>
    public abstract int Excute(int damage);

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
    /// <summary>
    /// 创建新效果应用，新的递归深度
    /// </summary>
    /// <param name="actionType">效果类型</param>
    /// <param name="actionArg">效果参数1</param>
    /// <param name="actionArg2">效果参数2</param>
    /// <param name="sourceData">效果来源</param>
    /// <param name="owner">所有者</param>
    /// <param name="target">目标</param>
    /// <param name="userdata">携带的自定义参数</param>
    /// <returns>创建的效果</returns>
    public static BattleAction CreateNew(BattleActionType actionType, int actionArg, int actionArg2, BattleEffectItemData sourceData, BattlePlayer owner, BattlePlayer target, object userdata)
    {
        if (dicActionType == null)
        {
            Init();
        }
        BattleAction battleAction = Activator.CreateInstance(dicActionType[actionType]) as BattleAction;
        battleAction.actionArg = actionArg;
        battleAction.actionArg2 = actionArg2;
        battleAction.sourceData = sourceData;
        battleAction.owner = owner;
        battleAction.target = target;
        battleAction.battleMgr = Game.BattleManager;
        battleAction.userdata = userdata;
        //StackTrace st = new StackTrace();
        //执行深度
        battleAction.depth = 1;
        return battleAction;
    }
    /// <summary>
    /// 效果内部创建效果应用，递归深度+1
    /// </summary>
    /// <param name="actionType">效果类型</param>
    /// <param name="actionArg">效果参数1</param>
    /// <param name="actionArg2">效果参数2</param>
    /// <param name="sourceData">效果来源</param>
    /// <param name="owner">所有者</param>
    /// <param name="target">目标</param>
    /// <param name="userdata">携带的自定义参数</param>
    /// <returns>创建的效果</returns>
    protected BattleAction Create(BattleActionType actionType, int actionArg, int actionArg2, BattleEffectItemData sourceData, BattlePlayer owner, BattlePlayer target, object userdata)
    {
        if (depth > MAX_EXCUTE_DEPTH)
        {
            return Create(BattleActionType.None, 0, 0, null, null, null, null);
        }
        BattleAction battleAction = CreateNew(actionType, actionArg, actionArg2, sourceData, owner, target, userdata);
        //StackTrace st = new StackTrace();
        //执行深度
        battleAction.depth = depth + 1;
        return battleAction;

    }
    #endregion
}
