using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class ProcedureManager
{
    static ProcedureBase current = null;
    static ProcedureManagerHelper helper = null;
    static bool isChanging = false;
    public static ProcedureBase Current { get { return current; } }

    public static void ChangeProcedure<T>(object userdata = null) where T : ProcedureBase, new()
    {
        ChangeProcedure(typeof(T), userdata);
    }

    public static void ChangeProcedure(string procedureName, object userdata = null)
    {
        ChangeProcedure(Assembly.GetExecutingAssembly().GetType(procedureName), userdata);
    }
    public static void ChangeProcedure(System.Type procedureType, object userdata = null)
    {
        if (helper == null)
        {
            helper = new GameObject("[ProcedureManagerHelper]").AddComponent<ProcedureManagerHelper>();
        }
        if (procedureType == null)
        {
            Debug.LogError("不存在的流程");
        }
        ProcedureBase next = System.Activator.CreateInstance(procedureType) as ProcedureBase;
        if (next == null)
        {
            return;
        }

        helper.StartCoroutine(ChangingProcedure(next, userdata));
        //return next;
    }

    static IEnumerator ChangingProcedure(ProcedureBase next, object userdata)
    {
        if (isChanging)
        {
            yield break;
        }
        isChanging = true;
        helper.StartCoroutine(next.OnInit(userdata));
        while (next.Progress < 1.0f && next.Progress >= 0f)
        {
            yield return null;
        }
        if (next.Progress < 0f)
        {
            isChanging = false;
            Messenger.Broadcast(MessageId.GAME_INIT_PROCEDURE_FAILED, next);
            yield break;
        }
        Messenger.Broadcast(MessageId.GAME_INIT_PROCEDURE_SUCCESS, next);
        if (current != null)
            current.OnExit(next);
        next.OnEnter(current);
        current = next;
        isChanging = false;
    }

}

