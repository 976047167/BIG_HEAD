using AppSettings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerData : MapPlayerData
{
    public int SkillId;
    public int AP = 0;
    public int MaxAP = 0;


    //public BattleCardData
    protected new List<BattleEquipData> m_EquipList = new List<BattleEquipData>();
    protected new List<BattleBuffData> m_BuffList = new List<BattleBuffData>();
    protected new List<BattleCardData> m_CardList = new List<BattleCardData>();
    protected List<BattleCardData> m_CurrentCardList = new List<BattleCardData>();
    protected List<BattleCardData> m_HandCardList = new List<BattleCardData>();
    protected List<BattleCardData> m_UsedCardList = new List<BattleCardData>();

    //public BattleCardData
    public new List<BattleEquipData> EquipList { get { return m_EquipList; } }
    /// <summary>
    /// 
    /// </summary>
    public new List<BattleBuffData> BuffList { get { return m_BuffList; } }
    /// <summary>
    /// 当前设置的卡牌库，除了初始化，不许改
    /// </summary>
    public new List<BattleCardData> CardList { get { return m_CardList; } }
    /// <summary>
    /// 当前战斗中的牌库
    /// </summary>
    public List<BattleCardData> CurrentCardList { get { return m_CurrentCardList; } }
    /// <summary>
    /// 当前的手牌
    /// </summary>
    public List<BattleCardData> HandCardList { get { return m_HandCardList; } }
    /// <summary>
    /// 已经使用过的卡牌，坟场
    /// </summary>
    public List<BattleCardData> UsedCardList { get { return m_UsedCardList; } }

    public BattlePlayer Player { get; private set; }

    public BattlePlayerData(MapPlayerData mapPlayerData, BattlePlayer owner) : base(null, null)
    {
        Name = mapPlayerData.Name;
        Level = mapPlayerData.Level;
        HP = mapPlayerData.HP;
        MaxHP = mapPlayerData.MaxHP;
        MP = mapPlayerData.MP;
        MaxMP = mapPlayerData.MaxMP;
        HeadIcon = mapPlayerData.HeadIcon;
        MapSkillID = mapPlayerData.MapSkillID;
        BattleSkillID = mapPlayerData.BattleSkillID;
        UsingDeck = mapPlayerData.UsingDeck;
        UsingCharacter = mapPlayerData.UsingCharacter;
        Food = mapPlayerData.Food;
        MaxFood = mapPlayerData.MaxFood;
        Gold = mapPlayerData.Gold;
        SkillId = mapPlayerData.BattleSkillID;
        ClassData = mapPlayerData.ClassData;
        //玩家的行动值初始
        AP = MaxAP = 0;

        CardList.Clear();

        EquipList.Capacity = (mapPlayerData.EquipList.Count);
        for (int i = 0; i < mapPlayerData.EquipList.Count; i++)
        {
            List<int> actions = mapPlayerData.EquipList[i].Data.ActionTypes;
            for (int j = 0; j < actions.Count; j++)
            {
                if (actions[i] == (int)BattleActionType.AddEquipment)
                {
                    EquipList.Add(new BattleEquipData(mapPlayerData.EquipList[j].Data.ActionParams[0], owner));
                    break;
                }
            }
        }
        CardList.Capacity = mapPlayerData.CardList.Count;
        for (int i = 0; i < mapPlayerData.CardList.Count; i++)
        {
            CardList.Add(new BattleCardData(mapPlayerData.CardList[i].CardId, owner));
        }
        BuffList.Capacity = mapPlayerData.BuffList.Count;
        for (int i = 0; i < mapPlayerData.BuffList.Count; i++)
        {
            BuffList.Add(new BattleBuffData(mapPlayerData.BuffList[i].Data.ActionParams[0], -1, 0, new BattleCardData(mapPlayerData.BuffList[i].CardId, owner), owner, owner));
        }

        CurrentCardList.Capacity = CardList.Count;
        for (int i = 0; i < CardList.Count; i++)
        {
            CurrentCardList.Add(new BattleCardData(CardList[i].CardId, owner));
        }
    }
    public BattlePlayerData(int monsterId, BattlePlayer owner) : base(null, null)
    {
        BattleMonsterTableSetting monster = BattleMonsterTableSettings.Get(monsterId);
        if (monster == null)
        {
            Debug.LogError("怪物表格配置错误");
            return;
        }
        m_CurrentCardList = new List<BattleCardData>(CardList);
        AP = 0;
        MaxAP = 0;
        HP = monster.HP;
        MaxHP = monster.MaxHp;
        MP = monster.MP;
        MaxMP = monster.MaxMP;
        AP = monster.AP;
        MaxAP = monster.MaxAP;
        Level = monster.Level;
        HeadIcon = monster.IconId;
        for (int i = 0; i < monster.BattleCards.Count; i++)
        {
            m_CardList.Add(new BattleCardData(monster.BattleCards[i], owner));
        }
    }


}
