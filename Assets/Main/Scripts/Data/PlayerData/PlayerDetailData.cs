using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
/// <summary>
/// 当前客户端玩家数据存放
/// </summary>
public class PlayerDetailData
{

    

    public List<Deck> Decks = new List<Deck>();
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