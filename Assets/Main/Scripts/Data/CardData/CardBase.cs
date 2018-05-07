using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using AppSettings;

public class CardBase
{
    public int CardId { get; private set; }
    BattleCardTableSetting CardData;
    public CardBase(int id)
    {
        BattleCardTableSetting cardData = BattleCardTableSettings.Get(id);
        if (cardData == null)
            throw new ArgumentOutOfRangeException("This CardID is wrong!");
        CardId = id;
        CardData = cardData;
    }

}