using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class MapCardNpc : MapCardBase
{
    int id;
    protected override void OnInit()
    {
        
        int NpcCount = NpcTableSettings.GetInstance().Count;
      
        id = Random.Range(1, NpcCount + 1);
    }

    protected override void OnPlayerEnter()
    {
        if (isFirstEnter)
        {
             int DialogId = NpcTableSettings.Get(id).DialogId;
             UIModule.Instance.OpenForm<WND_Dialog>(DialogId);
            // UIModule.Instance.OpenForm<WND_Bag>(0);
            //UIModule.Instance.OpenForm<WND_Kaku>(0);
        }

        base.OnPlayerEnter();
        //进入对话
        

    }
}
