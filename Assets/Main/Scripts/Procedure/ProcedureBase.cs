using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ProcedureBase
{
    /// <summary>
    /// 可以初始化失败
    /// </summary>
    /// <param name="userdata"></param>
    /// <returns></returns>
    public virtual bool OnInit(object userdata = null)
    {
        return true;
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
