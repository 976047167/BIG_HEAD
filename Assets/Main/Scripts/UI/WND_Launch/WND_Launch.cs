using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using BigHead.Net;

public class WND_Launch : UIFormBase
{
    private UISlider sliderProgress = null;

    float progress = 0;
    protected override void OnClose()
    {
        base.OnClose();
    }

    protected override void OnHide()
    {
        base.OnHide();
    }

    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);

        sliderProgress = transform.Find("Slider").GetComponent<UISlider>();
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        StartCoroutine(InitMgr());

    }
    IEnumerator InitMgr()
    {
        yield return null;
        Game.NetworkManager.Init();
        yield return null;
        I18N.SetLanguage(Game.Instance.language);
        yield return null;
        Game.DataManager.OnInit();
        yield return null;
    }
    protected override void OnShow()
    {
        base.OnShow();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (progress >= 100)
        {
            ProcedureManager.ChangeProcedure<Procedure_Login>();
        }
        progress += 3;
        sliderProgress.value = progress / 100f;
    }
}
