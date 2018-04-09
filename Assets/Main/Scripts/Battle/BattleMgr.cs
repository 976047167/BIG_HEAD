using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMgr
{

    UIBattleForm battleForm;
    public BattleState State { get; private set; }
    public bool CanUseCard { get; private set; }

    public BattlePlayer MyPlayer { get; private set; }
    public BattlePlayer OppPlayer { get; private set; }
    /// <summary>
    /// 开始战斗
    /// </summary>
    public void StartBattle(int monsterId)
    {
        State = BattleState.Loading;
        Debug.LogError("StartBattle => " + monsterId);
        Game.DataManager.SetOppData(monsterId);

        MyPlayer = new BattlePlayer(Game.DataManager.MyPlayerData);
        OppPlayer = new BattlePlayer(Game.DataManager.OppPlayerData);
        OppPlayer.StartAI();
        Game.UI.OpenForm<UIBattleForm>();
    }
    public void StopBattle()
    {
        Game.UI.CloseForm<UIBattleForm>();
        OppPlayer.StopAI();
        MyPlayer = null;
        OppPlayer = null;
        State = BattleState.None;
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
                ApplyPlayerBuffs(Game.DataManager.MyPlayerData, 1);
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
                ApplyPlayerBuffs(Game.DataManager.MyPlayerData, 2);
                State++;
                break;
            case BattleState.OppRoundStart:
                Game.DataManager.OppPlayerData.AP = Game.DataManager.OppPlayerData.MaxAP = Game.DataManager.OppPlayerData.MaxAP + 1;
                State++;
                break;
            case BattleState.OppDrawCard:
                ApplyPlayerBuffs(Game.DataManager.OppPlayerData, 1);
                DrawCard(Game.DataManager.OppPlayerData, 3);
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
                ApplyPlayerBuffs(Game.DataManager.OppPlayerData, 2);
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
                battleForm.ClearUsedCards();
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
        if (battleCardData.Data.Spending <= battleCardData.Owner.AP)
        {
            ApplyCardEffect(battleCardData);
            battleForm.UseCard(battleCardData);
            return true;
        }
        return false;
    }
    /// <summary>
    /// 触发buff的时机  1回合开始,2回合结束,3受到伤害,4发起伤害
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="count"></param>
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="actionTime">当前使用特效的时机</param>
    public void ApplyPlayerBuffs(BattlePlayerData playerData, int actionTime)
    {
        List<BattleBuffData> removeList = new List<BattleBuffData>();
        foreach (var buff in playerData.BuffList)
        {
            for (int i = 0; i < buff.Data.ActionTimes.Count; i++)
            {
                if (buff.Data.ActionTimes[i] == actionTime)
                {
                    ApplyAction(buff.Data.ActionTypes[i], buff.Data.ActionPrarms[i], buff.CardData, playerData, playerData);
                    buff.Time--;
                    if (buff.Time <= 0)
                    {
                        removeList.Add(buff);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 应用卡牌的效果
    /// </summary>
    /// <param name="cardData"></param>
    public void ApplyCardEffect(BattleCardData cardData)
    {
        cardData.Owner.UsedCardList.Add(cardData);
        cardData.Owner.AP -= cardData.Data.Spending;
        for (int i = 0; i < cardData.Data.ActionTypes.Count; i++)
        {
            ApplyAction(cardData.Data.ActionTypes[i], cardData.Data.ActionParams[i], cardData, cardData.Owner, null);
        }
    }
    public void ApplyAction(int actionType, int actionArg, BattleCardData cardData, BattlePlayerData owner, BattlePlayerData target)
    {
        if (target == null)
        {
            target = owner;
        }
        switch ((BattleActionType)actionType)
        {
            case BattleActionType.None:
                break;
            case BattleActionType.AddBuff:
                bool added = false;
                for (int i = 0; i < target.BuffList.Count; i++)
                {
                    if (actionArg == target.BuffList[i].BuffId)
                    {
                        added = true;
                        //刷新buff时间，不叠加
                        target.BuffList[i].Time = AppSettings.BattleBuffTableSettings.Get(target.BuffList[i].BuffId).Time;
                        break;
                    }
                }
                if (added == false)
                {
                    target.BuffList.Add(new BattleBuffData(actionArg, 0, cardData, owner, target));
                }
                break;
            case BattleActionType.Attack:
                if (owner == Game.DataManager.MyPlayerData)
                {
                    Game.DataManager.OppPlayerData.HP -= actionArg;
                }
                else if (owner == Game.DataManager.OppPlayerData)
                {
                    Game.DataManager.MyPlayerData.HP -= actionArg;
                }
                break;
            case BattleActionType.RecoverHP:
                target.HP += actionArg;
                target.HP = target.HP > target.MaxHP ? target.MaxHP : target.HP;
                break;
            case BattleActionType.RecoverMP:
                break;
            case BattleActionType.DrawCard:
                Game.BattleManager.DrawCard(owner, actionArg);
                break;
            case BattleActionType.AddEuipment:
                break;
            default:
                break;
        }
    }




    public class CardAction
    {
        public int ActionId;
        public int ActionArg;
        public int ActionTime;
        public BattleCardData CardData;
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
