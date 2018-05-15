using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
/// <summary>
/// 当前客户端玩家数据存放
/// </summary>
public class PlayerDetailData : MonoBehaviour
{

    public List<BattleCardData> EquipList = new List<BattleCardData>();
    /// <summary>
    /// 
    /// </summary>
    public List<BattleBuffData> BuffList = new List<BattleBuffData>();
    /// <summary>
    /// 当前设置的卡牌库，除了初始化，不许改
    /// </summary>
    public List<BattleCardData> CardList = new List<BattleCardData>();

    public Dictionary<uint,Deck> Decks = new Dictionary<uint, Deck>();
    public KaKu Kaku = new KaKu();

    public Dictionary<ClassType, ClassData> DicAllClassData = new Dictionary<ClassType, ClassData>();
}

public enum ClassType
{
    None = 0,
    Warriop,
    Shamam,
    Mage,
    Drvid,
    Paladin,
    Warlock,
    Rogue,
    Prisst,
    Hunter,
}