using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
/// <summary>
/// 切换单一场景流程,参数：场景ID
/// </summary>
public class Procedure_ChangeScene : ProcedureBase
{
    int sceneID = 0;
    public override void OnEnter(ProcedureBase last)
    {
        base.OnEnter(last);
        Game.UI.CloaseAllForm();
        Game.UI.OpenForm<WND_Loading>(sceneID);
    }

    public override void OnExit(ProcedureBase next)
    {
        base.OnExit(next);
        Game.UI.CloseForm<WND_Loading>();
    }

    public override IEnumerator OnInit(object userdata = null)
    {
        sceneID = (int)userdata;
        SceneTableSetting setting = SceneTableSettings.Get(sceneID);
        if (setting == null)
        {
            yield break;
        }
        yield return base.OnInit(userdata);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }


}
