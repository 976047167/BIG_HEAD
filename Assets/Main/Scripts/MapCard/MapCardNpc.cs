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
        id = Random.Range(0, 2);
    }

    public override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        //进入对话
        WND_ChosePass.ShowDialog(DialogSettings.Get(id).index);

    }
}
