using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AppSettings;

public class SceneMgr
{
    public static void ChangeScene(int sceneId)
    {
        ProcedureManager.ChangeProcedure<Procedure_ChangeScene>(sceneId);
    }
}
