using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
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
    public int Coin;
    public int Diamond;
    public int HeadIcon;
    public int MapSkillID;
    public int BattleSkillID;
    public uint UsingDeck;
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

}
