using AppSettings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using BigHead.protocol;

public class BattleMgr
{
    public const int MAX_HAND_CARD_COUNT = 7;
    public const int MAX_EQUIP_COUNT = 1;
    Dictionary<string, int> dicRoundCounter = null;
    Dictionary<string, int> dicBattleCounter = null;

    UIBattleForm battleForm;
    public RewardData RewardData { get; protected set; }
    Queue<UIAction> uiActions = new Queue<UIAction>();
    public BattleState State { get; private set; }
    public bool CanUseCard { get; private set; }
    public int RoundCount { get; private set; }
    public int MonsterId { get; private set; }
    public BattlePlayer MyPlayer { get; private set; }
    public BattlePlayer OppPlayer { get; private set; }
    public UIBattleForm BattleForm
    {
        get
        {
            return battleForm;
        }
    }
    /// <summary>
    /// 开始战斗
    /// </summary>
    public void StartBattle(int monsterId)
    {
        State = BattleState.Loading;
        lastState = BattleState.Loading;
        Debug.Log("StartBattle => " + monsterId);
        MonsterId = monsterId;
        OppPlayer = new BattlePlayer(monsterId);
        MyPlayer = new BattlePlayer(MapMgr.Instance.MyMapPlayer);
        dicRoundCounter = new Dictionary<string, int>();
        dicBattleCounter = new Dictionary<string, int>();
        RoundCount = 0;
        RewardData = null;
        OppPlayer.StartAI();
        uiActions.Clear();
        RegisterMessage();
        MyPlayer.Data.MP = MyPlayer.Data.MaxMP = 100;
        OppPlayer.Data.HP = 1;
        Game.UI.OpenForm<UIBattleForm>();
    }
    /// <summary>
    ///0Win 1Failure 2Me Escape 3Monster Escape
    /// </summary>
    public void SaveData(int reason)
    {
        //退出战斗
        if (MapMgr.Inited)
        {
            CGExitBattle exitBattle = new CGExitBattle();
            exitBattle.MonsterId = MonsterId;
            exitBattle.Reason = reason;
            exitBattle.PlayerData = new PBPlayerData();
            exitBattle.PlayerData.Hp = MyPlayer.Data.HP;
            exitBattle.PlayerData.MaxHp = MyPlayer.Data.MaxHP;
            exitBattle.PlayerData.Mp = MyPlayer.Data.MP;
            exitBattle.PlayerData.MaxMp = MyPlayer.Data.MaxMP;
            for (int i = 0; i < MyPlayer.Data.ItemList.Count; i++)
            {
                exitBattle.Items.Add(MyPlayer.Data.ItemList[i].ItemId);
            }

            Game.NetworkManager.SendToLobby(MessageId_Send.CGExitBattle, exitBattle);
        }
    }
    public void Clear()
    {
        //Game.UI.CloseForm<UIBattleForm>();
        RemoveMessage();
        OppPlayer.StopAI();
        MonsterId = 0;
        uiActions.Clear();
        MyPlayer.Data.BuffList.Clear();
        MyPlayer = null;
        OppPlayer = null;
        dicRoundCounter = null;
        dicBattleCounter = null;
        RewardData = null;
        State = BattleState.None;
        lastState = BattleState.None;
    }

    void RegisterMessage()
    {
        Messenger.AddListener<RewardData>(MessageId.MAP_GET_REWARD, GetMapReward);
        Messenger.AddListener<GCExitBattle>(MessageId_Receive.GCExitBattle, GetExitResponse);
    }

    void RemoveMessage()
    {
        Messenger.RemoveListener<RewardData>(MessageId.MAP_GET_REWARD, GetMapReward);
        Messenger.RemoveListener<GCExitBattle>(MessageId_Receive.GCExitBattle, GetExitResponse);
    }

    private void GetExitResponse(GCExitBattle data)
    {
        if (data.MonsterId == MonsterId)
        {
            switch (data.Reason)
            {
                case 0:
                    uiActions.Enqueue(new UIAction.UIWinBattle());
                    break;
                case 1:
                    uiActions.Enqueue(new UIAction.UILoseBattle());
                    break;
                case 2:
                    uiActions.Enqueue(new UIAction.UIMeEscapeBattle());
                    break;
                case 3:
                    uiActions.Enqueue(new UIAction.UIOppEscapeBattle());
                    break;
                default:
                    break;
            }
        }
    }

    private void GetMapReward(RewardData reward)
    {
        this.RewardData = reward;
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
        if (lastState == BattleState.BattleEnd_Win
            || lastState == BattleState.BattleEnd_Lose
            || lastState == BattleState.None
            || lastState == BattleState.BattleEnd_MyEscape
            || lastState == BattleState.BattleEnd_OppEscape)
        {
            return;
        }
        if (OppPlayer.Data.HP <= 0)
        {
            State = BattleState.BattleEnd_Win;
        }
        if (MyPlayer.Data.HP <= 0)
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
                //MyPlayer.Data.AP = MyPlayer.Data.MaxAP = MyPlayer.Data.MaxAP + 1;
                RoundCount++;
                State++;
                break;
            case BattleState.MyDrawCard:
                //ApplyPlayerBuffs(MyPlayer, 1);
                MyPlayer.ApplyTimeEffects(BattleActionTime.RoundStart);
                //DrawCard(MyPlayer.Data, 3);
                MyPlayer.ApplyAction(BattleActionType.DrawCard, 3);
                State++;
                break;
            case BattleState.MyRound:
                CanUseCard = true;
                break;
            case BattleState.MyUsingCard:
                break;
            case BattleState.MyRoundEnd:
                CanUseCard = false;
                //ApplyPlayerBuffs(MyPlayer, 2);
                MyPlayer.ApplyTimeEffects(BattleActionTime.RoundEnd);
                State++;
                break;
            case BattleState.OppRoundStart:
                //OppPlayer.Data.AP = OppPlayer.Data.MaxAP = OppPlayer.Data.MaxAP + 1;
                State++;
                break;
            case BattleState.OppDrawCard:
                //ApplyPlayerBuffs(OppPlayer, 1);
                //DrawCard(OppPlayer.Data, 3);
                OppPlayer.ApplyTimeEffects(BattleActionTime.RoundStart);
                OppPlayer.ApplyAction(BattleActionType.DrawCard, 3);
                State++;
                break;
            case BattleState.OppRound:
                //开启AI
                OppPlayer.UpdateAI();
                break;
            case BattleState.OppUsingCard:
                State++;
                break;
            case BattleState.OppRoundEnd:
                //ApplyPlayerBuffs(OppPlayer, 2);
                OppPlayer.ApplyTimeEffects(BattleActionTime.RoundEnd);
                State = BattleState.MyRoundStart;
                break;
            case BattleState.BattleEnd_MyEscape:
                uiActions.Enqueue(new UIAction.UIMeEscapeBattle());
                MyPlayer.Data.HP = 1;
                SaveData(2);
                break;
            case BattleState.BattleEnd_OppEscape:
                uiActions.Enqueue(new UIAction.UIOppEscapeBattle());
                SaveData(3);
                break;
            case BattleState.BattleEnd_Win:

                SaveData(0);
                break;
            case BattleState.BattleEnd_Lose:
                //battleForm.LoseBattle();
                uiActions.Enqueue(new UIAction.UILoseBattle());
                SaveData(1);

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
            case BattleState.BattleEnd_MyEscape:
                break;
            case BattleState.BattleEnd_OppEscape:
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
                AddUIAction(new UIAction.UIRoundStart(MyPlayer));
                break;
            case BattleState.MyDrawCard:
                break;
            case BattleState.MyRound:
                break;
            case BattleState.MyUsingCard:
                break;
            case BattleState.MyRoundEnd:
                //battleForm.ClearUsedCards();
                AddUIAction(new UIAction.UIRoundEnd(MyPlayer));
                break;
            case BattleState.OppRoundStart:
                AddUIAction(new UIAction.UIRoundStart(OppPlayer));
                break;
            case BattleState.OppDrawCard:
                break;
            case BattleState.OppRound:
                break;
            case BattleState.OppUsingCard:
                break;
            case BattleState.OppRoundEnd:
                AddUIAction(new UIAction.UIRoundEnd(OppPlayer));
                //battleForm.ClearUsedCards();
                break;
            case BattleState.BattleEnd_MyEscape:
                break;
            case BattleState.BattleEnd_OppEscape:
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
            dicRoundCounter.Clear();
        }
        else if (State == BattleState.OppRound)
        {
            State = BattleState.OppRoundEnd;
            dicRoundCounter.Clear();
        }

    }

    public bool UseCard(BattleCardData battleCardData)
    {
        if (battleCardData.Data.Spending <= battleCardData.Owner.Data.MP)
        {
            battleCardData.Owner.Data.HandCardList.Remove(battleCardData);
            UIAction.UIUseCard useCard = new UIAction.UIUseCard(battleCardData);
            useCard.AddBindUIAction(new UIAction.UIMpSpend(battleCardData.Owner, battleCardData.Data.Spending));
            AddUIAction(useCard);

            //ApplyCardEffect(battleCardData);
            battleCardData.Owner.ApplyCardEffect(battleCardData);
            return true;
        }
        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="count"></param>
    public void DrawCard(BattlePlayerData playerData, int count = 1)
    {

        for (int i = 0; i < count; i++)
        {
            if (playerData.HandCardList.Count >= MAX_HAND_CARD_COUNT)
            {
                return;
            }
            if (playerData.CurrentCardList.Count <= 0)
            {
                for (int j = 0; j < playerData.CardList.Count; j++)
                {
                    playerData.CurrentCardList.Add(new BattleCardData(playerData.CardList[j].Data.Id, playerData.CardList[j].Owner));
                }
                //playerData.CurrentCardList = new List<BattleCardData>(playerData.CardList);
            }
            BattleCardData card = playerData.CurrentCardList[UnityEngine.Random.Range(0, playerData.CurrentCardList.Count)];
            playerData.CurrentCardList.Remove(card);
            card.Owner.Data.HandCardList.Add(card);
            AddUIAction(new UIAction.UIDrawCard(card));
        }
    }
    /// <summary>
    /// 逃离战斗
    /// </summary>
    public void EscapeBattle(BattlePlayer player)
    {
        Debug.LogError((player.IsMe ? "[我]" : "[怪]") + "逃跑了");
        if (player.IsMe)
        {
            State = BattleState.BattleEnd_MyEscape;
        }
        else
        {
            State = BattleState.BattleEnd_OppEscape;
        }
    }
    public void AddUIAction(UIAction uiAction)
    {
        uiActions.Enqueue(uiAction);
    }

    public UIAction GetTopUIAction()
    {
        if (uiActions.Count > 0)
        {
            return uiActions.Dequeue();
        }
        return null;
    }

    /// <summary>
    /// -1取消计数，0归零，大于0，增加这么多
    /// </summary>
    /// <param name="countKey"></param>
    /// <param name="count"></param>
    public void SetRoundCounter(string countKey, int count = 1)
    {
        if (!dicRoundCounter.ContainsKey(countKey))
        {
            dicRoundCounter[countKey] = 0;
        }
        if (count == -1)
        {
            dicRoundCounter.Remove(countKey);
        }
        else if (count == 0)
        {
            dicRoundCounter[countKey] = 0;
        }
        else
            dicRoundCounter[countKey] += count;
    }
    public int GetRoundCounter(string countKey)
    {
        if (!dicRoundCounter.ContainsKey(countKey))
        {
            return 0;
        }
        return dicRoundCounter[countKey];
    }
    /// <summary>
    /// -1取消计数，0归零，大于0，增加这么多
    /// </summary>
    /// <param name="countKey"></param>
    /// <param name="count"></param>
    public void SetBattleCounter(string countKey, int count = 1)
    {
        if (!dicBattleCounter.ContainsKey(countKey))
        {
            dicBattleCounter[countKey] = 0;
        }
        if (count == -1)
        {
            dicBattleCounter.Remove(countKey);
        }
        else if (count == 0)
        {
            dicBattleCounter[countKey] = 0;
        }
        else
            dicBattleCounter[countKey] += count;
    }
    public int GetBattleCounter(string countKey)
    {
        if (!dicBattleCounter.ContainsKey(countKey))
        {
            return 0;
        }
        return dicBattleCounter[countKey];
    }



    /// <summary>
    /// 回合开始 -> 抽卡阶段 -> 行动阶段（出牌） -> 行动结束-> 回合结束
    /// </summary>
    public enum BattleState : int
    {
        None = 0,
        /// <summary>
        /// 加载战斗界面
        /// </summary>
        Loading,
        /// <summary>
        /// 加载完成等待数据
        /// </summary>
        Ready,
        /// <summary>
        /// 我方回合开始
        /// </summary>
        MyRoundStart,
        /// <summary>
        /// 我方抽牌
        /// </summary>
        MyDrawCard,
        /// <summary>
        /// 我方回合
        /// </summary>
        MyRound,
        /// <summary>
        /// 我方战斗UI表现卡牌的使用
        /// </summary>
        MyUsingCard,
        /// <summary>
        /// 我方回合结束
        /// </summary>
        MyRoundEnd,
        /// <summary>
        /// 敌方回合开始
        /// </summary>
        OppRoundStart,
        /// <summary>
        /// 敌方抽牌
        /// </summary>
        OppDrawCard,
        /// <summary>
        /// 敌方回合
        /// </summary>
        OppRound,
        /// <summary>
        /// 敌方卡牌使用表现
        /// </summary>
        OppUsingCard,
        /// <summary>
        /// 敌方回合结束
        /// </summary>
        OppRoundEnd,
        /// <summary>
        /// 我方逃离战斗
        /// </summary>
        BattleEnd_MyEscape,
        /// <summary>
        /// 敌方逃离战斗
        /// </summary>
        BattleEnd_OppEscape,
        /// <summary>
        /// 战斗结束，胜利，发奖励
        /// </summary>
        BattleEnd_Win,
        /// <summary>
        /// 战斗结束，失败
        /// </summary>
        BattleEnd_Lose,
    }
}
