using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerAI
{
    BattlePlayer player;
    /// <summary>
    /// 初始化AI
    /// </summary>
    /// <param name="player"></param>
    public BattlePlayerAI(BattlePlayer player)
    {
        this.player = player;
    }

    public void StartAI()
    {
        //加载相应的AI策略
    }

    public void UpdateAI()
    {
        //每次决策执行
        int i = 0;
        int count = player.Data.HandCardList.Count;
        while (i < count)
        {
            if (player.Data.HandCardList[i].Data.Spending <= player.Data.AP)
            {
                Debug.LogError("自动使用:" + I18N.Get(player.Data.HandCardList[i].Data.Name));
                Game.BattleManager.UseCard(player.Data.HandCardList[i]);
                count--;
                i--;
            }
            i++;
        }
        player.EndRound();
    }

    public void StopAI()
    {

    }
}
