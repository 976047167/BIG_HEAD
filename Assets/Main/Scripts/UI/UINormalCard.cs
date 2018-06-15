using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class UINormalCard : MonoBehaviour
{
    public int CardId {get;private set;}
    public BattleCardTableSetting CardData { get; private set; }
    private int cardNum;
    private UITexture Icon;
    private UILabel labCardNum;
    private UILabel labSpending;
    private void Awake()
    {
        Icon = transform.Find("spFrameIcon/Icon").GetComponent<UITexture>();
        labCardNum = transform.Find("spFrameNum/labNum").GetComponent<UILabel>();
        labSpending = transform.Find("spFrameSpending/labSpending").GetComponent<UILabel>();
    }


    public void SetCard(int cardId)
    {

        CardId = cardId;
        CardNum =1 ;
        CardData = BattleCardTableSettings.Get(CardId);
        Icon.Load(CardData.IconLeftID);
        labSpending.text = "" + CardData.Spending;


        UIEventListener.Get(gameObject).onClick = (GameObject a) =>
        {
            UIModule.Instance.OpenForm<WND_ShowCard>(CardId);


        };
        

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