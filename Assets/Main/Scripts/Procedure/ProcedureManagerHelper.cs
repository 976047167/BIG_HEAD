using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureManagerHelper : MonoBehaviour
{
    [SerializeField]
    private string current = "";
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (ProcedureManager.Current != null)
            current = ProcedureManager.Current.ToString();
#endif
        if (ProcedureManager.Current != null)
            ProcedureManager.Current.OnUpdate();
    }
}
