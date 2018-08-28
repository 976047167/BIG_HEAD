using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WND_BattleResult : UIFormBase
{
    ResultState result = ResultState.Win;

    GameObject goMask = null;
    UITexture textureTitle = null;
    UILabel lblResult = null;

    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        goMask = transform.Find("mask").gameObject;
        textureTitle = transform.Find("title").GetComponent<UITexture>();
        lblResult = transform.Find("result").GetComponent<UILabel>();

        UIEventListener.Get(goMask).onClick = OnClick_CloseUI;
        result = (ResultState)userdata;

    }

    protected override void OnOpen()
    {
        base.OnOpen();
        switch (result)
        {
            case ResultState.Win:
                lblResult.text = I18N.Get(1003009);
                lblResult.color = new Color32(255, 82, 79, 255);
                break;
            case ResultState.Lose:
                lblResult.text = I18N.Get(1003010);
                lblResult.color = new Color32(128, 128, 128, 255);
                break;
            case ResultState.MeEscape:
                lblResult.text = I18N.Get(1003010);
                lblResult.color = new Color32(128, 128, 128, 255);
                break;
            case ResultState.OppEscape:
                lblResult.text = I18N.Get(1003009);
                lblResult.color = new Color32(255, 82, 79, 255);
                break;
            default:
                break;
        }
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

    private void OnClick_CloseUI(GameObject go)
    {
        if (Game.BattleManager.RewardData!=null)
        {
            Game.UI.OpenForm<WND_Reward>(Game.BattleManager.RewardData);
        }
        else
        {
            Game.UI.CloseForm<UIBattleForm>();
        }
        Game.UI.CloseForm(this);
    }

    enum ResultState : int
    {
        Win = 0,
        Lose,
        MeEscape,
        OppEscape,
    }
}
