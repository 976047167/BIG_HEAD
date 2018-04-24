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
    static ResourceManagerHelper helper;

    static Dictionary<string, AssetLoader> dicAssetLoader = new Dictionary<string, AssetLoader>();
    public static void Init()
    {
        if (helper == null)
        {
            helper = new GameObject().AddComponent<ResourceManagerHelper>();
        }
    }
    //Dictionary<string,GameObject> 
    public static void Load<T>(string path, object arg, Action<string, object, T> callback, Action<string, object> failure) where T : Object
    {
        //T obj = Resources.Load<T>(path);
        //if (callback != null)
        //{
        //    callback(path, arg, obj);
        //}
        AssetLoader assetLoader;
        if (dicAssetLoader.ContainsKey(path))
            assetLoader = dicAssetLoader[path];
        else
        {
            assetLoader = new AssetLoader(path);
            dicAssetLoader.Add(path, assetLoader);
        }
        assetLoader.AddLoadRequest(new LoadRequest<T>(callback, failure));
        helper.StartCoroutine(assetLoader.Load());

    }

    public static void LoadTexture(string path, object arg, Action<string, object, Texture2D> callback)
    {

        Load<Texture2D>("UITexture/" + path, arg, callback, null);
    }

    static void LoadSuccess(AssetLoader loader)
    {

    }
    static void LoadFailed(AssetLoader loader)
    {

    }
}
public abstract class LoadRequest
{
    public abstract void LoadSuccess(AssetLoader loader);
    public abstract void LoadFailed(AssetLoader loader);
}
public class LoadRequest<T> : LoadRequest where T : Object
{
    Action<string, object, T> callback;
    Action<string, object> failure;
    public LoadRequest(Action<string, object, T> callback, Action<string, object> failure)
    {
        this.callback = callback;
        this.failure = failure;
    }
    public override void LoadFailed(AssetLoader loader)
    {
        throw new NotImplementedException();
    }

    public override void LoadSuccess(AssetLoader loader)
    {
        throw new NotImplementedException();
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
#if UNITY_EDITOR
        FullPath = GetRemotePath(Application.dataPath+"Main/BundleEditor", AssetPath);
#else
        FullPath = GetRemotePath(Application.streamingAssetsPath, AssetPath);
#endif

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

public class ResourceManagerHelper : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        name = "[ResourceManagerHelper]";
    }
}