using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCardDoor : MapCardBase
{

    protected override void OnPlayerEnter()
    {
        UIModule.Instance.OpenForm<WND_Dialog>(32);
        base.OnPlayerEnter();
        //MapLogic.Instance
        //下一关
    }
}
