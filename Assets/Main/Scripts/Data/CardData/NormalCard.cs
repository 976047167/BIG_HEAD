using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using AppSettings;

public class NormalCard
{
    public int CardId { get; private set; }
    public BattleCardTableSetting Data;
    public readonly bool IsTempCard = false;

    public NormalCard(int id, bool temp = true)
    {
        BattleCardTableSetting cardData = BattleCardTableSettings.Get(id);
        if (cardData == null)
            throw new ArgumentOutOfRangeException("This CardID is wrong!");
        CardId = id;
        IsTempCard = temp;
        Data = cardData;
    }

}