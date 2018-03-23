using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class BattleBuffData
{
    public int BuffId { get; private set; }
    public BattleBuffTableSetting Data { get; private set; }
    /// <summary>
    /// 触发buff的卡牌信息，没有那就是自带的buff
    /// </summary>
    public BattleCardData CardData { get; private set; }

    public BattleBuffData(int buffId, BattleCardData cardData = null)
    {
        BuffId = buffId;
        Data = BattleBuffTableSettings.Get(buffId);
        if (Data == null)
        {
            Debug.LogError("BuffId: " + buffId + " not exist!");
            return;
        }
        CardData = cardData;
    }

}
