using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using KEngine.Modules;
using UnityEngine.SceneManagement;
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

    public const string BUNDLE_SUFFIX = ".bundle";
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
        //if (!EditorMode)
        {
            SettingModule.CustomLoadSettingString = LoadSettingFromCache;
            TablePreloaded = false;
            PreloadDataTables();
        }
    }
    public static bool TablePreloaded { get; private set; }
    static int TableCount;
    //缓存表格数据
    static Dictionary<string, string> TableCache = new Dictionary<string, string>();

    static string LoadSettingFromCache(string path)
    {
        if (TableCache.ContainsKey(path))
        {
            return TableCache[path];
        }
        return string.Empty;
    }

    public static void PreloadDataTables()
    {
        Type baseType = typeof(IReloadableSettings);
        Type[] types = Assembly.GetExecutingAssembly().GetExportedTypes();
        List<Type> subTypes = new List<Type>();
        List<string> tableNames = new List<string>();
        Type type = null;
        for (int i = 0; i < types.Length; i++)
        {
            type = types[i];
            if (baseType != type && baseType.IsAssignableFrom(type))
            {
                subTypes.Add(type);
            }
        }
        TableCount = subTypes.Count;
        for (int i = 0; i < subTypes.Count; i++)
        {
            type = subTypes[i];
            FieldInfo fieldInfo = type.GetField("TabFilePaths", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            string[] paths = fieldInfo.GetValue(null) as string[];
            Debug.Log("preload table:" + paths[0]);
            for (int j = 0; j < paths.Length; j++)
            {
                string tableName = paths[j].Split('.')[0];
                LoadDataTable(tableName,
                    (str, userData, data) =>
                    {
                        TableCache.Add(userData[0] as string, data);
                        if (TableCount == TableCache.Count)
                            TablePreloaded = true;
                    },
                    (str, userData) => { Debug.LogError(str); },
                    paths[j]);
            }
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
    static void Load<T>(string path, AssetType assetType, Action<string, object[], T> callback, Action<string, object[]> failure, params object[] userData) where T : Object
    {
        AssetLoader assetLoader;
        if (dicAssetLoader.ContainsKey(path))
            assetLoader = dicAssetLoader[path];
        else
        {
            assetLoader = new AssetLoader(path, assetType);
            dicAssetLoader.Add(path, assetLoader);
        }
        if (assetType == AssetType.UnityAsset)
        {
            assetLoader.AddLoadRequest(new LoadRequest<T>(callback, failure, userData));
        }
        helper.StartCoroutine(assetLoader.Load());
    }

    public static void LoadTexture(string path, Action<string, object[], Texture2D> callback, Action<string, object[]> failure, params object[] userData)
    {
        Load<Texture2D>("UITexture/" + path + (EditorMode ? ".png" : BUNDLE_SUFFIX), AssetType.UnityAsset, callback, failure, userData);
    }
    public static void LoadGameObject(string path, Action<string, object[], GameObject> callback, Action<string, object[]> failure, params object[] userData)
    {
        Load<GameObject>("Prefabs/" + path + (EditorMode ? ".prefab" : BUNDLE_SUFFIX), AssetType.UnityAsset, (str, args, go) => callback(str, args, GameObject.Instantiate(go)), failure, userData);
    }
    public static void LoadDataTable(string path, Action<string, object[], string> callback, Action<string, object[]> failure, params object[] userData)
    {
        path = "DataTable/" + path + (EditorMode ? ".txt" : BUNDLE_SUFFIX);
        Load<TextAsset>(path, AssetType.UnityAsset, (str, userdata, ta) => { callback(str, userData, ta.text); }, failure, userData);
        //AssetLoader assetLoader;
        //if (dicAssetLoader.ContainsKey(path))
        //    assetLoader = dicAssetLoader[path];
        //else
        //{
        //    assetLoader = new AssetLoader(path, AssetType.Byte);
        //    dicAssetLoader.Add(path, assetLoader);
        //}
        //assetLoader.AddLoadRequest(new LoadRequestBytes(callback, failure, userData));
        //helper.StartCoroutine(assetLoader.Load());
    }
    /// <summary>
    /// 场景需要专门去处理，暂不管
    /// </summary>
    public static void LoadScene(string path, Action<string, object[]> callback, Action<string, object[]> failure, params object[] userData)
    {
        if (EditorMode)
        {
            SceneManager.LoadScene(path);
        }

        path = "Scenes/" + path + (EditorMode ? ".scene" : BUNDLE_SUFFIX);
        AssetLoader assetLoader;
        if (dicAssetLoader.ContainsKey(path))
            assetLoader = dicAssetLoader[path];
        else
        {
            assetLoader = new AssetLoader(path, AssetType.Scene);
            dicAssetLoader.Add(path, assetLoader);
        }
        assetLoader.AddLoadRequest(new LoadRequestScene(callback, failure, userData));
        helper.StartCoroutine(assetLoader.Load());
    }
    public static string GetPlatformName()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.OSXDashboardPlayer:
                return "OSX";
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                return "Windows";
            case RuntimePlatform.IPhonePlayer:
                return "iOS";
            case RuntimePlatform.Android:
                return "Android";
            case RuntimePlatform.LinuxPlayer:
            case RuntimePlatform.LinuxEditor:
                return "Linux";
            default:
                return "";
        }
    }
}
public interface ILoadRequest
{
    void LoadSuccess(AssetLoader loader);
    void LoadFailed(AssetLoader loader);
}
public class LoadRequestBytes : ILoadRequest
{
    protected Action<string, object[], byte[]> callback;
    protected Action<string, object[]> failure;
    protected object[] userData;
    public LoadRequestBytes(Action<string, object[], byte[]> callback, Action<string, object[]> failure, params object[] userData)
    {
        this.userData = userData;
        this.callback = callback;
        this.failure = failure;
    }
    public void LoadSuccess(AssetLoader loader)
    {
        byte[] data = loader.GetBytes();
        if (callback != null)
        {
            callback(loader.AssetPath, userData, data);
        }
    }
    public void LoadFailed(AssetLoader loader)
    {
        if (failure != null)
        {
            failure(loader.AssetPath, userData);
        }
    }
}
public class LoadRequestScene : ILoadRequest
{
    protected Action<string, object[]> callback;
    protected Action<string, object[]> failure;
    public LoadRequestScene(Action<string, object[]> callback, Action<string, object[]> failure, params object[] userData)
    {
        this.userData = userData;
        this.callback = callback;
        this.failure = failure;
    }
    protected object[] userData;
    public void LoadSuccess(AssetLoader loader)
    {
        loader.ShowLoadedScene();
        if (callback != null)
        {
            callback(loader.AssetPath, userData);
        }
    }
    public void LoadFailed(AssetLoader loader)
    {
        if (failure != null)
        {
            failure(loader.AssetPath, userData);
        }
    }
}
public class LoadRequest<T> : ILoadRequest where T : Object
{
    protected Action<string, object[], T> callback;
    protected Action<string, object[]> failure;
    protected object[] userData;
    public LoadRequest(Action<string, object[], T> callback, Action<string, object[]> failure, params object[] userData)
    {
        this.userData = userData;
        this.callback = callback;
        this.failure = failure;
    }
    public void LoadFailed(AssetLoader loader)
    {
        if (failure != null)
        {
            failure(loader.AssetPath, userData);
        }
    }

    public void LoadSuccess(AssetLoader loader)
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
    public AssetType AssetType { get; private set; }
    public string FullPath { get; private set; }
    public AssetLoadState LoadState { get; private set; }
    public AssetBundle assetBundle { get; private set; }
    public WWW www { get; private set; }
    public float progress { get; private set; }

    Queue<ILoadRequest> requests = new Queue<ILoadRequest>();

    protected Object[] assets;
    protected byte[] bytes;
    protected AssetBundleRequest assetBundleRequest;
    public AssetLoader(string assetPath, AssetType assetType)
    {
        AssetPath = assetPath;
        this.AssetType = assetType;
        if (ResourceManager.EditorMode)
            FullPath = "Assets/Main/BundleEditor/" + AssetPath;
        else
            FullPath = GetRemotePath(Application.streamingAssetsPath, ResourceManager.GetPlatformName().ToLower(), AssetPath.ToLower());

        LoadState = AssetLoadState.None;
    }

    public void AddLoadRequest(ILoadRequest request)
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
            if (AssetType == AssetType.Byte)
            {
                bytes = (asset as TextAsset).bytes;
            }
            else if (AssetType == AssetType.UnityAsset)
            {
                assets = new Object[] { asset };
            }
            //else if (AssetType == AssetType.Scene)
            //{
            //    //加载场景使用
            //    AssetBundleRequest bundleRequest = www.assetBundle.LoadAllAssetsAsync();
            //    assetBundleRequest = bundleRequest;
            //    bundleRequest.allowSceneActivation = false;
            //    while (bundleRequest.isDone == false)
            //    {
            //        yield return null;
            //    }
            //}

        }
        else
#endif
        {
            www = new WWW(FullPath);
            LoadState = AssetLoadState.Loading;
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError("Download asset failure!+ " + www.url + "\n" + www.error);
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
            if (AssetType == AssetType.Byte)
            {
                bytes = www.bytes;
            }
            else if (AssetType == AssetType.UnityAsset)
            {
                assets = www.assetBundle.LoadAllAssets();
            }
            else if (AssetType == AssetType.Scene)
            {
                //加载场景使用
                AssetBundleRequest bundleRequest = www.assetBundle.LoadAllAssetsAsync();
                assetBundleRequest = bundleRequest;
                bundleRequest.allowSceneActivation = false;
                while (bundleRequest.isDone == false)
                {
                    yield return null;
                }
            }
        }
        LoadState = AssetLoadState.Loaded;
        while (requests.Count > 0)
        {
            requests.Dequeue().LoadSuccess(this);
        }
    }

    public T GetAsset<T>() where T : Object
    {
        if (AssetType == AssetType.UnityAsset && LoadState == AssetLoadState.Loaded)
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
    public byte[] GetBytes()
    {
        if (AssetType == AssetType.Byte && LoadState == AssetLoadState.Loaded)
        {
            return bytes;
        }
        return null;
    }
    public void ShowLoadedScene()
    {
        if (AssetType == AssetType.Scene && LoadState == AssetLoadState.Loaded)
        {
            assetBundleRequest.allowSceneActivation = true;
            assetBundleRequest = null;
        }
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
public enum AssetType
{
    UnityAsset = 0,
    Byte,
    Scene,
}
