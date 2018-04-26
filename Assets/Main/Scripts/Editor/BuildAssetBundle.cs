using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildAssetBundleEditor : MonoBehaviour
{
    /*
     下面是被指定的键（它们也可以组合起来使用）：
    %-CTRL 在Windows / CMD在OSX
    # -Shift
    & -Alt
    LEFT/RIGHT/UP/DOWN-光标键
    F1…F12
    HOME,END,PGUP,PDDN
    字母键不是key-sequence的一部分，要让字母键被添加到key-sequence中必须在前面加上下划线（例如：_g对应于快捷键”G”）。
    */


    public static string sourcePath = Application.dataPath + "/Main/BundleEditor";
    const string AssetBundlesOutputPath = "Assets/OutPut";
    [MenuItem("Tools/AssetBundle/Build AssetBundle %&B")]
    static void BuildAssetBundle()
    {
        ClearAssetBundlesName();

        Pack(sourcePath);

        string outputPath = Path.Combine(AssetBundlesOutputPath, Platform.GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget));
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        //根据BuildSetting里面所激活的平台进行打包 设置过AssetBundleName的都会进行打包  
        BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);

        AssetDatabase.Refresh();

        Debug.Log("打包完成");
    }



    /// <summary>  
    /// 清除之前设置过的AssetBundleName，避免产生不必要的资源也打包  
    /// 之前说过，只要设置了AssetBundleName的，都会进行打包，不论在什么目录下  
    /// </summary>  
    [MenuItem("Tools/AssetBundle/Clear AssetBundlesName")]
    static void ClearAssetBundlesName()
    {
        string[] oldAssetBundleNames = AssetDatabase.GetAllAssetBundleNames();
        int length = oldAssetBundleNames.Length;
        Debug.Log(length);
        //string[] oldAssetBundleNames = new string[length];
        //for (int i = 0; i < length; i++)
        //{
        //    oldAssetBundleNames[i] = AssetDatabase.GetAllAssetBundleNames()[i];
        //}

        for (int j = 0; j < oldAssetBundleNames.Length; j++)
        {
            AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[j], true);
        }
        length = AssetDatabase.GetAllAssetBundleNames().Length;
        Debug.Log(length);
    }

    static void Pack(string source)
    {
        //Debug.Log("Pack source " + source);  
        DirectoryInfo folder = new DirectoryInfo(source);
        FileSystemInfo[] files = folder.GetFileSystemInfos();
        int length = files.Length;
        for (int i = 0; i < length; i++)
        {
            if (files[i] is DirectoryInfo)
            {
                Pack(files[i].FullName);
            }
            else
            {
                if (!files[i].Name.StartsWith(".") && !files[i].Name.EndsWith(".meta"))
                {
                    fileWithDepends(files[i].FullName);
                }
            }
        }
    }
    //设置要打包的文件  
    static void fileWithDepends(string source)
    {

        string _source = Replace(source);
        string _assetPath = "Assets" + _source.Substring(Application.dataPath.Length);
        Debug.Log("file source : " + source + "\n" + _assetPath);
        //依赖项咱不单独打包
        AssetImporter assetImporter = AssetImporter.GetAtPath(_assetPath);
        assetImporter.assetBundleName = source.Substring(sourcePath.Length + 1);
        //自动获取依赖项并给其资源设置AssetBundleName  
        //string[] dps = AssetDatabase.GetDependencies(_assetPath);
        //foreach (var dp in dps)
        //{
        //    Debug.Log("dp " + dp);
        //    if (dp.EndsWith(".cs"))
        //        continue;
        //    AssetImporter assetImporter = AssetImporter.GetAtPath(dp);
        //    string pathTmp = dp.Substring("Assets".Length + 1);
        //    string assetName = pathTmp.Substring(pathTmp.IndexOf("/") + 1);
        //    assetName = assetName.Replace(Path.GetExtension(assetName), ".data");
        //    Debug.Log(assetName);
        //    assetImporter.assetBundleName = assetName;
        //}

    }

    //设置要打包的文件  
    static void file(string source)
    {
        Debug.Log("file source " + source);
        string _source = Replace(source);
        string _assetPath = "Assets" + _source.Substring(Application.dataPath.Length);
        string _assetPath2 = _source.Substring(Application.dataPath.Length + 1);
        //Debug.Log (_assetPath);  

        //在代码中给资源设置AssetBundleName  
        AssetImporter assetImporter = AssetImporter.GetAtPath(_assetPath);
        string[] dps = AssetDatabase.GetDependencies(_assetPath);
        foreach (var dp in dps)
        {
            Debug.Log("dp " + dp);
        }
        string assetName = _assetPath2.Substring(_assetPath2.IndexOf("/") + 1);
        assetName = assetName.Replace(Path.GetExtension(assetName), ".unity3d");
        Debug.Log(assetName);
        assetImporter.assetBundleName = assetName;
    }

    static string Replace(string s)
    {
        return s.Replace("\\", "/");
    }
}

public class Platform
{
    public static string GetPlatformFolder(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.Android:
                return "Android";
            case BuildTarget.iOS:
                return "IOS";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "Windows";
            case BuildTarget.StandaloneOSXIntel:
            case BuildTarget.StandaloneOSXIntel64:
            case BuildTarget.StandaloneOSXUniversal:
                return "OSX";
            default:
                return null;
        }
    }
}

