using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class UIPromptBox : UIFormBase {
    UITexture icon;
    UILabel describe;

    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        icon = transform.Find("icon").GetComponent<UITexture>();
        describe = transform.Find("describe").GetComponent<UILabel>();
    }

    public void  SetData(int id)
    {
       //PromptTableSetting prompt =   PromptTableSettings.Get(id);
       //icon.Load(prompt.ImagePath) ;
       // describe.text = prompt.Describe;
    }
}
