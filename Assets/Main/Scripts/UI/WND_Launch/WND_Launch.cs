using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    protected override void OnShow()
    {
        base.OnShow();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        
        if (progress>=100)
        {
            ProcedureManager.ChangeProcedure<Procedure_Login>();
        }
        progress++;
        sliderProgress.value = progress / 100f;
    }
}
