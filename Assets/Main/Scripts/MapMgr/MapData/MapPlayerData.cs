using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
using BigHead.protocol;
/// <summary>
/// 地图玩家数据类
/// </summary>
public class MapPlayerData
{
    public ulong Id = 0;
    public string Name;
    public int Level;
    public int Exp = 0;
    public int MaxExp = 1;
    /// <summary>
    /// 血量
    /// </summary>
    public int HP;
    public int MaxHP;
    public int MP;
    public int MaxMP;
    public int Food;
    public int MaxFood;
    public int Gold;
    public int Diamond;
    public int HeadIcon;
    public int MapSkillID;
    public int BattleSkillID;

    public int UsingCharacter;
    public ClassData ClassData;

    protected List<NormalCard> m_EquipList = new List<NormalCard>();
    /// <summary>
    /// 
    /// </summary>
    protected List<NormalCard> m_BuffList = new List<NormalCard>();
    /// <summary>
    /// 当前设置的卡牌库，除了初始化，不许改
    /// </summary>
    protected List<NormalCard> m_CardList = new List<NormalCard>();
    /// <summary>
    /// 消耗品
    /// </summary>
    protected List<ItemData> m_ItemList = new List<ItemData>();
    protected Deck m_Deck = null;
    public List<NormalCard> EquipList { get { return m_EquipList; } }
    /// <summary>
    /// 身上自带的永久性buff
    /// </summary>
    public List<NormalCard> BuffList { get { return m_BuffList; } }
    /// <summary>
    /// 当前设置的卡牌库，除了初始化，不许改
    /// </summary>
    public List<NormalCard> CardList { get { return m_CardList; } }
    /// <summary>
    /// 消耗品
    /// </summary>
    public List<ItemData> ItemList { get { return m_ItemList; } }

    public Deck Deck { get { return m_Deck; } }

    protected PlayerData playerData;

    public MapPlayerData(PlayerData playerData, PlayerDetailData playerDetailData)
    {
        if (playerData == null)
        {
            return;
        }
        this.playerData = playerData;
        Id = playerData.ID;
        Name = playerData.Name;
        Level = playerData.Level;
        Exp = playerData.Exp;
        LevelTableSetting levelTable = LevelTableSettings.Get(Level);
        if (levelTable != null)
        {
            MaxExp = levelTable.Exp[(int)playerData.ClassData.Type];
        }
        HP = playerData.HP;
        MaxHP = playerData.MaxHP;
        MP = playerData.MP;
        MaxMP = playerData.MaxMP;
        HeadIcon = playerData.HeadIcon;
        MapSkillID = playerData.MapSkillID;
        BattleSkillID = playerData.BattleSkillID;

        UsingCharacter = playerData.UsingCharacter;
        Food = playerData.Food;
        MaxFood = playerData.MaxFood;
        Gold = playerData.Gold;
        if (playerDetailData != null)
        {
            m_EquipList = new List<NormalCard>(playerDetailData.EquipList);
            m_BuffList = new List<NormalCard>(playerDetailData.BuffList);
            m_CardList = new List<NormalCard>(playerDetailData.CardList);
            ClassData = playerData.ClassData;
        }
    }
    public void Update(PBMapPlayerData mapPlayerData)
    {
        if (Id != mapPlayerData.PlayerData.PlayerId)
        {
            Debug.LogError(Id + "不是一个玩家的数据!" + mapPlayerData.PlayerData.PlayerId);
            return;
        }
        PlayerData playerData = new PlayerData();
        playerData.Update(mapPlayerData.PlayerData);

        if (playerData == null)
        {
            return;
        }
        Name = playerData.Name;
        Level = playerData.Level;
        Exp = playerData.Exp;
        LevelTableSetting levelTable = LevelTableSettings.Get(Level);
        if (levelTable != null)
        {
            MaxExp = levelTable.Exp[(int)playerData.ClassData.Type];
        }
        HP = playerData.HP;
        MaxHP = playerData.MaxHP;
        MP = playerData.MP;
        MaxMP = playerData.MaxMP;
        HeadIcon = playerData.HeadIcon;
        MapSkillID = playerData.MapSkillID;
        BattleSkillID = playerData.BattleSkillID;

        UsingCharacter = playerData.UsingCharacter;
        Food = playerData.Food;
        MaxFood = playerData.MaxFood;
        Gold = playerData.Gold;
        for (int i = 0; i < mapPlayerData.Equips.Count; i++)
        {
            m_EquipList.Add(new NormalCard(mapPlayerData.Equips[i], false));
        }
        for (int i = 0; i < mapPlayerData.Buffs.Count; i++)
        {
            m_BuffList.Add(new NormalCard(mapPlayerData.Buffs[i], false));
        }
        m_Deck = new Deck(mapPlayerData.Deck);
        m_CardList = m_Deck.Cards;
        for (int i = 0; i < mapPlayerData.Items.Count; i++)
        {
            m_ItemList.Add(new ItemData(mapPlayerData.Items[i]));
        }
        ClassData = playerData.ClassData;
    }
}


