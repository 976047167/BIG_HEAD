using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
/// <summary>
/// 地图玩家数据类
/// </summary>
public class MapPlayerData : PlayerData
{
    protected PlayerData playerData;

    public MapPlayerData(PlayerData playerData)
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
        UsingDeck = playerData.UsingDeck;
        UsingCharacter = playerData.UsingCharacter;
        Food = playerData.Food;
        MaxFood = playerData.MaxFood;
        Gold = playerData.Gold;
        m_EquipList = new List<NormalCard>(playerData.EquipList);
        m_BuffList = new List<NormalCard>(playerData.BuffList);
        m_CardList = new List<NormalCard>(playerData.CardList);
        ClassData = playerData.ClassData;

        m_MapEquipList = new List<NormalCard>(m_EquipList);
        m_MapBuffList = new List<NormalCard>(m_BuffList);
        m_MapCardList = new List<NormalCard>(m_CardList);

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
