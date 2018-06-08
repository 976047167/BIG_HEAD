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
    protected bool isLoadSceneSuccess = false;
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
        Game.UI.CloaseAllForm(GetType());
        LoadScene(nextSceneID);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (progress >= 100 && isLoadSceneSuccess)
        {
            if (sceneTable.Procedure != "NULL")
            {
                ProcedureManager.ChangeProcedure(sceneTable.Procedure);
            }

        }
        if (progress < 100)
        {
            progress++;
        }
        if (progress >= 100f && isLoadSceneSuccess == false)
        {
            progress = 99f;
        }
        sliderProgress.value = progress / 100f;
    }


    protected void LoadScene(int sceneId)
    {
        SceneTableSetting setting = SceneTableSettings.Get(sceneId);
        if (setting == null)
        {
            Debug.LogError("要加载的场景不存在表中");
            return;
        }
        sceneTable = setting;
        ResourceManager.LoadScene(setting.Path, LoadSceneSuccess, LoadSceneFailed, false);
    }

    protected void LoadSceneSuccess(string path, object[] args)
    {
        isLoadSceneSuccess = true;
    }
    protected void LoadSceneFailed(string path, object[] args)
    {
        Debug.LogError("要加载的场景不存在->" + path);
    }

}
