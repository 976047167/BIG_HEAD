using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 奖励数据
/// </summary>
public class RewardData
{
    protected int food = 0;
    protected int[] items = null;
    protected int gold;
    protected int diamond;
    protected int oldLevel;
    protected int oldExp;
    protected int addedExp;
    /// <summary>
    /// 奖励数据
    /// </summary>
    /// <param name="gold">金币</param>
    /// <param name="diamond">钻石</param>
    /// <param name="exp">经验</param>
    /// <param name="food">食物</param>
    /// <param name="rewards">奖励物品</param>
    public RewardData(int gold, int diamond,int oldLevel,int oldexp, int addedExp, int food, params int[] items)
    {
        this.gold = gold;
        this.diamond = diamond;
        this.addedExp = addedExp;
        this.oldLevel = oldLevel;
        this.oldExp = oldexp;
        this.food = food;
        this.items = items;
    }

    public int Gold
    {
        get
        {
            return gold;
        }
    }

    public int Diamond
    {
        get
        {
            return diamond;
        }
    }

    public int AddedExp
    {
        get
        {
            return addedExp;
        }

    }
    public int OldExp
    {
        get
        {
            return oldExp;
        }

    }
    public int OldLevel { get { return oldLevel; } }
    public int Food
    {
        get
        {
            return food;
        }
    }

    public int[] Items
    {
        get
        {
            return items;
        }
    }

}
