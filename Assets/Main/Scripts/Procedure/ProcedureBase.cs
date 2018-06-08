using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProcedureBase
{
    public virtual void OnInit(object userdata = null)
    {

    }
    public virtual void OnEnter(ProcedureBase last)
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnExit(ProcedureBase next)
    {

    }
}
