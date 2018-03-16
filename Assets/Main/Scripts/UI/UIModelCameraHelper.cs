using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModelCameraHelper : MonoBehaviour
{

    private void Awake()
    {
        if (UIModule.Instance.SetUICamera(this))
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
