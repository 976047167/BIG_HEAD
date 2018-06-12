using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 地图玩家数据类
/// </summary>
public class MapPlayerData : PlayerData
{
    public MapPlayerData(PlayerData playerData)
    {
        Name = playerData.Name;
        Level = playerData.Level;
        HP = playerData.HP;
        MaxHP = playerData.MaxHP;
        MP = playerData.MP;
        MaxMP = playerData.MaxMP;
        HeadIcon = playerData.HeadIcon;
        MapSkillID = playerData.MapSkillID;
        BattleSkillID = playerData.BattleSkillID;
        UsingDeck = playerData.UsingDeck;
        UsingCharacter = playerData.UsingCharacter;
        m_EquipList = new List<NormalCard>(playerData.EquipList);
        m_BuffList = new List<NormalCard>(playerData.BuffList);
        m_CardList = new List<NormalCard>(playerData.CardList);

        m_MapEquipList = new List<NormalCard>(m_EquipList);
        m_MapBuffList = new List<NormalCard>(m_BuffList);
        m_MapCardList = new List<NormalCard>(m_MapCardList);
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
