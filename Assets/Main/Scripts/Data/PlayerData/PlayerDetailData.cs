using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
/// <summary>
/// 当前客户端玩家数据存放
/// </summary>
public class PlayerDetailData
{

    

    public Deck Deck = new Deck();
    public KaKu Kaku = new KaKu();
    
    public Dictionary<ClassType, ClassData> DicAllClassData = new Dictionary<ClassType, ClassData>();
}

public enum ClassType
{
    None = 0,
    /// <summary>
    /// 战士
    /// </summary>
    Warriop,
    /// <summary>
    /// 萨满
    /// </summary>
    Shamam,
    /// <summary>
    /// 法师
    /// </summary>
    Mage,
    /// <summary>
    /// 德鲁伊
    /// </summary>
    Drvid,
    /// <summary>
    /// 武僧
    /// </summary>
    Paladin,
    /// <summary>
    /// 术士
    /// </summary>
    Warlock,
    /// <summary>
    /// 盗贼
    /// </summary>
    Rogue,
    /// <summary>
    /// 牧师
    /// </summary>
    Prisst,
    /// <summary>
    /// 猎人
    /// </summary>
    Hunter,
}