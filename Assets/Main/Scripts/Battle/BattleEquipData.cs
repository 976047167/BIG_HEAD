using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class BattleEquipData : BattleEffectItemData
{
    public int EquipId { get; private set; }
    public int Time { get; set; }

    public EquipType Type { get; private set; }
    public BattleEquipTableSetting Data { get; private set; }
    /// <summary>
    /// 触发buff的卡牌信息，没有那就是自带的装备
    /// </summary>
    public BattleEffectItemData CardData { get; private set; }

    public BattleEquipData(int equipId, BattleEffectItemData cardData, BattlePlayer owner)
    {
        EquipId = equipId;

        Data = BattleEquipTableSettings.Get(equipId);
        if (Data == null)
        {
            Debug.LogError("BuffId: " + equipId + " not exist!");
            return;
        }
        Type = (EquipType)Data.Type;
        CardData = cardData;
        Owner = owner;
    }
    public BattleEquipData(int equipId, BattlePlayer owner)
    {
        EquipId = equipId;

        Data = BattleEquipTableSettings.Get(equipId);
        if (Data == null)
        {
            Debug.LogError("BuffId: " + equipId + " not exist!");
            return;
        }
        Type = (EquipType)Data.Type;
        Owner = owner;
        ItemType = BattleEffectItemType.Equip;
    }
}
public enum EquipType
{
    None = 0,
    /// <summary>
    /// 武器
    /// </summary>
    Weapon,
    /// <summary>
    /// 头盔
    /// </summary>
    Helmet,
    /// <summary>
    /// 防具
    /// </summary>
    Armor,
}
