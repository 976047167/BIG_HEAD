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
    public KaKu ExtraKakuCards = new KaKu();
    public Deck ExtraDeckCards = new Deck();
    public Dictionary<ClassType, ClassData> DicAllClassData = new Dictionary<ClassType, ClassData>();

    protected List<NormalCard> AllCardList = new List<NormalCard>();
}

