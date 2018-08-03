using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
/// <summary>
/// 地图玩家数据类
/// </summary>
public class MapPlayerData
{
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

    public int UsingDeck;
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
    protected Dictionary<int, Deck> decks = new Dictionary<int, Deck>();
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

    protected PlayerData playerData;

    public MapPlayerData(PlayerData playerData, PlayerDetailData playerDetailData)
    {
        if (playerData == null)
        {
            return;
        }
        this.playerData = playerData;
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
            UsingDeck = playerDetailData.UsingDeck;
            m_EquipList = new List<NormalCard>(playerDetailData.EquipList);
            m_BuffList = new List<NormalCard>(playerDetailData.BuffList);
            m_CardList = new List<NormalCard>(playerDetailData.CardList);
            ClassData = playerData.ClassData;

            m_MapEquipList = new List<NormalCard>(m_EquipList);
            m_MapBuffList = new List<NormalCard>(m_BuffList);
            m_MapCardList = new List<NormalCard>(m_CardList);
        }
    }
    protected List<NormalCard> m_MapEquipList = new List<NormalCard>();
    /// <summary>
    /// 
    /// </summary>
    protected List<NormalCard> m_MapBuffList = new List<NormalCard>();
    /// <summary>
    /// 当前设置的卡牌库，除了初始化，不许改
    /// </summary>
    private List<NormalCard> m_MapCardList = new List<NormalCard>();

    public List<NormalCard> MapEquipList { get { return m_MapEquipList; } }

    public List<NormalCard> MapBuffList { get { return m_MapBuffList; } }

    public List<NormalCard> MapCardList { get { return m_MapCardList; } }

}
