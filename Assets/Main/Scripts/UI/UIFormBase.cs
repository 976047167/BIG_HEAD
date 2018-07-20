using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
/// <summary>
/// 每个子类都要写注释说明，userdata是什么
/// </summary>
public abstract class UIFormBase : MonoBehaviour
{
    public UIFormTableSetting Table { get; private set; }
    private UIFormState state;
    public UIFormState State { get { return state; } }
    private bool isOpen = false;
    public bool IsOpen { get { return isOpen; } }
    /// <summary>
    /// 这个方法不允许调用，uimodule专用
    /// </summary>
    public void Init(UIFormTableSetting table, object userdata)
    {
        Table = table;
        state = UIFormState.Hide;
        OnInit(userdata);
        isOpen = false;
    }
    public void Open()
    {
        isOpen = true;
        gameObject.SetActive(true);
        OnOpen();
        Show();
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
        if (State == UIFormState.Show)
        {
            return;
        }
        state = UIFormState.Show;
        gameObject.SetActive(true);
        OnShow();
    }
    public void Hide()
    {
        if (State == UIFormState.Hide)
        {
            return;
        }
        state = UIFormState.Hide;
        OnHide();
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 因UI规则进行隐藏
    /// </summary>
    public void Cover()
    {
        if (gameObject.activeSelf)
        {
            OnHide();
        }
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 因UI规则进行显示
    /// </summary>
    public void Resume()
    {
        if (gameObject.activeSelf == false && state == UIFormState.Show)
        {
            OnShow();
        }
        gameObject.SetActive(state == UIFormState.Show);
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


