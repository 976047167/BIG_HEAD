using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : UIFormBase
{
    GameObject btnKaku;
    GameObject btnBattleSettings;
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        btnKaku = transform.Find("btnKaku").gameObject;
        UIEventListener.Get(btnKaku).onClick = MenuClick;
        btnBattleSettings = transform.Find("btnBattleSettings").gameObject;
        UIEventListener.Get(btnBattleSettings).onClick = BattleSettingsClick;
    }


    void MenuClick(GameObject btn)
    {
        Game.UI.OpenForm<WND_Kaku>(true);
    }
    void BattleSettingsClick(GameObject btn)
    {
        Game.UI.OpenForm<WND_InstanceSetting>();
    }
}
