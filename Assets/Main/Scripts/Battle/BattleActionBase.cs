using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏里面的每一种效果
/// </summary>
public abstract class BattleActionBase
{
    /// <summary>
    /// 当前action的ID
    /// </summary>
    /// <returns></returns>
    public abstract BattleActionType ActionId();
    /// <summary>
    /// 效果的实现
    /// </summary>
    /// <param name="num"></param>
    public abstract void GameAction(int num);
    
}
