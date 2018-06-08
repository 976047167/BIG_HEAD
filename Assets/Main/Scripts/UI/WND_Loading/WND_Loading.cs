using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
/// <summary>
/// 加载场景的过场界面，参数：int->场景ID
/// </summary>
public class WND_Loading : UIFormBase
{
    protected int nextSceneID = 0;
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        nextSceneID = (int)userdata;
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        SceneMgr.LoadScene(nextSceneID);
    }


}
