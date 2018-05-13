using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using AppSettings;

public class NormalCard
{
    public int CardId { get; private set; }
    public BattleCardTableSetting CardData;
    public NormalCard(int id)
    {
        BattleCardTableSetting cardData = BattleCardTableSettings.Get(id);
        if (cardData == null)
            throw new ArgumentOutOfRangeException("This CardID is wrong!");
        CardId = id;
        CardData = cardData;
    }

}