using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class BattleCardData : BattleEffectItemData
{
    public int CardId { get; private set; }
    static int uniqueId = 0;

    public BattleCardTableSetting Data { get; private set; }
    public BattleCardData(int cardId, BattlePlayer owner)
    {
        uniqueId++;
        if (uniqueId >= int.MaxValue)
        {
            uniqueId = int.MinValue;
        }
        CardId = uniqueId;
        Data = BattleCardTableSettings.Get(cardId);
        if (Data == null)
        {
            Debug.LogError("cardId: " + cardId + " not exist!");
            return;
        }
        Owner = owner;
        ItemType = BattleEffectItemType.Card;
    }
}
