using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedure_CreateCharacter : ProcedureBase
{
    public override void OnEnter(ProcedureBase last)
    {
        base.OnEnter(last);
        Game.UI.OpenForm<WND_CreateCharacter>();
    }

    public override void OnExit(ProcedureBase next)
    {
        base.OnExit(next);
        Game.UI.CloseForm<WND_CreateCharacter>();
    }

    public override bool OnInit(object userdata = null)
    {
        return base.OnInit(userdata);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
