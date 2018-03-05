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
    public static UIModule Instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = new UIModule();
            }
            return _instance;
        }
    }

    Dictionary<Type, UIFormBase> DicOpenedUIForm = new Dictionary<Type, UIFormBase>();
    
    public void OpenUIForm<T>() where T:UIFormBase
    {
        if (DicOpenedUIForm.ContainsKey(typeof(T)))
        {
            DicOpenedUIForm[typeof(T)].gameObject.SetActive(true);
            return;
        }
        UIConfig config = DicUIConfig[typeof(T)];
        if (config==null)
        {
            Debug.LogError("The UI[" + typeof(T).ToString() + "] is not configed!");
            return;
        }
        GameObject uiForm = Resources.Load<GameObject>(config.PrefabName);
        if (uiForm==null)
        {
            Debug.LogError("The UI prefab[" + config.PrefabName + "] is not exist!");
            return;
        }
        GameObject form = GameObject.Instantiate<GameObject>(uiForm, UICamera.mainCamera.transform);
        form.transform.localPosition = Vector3.zero;
        form.transform.localScale = Vector3.one;
        form.transform.localEulerAngles = Vector3.zero;
        form.SetActive(true);
        DicOpenedUIForm[typeof(T)] = form.GetComponent<T>();
        DicOpenedUIForm[typeof(T)].Init();
    }



    static Dictionary<Type, UIConfig> DicUIConfig = new Dictionary<Type, UIConfig>()
    {
        {typeof(UIBattleForm),new UIConfig("Prefabs/UIForm/WND_BattleForm") }
    };
    class UIConfig
    {
        public string PrefabName;
        
        public UIConfig(string prefabName)
        {
            PrefabName = prefabName;
        }
    }
}
