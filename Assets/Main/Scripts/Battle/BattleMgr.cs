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
        SetMyBattleCardList();
        SetOppBattleCardList();
        Game.UI.OpenForm<UIBattleForm>();
    }
    void SetMyBattleCardList()
    {
        Game.DataManager.MyPlayerData.CurrentCardList = new List<BattleCardData>(Game.DataManager.MyPlayerData.CardList);
        Game.DataManager.MyPlayerData.AP = 0;
        Game.DataManager.MyPlayerData.MaxAP = 0;
    }
    void SetOppBattleCardList()
    {
        Game.DataManager.OppPlayerData.CurrentCardList = new List<BattleCardData>(Game.DataManager.OppPlayerData.CardList);
        Game.DataManager.OppPlayerData.AP = 0;
        Game.DataManager.OppPlayerData.MaxAP = 0;
    }
    /// <summary>
    /// UI加载完毕，准备开始游戏
    /// </summary>
    public void ReadyStart(UIBattleForm battleForm)
    {
        State = BattleState.Ready;
        //预装的buff和武器、技能等上膛
        this.battleForm = battleForm;

        State = BattleState.MyRoundStart;
    }

    BattleState lastState = BattleState.Loading;
    public void UpdateScope()
    {
        if (Game.DataManager.OppPlayerData.HP <= 0)
        {
            State = BattleState.BattleEnd_Win;
        }
        if (Game.DataManager.MyPlayerData.HP <= 0)
        {
            State = BattleState.BattleEnd_Lose;
        }
        if (lastState != State)
        {
            lastState = State;
            OnExitState();
            OnEnterState();
            return;
        }
        OnUpdateState();
    }

    void OnEnterState()
    {
        switch (State)
        {
            case BattleState.Loading:
                break;
            case BattleState.Ready:
                break;
            case BattleState.MyRoundStart:
                Game.DataManager.MyPlayerData.AP = Game.DataManager.MyPlayerData.MaxAP = Game.DataManager.MyPlayerData.MaxAP + 1;
                State++;
                break;
            case BattleState.MyDrawCard:
                DrawCard(Game.DataManager.MyPlayerData, 3);
                State++;
                break;
            case BattleState.MyRound:
                CanUseCard = true;
                break;
            case BattleState.MyUsingCard:
                break;
            case BattleState.MyRoundEnd:
                CanUseCard = false;
                State++;
                break;
            case BattleState.OppRoundStart:
                Game.DataManager.OppPlayerData.AP = Game.DataManager.OppPlayerData.MaxAP = Game.DataManager.OppPlayerData.MaxAP + 1;
                State++;
                break;
            case BattleState.OppDrawCard:
                DrawCard(Game.DataManager.OppPlayerData, 3);
                State++;
                break;
            case BattleState.OppRound:
                //开启AI
                State++;
                break;
            case BattleState.OppUsingCard:
                State++;
                break;
            case BattleState.OppRoundEnd:
                State = BattleState.MyRoundStart;
                break;
            case BattleState.BattleEnd_Win:
                battleForm.WinBattle();
                break;
            case BattleState.BattleEnd_Lose:
                battleForm.LoseBattle();
                break;
            default:
                break;
        }
    }
    void OnUpdateState()
    {
        switch (State)
        {
            case BattleState.Loading:
                break;
            case BattleState.Ready:
                break;
            case BattleState.MyRoundStart:
                break;
            case BattleState.MyDrawCard:
                break;
            case BattleState.MyRound:
                break;
            case BattleState.MyUsingCard:
                break;
            case BattleState.MyRoundEnd:
                break;
            case BattleState.OppRoundStart:
                break;
            case BattleState.OppDrawCard:
                break;
            case BattleState.OppRound:
                break;
            case BattleState.OppUsingCard:
                break;
            case BattleState.OppRoundEnd:
                break;
            case BattleState.BattleEnd_Win:
                break;
            case BattleState.BattleEnd_Lose:
                break;
            default:
                break;
        }
    }
    void OnExitState()
    {
        if (State == BattleState.BattleEnd_Win || State == BattleState.BattleEnd_Lose)
        {
            return;
        }
        switch (lastState)
        {
            case BattleState.Loading:
                break;
            case BattleState.Ready:
                break;
            case BattleState.MyRoundStart:
                break;
            case BattleState.MyDrawCard:
                break;
            case BattleState.MyRound:
                break;
            case BattleState.MyUsingCard:
                break;
            case BattleState.MyRoundEnd:
                break;
            case BattleState.OppRoundStart:
                break;
            case BattleState.OppDrawCard:
                break;
            case BattleState.OppRound:
                break;
            case BattleState.OppUsingCard:
                break;
            case BattleState.OppRoundEnd:
                break;
            case BattleState.BattleEnd_Win:
                break;
            case BattleState.BattleEnd_Lose:
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 玩家点击回合结束的按钮
    /// </summary>
    public void RoundEnd()
    {
        if (State == BattleState.MyRound)
        {
            State = BattleState.MyRoundEnd;
        }
        else if (State == BattleState.OppRound)
        {
            State = BattleState.OppRoundEnd;
        }
    }
    public void DrawCard(BattlePlayerData playerData, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            if (playerData.CurrentCardList.Count <= 0)
            {
                playerData.CurrentCardList = new List<BattleCardData>(playerData.CardList);
            }
            BattleCardData card = playerData.CurrentCardList[playerData.CurrentCardList.Count - 1];
            playerData.CurrentCardList.RemoveAt(playerData.CurrentCardList.Count - 1);
            card.Owner.HandCardList.Add(card);
            battleForm.AddHandCard(card);
        }
    }
    public class CardAction
    {
        public int ActionId;
        public int ActionArg;
        public BattleCardData CardData;
    }

    /// <summary>
    /// 回合开始 -> 抽卡阶段 -> 行动阶段（出牌） -> 行动结束-> 回合结束
    /// </summary>
    public enum BattleState : int
    {
        Loading = 0,
        Ready,
        MyRoundStart,
        MyDrawCard,
        MyRound,
        MyUsingCard,
        MyRoundEnd,
        OppRoundStart,
        OppDrawCard,
        OppRound,
        OppUsingCard,
        OppRoundEnd,
        BattleEnd_Win,
        BattleEnd_Lose,
    }
}
