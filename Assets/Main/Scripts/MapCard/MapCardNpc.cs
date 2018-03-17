using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class MapCardNpc : MapCardBase
{
    int id;
    public override void Init()
    {
        base.Init();
        
        int NpcCount = NpcTableSettings.GetInstance().Count;
      
        id = Random.Range(1, NpcCount);
    }

    public override void OnPlayerEnter()
    {
        if (isFirstEnter)
        {
            int DialogId = NpcTableSettings.Get(id).DialogId;
            UIModule.Instance.OpenForm<WND_ChosePass>(DialogId);
        }
     
        base.OnPlayerEnter();
        //进入对话
   

    }
}
