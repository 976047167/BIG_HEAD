using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using AppSettings;

public class NormalCard
{
    public int CardId { get; private set; }
    public BattleCardTableSetting CardData;
    public readonly uint Uid;
    public NormalCard(int id ,uint uid = 0)
    {
        BattleCardTableSetting cardData = BattleCardTableSettings.Get(id);
        if (cardData == null)
            throw new ArgumentOutOfRangeException("This CardID is wrong!");
        CardId = id;
        Uid = uid;
        CardData = cardData;
    }

}