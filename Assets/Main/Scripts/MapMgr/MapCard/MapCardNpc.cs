using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class MapCardNpc : MapCardBase
{
    NpcTableSetting npcData = null;
    protected override void OnInit()
    {
        npcData = NpcTableSettings.Get(TableData.DataId);
    }

    protected override void OnPlayerEnter()
    {
        if (isFirstEnter)
        {
            //int DialogId = NpcTableSettings.Get(id).DialogId;
            //UIModule.Instance.OpenForm<WND_Dialog>(DialogId);
            // UIModule.Instance.OpenForm<WND_Bag>(0);
            //UIModule.Instance.OpenForm<WND_Kaku>(0);
            UIUtility.ShowMapDialog(TableData.Id);
        }

        base.OnPlayerEnter();
        //进入对话


    }
}
