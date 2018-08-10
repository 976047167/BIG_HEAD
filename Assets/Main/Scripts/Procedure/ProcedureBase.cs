using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ProcedureBase
{
    public float Progress { get; protected set; }
    /// <summary>
    /// 可以初始化失败
    /// </summary>
    /// <param name="userdata"></param>
    /// <returns></returns>
    public virtual IEnumerator OnInit(object userdata = null)
    {
        yield return null;
        Progress = 1f;
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
