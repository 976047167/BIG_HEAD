using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WND_BattleSetting : UIFormBase
{
    [Header("窗口控件")]
    [SerializeField]
    GameObject btnBg;
    [SerializeField]
    GameObject btnSoundStateOpen;
    [SerializeField]
    GameObject btnSoundStateClose;
    [SerializeField]
    GameObject btnEscapeBattle;

    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        btnBg = transform.Find("bg").gameObject;
        btnSoundStateOpen = transform.Find("contentGrid/sound/stateOpen").gameObject;
        btnSoundStateClose = transform.Find("contentGrid/sound/stateClose").gameObject;

        btnEscapeBattle = transform.Find("contentGrid/exitBattle/Sprite").gameObject;
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

    protected void OnClick_btnBg(GameObject go)
    {

    }
    protected void OnClick_btnSound(GameObject go)
    {

    }
    protected void OnClick_btnExitBattle(GameObject go)
    {

    }

}
