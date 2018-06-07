using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AppSettings;

public class SceneMgr
{

    public static void LoadScene(int seneId)
    {
        UIModule.Instance.OpenForm<WND_Loading>(new object[] { seneId, false });
    }
    public static void LoadSceneAdditive(int seneId)
    {
        UIModule.Instance.OpenForm<WND_Loading>(new object[] { seneId, true });
    }
}
