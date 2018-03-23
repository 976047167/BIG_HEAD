using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMgr
{

    UIBattleForm battleForm;
    public BattleState State { get; private set; }
    public bool CanUseCard { get; private set; }
    /// <summary>
    /// 开始战斗
    /// </summary>
    public void StartBattle(int monsterId)
    {
        State = BattleState.Loading;
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
        State = BattleState.Ready;
        //预装的buff和武器、技能等上膛
        this.battleForm = battleForm;
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
        if (State == BattleState.MyUsingCard || State == BattleState.OppUsingCard)
        {
            return;
        }
        //demo版本，玩家先手
        if (State == BattleState.Ready)
        {
            State = BattleState.MyRoundStart;
            //触发回合开始的效果
        }
        if (State == BattleState.OppRoundEnd)
        {
            State = BattleState.MyRoundStart;
        }
        else if (State == BattleState.MyRoundEnd)
        {
            State = BattleState.MyRoundStart;
        }


    }

    public void Rounding()
    {
        if (State == BattleState.MyUsingCard || State == BattleState.OppUsingCard)
        {
            return;
        }
        if (State == BattleState.MyRoundStart)
        {
            State = BattleState.MyRound;
            //解锁玩家操作，可以使用卡牌
            CanUseCard = true;
        }
        else if (State == BattleState.OppRoundStart)
        {
            State = BattleState.OppRound;
            //TODO: AI启动
        }

    }
    /// <summary>
    /// 玩家点击回合结束的按钮
    /// </summary>
    public void RoundEnd()
    {
        if (State == BattleState.MyUsingCard || State == BattleState.OppUsingCard)
        {
            return;
        }
        if (State == BattleState.MyRound)
        {
            State = BattleState.MyRoundEnd;
        }
        else if (State == BattleState.OppRound)
        {
            State = BattleState.OppRoundEnd;
        }
    }
    public class CardAction
    {
        public int ActionId;
        public int ActionArg;
        public BattleCardData CardData;
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
