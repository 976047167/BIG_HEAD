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
    public BattlePlayerData TargetPlayerData { get; private set; }
    public BattlePlayerData Owner { get; private set; }

    public BattleBuffData(int buffId, int extTime, BattleCardData cardData, BattlePlayerData owner, BattlePlayerData target)
    {
        BuffId = buffId;

        Data = BattleBuffTableSettings.Get(buffId);
        Time = extTime + Data.Time;
        if (Data == null)
        {
            Debug.LogError("BuffId: " + buffId + " not exist!");
            return;
        }
        CardData = cardData;
        Owner = owner;
        TargetPlayerData = target;
    }

}
