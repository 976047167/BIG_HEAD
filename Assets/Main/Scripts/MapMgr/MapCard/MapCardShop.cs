using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class MapCardShop : MapCardBase
{
    int shopId;

    protected override void OnPlayerEnter()
    {

        int DialogId = ShopTableSettings.Get(shopId).DialogId;
        UIUtility.ShowMapDialog(TableData.Id);
        base.OnPlayerEnter();
        //进入商店
    }
    protected override void OnInit()
    {
        int count = ShopTableSettings.GetInstance().Count;
        shopId = Random.Range(1, count + 1);
    }
}
