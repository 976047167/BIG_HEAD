using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_ShowCard : UIFormBase
{
    private GameObject btnDestroy;
    private UITexture icon;
    private UILabel labName;
    private UILabel spending;
    private UILabel damage;
    private UILabel describle;
    void Awake()
    {
        btnDestroy = transform.Find("btnDestroy").gameObject;
        icon = transform.Find("CardBg/Icon").GetComponent<UITexture>();
        labName = transform.Find("CardBg/Name").GetComponent<UILabel>();
        spending = transform.Find("CardBg/ExpendCount").GetComponent<UILabel>();
        damage = transform.Find("CardBg/DamageCount").GetComponent<UILabel>();
        describle = transform.Find("CardBg/Describle").GetComponent<UILabel>();
        UIEventListener.Get(btnDestroy).onClick = (GameObject a) => {
            UIModule.Instance.CloseForm<WND_ShowCard>();

        };

    }
    // Use this for initialization
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        InitCard((int)userdata);
    }
    private void InitCard(int id)
    {
        BattleCardTableSetting card =  BattleCardTableSettings.Get(id);
        icon.Load(card.Icon);
        labName.text = card.Name;
        spending.text = ""+ card.Spending;
        describle.text = card.Desc; 
    }

}
