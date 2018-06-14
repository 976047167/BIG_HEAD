using AppSettings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class BattleMgr
{
    public const int MAX_HAND_CARD_COUNT = 7;

    Dictionary<string, int> dicCounter = null;

    UIBattleForm battleForm;
    Queue<UIAction> uiActions = new Queue<UIAction>();
    public BattleState State { get; private set; }
    public bool CanUseCard { get; private set; }

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
        Debug.Log("StartBattle => " + monsterId);
        MonsterId = monsterId;
        SetOppData(monsterId);
        MyPlayer = new BattlePlayer(Game.DataManager.MyPlayer);
        dicCounter = new Dictionary<string, int>();

        OppPlayer.StartAI();
        Game.UI.OpenForm<UIBattleForm>();
    }
    public void StopBattle()
    {
        //Game.UI.CloseForm<UIBattleForm>();
        OppPlayer.StopAI();
        MonsterId = 0;
        uiActions.Clear();
        MyPlayer.Data.BuffList.Clear();
        MyPlayer = null;
        OppPlayer = null;
        dicCounter = null;
        State = BattleState.None;
    }
    public void SetOppData(int monsterId)
    {
        BattleMonsterTableSetting monster = BattleMonsterTableSettings.Get(monsterId);
        if (monster == null)
        {
            Debug.LogError("怪物表格配置错误");
            return;
        }
        OppPlayer = new BattlePlayer(monsterId);
        OppPlayer.Data.HP = monster.HP;
        OppPlayer.Data.MaxHP = monster.MaxHp;
        OppPlayer.Data.MP = monster.MP;
        OppPlayer.Data.MaxMP = monster.MaxMP;
        OppPlayer.Data.AP = monster.AP;
        OppPlayer.Data.MaxAP = monster.MaxAP;
        OppPlayer.Data.Level = monster.Level;
        OppPlayer.Data.HeadIcon = monster.IconId;
        for (int i = 0; i < monster.BattleCards.Count; i++)
        {
            OppPlayer.Data.CardList.Add(new BattleCardData(monster.BattleCards[i], OppPlayer));
        }
        //TODO: Buff Equip
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
        if (State == BattleState.BattleEnd_Win || State == BattleState.BattleEnd_Lose || State == BattleState.None)
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
                MyPlayer.Data.AP = MyPlayer.Data.MaxAP = MyPlayer.Data.MaxAP + 1;
                State++;
                break;
            case BattleState.MyDrawCard:
                //ApplyPlayerBuffs(MyPlayer, 1);
                MyPlayer.ApplyBuffs(1);
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
                MyPlayer.ApplyBuffs(2);
                State++;
                break;
            case BattleState.OppRoundStart:
                OppPlayer.Data.AP = OppPlayer.Data.MaxAP = OppPlayer.Data.MaxAP + 1;
                State++;
                break;
            case BattleState.OppDrawCard:
                //ApplyPlayerBuffs(OppPlayer, 1);
                //DrawCard(OppPlayer.Data, 3);
                OppPlayer.ApplyBuffs(1);
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
                OppPlayer.ApplyBuffs(2);
                State = BattleState.MyRoundStart;
                break;
            case BattleState.BattleEnd_Win:
                //battleForm.WinBattle();
                StopBattle();
                break;
            case BattleState.BattleEnd_Lose:
                //battleForm.LoseBattle();
                StopBattle();
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
    public bool UseCard(BattleCardData battleCardData)
    {
        if (battleCardData.Data.Spending <= battleCardData.Owner.Data.AP)
        {
            battleCardData.Owner.Data.HandCardList.Remove(battleCardData);
            UIAction.UIUseCard useCard = new UIAction.UIUseCard(battleCardData);
            useCard.AddBindUIAction(new UIAction.UIApSpend(battleCardData.Owner, battleCardData.Data.Spending));
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

    public void AddUIAction(UIAction uiAction)
    {
        uiActions.Enqueue(uiAction);
    }

    public UIAction GetTopUIAction()
    {
        if (uiActions.Count>0)
        {
            return uiActions.Dequeue();
        }
        return null;
    }
    /// <summary>
    /// 触发buff的时机  1回合开始,2回合结束,3受到伤害,4发起伤害
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="actionTime">当前使用特效的时机</param>
    public void ApplyPlayerBuffs(BattlePlayer playerData, int actionTime)
    {
        List<BattleBuffData> removeList = new List<BattleBuffData>();
        foreach (var buff in playerData.Data.BuffList)
        {
            for (int i = 0; i < buff.Data.ActionTimes.Count; i++)
            {
                if (buff.Data.ActionTimes[i] == actionTime)
                {
                    ApplyAction(buff.Data.ActionTypes[i], buff.Data.ActionPrarms[i], buff.CardData, playerData, playerData);
                    buff.Time--;
                    if (buff.Time == 0)
                    {
                        removeList.Add(buff);
                    }
                    if (buff.Time < 0)
                    {
                        buff.Time = -1;
                    }
                }
            }
        }
        foreach (var item in removeList)
        {
            playerData.Data.BuffList.Remove(item);
        }
    }
    /// <summary>
    /// 应用卡牌的效果
    /// </summary>
    /// <param name="cardData"></param>
    public void ApplyCardEffect(BattleCardData cardData)
    {
        cardData.Owner.Data.UsedCardList.Add(cardData);
        cardData.Owner.Data.AP -= cardData.Data.Spending;
        for (int i = 0; i < cardData.Data.ActionTypes.Count; i++)
        {
            ApplyAction(cardData.Data.ActionTypes[i], cardData.Data.ActionParams[i], cardData, cardData.Owner, null);
        }
    }
    void ApplyAction(int actionType, int actionArg, BattleCardData cardData, BattlePlayer owner, BattlePlayer target)
    {
        if (target == null)
        {
            if (owner == OppPlayer)
            {

            }
            target = owner;
        }
        switch ((BattleActionType)actionType)
        {
            case BattleActionType.None:
                break;
            case BattleActionType.AddBuff:
                bool added = false;
                for (int i = 0; i < target.Data.BuffList.Count; i++)
                {
                    if (actionArg == target.Data.BuffList[i].BuffId)
                    {
                        added = true;
                        //刷新buff时间，不叠加
                        target.Data.BuffList[i].Time = AppSettings.BattleBuffTableSettings.Get(target.Data.BuffList[i].BuffId).Time;
                        break;
                    }
                }
                if (added == false)
                {
                    BattleBuffData buffData = new BattleBuffData(actionArg, 0, cardData, owner, target);
                    target.Data.BuffList.Add(buffData);
                    AddUIAction(new UIAction.UIAddBuff(buffData));
                }
                break;
            case BattleActionType.Attack:
                if (owner == MyPlayer)
                {
                    OppPlayer.Data.HP -= actionArg;
                    AddUIAction(new UIAction.UIHpDamage(OppPlayer, actionArg));
                }
                else if (owner == OppPlayer)
                {
                    MyPlayer.Data.HP -= actionArg;
                    AddUIAction(new UIAction.UIHpDamage(MyPlayer, actionArg));
                }

                break;
            case BattleActionType.RecoverHP:
                target.Data.HP += actionArg;
                target.Data.HP = target.Data.HP > target.Data.MaxHP ? target.Data.MaxHP : target.Data.HP;
                AddUIAction(new UIAction.UIHpRecover(target, actionArg));
                break;
            case BattleActionType.RecoverMP:
                break;
            case BattleActionType.DrawCard:
                DrawCard(owner.Data, actionArg);
                break;
            case BattleActionType.AddEuipment:
                break;
            default:
                break;
        }
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
        /// 战斗结束，胜利
        /// </summary>
        BattleEnd_Win,
        /// <summary>
        /// 战斗结束，失败
        /// </summary>
        BattleEnd_Lose,
    }
}
