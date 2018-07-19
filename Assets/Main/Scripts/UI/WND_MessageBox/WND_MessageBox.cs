using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// 请使用UIUtility.ShowMessageBox来打开
/// </summary>
public class WND_MessageBox : UIFormBase
{

    protected UILabel lblContent;
    [SerializeField]
    protected GameObject btnMask;
    [SerializeField]
    protected UISprite spMask;
    [SerializeField]
    protected GameObject btnYes;
    [SerializeField]
    protected GameObject btnNo;
    [SerializeField]
    protected GameObject btnCancel;
    [SerializeField]
    protected GameObject aniRoot;

    protected MessageBoxType messageBoxType = MessageBoxType.Confirm;

    protected MessageBoxCallback messageBoxCallback = null;

    protected string content = "";

    //private void Awake()
    //{
    //    OnInit(null);
    //}

    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        lblContent = transform.Find("aniRoot/bg/content").GetComponent<UILabel>();
        btnMask = transform.Find("mask").gameObject;
        spMask = btnMask.GetComponent<UISprite>();
        btnYes = transform.Find("aniRoot/Yes").gameObject;
        btnNo = transform.Find("aniRoot/NO").gameObject;
        btnCancel = transform.Find("aniRoot/Cancel").gameObject;
        aniRoot = transform.Find("aniRoot").gameObject;
        object[] args = (object[])userdata;
        messageBoxType = (MessageBoxType)args[0];
        content = args[1].ToString();
        messageBoxCallback = (MessageBoxCallback)args[2];
        UIEventListener.Get(btnMask).onClick = OnClick_Mask;
        UIEventListener.Get(btnYes).onClick = OnClick_Yes;
        UIEventListener.Get(btnNo).onClick = OnClick_No;
        UIEventListener.Get(btnCancel).onClick = OnClick_Cancel;
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        lblContent.text = content;
        switch (messageBoxType)
        {
            case MessageBoxType.Confirm:
                btnYes.SetActive(true);
                btnNo.SetActive(false);
                btnCancel.SetActive(false);
                break;
            case MessageBoxType.YesNo:
                btnYes.SetActive(true);
                btnNo.SetActive(true);
                btnCancel.SetActive(false);
                break;
            case MessageBoxType.YesNoCancel:
                btnYes.SetActive(true);
                btnNo.SetActive(true);
                btnCancel.SetActive(true);
                break;
            default:
                break;
        }
        DOTween.To(() => Vector3.zero, (scale) => aniRoot.transform.localScale = scale, Vector3.one, 0.3f);
        DOTween.To(() => 0.01f, (scale) => spMask.alpha = scale, 0.3f, 0.3f);
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

    protected void OnClick_Mask(GameObject go)
    {
        //if (messageBoxCallback != null)
        //{
        //    messageBoxCallback(MessageBoxReturnType.Cancel);
        //}
        //CloseUI();
    }
    protected void OnClick_Yes(GameObject go)
    {
        if (messageBoxCallback != null)
        {
            messageBoxCallback(MessageBoxReturnType.Yes);
        }
        CloseUI();
    }
    protected void OnClick_No(GameObject go)
    {
        if (messageBoxCallback != null)
        {
            messageBoxCallback(MessageBoxReturnType.No);
        }
        CloseUI();
    }
    protected void OnClick_Cancel(GameObject go)
    {
        if (messageBoxCallback != null)
        {
            messageBoxCallback(MessageBoxReturnType.Cancel);
        }
        CloseUI();
    }

    void CloseUI()
    {
        DOTween.To(() => Vector3.one, (scale) => aniRoot.transform.localScale = scale, Vector3.zero, 0.3f)
            .OnComplete(() => { Game.UI.CloseForm(this); });
        DOTween.To(() => 0.3f, (scale) => spMask.alpha = scale, 0.01f, 0.3f);
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
