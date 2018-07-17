using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAssetHelper : MonoBehaviour
{
    OnAssetDestory destory = null;
    public void Init(OnAssetDestory destory)
    {
        this.destory = destory;
    }
    private void OnDestroy()
    {
        if (destory != null)
            destory();
    }
}
