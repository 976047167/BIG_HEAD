using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 每个子类都要写注释说明，userdata是什么
/// </summary>
public abstract class UIFormBase : MonoBehaviour
{

    /// <summary>
    /// 这个方法不允许调用，uimodule专用
    /// </summary>
    public void Init(object userdata)
    {
        OnInit(userdata);
        OnOpen();
        OnShow();
    }
    /// <summary>
    /// 这个方法不允许调用，uimodule专用
    /// </summary>
    public void Close()
    {
        OnHide();
        OnClose();
    }
    /// <summary>
    /// 这个方法不允许调用，uimodule专用
    /// </summary>
    public void FormUpdate()
    {
        OnUpdate();
    }

    public void Show()
    {
        if (gameObject.activeInHierarchy)
        {
            return;
        }
        gameObject.SetActive(true);
        OnShow();
    }
    public void Hide()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        OnHide();
        gameObject.SetActive(false);
    }
    #region override
    /// <summary>
    /// 控件获取，本地化，注册事件
    /// </summary>
    /// <param name="userdata"></param>
    protected virtual void OnInit(object userdata)
    {

    }
    /// <summary>
    /// 打开后的窗口操作
    /// </summary>
    protected virtual void OnOpen()
    {

    }
    /// <summary>
    /// 显示时发生，第一次打开也触发
    /// </summary>
    protected virtual void OnShow()
    {

    }
    /// <summary>
    /// 每帧更新的操作，不推荐使用每帧更新来完成逻辑
    /// </summary>
    protected virtual void OnUpdate()
    {

    }
    /// <summary>
    /// 隐藏时发生，关闭前也会执行
    /// </summary>
    protected virtual void OnHide()
    {

    }
    /// <summary>
    /// 关闭时的操作,注销事件
    /// </summary>
    protected virtual void OnClose()
    {

    }
    #endregion


}


