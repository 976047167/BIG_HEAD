using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class BattleCardData
{
    public int CardId = 0;

    public BattleCardData(int cardId)
    {
        BattleCardTableSetting row = BattleCardTableSettings.Get(cardId);
        if (row == null)
        {
            Debug.LogError("cardId: " + cardId + " not exist!");
            return;
        }
    }
}
