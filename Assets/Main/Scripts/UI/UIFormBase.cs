using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIFormBase : MonoBehaviour
{


    public void Init(object userdata)
    {
        OnInit(userdata);
    }
    protected virtual void OnInit(object userdata)
    {

    }

    protected virtual void OnOpen()
    {
         
    }
    
}
