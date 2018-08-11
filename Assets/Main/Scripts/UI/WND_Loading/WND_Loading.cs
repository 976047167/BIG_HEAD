using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
/// <summary>
/// 加载场景的过场界面，参数：int->场景ID
/// </summary>
public class WND_Loading : UIFormBase
{
    private UISlider sliderProgress = null;

    protected int nextSceneID = 0;
    protected float progress = 0f;
    protected SceneTableSetting sceneTable = null;
    [SerializeField]
    protected LoadState loadState = LoadState.None;
    protected LoadState lastLoadState = LoadState.None;
    static OnAssetDestory lastSceneDestory = null;
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        nextSceneID = (int)userdata;
        //Messenger.AddListener(MessageID.UI_FORM_LOADED)
        sliderProgress = transform.Find("Slider").GetComponent<UISlider>();
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        loadState = LoadState.CheckValid;
        Messenger.AddListener<ProcedureBase>(MessageId.GAME_INIT_PROCEDURE_SUCCESS, InitProcedureSuccess);
        Messenger.AddListener<ProcedureBase>(MessageId.GAME_INIT_PROCEDURE_FAILED, InitProcedureFailed);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (lastLoadState != loadState)
        {
            lastLoadState = loadState;
            switch (loadState)
            {
                case LoadState.None:
                    loadState = LoadState.CheckValid;
                    break;
                case LoadState.CheckValid:
                    SceneTableSetting setting = SceneTableSettings.Get(nextSceneID);
                    if (setting == null)
                    {
                        Debug.LogError("要加载的场景不存在表中");
                        loadState = LoadState.Failed;
                        return;
                    }
                    sceneTable = setting;
                    loadState = LoadState.CloseForms;
                    break;
                case LoadState.CloseForms:
                    Game.UI.CloaseAllForm(Table.Id);
                    loadState = LoadState.LoadScene;
                    break;
                case LoadState.LoadScene:
                    LoadScene(nextSceneID);

                    break;
                case LoadState.ClearMemery:
                    ClearMemery();
                    loadState = LoadState.InitNextProcedure;
                    break;
                case LoadState.InitNextProcedure:
                    if (sceneTable.Procedure != "NULL")
                    {
                        ProcedureManager.ChangeProcedure(sceneTable.Procedure);
                    }
                    break;
                case LoadState.Success:
                    break;
                case LoadState.Failed:
                    break;
                default:
                    break;
            }

        }
        if (progress < 100)
        {
            progress += 3;
        }
        if (progress >= 100f * ((float)loadState / (float)LoadState.Success))
        {
            progress = 100f * ((float)loadState / (float)LoadState.Success);
        }
        sliderProgress.value = progress / 100f;
        if (progress >= 100f)
        {
            Game.UI.CloseForm<WND_Loading>();
        }
    }


    protected void LoadScene(int sceneId)
    {
        ResourceManager.LoadScene(sceneTable.Path, LoadSceneSuccess, LoadSceneFailed, false);
    }

    protected void LoadSceneSuccess(string path, object[] args, OnAssetDestory onAssetDestory)
    {
        if (lastSceneDestory != null)
        {
            lastSceneDestory();
        }
        loadState = LoadState.ClearMemery;
        lastSceneDestory = onAssetDestory;

    }
    protected void LoadSceneFailed(string path, object[] args)
    {
        Debug.LogError("要加载的场景不存在->" + path);
    }

    protected void ClearMemery()
    {
        ResourceManager.ReleaseBundle();
    }

    protected void InitProcedureSuccess(ProcedureBase next)
    {
        loadState = LoadState.Success;
    }
    protected void InitProcedureFailed(ProcedureBase next)
    {
        Debug.LogError("初始化下个流程失败!");
    }

    protected override void OnClose()
    {
        Messenger.RemoveListener<ProcedureBase>(MessageId.GAME_INIT_PROCEDURE_SUCCESS, InitProcedureSuccess);
        Messenger.RemoveListener<ProcedureBase>(MessageId.GAME_INIT_PROCEDURE_FAILED, InitProcedureFailed);
    }

    public enum LoadState
    {
        None = 0,
        CheckValid,
        CloseForms,
        LoadScene,
        ClearMemery,
        InitNextProcedure,
        Success,
        Failed,
    }
}
