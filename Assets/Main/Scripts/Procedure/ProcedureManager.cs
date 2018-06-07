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
        if (current == null)
        {
            helper = new GameObject("[ProcedureManagerHelper]").AddComponent<ProcedureManagerHelper>();
        }
        else
            current.OnExit();
        current = new T();
        current.OnInit(userdata);
        current.OnEnter();
    }


}

