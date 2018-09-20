using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class MapCardBox : MapCardBase
{
    int boxId;

    protected override void OnPlayerEnter()
    {
        if (isFirstEnter)
        {
            UIUtility.ShowMapDialog(TableData.Id);
        }

        base.OnPlayerEnter();
        //进入商店
        Used = true;
    }
    protected override void OnInit()
    {
        int count = BoxTableSettings.GetInstance().Count;
        boxId = Random.Range(1, count + 1);
    }
}
