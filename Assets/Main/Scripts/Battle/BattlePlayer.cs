using AppSettings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer
{
    public bool UseAI { private set; get; }
    public BattlePlayerData Data { get; private set; }
    private BattlePlayerAI playerAI;
    public bool IsMe { get; private set; }
    /// <summary>
    /// 如果不是玩家，这个是空的
    /// </summary>
    public MapPlayer Player { get; private set; }

    public BattlePlayer(MapPlayer mapPlayer)
    {
        this.Player = mapPlayer;
        Data = new BattlePlayerData();
        Data.Name = mapPlayer.Data.Name;
        Data.HP = mapPlayer.Data.HP;
        Data.MaxHP = mapPlayer.Data.MaxHP;
        Data.MP = mapPlayer.Data.MP;
        Data.MaxMP = mapPlayer.Data.MaxMP;
        //玩家的行动值初始1
        Data.AP = Data.MaxAP = 0;
        Data.Level = mapPlayer.Data.Level;
        Data.SkillId = mapPlayer.Data.BattleSkillID;
        Data.HeadIcon = mapPlayer.Data.HeadIcon;
        Data.CardList.Clear();

        Data.EquipList = new List<BattleEquipData>(mapPlayer.Data.EquipList.Count);
        for (int i = 0; i < mapPlayer.Data.EquipList.Count; i++)
        {
            List<int> actions = mapPlayer.Data.EquipList[i].Data.ActionTypes;
            for (int j = 0; j < actions.Count; j++)
            {
                if (actions[i] == (int)BattleActionType.AddEquipment)
                {
                    Data.EquipList.Add(new BattleEquipData(mapPlayer.Data.EquipList[j].Data.ActionParams[0], this));
                    break;
                }
            }
        }
        Data.CardList = new List<BattleCardData>(mapPlayer.Data.CardList.Count);
        for (int i = 0; i < mapPlayer.Data.CardList.Count; i++)
        {
            Data.CardList.Add(new BattleCardData(mapPlayer.Data.CardList[i].CardId, this));
        }
        Data.BuffList = new List<BattleBuffData>(mapPlayer.Data.BuffList.Count);
        for (int i = 0; i < mapPlayer.Data.BuffList.Count; i++)
        {
            Data.BuffList.Add(new BattleBuffData(mapPlayer.Data.BuffList[i].Data.ActionParams[0], -1, 0, new BattleCardData(mapPlayer.Data.BuffList[i].CardId, this), this, this));
        }



        Data.CurrentCardList = new List<BattleCardData>(Data.CardList);
        IsMe = mapPlayer.Player == Game.DataManager.MyPlayer;

    }
    public BattlePlayer(int monsterId)
    {
        BattleMonsterTableSetting monster = BattleMonsterTableSettings.Get(monsterId);
        if (monster == null)
        {
            Debug.LogError("怪物表格配置错误");
            return;
        }
        Data = new BattlePlayerData();
        Data.CurrentCardList = new List<BattleCardData>(Data.CardList);
        Data.AP = 0;
        Data.MaxAP = 0;
        IsMe = false;
        Data.HP = monster.HP;
        Data.MaxHP = monster.MaxHp;
        Data.MP = monster.MP;
        Data.MaxMP = monster.MaxMP;
        Data.AP = monster.AP;
        Data.MaxAP = monster.MaxAP;
        Data.Level = monster.Level;
        Data.HeadIcon = monster.IconId;
        for (int i = 0; i < monster.BattleCards.Count; i++)
        {
            Data.CardList.Add(new BattleCardData(monster.BattleCards[i], this));
        }

    }
    /// <summary>
    /// 应用卡牌的效果
    /// </summary>
    /// <param name="cardData"></param>
    public void ApplyCardEffect(BattleCardData cardData)
    {
        cardData.Owner.Data.UsedCardList.Add(cardData);
        cardData.Owner.Data.MP -= cardData.Data.Spending;
        for (int i = 0; i < cardData.Data.ActionTypes.Count; i++)
        {
            ApplyAction(cardData.Data.ActionTypes[i], cardData.Data.ActionParams[i], cardData.Data.ActionParams2[i], cardData, cardData.Owner, null, cardData);
        }
    }
    /// <summary>
    /// 应用触发效果
    /// </summary>
    public void ApplyTimeEffects(BattleActionTime actionTime)
    {
        ApplyBuffs(actionTime);
        ApplyEquips(actionTime);
    }
    /// <summary>
    /// 触发buff的时机
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="actionTime">当前使用特效的时机</param>
    public void ApplyBuffs(BattleActionTime actionTime)
    {
        List<BattleBuffData> removeList = new List<BattleBuffData>();
        bool remove = false;
        foreach (var buff in this.Data.BuffList)
        {
            for (int i = 0; i < buff.Data.ActionTimes.Count; i++)
            {
                if (buff.Data.ActionTimes[i] == (int)actionTime)
                {
                    ApplyAction(buff.Data.ActionTypes[i], buff.Data.ActionParams[i], buff.Data.ActionParams2[i], buff, this, this, buff);
                    if (buff.Layer > 0)
                    {
                        buff.Layer--;
                    }
                    if (buff.Layer == 0)
                    {
                        remove = true;
                    }
                }
            }
            if (actionTime == BattleActionTime.RoundEnd)
            {
                if (buff.Time > 0)
                {
                    buff.Time--;
                }
                if (buff.Time == 0)
                {
                    remove = true;
                }
            }
            if (remove)
            {
                removeList.Add(buff);
                remove = false;
            }
        }

        foreach (var item in removeList)
        {
            this.Data.BuffList.Remove(item);
        }
    }
    public void ApplyEquips(BattleActionTime actionTime)
    {
        List<BattleEquipData> removeList = new List<BattleEquipData>();
        foreach (var equip in this.Data.EquipList)
        {
            for (int i = 0; i < equip.Data.ActionTimes.Count; i++)
            {
                if (equip.Data.ActionTimes[i] == (int)actionTime)
                {
                    ApplyAction(equip.Data.ActionTypes[i], equip.Data.ActionPrarms[i], equip.Data.ActionParams2[i], equip, this, null, equip);
                    //equip.Time--;
                    //if (equip.Time == 0)
                    //{
                    //    removeList.Add(equip);
                    //}
                    //if (equip.Time < 0)
                    //{
                    //    equip.Time = -1;
                    //}
                }
            }
        }
        foreach (var item in removeList)
        {
            this.Data.EquipList.Remove(item);
        }
    }
    void ApplyAction(int actionType, int actionArg, int actionArg2, BattleEffectItemData cardData, BattlePlayer owner, BattlePlayer target, object userdata)
    {
        if (target == null)
        {
            target = IsMe ? Game.BattleManager.OppPlayer : Game.BattleManager.MyPlayer;
        }
        BattleAction battleAction = BattleAction.CreateNew((BattleActionType)actionType, actionArg, actionArg2, cardData, owner, target, userdata);
        battleAction.Excute();
    }
    /// <summary>
    /// 游戏逻辑里面的效果实现
    /// </summary>
    public void ApplyAction(BattleActionType actionType, int actionArg, int actionArg2 = 0)
    {
        ApplyAction((int)actionType, actionArg, actionArg2, null, this, this, null);
    }
    public void EndRound()
    {
        Game.BattleManager.RoundEnd();
    }

    public void StartAI()
    {
        if (playerAI == null)
        {
            playerAI = new BattlePlayerAI(this);
        }
        UseAI = true;
        playerAI.StartAI();
    }

    public void UpdateAI()
    {
        if (UseAI == false)
        {
            return;
        }
        playerAI.UpdateAI();
    }
    public void StopAI()
    {
        if (UseAI)
        {
            UseAI = false;
            playerAI.StopAI();
        }
    }

}
