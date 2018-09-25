using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerHelper : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        name = "[SoundManagerHelper]";
    }
}
