using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class ShaderFix : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        Renderer renderer = transform.GetComponent<Renderer>();
        if (renderer!=null&&renderer.material!=null)
        {
            renderer.material.shader = Shader.Find(renderer.material.shader.name);
        }
    }

}
#endif
