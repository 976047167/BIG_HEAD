using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class UINormalCard : MonoBehaviour
{
    public int CardId { get; private set; }
    public BattleCardTableSetting CardData { get; private set; }
    private int cardNum;
    private UILabel lblName;
    private UITexture leftIcon;
    private UITexture rightIcon;
    private UILabel labCardNum;
    private UISprite spFrameNum;
    private UILabel labSpending;
    private UILabel labAttack;
    private UISprite spAttack;
    private UISprite spSkill;
    private UISprite spItem;
    private UISprite spEquip;
    private void Awake()
    {
        lblName = transform.Find("labName").GetComponent<UILabel>();
        leftIcon = transform.Find("spLeft/Icon").GetComponent<UITexture>();
        rightIcon = transform.Find("spRight/Icon").GetComponent<UITexture>();
        spFrameNum = transform.Find("spFrameNum").GetComponent<UISprite>();
        labCardNum = transform.Find("spFrameNum/labNum").GetComponent<UILabel>();
        labSpending = transform.Find("spFrameSpending/labSpending").GetComponent<UILabel>();
        spAttack = transform.Find("spAttack").GetComponent<UISprite>();
        spSkill = transform.Find("spSkill").GetComponent<UISprite>();
        spItem = transform.Find("spItem").GetComponent<UISprite>();
        spEquip = transform.Find("spEquip").GetComponent<UISprite>();
        labAttack = spAttack.transform.Find("labAttack").GetComponent<UILabel>();
    }

    public void SetCard(int cardId, int count = 1, bool showTips = false)
    {

        CardId = cardId;
        CardNum = count;
        CardData = BattleCardTableSettings.Get(CardId);
        leftIcon.Load(CardData.IconLeftID);
        rightIcon.Load(CardData.IconRightID);
        lblName.text = I18N.Get(CardData.Name);
        labSpending.text = CardData.Spending.ToString();
        ShowCardType(CardData.Type);

        if (CardData.Type == 0 && CardData.ActionTypes[0] == 1)
        {
            labAttack.text = CardData.ActionParams[0].ToString();
        }
        if (showTips)
        {
            UIUtility.SetCardTips(gameObject, CardId, CardNum);
        }

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

    public int CardNum
    {
        set
        {
            if (value < 0)
            {
                Debug.LogError("Wrong Card Num!");
                return;
            }


            cardNum = value;
            spFrameNum.gameObject.SetActive(cardNum <= 1);
            labCardNum.text = "" + cardNum;
            UIUtility.SetCardTips(gameObject, CardId, cardNum);
        }
        get
        {
            return cardNum;
        }
    }


}