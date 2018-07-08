using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class BattleBuffData : BattleEffectItemData
{
    public int BuffId { get; private set; }
    /// <summary>
    /// 持续时间
    /// </summary>
    public int Time { get; set; }
    /// <summary>
    /// 叠加层数,目前都是用Time表示
    /// </summary>
    public int Layer { get; set; }
    public BattleBuffTableSetting Data { get; private set; }
    /// <summary>
    /// 触发buff的卡牌信息，没有那就是自带的buff
    /// </summary>
    public BattleEffectItemData CardData { get; private set; }
    public BattlePlayer Target { get; private set; }

    public BattleBuffData(int buffId, int time, BattleEffectItemData cardData, BattlePlayer owner, BattlePlayer target)
    {
        BuffId = buffId;

        Data = BattleBuffTableSettings.Get(buffId);

        if (Data == null)
        {
            Debug.LogError("BuffId: " + buffId + " not exist!");
            return;
        }
        Time = time == 0 ? Data.Time : time;
        CardData = cardData;
        Owner = owner;
        Target = target;
        ItemType = BattleEffectItemType.Buff;
    }

}
