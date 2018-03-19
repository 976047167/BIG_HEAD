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
    public string HeadIcon;

    //public BattleCardData
    public List<BattleCardData> EquipList = new List<BattleCardData>();
    public List<BattleBuff> BuffList = new List<BattleBuff>();
    public List<BattleCardData> CardList = new List<BattleCardData>();
    public List<BattleCardData> CurrentCardList = new List<BattleCardData>();
    public List<BattleCardData> UsedCardList = new List<BattleCardData>();


}
