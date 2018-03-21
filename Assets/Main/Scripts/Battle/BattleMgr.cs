using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMgr
{

    UIBattleForm battleForm;
    public BattleState State { get; private set; }
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
        State = BattleState.Loading;



    }
    /// <summary>
    /// UI加载完毕，准备开始游戏
    /// </summary>
    public void ReadyStart(UIBattleForm battleForm)
    {
        //预装的buff和武器、技能等上膛
        this.battleForm = battleForm;
        State = BattleState.Ready;
        for (int i = 0; i < 3; i++)
        {
            BattleCardData card = Game.DataManager.MyPlayerData.CurrentCardList[Game.DataManager.MyPlayerData.CurrentCardList.Count - 1];
            Game.DataManager.MyPlayerData.CurrentCardList.RemoveAt(Game.DataManager.MyPlayerData.CurrentCardList.Count - 1);
            battleForm.AddHandCard(card);

        }

    }
    /// <summary>
    /// 一回合开始
    /// </summary>
    public void RoundStart()
    {

    }
    public enum BattleState
    {
        Loading = 0,
        Ready = 1,
        MyRoundStart = 2,
        MyRound = 3,
        MyUsingCard = 4,
        MyRoundEnd = 5,
        OppRoundStart = 6,
        OppRound = 7,
        OppUsingCard = 8,
        OppRoundEnd = 9,
        BattleEnd = 10,
    }
}
