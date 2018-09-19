using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCardDoor : MapCardBase
{
    bool isEntrance = false;

    protected override void OnPlayerEnter()
    {
        if (isEntrance)
        {
            return;
        }
        UIModule.Instance.OpenForm<WND_NpcDialog>(32);
        base.OnPlayerEnter();
        //MapLogic.Instance
        //下一关
    }
    protected override void RefreshState()
    {
        if (Position == MapMgr.Instance.MyMapPlayer.CurPos)
        {
            state = CardState.Front;
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            isEntrance = true;
        }
        else
            base.RefreshState();
    }
}
