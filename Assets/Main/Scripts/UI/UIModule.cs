using System;
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
        GameObject uiForm = Resources.Load<GameObject>(config.PrefabName);
        if (uiForm == null)
        {
            Debug.LogError("The UI prefab[" + config.PrefabName + "] is not exist!");
            return;
        }
        GameObject form = GameObject.Instantiate<GameObject>(uiForm, UICamera.mainCamera.transform);
        form.transform.localPosition = Vector3.zero;
        form.transform.localScale = Vector3.one;
        form.transform.localEulerAngles = Vector3.zero;
        form.SetActive(true);
        T script = form.GetComponent<T>();
        if (script == null)
        {
            Debug.LogError("The UI need a script[" + typeof(T).ToString() + "]!");
            GameObject.Destroy(form);
            return;
        }
        DicOpenedUIForm[typeof(T)] = script;
        script.Init(userdata);
    }

    public T GetForm<T>() where T : UIFormBase
    {
        return DicOpenedUIForm[typeof(T)] as T;
    }

    public void CloseForm<T>() where T : UIFormBase
    {
        if (DicOpenedUIForm[typeof(T)] == null)
        {
            return;
        }
        GameObject.Destroy(DicOpenedUIForm[typeof(T)].gameObject);
        DicOpenedUIForm.Remove(typeof(T));
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }

    static Dictionary<Type, UIConfig> DicUIConfig = new Dictionary<Type, UIConfig>()
    {
        {typeof(UIBattleForm),new UIConfig("Prefabs/UIForm/WND_BattleForm") },
        {typeof(UIMapInfo),new UIConfig("Prefabs/UIForm/WND_MapInfo") },
        {typeof(WND_ChosePass),new UIConfig("Prefabs/UIForm/WND_ChosePass") },
        {typeof(WND_Bag),new UIConfig("Prefabs/UIForm/WND_Bag") },
        //{typeof(WND_Deck),new UIConfig("Prefabs/UIForm/WND_Deck") },
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
