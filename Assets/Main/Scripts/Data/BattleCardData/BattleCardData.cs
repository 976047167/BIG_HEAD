using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class BattleCardData
{
    public int CardId { get; private set; }

    public BattleCardTableSetting Data { get; private set; }
    public BattleCardData(int cardId)
    {
        Data = BattleCardTableSettings.Get(cardId);
        if (Data == null)
        {
            Debug.LogError("cardId: " + cardId + " not exist!");
            return;
        }
    }
}
