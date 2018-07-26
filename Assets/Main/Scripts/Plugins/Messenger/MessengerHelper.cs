using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MessengerHelper : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void OnDisable()
    {
        Messenger.Cleanup();
    }
    private void Update()
    {
        Messenger.Update();
    }
}
