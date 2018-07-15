using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(ResourceManagerHelper))]
public class ResourceManagerHelperEditor : Editor
{



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        foreach (var item in AssetLoader.DicAssetLoader)
        {
            GUILayout.BeginHorizontal();
            if (item.Value.LoadState == AssetLoadState.Loaded)
            {
                GUI.color = Color.white;
            }
            else if (item.Value.LoadState == AssetLoadState.LoadFail || item.Value.LoadState == AssetLoadState.LoadDenpendenceFail)
            {
                GUI.color = Color.red;
            }
            else if (item.Value.LoadState == AssetLoadState.Realsed)
            {
                GUI.color = Color.yellow;
            }
            else
            {
                GUI.color = Color.blue;
            }

            GUILayout.Label(item.Value.AssetPath.Replace(ResourceManager.BUNDLE_SUFFIX, "") + "  [" + item.Value.LoadState.ToString() + "](" + item.Value.RefrenceCount + ")");
            GUI.color = Color.white;
            GUILayout.EndHorizontal();
        }
    }
}
