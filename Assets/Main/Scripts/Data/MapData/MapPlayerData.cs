using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 地图玩家数据类
/// </summary>
public class MapPlayerData
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

    public uint UsingDeck;
    public int UsingCharacter;

    private List<BattleCardData> m_EquipList = new List<BattleCardData>();
    /// <summary>
    /// 
    /// </summary>
    private List<BattleBuffData> m_BuffList = new List<BattleBuffData>();
    /// <summary>
    /// 当前设置的卡牌库，除了初始化，不许改
    /// </summary>
    private List<BattleCardData> mCardList = new List<BattleCardData>();

    public List<BattleCardData> EquipList { get { return m_EquipList; } }

    public List<BattleBuffData> BuffList { get { return m_BuffList; } }

    public List<BattleCardData> CardList { get { return mCardList; } }
}
