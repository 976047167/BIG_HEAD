using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string Name;
    public int Level;
    /// <summary>
    /// 血量
    /// </summary>
    public int HP;
    public int MaxHP;
    public int MP;
    public int MaxMP;
    public int HeadIcon;
    public int MapSkillID;
    public int BattleSkillID;
    public uint UsingDeck;
    public int UsingCharacter;

    private List<NormalCard> m_EquipList = new List<NormalCard>();
    /// <summary>
    /// 
    /// </summary>
    private List<NormalCard> m_BuffList = new List<NormalCard>();
    /// <summary>
    /// 当前设置的卡牌库，除了初始化，不许改
    /// </summary>
    private List<NormalCard> mCardList = new List<NormalCard>();

    public List<NormalCard> EquipList { get { return m_EquipList; } }

    public List<NormalCard> BuffList { get { return m_BuffList; } }

    public List<NormalCard> CardList { get { return mCardList; } }

}
