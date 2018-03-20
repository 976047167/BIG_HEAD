using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMgr
{

    UIBattleForm battleForm;

    /// <summary>
    /// 开始战斗
    /// </summary>
    public void StartBattle(int monsterId)
    {
        Debug.LogError("StartBattle => " + monsterId);
        Game.DataManager.SetOppData(monsterId);

        Game.DataManager.MyPlayerData.CurrentCardList = new List<BattleCardData>(Game.DataManager.MyPlayerData.CardList);

        Game.DataManager.OppPlayerData.CurrentCardList = new List<BattleCardData>(Game.DataManager.OppPlayerData.CardList);
        Game.UI.OpenForm<UIBattleForm>();
        



    }
    /// <summary>
    /// UI加载完毕，准备开始游戏
    /// </summary>
    public void ReadyStart(UIBattleForm battleForm)
    {
        //预装的buff和武器、技能等上膛
        this.battleForm = battleForm;


    }
    /// <summary>
    /// 一回合开始
    /// </summary>
    public void RoundStart()
    {

    }

}
