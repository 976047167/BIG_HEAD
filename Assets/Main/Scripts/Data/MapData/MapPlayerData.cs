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

    public List<BattleCardData> EquipList = new List<BattleCardData>();
    /// <summary>
    /// 
    /// </summary>
    public List<BattleBuffData> BuffList = new List<BattleBuffData>();
    /// <summary>
    /// 当前设置的卡牌库，除了初始化，不许改
    /// </summary>
    public List<BattleCardData> CardList = new List<BattleCardData>();
}
