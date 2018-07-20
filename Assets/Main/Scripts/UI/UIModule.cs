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

    bool inited = false;
    bool isRootLoaded = false;

    UIModelCameraHelper uiCameraHelper = null;
    void LoadUIRoot()
    {
        if (uiCamera != null)
        {
            isRootLoaded = true;
        }
        if (isRootLoaded)
        {
            return;
        }
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
            isRootLoaded = true;
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
            Debug.LogError("请先初始化UI模块!");
            return;
        }

        UIFormTableSetting config = UIFormTableSettings.Get((int)formId);
        if (config == null)
        {
            Debug.LogError("The UI[" + formId.ToString() + "] is not configed!");
            return;
        }
        UIFormBase form = GetForm(formId);
        if (form == null)
        {
            ResourceManager.LoadGameObject("UI/" + config.Path, LoadFormSuccess, LoadFormFailed, config, userdata);
            return;
        }
        ProcessForm(form, config, userdata, true);

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
        ProcessForm(script, config, userData[1], false);
        Messenger.Broadcast<UIFormBase>(MessageID.UI_FORM_LOADED, script);
    }

    Dictionary<UIFormsGroup, int> baseDepths = new Dictionary<UIFormsGroup, int>();
    Dictionary<UIFormsShowMode, List<UIFormBase>> dicForms = new Dictionary<UIFormsShowMode, List<UIFormBase>>();
    Dictionary<UIFormsShowMode, GameObject> dicFormRoots = new Dictionary<UIFormsShowMode, GameObject>();
    public IEnumerator Init()
    {
        if (inited)
        {
            yield return null;
        }
        LoadUIRoot();
        while (isRootLoaded == false)
        {
            yield return null;
        }
        string[] showModes = Enum.GetNames(typeof(UIFormsShowMode));
        for (int i = 0; i < showModes.Length; i++)
        {
            UIFormsShowMode mode = (UIFormsShowMode)Enum.Parse(typeof(UIFormsShowMode), showModes[i]);
            if (!dicForms.ContainsKey(mode))
            {
                dicForms[mode] = new List<UIFormBase>();
                GameObject modeRoot = new GameObject(mode.ToString());
                modeRoot.transform.SetParent(uiCamera);
                modeRoot.transform.localPosition = Vector3.zero;
                modeRoot.transform.localScale = Vector3.one;
                modeRoot.transform.localEulerAngles = Vector3.zero;
                dicFormRoots.Add(mode, modeRoot);
            }
        }
        inited = true;
    }
    void ProcessForm(UIFormBase form, UIFormTableSetting config, object userdata, bool isOld)
    {
        //设置层级
        UIPanel[] panels = form.transform.GetComponentsInChildren<UIPanel>(true);
        List<UIPanel> sortedPanels = new List<UIPanel>(panels);
        sortedPanels.Sort((p1, p2) => p1.depth.CompareTo(p2.depth));
        UIFormsGroup group = (UIFormsGroup)config.Group;
        for (int i = 0; i < sortedPanels.Count; i++)
        {
            if (!baseDepths.ContainsKey(group))
            {
                baseDepths[group] = config.Group * 1000 * 1000;
            }
            sortedPanels[i].depth = baseDepths[group] + i;
        }
        baseDepths[group] += panels.Length;
        //设置显示逻辑
        UIFormsShowMode mode = (UIFormsShowMode)config.ShowMode;

        form.transform.SetParent(dicFormRoots[mode].transform);
        form.transform.localPosition = Vector3.zero;
        form.transform.localScale = Vector3.one;
        form.transform.localEulerAngles = Vector3.zero;
        List<UIFormBase> formList = null;
        switch (mode)
        {
            case UIFormsShowMode.Pop:
                formList = dicForms[mode];
                if (formList.Count > 0 && formList[formList.Count - 1] != null)
                {
                    formList[formList.Count - 1].Cover();
                }
                form.Init(config, userdata);
                formList.Add(form);
                if (dicForms[UIFormsShowMode.Single].Count > 0)
                {
                    form.Cover();
                }
                break;
            case UIFormsShowMode.HideOther:
                formList = dicForms[mode];
                for (int i = 0; i < dicForms[UIFormsShowMode.HideOther].Count; i++)
                {
                    dicForms[UIFormsShowMode.HideOther][i].Cover();
                }
                for (int i = 0; i < dicForms[UIFormsShowMode.Normal].Count; i++)
                {
                    dicForms[UIFormsShowMode.Normal][i].Cover();
                }
                form.Init(config, userdata);
                formList.Add(form);
                if (dicForms[UIFormsShowMode.Single].Count > 0)
                {
                    form.Cover();
                }
                break;
            case UIFormsShowMode.Single:
                formList = dicForms[mode];
                for (int i = 0; i < dicForms[UIFormsShowMode.HideOther].Count; i++)
                {
                    dicForms[UIFormsShowMode.HideOther][i].Cover();
                }
                for (int i = 0; i < dicForms[UIFormsShowMode.Normal].Count; i++)
                {
                    dicForms[UIFormsShowMode.Normal][i].Cover();
                }
                for (int i = 0; i < dicForms[UIFormsShowMode.Pop].Count; i++)
                {
                    dicForms[UIFormsShowMode.Pop][i].Cover();
                }
                form.Init(config, userdata);
                formList.Add(form);
                break;
            case UIFormsShowMode.Normal:
            default:
                formList = dicForms[UIFormsShowMode.Normal];
                form.Init(config, userdata);
                formList.Add(form);
                if (dicForms[UIFormsShowMode.HideOther].Count > 0)
                {
                    form.Cover();
                }
                if (dicForms[UIFormsShowMode.Single].Count > 0)
                {
                    form.Cover();
                }
                break;
        }
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
        UIFormTableSetting config = UIFormTableSettings.Get((int)formId);
        if (config == null)
        {
            Debug.LogError("The UI[" + formId.ToString() + "] is not configed!");
            return null;
        }
        //可以重复打开多个
        if ((UIFormsGroup)config.Group == UIFormsGroup.Toast || (UIFormsGroup)config.Group == UIFormsGroup.Dialog)
        {
            return null;
        }
        UIFormBase form = null;
        switch ((UIFormsShowMode)config.ShowMode)
        {
            case UIFormsShowMode.Pop:
                foreach (var item in dicForms[UIFormsShowMode.Pop])
                {
                    if (item != null && item.Table.Id == config.Id)
                    {
                        form = item;
                    }
                }
                break;
            case UIFormsShowMode.HideOther:
                foreach (var item in dicForms[UIFormsShowMode.HideOther])
                {
                    if (item != null && item.Table.Id == config.Id)
                    {
                        form = item;
                    }
                }
                break;
            case UIFormsShowMode.Single:
                foreach (var item in dicForms[UIFormsShowMode.Single])
                {
                    if (item != null && item.Table.Id == config.Id)
                    {
                        form = item;
                    }
                }
                break;
            case UIFormsShowMode.Normal:
            default:
                foreach (var item in dicForms[UIFormsShowMode.Normal])
                {
                    if (item != null && item.Table.Id == config.Id)
                    {
                        form = item;
                    }
                }
                break;
        }
        return form;
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
        UIFormBase form = FindForm(formId);
        CloseForm(form);
    }
    public void CloseForm(UIFormBase form)
    {
        if (form == null)
        {
            return;
        }
        UIFormTableSetting config = form.Table;
        //设置显示逻辑
        UIFormsShowMode mode = (UIFormsShowMode)config.ShowMode;
        if (!dicForms.ContainsKey(mode))
        {
            dicForms[mode] = new List<UIFormBase>();
            GameObject modeRoot = new GameObject(mode.ToString());
            modeRoot.transform.SetParent(uiCamera);
            modeRoot.transform.localPosition = Vector3.zero;
            modeRoot.transform.localScale = Vector3.one;
            modeRoot.transform.localEulerAngles = Vector3.zero;
            dicFormRoots.Add(mode, modeRoot);
        }
        List<UIFormBase> formList = null;
        UIFormBase nextForm = null;
        int index = 1;
        switch (mode)
        {
            case UIFormsShowMode.Pop:
                formList = dicForms[mode];
                //关闭的窗口并不是当前显示的窗口
                if (form.State == UIFormState.Hide || form.gameObject.activeSelf == false)
                {
                    break;
                }
                //寻找下个可以显示的窗口
                while (nextForm == null && formList.Count > index)
                {
                    if (formList[formList.Count - 1 - index] != null && formList[formList.Count - 1 - index].State == UIFormState.Show)
                    {
                        nextForm = formList[formList.Count - 1 - index];
                    }
                    else
                    {
                        index++;
                    }
                }
                if (nextForm != null)
                {
                    nextForm.Resume();
                }
                break;
            case UIFormsShowMode.HideOther:
                formList = dicForms[mode];
                //关闭的窗口并不是当前显示的窗口
                if (form.State == UIFormState.Hide || form.gameObject.activeSelf == false)
                {
                    break;
                }
                //寻找下个可以显示的窗口
                while (nextForm == null && formList.Count > index)
                {
                    if (formList[formList.Count - 1 - index] != null && formList[formList.Count - 1 - index].State == UIFormState.Show)
                    {
                        nextForm = formList[formList.Count - 1 - index];
                    }
                    else
                    {
                        index++;
                    }
                }
                if (nextForm == null)
                {
                    for (int i = 0; i < dicForms[UIFormsShowMode.Normal].Count; i++)
                    {
                        dicForms[UIFormsShowMode.Normal][i].Resume();
                    }
                }
                if (nextForm != null)
                {
                    nextForm.Resume();
                }
                break;
            case UIFormsShowMode.Single:
                formList = dicForms[mode];
                //关闭的窗口并不是当前显示的窗口
                if (form.State == UIFormState.Hide || form.gameObject.activeSelf == false)
                {
                    break;
                }
                while (nextForm == null && formList.Count > index)
                {
                    if (formList[formList.Count - 1 - index] != null && formList[formList.Count - 1 - index].State == UIFormState.Show)
                    {
                        nextForm = formList[formList.Count - 1 - index];
                    }
                    else
                    {
                        index++;
                    }
                }
                //非独占，都显示弹窗
                if (nextForm == null)
                {
                    if (dicForms[UIFormsShowMode.Pop].Count > 0)
                    {
                        dicForms[UIFormsShowMode.Pop][dicForms[UIFormsShowMode.Pop].Count - 1].Resume();
                    }
                }
                //寻找模式窗口
                while (nextForm == null && dicForms[UIFormsShowMode.HideOther].Count > index)
                {
                    UIFormBase formBase = dicForms[UIFormsShowMode.HideOther][formList.Count - 1 - index];
                    if (formBase != null && formBase.State == UIFormState.Show)
                    {
                        nextForm = formBase;
                    }
                    else
                    {
                        index++;
                    }
                }
                //没有模式窗口，显示普通窗口
                if (nextForm == null)
                {
                    for (int i = 0; i < dicForms[UIFormsShowMode.Normal].Count; i++)
                    {
                        dicForms[UIFormsShowMode.Normal][i].Resume();
                    }
                }
                if (nextForm != null)
                {
                    nextForm.Resume();
                }
                break;
            case UIFormsShowMode.Normal:
            default:
                formList = dicForms[UIFormsShowMode.Normal];
                //关闭的窗口并不是当前显示的窗口
                if (form.State == UIFormState.Hide || form.gameObject.activeSelf == false)
                {
                    break;
                }
                while (nextForm == null && formList.Count > index)
                {
                    if (formList[formList.Count - 1 - index] != null && formList[formList.Count - 1 - index].State == UIFormState.Show)
                    {
                        nextForm = formList[formList.Count - 1 - index];
                    }
                    else
                    {
                        index++;
                    }
                }
                if (nextForm != null)
                {
                    nextForm.Resume();
                }
                break;
        }

        //寻找剩下的有没有相同的，有就不销毁
        int count = 0;
        index = 0;
        for (int i = 0; i < formList.Count; i++)
        {
            if (formList[i] == form)
            {
                count++;
                index = i;
            }
        }
        formList.RemoveAt(index);
        if (count > 1)
        {
            //Debug.LogError(form.Table.Id + ":" + form.Table.Name + "被重复打开了!");
            form.Cover();
        }
        else
        {
            form.Close();
            GameObject.Destroy(form.gameObject);
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }
    }
    /// <summary>
    /// 获取最新的一个打开的这个ID的窗口
    /// </summary>
    UIFormBase FindForm(int formId)
    {
        UIFormTableSetting config = UIFormTableSettings.Get((int)formId);
        if (config == null)
        {
            Debug.LogError("The UI[" + formId.ToString() + "] is not configed!");
            return null;
        }
        UIFormBase form = null;
        List<UIFormBase> list = null;
        switch ((UIFormsShowMode)config.ShowMode)
        {
            case UIFormsShowMode.Pop:
                list = dicForms[UIFormsShowMode.Pop];
                break;
            case UIFormsShowMode.HideOther:
                list = dicForms[UIFormsShowMode.HideOther];
                break;
            case UIFormsShowMode.Single:
                list = dicForms[UIFormsShowMode.Single];
                break;
            case UIFormsShowMode.Normal:
            default:
                list = dicForms[UIFormsShowMode.Normal];
                break;
        }
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i] != null && list[i].Table.Id == config.Id)
            {
                form = list[i];
                break;
            }
        }
        return form;
    }
    public void CloaseAllForm(Type[] exceptForms)
    {
        int[] formIds = new int[exceptForms.Length];
        for (int i = 0; i < exceptForms.Length; i++)
        {
            formIds[i] = (int)Enum.Parse(typeof(FormId), exceptForms[i].Name);
        }
        CloaseAllForm(formIds);
    }
    public void CloaseAllForm(FormId[] exceptForms)
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
        foreach (var formList in dicForms)
        {
            List<UIFormBase> removeList = new List<UIFormBase>();
            foreach (var form in formList.Value)
            {
                if (form == null)
                {
                    removeList.Add(form);
                    continue;
                }
                bool except = false;
                for (int i = 0; i < exceptForms.Length; i++)
                {
                    if (form.Table.Id == exceptForms[i])
                    {
                        except = true;
                        break;
                    }
                }
                if (except == false)
                {
                    removeList.Add(form);
                }
            }
            for (int i = 0; i < removeList.Count; i++)
            {
                CloseForm(removeList[i]);
            }
        }

    }

    public void UpdateForms()
    {
        foreach (var formList in dicForms)
        {
            List<int> removeList = new List<int>();
            for (int i = 0; i < formList.Value.Count; i++)
            {
                UIFormBase form = formList.Value[i];
                if (form == null)
                {
                    removeList.Add(i);
                    continue;
                }
                if (form.IsOpen == false)
                {
                    form.Open();
                }
                if (form.State == UIFormState.Show && form.gameObject.activeInHierarchy)
                {
                    form.FormUpdate();
                }

            }
            for (int i = 0; i < removeList.Count; i++)
            {
                formList.Value.RemoveAt(removeList[i]);
            }
        }
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
    /// <summary>按照堆栈规则弹出，不可以隐藏</summary>
    Pop,
    /// <summary>隐藏其他HideOther和普通界面</summary>
    HideOther,
    /// <summary>
    /// 唯一存在的窗口
    /// </summary>
    Single,
}

public enum UIFormState
{
    Hide = 0,
    Show = 1,
}
