using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
    public bool BundleEditorMode = true;
    public bool Online = false;
    public LanguageType language = LanguageType.None;

    public static Game Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(Init());

    }
    IEnumerator Init()
    {
        uiModule = UIModule.Instance;
        //资源第一优先级初始化
        yield return ResourceManager.Init(Game.Instance.BundleEditorMode);
        yield return ResourceManager.Preload();
        yield return uiModule.Init();
        dataMgr = new DataMgr();
        battleMgr = new BattleMgr();
        networkManager = new NetworkManager();
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
        ProcedureManager.ChangeProcedure<Procedure_Launch>();


        //SceneManager.LoadScene("Init");
        RegisterGlobalMessage();
    }
    public void ReStartGame()
    {
        StartCoroutine(Init());
    }

    void RegisterGlobalMessage()
    {
        Messenger.AddListener<int>(MessageId.GAME_CHANGE_SCENE, ChangeScene);
    }
    void RemoveGlobalMessage()
    {
        Messenger.RemoveListener<int>(MessageId.GAME_CHANGE_SCENE, ChangeScene);
    }
    void ChangeScene(int sceneId)
    {
        ProcedureManager.ChangeProcedure<Procedure_ChangeScene>(sceneId);
    }

    #region Game Module Managers
    static DataMgr dataMgr;
    static BattleMgr battleMgr;
    static UIModule uiModule;
    static NetworkManager networkManager;
    public static DataMgr DataManager { get { return dataMgr; } }
    public static BattleMgr BattleManager { get { return battleMgr; } }
    public static UIModule UI { get { return uiModule; } }
    public static NetworkManager NetworkManager { get { return networkManager; } }
    #endregion
}
