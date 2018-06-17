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
    private UILabel labAttack;
    private UISprite spAttack;
    private UISprite spSkill;
    private UISprite spSpending;
    private UISprite spItem;
    private UISprite spEquip;


    void Awake()
    {
        btnDestroy = transform.Find("btnDestroy").gameObject;
        icon = transform.Find("CardBg/iconFrame/icon").GetComponent<UITexture>();
        labName = transform.Find("CardBg/labName").GetComponent<UILabel>();
        spSpending = transform.Find("CardBg/describeFrame/spSpending").GetComponent<UISprite>();
        spendingNum = transform.Find("labSpending").GetComponent<UILabel>();
        describle = transform.Find("CardBg/describeFrame/ScrollView/labDescribe").GetComponent<UILabel>();
        spAttack = transform.Find("CardBg/describeFrame/spAttack").GetComponent<UISprite>();
        spSkill = transform.Find("CardBg/describeFrame/spSkill").GetComponent<UISprite>();
        spItem = transform.Find("CardBg/describeFrame/spItem").GetComponent<UISprite>();
        spEquip = transform.Find("CardBg/describeFrame/spEquip").GetComponent<UISprite>();
        labAttack = spAttack.transform.Find("labAttack").GetComponent<UILabel>();

        UIEventListener.Get(btnDestroy).onClick = Exit;

    }
    /// <summary>
    /// 加载
    /// </summary>
    /// <param name="userdata"></param>数组[0]类型，数组[1]id
    // Use this for initialization
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
       object[] args = (object[]) userdata;
        if (args.Length < 2)
        {
            Debug.LogError("WND_ShowCard OnInit : wrong args!");
            return;
        }
        switch ((int)args[0])
        {
            case 0:
                InitCard((int)args[1]);
                break;
             case 1:
                InitBuff((int)args[1]);
                break;
            case 2:
                InitEquip((int)args[1]);
                break;

        }


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
        if (card.Type == 0&& card.ActionTypes[0] ==1)
        {
            labAttack.text = card.ActionParams[0].ToString();
        }
    }

    private void InitBuff(int id)
    {
        BattleBuffTableSetting buff = BattleBuffTableSettings.Get(id);
        icon.Load(buff.IconID);
        labName.text = buff.Name;
        describle.text = buff.Desc;
        spSpending.gameObject.SetActive(false);

    }
    private void InitEquip(int id)
    {
        BattleEquipTableSetting equip = BattleEquipTableSettings.Get(id);
        icon.Load(equip.IconID);
        labName.text = equip.Name;
        describle.text = equip.Desc;
        spSpending.gameObject.SetActive(false);

    }
    private void ShowCardType(int cardType)
    {
        spAttack.gameObject.SetActive(cardType == 0);
        /*
        spEquip.gameObject.SetActive(cardType == 1);
        spSkill.gameObject.SetActive(cardType == 2);
        spItem.gameObject.SetActive(cardType == 3);
        */
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
