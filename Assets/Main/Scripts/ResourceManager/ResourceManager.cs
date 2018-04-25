using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using KEngine.Modules;
using Object = UnityEngine.Object;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// 资源加载类
/// </summary>
public class ResourceManager
{
    static ResourceManagerHelper helper;
    public static bool EditorMode { private set; get; }

    static Dictionary<string, AssetLoader> dicAssetLoader = new Dictionary<string, AssetLoader>();
    public static void Init(bool isEditorMode = false)
    {
#if UNITY_EDITOR
        EditorMode = isEditorMode;
#endif
        if (helper == null)
        {
            helper = new GameObject().AddComponent<ResourceManagerHelper>();
        }
        if (EditorMode)
        {
            SettingModule.CustomLoadSetting = LoadSettingFromCache;
        }
    }
    //缓存表格数据
    static Dictionary<string, byte[]> TableCache = new Dictionary<string, byte[]>();

    static byte[] LoadSettingFromCache(string path)
    {
        if (TableCache.ContainsKey(path))
        {
            return TableCache[path];
        }
        return new byte[0];
    }

    public void PreloadDataTables()
    {
        Type baseType = typeof(TableML.TableRowFieldParser);
        Type[] types = Assembly.GetExecutingAssembly().GetExportedTypes();
        List<Type> subTypes = new List<Type>();
        List<string> tableNames = new List<string>();
        Type type = null;
        for (int i = 0; i < types.Length; i++)
        {
            type = types[i];
            if (type.IsSubclassOf(baseType))
            {
                subTypes.Add(type);
            }
        }
        for (int i = 0; i < subTypes.Count; i++)
        {
            type = subTypes[i];
            var p = type.GetProperties(BindingFlags.Static | BindingFlags.Public);

            //foreach (var item in p)
            //{
            //    Console.WriteLine("Name: {0}", item.Name);
            //}

            //foreach (FieldInfo field in type.GetFields())
            //{
            //    Console.WriteLine("Field: {0}, Value:{1}", field.Name, field.GetValue(obj));
            //}
        }
    }
    /// <summary>
    /// 给过来的直接是prefab 
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="path">资源相对路径</param>
    /// <param name="callback">加载成功的回调，成功了拿到的是Prefab</param>
    /// <param name="failure">失败的通知</param>
    /// <param name="userData">用户自定义数据数组</param>
    static void Load<T>(string path, Action<string, object[], T> callback, Action<string, object[]> failure, params object[] userData) where T : Object
    {
        AssetLoader assetLoader;
        if (dicAssetLoader.ContainsKey(path))
            assetLoader = dicAssetLoader[path];
        else
        {
            assetLoader = new AssetLoader(path);
            dicAssetLoader.Add(path, assetLoader);
        }
        assetLoader.AddLoadRequest(new LoadRequest<T>(callback, failure, userData));
        helper.StartCoroutine(assetLoader.Load());

    }

    public static void LoadTexture(string path, Action<string, object[], Texture2D> callback, Action<string, object[]> failure, params object[] userData)
    {
        Load<Texture2D>("UITexture/" + path + ".png", callback, failure, userData);
    }
    public static void LoadGameObject(string path, Action<string, object[], GameObject> callback, Action<string, object[]> failure, params object[] userData)
    {
        Load<GameObject>("Prefabs/" + path + ".prefab", (str, args, go) => callback(str, args, GameObject.Instantiate(go)), failure, userData);
    }
    public static void LoadDataTable(string path, Action<string, object[], TextAsset> callback, Action<string, object[]> failure, params object[] userData)
    {
        Load<TextAsset>("DataTable/" + path + ".txt", callback, failure, userData);
    }
    /// <summary>
    /// 场景需要专门去处理，暂不管
    /// </summary>
    public static void LoadScene(string path, Action<string, object[], TextAsset> callback, Action<string, object[]> failure, params object[] userData)
    {
        //Load<Scene>
    }
}
public abstract class LoadRequest
{
    public abstract void LoadSuccess(AssetLoader loader);
    public abstract void LoadFailed(AssetLoader loader);
}
public class LoadRequest<T> : LoadRequest where T : Object
{
    Action<string, object[], T> callback;
    Action<string, object[]> failure;
    object[] userData;
    public LoadRequest(Action<string, object[], T> callback, Action<string, object[]> failure, params object[] userData)
    {
        this.userData = userData;
        this.callback = callback;
        this.failure = failure;
    }
    public override void LoadFailed(AssetLoader loader)
    {
        if (failure != null)
        {
            failure(loader.AssetPath, userData);
        }
    }

    public override void LoadSuccess(AssetLoader loader)
    {
        T prefab = loader.GetAsset<T>();
        if (prefab != null)
        {
            if (callback != null)
            {
                callback(loader.AssetPath, userData, prefab);
            }
            return;
        }
        Debug.LogError("Cannot Find this type asset at " + loader.FullPath + "! [" + typeof(T).ToString() + "]");
        if (failure != null)
        {
            failure(loader.AssetPath, userData);
        }
    }
}

public class AssetLoader
{
    public string AssetPath { get; private set; }
    public string FullPath { get; private set; }
    public AssetLoadState LoadState { get; private set; }
    public AssetBundle assetBundle { get; private set; }
    public WWW www { get; private set; }
    public float progress { get; private set; }

    Queue<LoadRequest> requests = new Queue<LoadRequest>();

    protected Object[] assets;
    public AssetLoader(string assetPath)
    {
        AssetPath = assetPath;
        if (ResourceManager.EditorMode)
            FullPath = "Assets/Main/BundleEditor/" + AssetPath;
        else
            FullPath = GetRemotePath(Application.streamingAssetsPath, AssetPath);

        LoadState = AssetLoadState.None;
    }

    public void AddLoadRequest(LoadRequest request)
    {
        requests.Enqueue(request);
    }
    public IEnumerator Load()
    {
        if (LoadState == AssetLoadState.Loaded)
        {
            while (requests.Count > 0)
            {
                requests.Dequeue().LoadSuccess(this);
            }
            yield break;
        }
        //已经释放或加载失败，重新开始加载
        if (LoadState != AssetLoadState.LoadFail || LoadState != AssetLoadState.Realsed)
            LoadState = AssetLoadState.None;
        //已经开始加载
        if (LoadState != AssetLoadState.None)
        {
            yield break;
        }
        LoadState = AssetLoadState.Start;
#if UNITY_EDITOR
        if (ResourceManager.EditorMode)
        {
            UnityEngine.Object asset = AssetDatabase.LoadMainAssetAtPath(FullPath);
            if (asset == null)
            {
                Debug.LogError("Get asset failure!+ " + FullPath);
                LoadState = AssetLoadState.LoadFail;
                while (requests.Count > 0)
                {
                    requests.Dequeue().LoadFailed(this);
                }
                yield break;
            }
            assets = new Object[] { asset };
        }
        else
#endif
        {
            www = new WWW(FullPath);
            LoadState = AssetLoadState.Loading;
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError("Download asset failure!+ " + www.url);
                LoadState = AssetLoadState.LoadFail;
                while (requests.Count > 0)
                {
                    requests.Dequeue().LoadFailed(this);
                }
                yield break;
            }
            assetBundle = www.assetBundle;
            if (assetBundle == null)
            {
                Debug.LogError("Get asset failure!+ " + www.url);
                LoadState = AssetLoadState.LoadFail;
                while (requests.Count > 0)
                {
                    requests.Dequeue().LoadFailed(this);
                }
                yield break;
            }
            //加载场景使用
            AssetBundleRequest bundleRequest = www.assetBundle.LoadAllAssetsAsync();
            while (bundleRequest.isDone == false)
            {
                yield return null;
            }
            assets = bundleRequest.allAssets;
        }
        LoadState = AssetLoadState.Loaded;
        while (requests.Count > 0)
        {
            requests.Dequeue().LoadSuccess(this);
        }
    }

    public T GetAsset<T>() where T : Object
    {
        if (LoadState == AssetLoadState.Loaded)
        {
            for (int i = 0; i < assets.Length; i++)
            {
                if (assets[i] is T)
                {
                    return assets[i] as T;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// 获取规范的路径。
    /// </summary>
    /// <param name="path">要规范的路径。</param>
    /// <returns>规范的路径。</returns>
    public static string GetRegularPath(string path)
    {
        if (path == null)
        {
            return null;
        }

        return path.Replace('\\', '/');
    }

    /// <summary>
    /// 获取连接后的路径。
    /// </summary>
    /// <param name="path">路径片段。</param>
    /// <returns>连接后的路径。</returns>
    public static string GetCombinePath(params string[] path)
    {
        if (path == null || path.Length < 1)
        {
            return null;
        }

        string combinePath = path[0];
        for (int i = 1; i < path.Length; i++)
        {
            combinePath = System.IO.Path.Combine(combinePath, path[i]);
        }

        return GetRegularPath(combinePath);
    }

    /// <summary>
    /// 获取远程格式的路径（带有file:// 或 http:// 前缀）。
    /// </summary>
    /// <param name="path">原始路径。</param>
    /// <returns>远程格式路径。</returns>
    public static string GetRemotePath(params string[] path)
    {
        string combinePath = GetCombinePath(path);
        if (combinePath == null)
        {
            return null;
        }

        return combinePath.Contains("://") ? combinePath : ("file:///" + combinePath).Replace("file:////", "file:///");
    }
}
public enum AssetLoadState
{
    None = 0,
    Start,
    Loading,
    LoadFail,
    Loaded,
    Realsed,
}

