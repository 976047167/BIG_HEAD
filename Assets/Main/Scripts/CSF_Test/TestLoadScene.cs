using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLoadScene : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {
        SceneManager.LoadScene("Main/BundleEditor/Scenes/Lobby");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
