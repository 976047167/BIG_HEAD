using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;
/// <summary>
/// 资源加载类
/// </summary>
public class ResourceManager
{
    public static void Load<T>(string path, object arg, Action<string, object, T> callback) where T : Object
    {
        T obj = Resources.Load<T>(path);
        if (callback != null)
        {
            callback(path, arg, obj);
        }
    }

    public static void LoadTexture(string path, object arg, Action<string, object, Texture2D> callback)
    {

        Load("UITexture/" + path, arg, callback);
    }


}
