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
            int DialogId = BoxTableSettings.Get(boxId).DialogId;
            UIModule.Instance.OpenForm<WND_Dialog>(DialogId);
        }

        base.OnPlayerEnter();
        //进入商店
    }
    protected override void OnInit()
    {
        int count = BoxTableSettings.GetInstance().Count;
        boxId = Random.Range(1, count + 1);
    }
}
