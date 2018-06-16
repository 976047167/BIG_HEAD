using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_ShowCard : UIFormBase
{
    private GameObject btnDestroy;
    private UITexture icon;
    private UILabel labName;
    private UILabel spendingNum;
    private UILabel describle;
    private UISprite spAttack;
    private UISprite spSkill;
    private UISprite spItem;
    private UISprite spEquip;


    void Awake()
    {
        btnDestroy = transform.Find("btnDestroy").gameObject;
        icon = transform.Find("CardBg/iconFrame/icon").GetComponent<UITexture>();
        labName = transform.Find("CardBg/labName").GetComponent<UILabel>();
        spendingNum = transform.Find("CardBg/describeFrame/spSpending/labSpending").GetComponent<UILabel>();
        describle = transform.Find("CardBg/describeFrame/ScrollView/labDescribe").GetComponent<UILabel>();
        spAttack = transform.Find("CardBg/describeFrame/spAttack").GetComponent<UISprite>();
        spSkill = transform.Find("CardBg/describeFrame/spSkill").GetComponent<UISprite>();
        spItem = transform.Find("CardBg/describeFrame/spItem").GetComponent<UISprite>();
        spEquip = transform.Find("CardBg/describeFrame/spEquip").GetComponent<UISprite>();


        UIEventListener.Get(btnDestroy).onClick = Exit;

    }
    // Use this for initialization
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        InitCard((int)userdata);
    }
    /// <summary>
    /// 加载卡牌
    /// </summary>
    /// <param name="id"></param>
    private void InitCard(int id)
    {
        BattleCardTableSetting card =  BattleCardTableSettings.Get(id);
        icon.Load(card.ShowID);
        labName.text = card.Name;
        spendingNum.text = card.Spending.ToString();
        describle.text = card.Desc;
        ShowCardType(card.Type);
    }
    private void ShowCardType(int cardType)
    {
        spAttack.gameObject.SetActive(cardType == 0);
        spEquip.gameObject.SetActive(cardType == 1);
        spSkill.gameObject.SetActive(cardType == 2);
        spItem.gameObject.SetActive(cardType == 3);
    }


    /// <summary>
    /// 退出
    /// </summary>
    /// <param name="obj"></param>
    private void Exit(GameObject obj)
    {
        UIModule.Instance.CloseForm<WND_ShowCard>();
    }
}
