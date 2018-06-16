using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class UINormalCard : MonoBehaviour
{
    public int CardId {get;private set;}
    public BattleCardTableSetting CardData { get; private set; }
    private int cardNum;
    private UITexture leftIcon;
    private UITexture rightIcon;
    private UILabel labCardNum;
    private UILabel labSpending;
    private UISprite spAttack;
    private UISprite spSkill;
    private UISprite spItem;
    private UISprite spEquip;
    private void Awake()
    {
        leftIcon = transform.Find("spLeft/spFrameIcon/Icon").GetComponent<UITexture>();
        rightIcon = transform.Find("spRight/spFrameIcon/Icon").GetComponent<UITexture>();
        labCardNum = transform.Find("spFrameNum/labNum").GetComponent<UILabel>();
        labSpending = transform.Find("spFrameSpending/labSpending").GetComponent<UILabel>();
        spAttack = transform.Find("spAttack").GetComponent<UISprite>();
        spSkill = transform.Find("spSkill").GetComponent<UISprite>();
        spItem = transform.Find("spItem").GetComponent<UISprite>();
        spEquip = transform.Find("spEquip").GetComponent<UISprite>();

    }


    public void SetCard(int cardId)
    {

        CardId = cardId;
        CardNum =1 ;
        CardData = BattleCardTableSettings.Get(CardId);
        leftIcon.Load(CardData.IconLeftID);
        rightIcon = transform.Find("spRight/spFrameIcon/Icon").GetComponent<UITexture>();
        labSpending.text = "" + CardData.Spending;
        ShowCardType(CardData.Type);

        UIEventListener.Get(gameObject).onClick = (GameObject a) =>
        {
            Game.UI.OpenForm<WND_ShowCard>(CardId);


        };
        

    }
    private void ShowCardType(int cardType)
    {
        spAttack.gameObject.SetActive(cardType == 0);
        spEquip.gameObject.SetActive(cardType == 1);
        spSkill.gameObject.SetActive(cardType == 2);
        spItem.gameObject.SetActive(cardType == 3);
    }

    public int CardNum
    {
        set{
            if (value < 0 ){
                Debug.LogError("Wrong Card Num!");            
                return;
            }


            cardNum = value;
            labCardNum.text = "" + cardNum;
        }
        get{
          return   cardNum ;
        }
    }


}