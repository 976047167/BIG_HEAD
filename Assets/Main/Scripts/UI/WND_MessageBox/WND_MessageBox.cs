using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WND_MessageBox : UIFormBase
{


    [SerializeField]
    protected GameObject btnMask;
    [SerializeField]
    protected GameObject btnYes;
    [SerializeField]
    protected GameObject btnNo;
    [SerializeField]
    protected GameObject btnCancel;

    protected MessageBoxType messageBoxType = MessageBoxType.Confirm;

    protected MessageBoxCallback messageBoxCallback = null;

    protected string content = "";

    private void Awake()
    {
        OnInit(null);
    }

    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        btnMask = transform.Find("mask").gameObject;
        btnYes = transform.Find("Yes").gameObject;
        btnNo = transform.Find("NO").gameObject;
        btnCancel = transform.Find("Cancel").gameObject;
        object[] args = (object[])userdata;
        messageBoxType = (MessageBoxType)args[0];
        content = args[1].ToString();
        messageBoxCallback = (MessageBoxCallback)args[2];

    }

    protected override void OnOpen()
    {
        base.OnOpen();
    }

    protected override void OnShow()
    {
        base.OnShow();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    protected override void OnHide()
    {
        base.OnHide();
    }

    protected override void OnClose()
    {
        base.OnClose();
    }


    public delegate void MessageBoxCallback(MessageBoxReturnType returnType);

}
public enum MessageBoxType
{
    Confirm = 0,
    YesNo = 1,
    YesNoCancel = 2,
}
public enum MessageBoxReturnType
{
    Cancel = 0,
    Yes = 1,
    No = 2,
}
