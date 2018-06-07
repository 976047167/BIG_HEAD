using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIFormBase : MonoBehaviour
{

    /// <summary>
    /// 这个方法不允许调用，uimodule专用
    /// </summary>
    public void Init(object userdata)
    {
        OnInit(userdata);
    }
    /// <summary>
    /// 这个方法不允许调用，uimodule专用
    /// </summary>
    public void Close()
    {
        OnClose();
    }

    protected virtual void OnInit(object userdata)
    {

    }

    protected virtual void OnOpen()
    {
         
    }

    protected virtual void OnClose()
    {

    }
    
}
