using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        dataMgr = new DataMgr();
        battleMgr = new BattleMgr();
        uiModule = UIModule.Instance;
        ResourceManager.Init();
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if !UNITY_EDITOR
            Application.Quit();
#endif
        }
    }

    void StartGame()
    {
        dataMgr.OnInit();
        SceneManager.LoadScene("Main");

    }

    #region Game Module Managers
    static DataMgr dataMgr;
    static BattleMgr battleMgr;
    static UIModule uiModule;
    public static DataMgr DataManager { get { return dataMgr; } }
    public static BattleMgr BattleManager { get { return battleMgr; } }
    public static UIModule UI { get { return uiModule; } }
    #endregion
}
