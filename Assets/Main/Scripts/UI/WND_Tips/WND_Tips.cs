using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WND_Tips : UIFormBase
{
    UIPanel panel;
    UILabel lblTips;
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        lblTips = transform.Find("Info").GetComponent<UILabel>();
        panel = transform.GetComponent<UIPanel>();

        object[] args = (object[])userdata;
        float duration = (float)args[0];
        float time = (float)args[1];
        string tips = (string)args[2];

        lblTips.text = tips;


        DOTween.To(() => { return new Vector3(0f, -150f, 0f); }, (v3) => { transform.localPosition = v3; }, Vector3.zero, duration)
            .Play();
        DOTween.To(() => { return 0f; }, (alpha) => { panel.alpha = alpha; }, 1f, duration)
            .Play();
        DOTween.To(() => { return Vector3.zero; }, (v3) => { transform.localPosition = v3; }, new Vector3(0f, 150f, 0f), duration)
            .SetDelay(time + duration)
            .OnComplete(() => { Game.UI.CloseForm(this); });
        DOTween.To(() => { return 1f; }, (alpha) => { panel.alpha = alpha; }, 0f, duration)
            .SetDelay(time + duration);
    }



}
