using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerAI
{
    BattlePlayer playerData;
    /// <summary>
    /// 初始化AI
    /// </summary>
    /// <param name="playerData"></param>
    public BattlePlayerAI(BattlePlayer playerData)
    {
        this.playerData = playerData;
    }

    public void StartAI()
    {
        //加载相应的AI策略
    }

    public void UpdateAI()
    {
        //每次决策执行
        int i = 0;
        int count = playerData.Data.HandCardList.Count;
        while (i > 0)
        {
            if (playerData.Data.HandCardList[i].Data.Spending <= playerData.Data.AP)
            {
                Game.BattleManager.UseCard(playerData.Data.HandCardList[i]);
            }
        }
    }

    public void StopAI()
    {

    }
}
