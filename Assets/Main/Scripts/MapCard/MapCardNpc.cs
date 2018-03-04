using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCardNpc : MapCardBase
{
    int id;
    public override void Init()
    {
        base.Init();
        id = Random.Range(0, 10);
    }

    public override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        //进入对话

    }
}
