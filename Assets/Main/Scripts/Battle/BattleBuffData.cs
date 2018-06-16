using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class BattleBuffData
{
    public int BuffId { get; private set; }
    public int Time { get; set; }
    public BattleBuffTableSetting Data { get; private set; }
    /// <summary>
    /// 触发buff的卡牌信息，没有那就是自带的buff
    /// </summary>
    public BattleCardData CardData { get; private set; }
    public BattlePlayer TargetPlayerData { get; private set; }
    public BattlePlayer Owner { get; private set; }

    public BattleBuffData(int buffId, int extTime, BattleCardData cardData, BattlePlayer owner, BattlePlayer target)
    {
        BuffId = buffId;

        Data = BattleBuffTableSettings.Get(buffId);
        
        if (Data == null)
        {
            Debug.LogError("BuffId: " + buffId + " not exist!");
            return;
        }
        Time = extTime + Data.Time;
        CardData = cardData;
        Owner = owner;
        TargetPlayerData = target;
    }

}
