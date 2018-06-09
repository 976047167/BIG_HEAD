using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerData
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
    public int AP;
    public int MaxAP;
    public int HeadIcon;

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
    /// <summary>
    /// 当前战斗中的牌库
    /// </summary>
    public List<BattleCardData> CurrentCardList = new List<BattleCardData>();
    /// <summary>
    /// 当前的手牌
    /// </summary>
    public List<BattleCardData> HandCardList = new List<BattleCardData>();
    /// <summary>
    /// 已经使用过的卡牌，坟场
    /// </summary>
    public List<BattleCardData> UsedCardList = new List<BattleCardData>();


}
