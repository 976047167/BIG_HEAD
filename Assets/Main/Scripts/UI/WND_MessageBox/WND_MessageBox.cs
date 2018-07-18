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
}
