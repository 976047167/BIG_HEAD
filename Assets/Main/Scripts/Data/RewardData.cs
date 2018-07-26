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
    protected int exp;
    /// <summary>
    /// 奖励数据
    /// </summary>
    /// <param name="gold">金币</param>
    /// <param name="diamond">钻石</param>
    /// <param name="exp">经验</param>
    /// <param name="food">食物</param>
    /// <param name="rewards">奖励物品</param>
    public RewardData(int gold, int diamond, int exp, int food, params int[] items)
    {
        this.gold = gold;
        this.diamond = diamond;
        this.exp = exp;
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

    public int Exp
    {
        get
        {
            return exp;
        }

    }

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
