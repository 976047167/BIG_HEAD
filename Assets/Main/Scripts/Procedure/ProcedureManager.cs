using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class ProcedureManager
{
    static ProcedureBase current = null;
    static ProcedureManagerHelper helper = null;
    public static ProcedureBase Current { get { return current; } }

    public static void ChangeProcedure<T>(object userdata = null) where T : ProcedureBase, new()
    {
        if (helper == null)
        {
            helper = new GameObject("[ProcedureManagerHelper]").AddComponent<ProcedureManagerHelper>();
        }
        ProcedureBase next = new T();
        next.OnInit(userdata);
        if (current != null)
            current.OnExit(next);
        next.OnEnter(current);
        current = next;
    }


}

