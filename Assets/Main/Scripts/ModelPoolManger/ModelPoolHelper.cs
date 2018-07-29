using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPoolHelper : MonoBehaviour {

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        name = "[ModelPoolHelper]";
    }
}
