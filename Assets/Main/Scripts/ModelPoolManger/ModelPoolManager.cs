using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
using System;

public class ModelPoolManager  {
    static ModelPoolHelper helper;
    static bool modelPreloaded;
    static Dictionary<string, GameObject> dicModel;
    static Dictionary<string, List<GameObject>> ModelItemPool;
    int PoolMax = 10;
    static public IEnumerator Init()
    {
        if (helper == null)
        {
            helper =  new GameObject().AddComponent<ModelPoolHelper>();
        }
        modelPreloaded = false;
        PreLoad();
        while (!modelPreloaded)
        {
            yield return null;
        }
    }

   static  private void PreLoad() {
        int modelNum = UIItemModelTableSettings.GetInstance().Count;
        dicModel = new Dictionary<string, GameObject>();
        ModelItemPool = new Dictionary<string, List<GameObject>>();
        for (int i =1;i < modelNum + 1;i++)
        {
            UIItemModelTableSetting model = UIItemModelTableSettings.Get(i);
             ResourceManager.LoadGameObject(model.Path, (string str, object[] obj, GameObject go) => {
                 go.transform.SetParent(helper.transform);
                 go.transform.localScale = new Vector3(1, 1, 1);
                 go.SetActive(false);
                 dicModel.Add(model.Name, go);
                 ModelItemPool.Add(model.Name, new List<GameObject>());
                 if (dicModel.Count == modelNum)
                 {
                     modelPreloaded = true;
                 }
                 
             }, (str, obj) => {
                 Debug.LogError(string.Format("{0}预加载出现问题" ,str));
             });
        } 
    }
    
    public GameObject GetModelItem<T>(Transform parent, object itemData = null)where T: IModelItem
    {
        GameObject go = null;
        string objName = typeof(T).Name;
        if (ModelItemPool.ContainsKey(objName)&& ModelItemPool[objName].Count > 0)
        {
            go = ModelItemPool[objName][0];
            ModelItemPool[objName].Remove(go);
            //返回结果
        }
        else
        {
            go = UnityEngine.Object.Instantiate(dicModel[objName]);
        }
        go.transform.SetParent(parent, true);
        go.transform.localScale = new Vector3(1, 1, 1);
        go.GetComponent<T>().SetData(itemData);
        go.SetActive(false);
        
        return go;
    }
    public void RecycleObj<T>(GameObject obj) where T : IModelItem
    {
        string modelName = typeof(T).Name;
        if (!ModelItemPool.ContainsKey(modelName))
        {
            Debug.LogError(modelName + "无此模板池！");
            UnityEngine.Object.Destroy(obj);
            return;
        }
        if (ModelItemPool[modelName].Count >= PoolMax) {
            UnityEngine.Object.Destroy(obj);
            return;
        }
        obj.transform.SetParent(helper.transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.SetActive(false);
        ModelItemPool[modelName].Add(obj);
    }
}

public interface IModelItem
{
    void SetData(object modelData);
}
