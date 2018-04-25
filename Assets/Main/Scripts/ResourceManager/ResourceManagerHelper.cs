using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManagerHelper : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        name = "[ResourceManagerHelper]";
    }
}
