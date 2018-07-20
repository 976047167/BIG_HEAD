using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

/// <summary>
/// 一个物品对象
/// </summary>
public class ItemData
{
    public int ItemId { get; private set; }

    public ItemData(int itemId)
    {
        this.ItemId = itemId;
    }
}
