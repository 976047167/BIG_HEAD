using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Loading状态，切换场景流程
/// </summary>
public class Procedure_Loading : ProcedureBase
{
    public override void OnEnter(ProcedureBase last)
    {
        base.OnEnter(last);
        Game.UI.OpenForm<WND_Loading>();
    }

    public override void OnExit(ProcedureBase next)
    {
        base.OnExit(next);
    }

    public override void OnInit(object userdata = null)
    {
        base.OnInit(userdata);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    
}
