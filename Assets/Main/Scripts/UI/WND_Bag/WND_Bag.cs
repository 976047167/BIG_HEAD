﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_Bag : UIFormBase
{
    private UIGrid grid;
    void Awake()
    {
        grid = transform.Find("bg/ScrollView/Grid").GetComponent<UIGrid>();

    }
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        List<NormalCard> cardList = Game.DataManager.MyPlayer.DetailData.CardList;
        LoadCard(cardList);
    }
    //key为卡片id，value为卡片张数；
    private void LoadCard(List<NormalCard> cardList)
    {
        foreach (var card in cardList)
        {

            //GameObject item = Instantiate(battleCard);
            //int id = card.CardId;
            //item.name = "Card" + id;
            ////item.GetComponent<UIBattleCard>().SetData(id);
            //item.AddComponent<UIDragScrollView>();
            //item.transform.parent = grid.transform;
            //item.transform.localPosition = new Vector3();
            //item.transform.localScale = new Vector3(1, 1, 1);
            //item.SetActive(true);
            //UIEventListener.Get(item).onClick = (GameObject a) =>
            //{
            //    UIModule.Instance.OpenForm<WND_ShowCard>(id);


            //};
        }
        grid.repositionNow = true;

    }
}
