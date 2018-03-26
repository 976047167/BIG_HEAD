using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WND_Kaku : UIFormBase {
    private UIGrid deckGrid;
    private GameObject battleCard;
    private UIGrid kakuGrid;
    void Awake()
    {
        deckGrid = transform.Find("bgDeck/ScrollView/Grid").GetComponent<UIGrid>();
        kakuGrid = transform.Find("bgKaku/ScrollView/Grid").GetComponent<UIGrid>();
        battleCard = Resources.Load("Prefabs/Card/NormalCard") as GameObject;

    }
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        List<BattleCardData> deckCardList = Game.DataManager.MyPlayerData.CardList;
        List<BattleCardData> KakuCardList = Game.DataManager.Kaku;
        LoadDeckCard((List<BattleCardData>)deckCardList);
        LoadKaKuCard((List<BattleCardData>)KakuCardList);
    }
    //key为卡片id，value为卡片张数；
    private void LoadDeckCard(List<BattleCardData> cardList)
    {
        foreach (var card in cardList)
        {

            GameObject item = Instantiate(battleCard);
            int id = card.Data.Id;
            item.name = "Card" + id;
            item.GetComponent<UINormalCard>().SetData(card);
            item.GetComponent<UINormalCard>().Parent = transform;
            item.GetComponent<UINormalCard>().Grid = deckGrid;
            item.AddComponent<UIDragScrollView>();
            item.transform.parent = deckGrid.transform;
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);
            UIEventListener.Get(item).onClick = (GameObject a) =>
            {
                UIModule.Instance.OpenForm<WND_ShowCard>(id);


            };
        }
        deckGrid.repositionNow = true;

    }
    private void LoadKaKuCard(List<BattleCardData> cardList)
    {
        foreach (var card in cardList)
        {

            GameObject item = Instantiate(battleCard);
            int id = card.Data.Id;
            item.name = "Card" + id;
            item.GetComponent<UINormalCard>().SetData(card);
            item.GetComponent<UINormalCard>().Parent = transform;
            item.GetComponent<UINormalCard>().Grid = kakuGrid;
            item.AddComponent<UIDragScrollView>();
            item.transform.parent = kakuGrid.transform;
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);
            UIEventListener.Get(item).onClick = (GameObject a) =>
            {
                UIModule.Instance.OpenForm<WND_ShowCard>(id);


            };
        }
        kakuGrid.repositionNow = true;

    }

}
