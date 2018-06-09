using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using AppSettings;

public class NormalCard
{
    public int CardId { get; private set; }
    public BattleCardTableSetting Data;
    public readonly uint Uid;
    public readonly bool IsTempCard = false;

    public NormalCard(int id, uint uid = 0)
    {
        BattleCardTableSetting cardData = BattleCardTableSettings.Get(id);
        if (cardData == null)
            throw new ArgumentOutOfRangeException("This CardID is wrong!");
        CardId = id;
        Uid = uid;
        IsTempCard = (uid == 0);
        Data = cardData;
    }

}