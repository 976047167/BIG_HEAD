using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string Name;
    public int Level;
    public int SkillId;
    /// <summary>
    /// 血量
    /// </summary>
    public int HP;
    public int MaxHP;
    public int MP;
    public int MaxMP;
    public string HeadIcon;

    //public BattleCardData
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
