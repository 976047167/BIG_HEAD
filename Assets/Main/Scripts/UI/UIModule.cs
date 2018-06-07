﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModule
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

    Dictionary<Type, UIFormBase> DicOpenedUIForm = new Dictionary<Type, UIFormBase>();
    static int baseDepth = 0;
    public void OpenForm<T>(object userdata = null) where T : UIFormBase
    {
        if (DicOpenedUIForm.ContainsKey(typeof(T)) && DicOpenedUIForm[typeof(T)] != null)
        {
            DicOpenedUIForm[typeof(T)].gameObject.SetActive(true);
            return;
        }
        UIConfig config = DicUIConfig[typeof(T)];
        if (config == null)
        {
            Debug.LogError("The UI[" + typeof(T).ToString() + "] is not configed!");
            return;
        }
        ResourceManager.LoadGameObject(config.PrefabName, LoadFormSuccess, LoadFormFailed, typeof(T), userdata);

    }
    void LoadFormSuccess(string path, object[] userData, GameObject uiForm)
    {
        UIConfig config = DicUIConfig[(userData[0] as Type)];
        if (uiForm == null)
        {
            Debug.LogError("The UI prefab[" + config.PrefabName + "] is not exist!");
            return;
        }
        GameObject form = uiForm;
        form.transform.SetParent(uiCamera);
        form.transform.localPosition = Vector3.zero;
        form.transform.localScale = Vector3.one;
        form.transform.localEulerAngles = Vector3.zero;
        form.SetActive(true);
        UIPanel[] panels = form.transform.GetComponentsInChildren<UIPanel>(true);
        List<UIPanel> sortedPanels = new List<UIPanel>(panels);
        sortedPanels.Sort((p1, p2) => p1.depth.CompareTo(p2.depth));
        for (int i = 0; i < sortedPanels.Count; i++)
        {
            sortedPanels[i].depth = baseDepth + i;
        }
        baseDepth += panels.Length;
        UIFormBase script = form.GetComponent((userData[0] as Type)) as UIFormBase;
        if (script == null)
        {
            Debug.LogError("The UI need a script[" + typeof(UIFormBase).ToString() + "]!");
            GameObject.Destroy(form);
            return;
        }
        DicOpenedUIForm[(userData[0] as Type)] = script;
        script.Init(userData[1]);
    }
    void LoadFormFailed(string path, object[] userData)
    {
        Debug.LogError("Open UIForm " + userData[0] + " Failed!");
    }
    public T GetForm<T>() where T : UIFormBase
    {
        return DicOpenedUIForm[typeof(T)] as T;
    }

    public void CloseForm<T>() where T : UIFormBase
    {
        CloseForm(typeof(T));
    }
    public void CloseForm(Type formType)
    {
        if (DicOpenedUIForm.ContainsKey(formType) == false || DicOpenedUIForm[formType] == null)
        {
            return;
        }
        DicOpenedUIForm[formType].Close();
        GameObject.Destroy(DicOpenedUIForm[formType].gameObject);
        DicOpenedUIForm.Remove(formType);
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }

    public void CloaseAllForm(params Type[] exceptForms)
    {
        List<Type> removeList = new List<Type>();
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

    static Dictionary<Type, UIConfig> DicUIConfig = new Dictionary<Type, UIConfig>()
    {
        {typeof(UIBattleForm),new UIConfig("UIForm/WND_BattleForm") },
        {typeof(UIMapInfo),new UIConfig("UIForm/WND_MapInfo") },
        {typeof(WND_Dialog),new UIConfig("UIForm/WND_Dialog") },
        {typeof(WND_Bag),new UIConfig("UIForm/WND_Bag") },
        {typeof(WND_ShowCard),new UIConfig("UIForm/WND_ShowCard") },
        {typeof(WND_Kaku),new UIConfig("UIForm/WND_Kaku") },
        {typeof(UIMenu),new UIConfig("UIForm/WND_Menu") },
        {typeof(WND_Reward),new UIConfig("UIForm/WND_Reward") },
        {typeof(WND_MainTown),new UIConfig("UIForm/WND_MainTown") },
        {typeof(WND_ChoseDeck),new UIConfig("UIForm/WND_ChoseDeck") },
    };
    public bool SetUICamera(UIModelCameraHelper uiCameraHelper)
    {
        if (uiCamera == null)
        {
            uiRoot = uiCameraHelper.transform;
            uiCamera = uiRoot.Find("Camera");
            return true;
        }
        GameObject.Destroy(uiCameraHelper.gameObject);
        return false;
    }
    class UIConfig
    {
        public string PrefabName;

        public UIConfig(string prefabName)
        {
            PrefabName = prefabName;
        }
    }
}
