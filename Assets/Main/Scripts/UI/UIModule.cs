using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
using System.Reflection;

public partial class UIModule
{
    private UIModule()
    {

    }
    private static UIModule _instance;
    private static Transform uiRoot;
    private static Transform uiCamera;
    public static UIModule Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIModule();
            }
            return _instance;
        }
    }

    Dictionary<int, UIFormBase> DicOpenedUIForm = new Dictionary<int, UIFormBase>();

    bool isLoadingRoot = false;
    List<int> waitLoadList = new List<int>();
    UIModelCameraHelper uiCameraHelper = null;
    void LoadUIRoot(int formId)
    {
        if (isLoadingRoot)
        {
            waitLoadList.Add(formId);
            Debug.LogError("拥挤加载!");
            return;
        }
        waitLoadList.Add(formId);
        isLoadingRoot = true;
        ResourceManager.LoadGameObject("UI/UI Root", (path, args, root) =>
        {
            if (uiCamera == null)
            {
                uiRoot = root.transform;
                uiCamera = uiRoot.Find("Camera");
                uiCameraHelper = uiRoot.GetComponent<UIModelCameraHelper>();
                if (uiCameraHelper == null)
                {
                    uiCameraHelper = root.AddComponent<UIModelCameraHelper>();
                }
                GameObject.DontDestroyOnLoad(root);
            }
            else if (root.transform != uiRoot)
                GameObject.Destroy(root);
            isLoadingRoot = false;
            for (int i = 0; i < waitLoadList.Count; i++)
            {
                int form = waitLoadList[i];
                OpenForm(form);
            }
        }, (path, args) => { Debug.LogError("没有找到uiRoot->" + path); });
    }

    public void OpenForm<T>(object userdata = null) where T : UIFormBase
    {
        OpenForm((int)Enum.Parse(typeof(FormId), typeof(T).Name), userdata);
    }
    public void OpenForm(FormId formId, object userdata = null)
    {
        OpenForm((int)formId, userdata);
    }
    public void OpenForm(int formId, object userdata = null)
    {
        if (uiCamera == null)
        {
            LoadUIRoot(formId);
            return;
        }
        if (DicOpenedUIForm.ContainsKey(formId) && DicOpenedUIForm[formId] != null)
        {
            DicOpenedUIForm[formId].Show();
            return;
        }
        UIFormTableSetting config = UIFormTableSettings.Get((int)formId);
        if (config == null)
        {
            Debug.LogError("The UI[" + formId.ToString() + "] is not configed!");
            return;
        }
        ResourceManager.LoadGameObject("UI/" + config.Path, LoadFormSuccess, LoadFormFailed, config, userdata);
    }
    void LoadFormSuccess(string path, object[] userData, GameObject uiForm)
    {
        UIFormTableSetting config = (userData[0] as UIFormTableSetting);
        if (uiForm == null)
        {
            Debug.LogError("The UI prefab[" + config.Path + "] is not exist!");
            return;
        }
        UIFormBase script = uiForm.GetComponent<UIFormBase>();
        if (script == null)
        {
            Debug.LogError("The UI need a script[" + typeof(UIFormBase).ToString() + "]!");
            GameObject.Destroy(uiForm);
            return;
        }
        uiForm.transform.SetParent(uiCamera);
        uiForm.transform.localPosition = Vector3.zero;
        uiForm.transform.localScale = Vector3.one;
        uiForm.transform.localEulerAngles = Vector3.zero;
        uiForm.SetActive(true);
        DicOpenedUIForm[config.Id] = script;
        script.Init(userData[1]);
        Messenger.Broadcast<UIFormBase>(MessageID.UI_FORM_LOADED, script);
    }
    void LoadFormFailed(string path, object[] userData)
    {
        Debug.LogError("Open UIForm " + (userData[0] as UIFormTableSetting).Path + " Failed!");
    }
    public T GetForm<T>() where T : UIFormBase
    {
        FormId formId = (FormId)Enum.Parse(typeof(FormId), typeof(T).Name);
        return GetForm((int)formId) as T;
    }
    public UIFormBase GetForm(FormId formId)
    {
        return GetForm((int)formId);
    }
    public UIFormBase GetForm(int formId)
    {
        if (DicOpenedUIForm.ContainsKey(formId) == false)
        {
            return null;
        }
        return DicOpenedUIForm[formId];
    }
    public void CloseForm<T>() where T : UIFormBase
    {
        FormId formId = (FormId)Enum.Parse(typeof(FormId), typeof(T).Name);
        CloseForm((int)formId);
    }
    public void CloseForm(FormId formId)
    {
        CloseForm((int)formId);
    }
    public void CloseForm(int formId)
    {
        if (DicOpenedUIForm.ContainsKey(formId) == false || DicOpenedUIForm[formId] == null)
        {
            return;
        }
        DicOpenedUIForm[formId].Close();
        GameObject.Destroy(DicOpenedUIForm[formId].gameObject);
        DicOpenedUIForm.Remove(formId);
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
    public void CloaseAllForm(params Type[] exceptForms)
    {
        int[] formIds = new int[exceptForms.Length];
        for (int i = 0; i < exceptForms.Length; i++)
        {
            formIds[i] = (int)Enum.Parse(typeof(FormId), exceptForms[i].Name);
        }
        CloaseAllForm(formIds);
    }
    public void CloaseAllForm(params FormId[] exceptForms)
    {
        int[] formIds = new int[exceptForms.Length];
        for (int i = 0; i < exceptForms.Length; i++)
        {
            formIds[i] = (int)exceptForms[i];
        }
        CloaseAllForm(formIds);
    }
    public void CloaseAllForm(params int[] exceptForms)
    {
        List<int> removeList = new List<int>();
        foreach (var form in DicOpenedUIForm)
        {
            if (form.Value == null)
            {
                removeList.Add(form.Key);
                continue;
            }
            bool except = false;
            for (int i = 0; i < exceptForms.Length; i++)
            {
                if (form.Key == exceptForms[i])
                {
                    except = true;
                    break;
                }
            }
            if (except == false)
            {
                removeList.Add(form.Key);
            }
        }
        for (int i = 0; i < removeList.Count; i++)
        {
            CloseForm(removeList[i]);
        }
    }
    List<int> m_RemoveList = new List<int>();
    List<UIFormBase> m_RormList = new List<UIFormBase>();
    public void UpdateForms()
    {
        foreach (var form in DicOpenedUIForm)
        {
            if (form.Value == null)
            {
                m_RemoveList.Add(form.Key);
                continue;
            }
            m_RormList.Add(form.Value);
        }
        for (int i = 0; i < m_RormList.Count; i++)
        {
            m_RormList[i].FormUpdate();
        }
        for (int i = 0; i < m_RemoveList.Count; i++)
        {
            CloseForm(m_RemoveList[i]);
        }
        m_RemoveList.Clear();
        m_RormList.Clear();
    }


    public bool SetUICamera(UIModelCameraHelper uiCameraHelper)
    {
        if (this.uiCameraHelper == uiCameraHelper)
        {
            return true;
        }
        if (uiCamera == null)
        {
            uiRoot = uiCameraHelper.transform;
            uiCamera = uiRoot.Find("Camera");
            this.uiCameraHelper = uiCameraHelper;
            return true;
        }
        GameObject.Destroy(uiCameraHelper.gameObject);
        return false;
    }
    //class UIConfig
    //{
    //    public string PrefabName;

    //    public UIConfig(string prefabName)
    //    {
    //        PrefabName = prefabName;
    //    }
    //}

}
/// <summary>
/// UI窗口分组
/// </summary>
public enum UIFormsGroup : int
{
    /// <summary>默认</summary>
    Default = 0,
    /// <summary>提示，可重复打开多个</summary>
    Toast = 1,
    /// <summary>
    /// 对话框，可重复打开多个
    /// </summary>
    Dialog = 2,
    /// <summary>
    /// 顶层窗口
    /// </summary>
    Top = 3,
}
/// <summary>
/// UI窗体显示类型
/// </summary>
public enum UIFormsShowMode : int
{
    /// <summary>普通显示</summary>
    Normal,
    /// <summary>反向切换</summary>
    ReverseChange,
    /// <summary>隐藏其他界面</summary>
    HideOther,
}