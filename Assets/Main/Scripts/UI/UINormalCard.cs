using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class UINormalCard : MonoBehaviour
{
    public int CardId {get;private set;}
    private int cardNum;
     protected void Start()
    {
       
    }


    public void SetCard(int cardId)
    {

        CardId = cardId;
        CardNum =1 ;
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
        }
        get{
          return   cardNum ;
        }
    }


}