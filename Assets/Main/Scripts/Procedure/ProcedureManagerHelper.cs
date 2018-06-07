using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureManagerHelper : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (ProcedureManager.Current != null)
            ProcedureManager.Current.OnUpdate();
    }
}
