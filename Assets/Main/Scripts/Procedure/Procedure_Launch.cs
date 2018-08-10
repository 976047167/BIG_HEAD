using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedure_Launch : ProcedureBase
{
    int progress = 0;
    public override void OnEnter(ProcedureBase last)
    {
        base.OnEnter(last);
        Game.UI.OpenForm<WND_Launch>();
    }

    public override void OnExit(ProcedureBase next)
    {
        base.OnExit(next);
        Game.UI.CloseForm<WND_Launch>();
    }

    public override IEnumerator OnInit(object userdata = null)
    {
        yield return base.OnInit(userdata);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }
}
