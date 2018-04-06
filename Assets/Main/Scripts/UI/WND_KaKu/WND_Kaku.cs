using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WND_Kaku : UIFormBase {
    private UIGrid deckGrid;
    private GameObject battleCard;
    private UIGrid kakuGrid;
    public UIPanel MovingPanel;
    private GameObject _upClick;
    private GameObject _downClick;
    private float _gridPosY;
    private float _cellHeight;
    private UIPanel KakuPanel;
    void Awake()
    {
        deckGrid = transform.Find("bgDeck/ScrollView/Grid").GetComponent<UIGrid>();
        kakuGrid = transform.Find("bgKaku/Panel/Grid").GetComponent<UIGrid>();
        KakuPanel = transform.Find("bgKaku/Panel").GetComponent<UIPanel>();
        _gridPosY = kakuGrid.transform.localPosition.y;
        _cellHeight = kakuGrid.cellHeight;
        battleCard = Resources.Load("Prefabs/Card/NormalCard") as GameObject;
        MovingPanel = transform.Find("MovingPanel").GetComponent<UIPanel>();
        _upClick = transform.Find("bgKaku/Panel/up").gameObject;
        _downClick = transform.Find("bgKaku/Panel/down").gameObject;
        UIEventListener.Get(_upClick).onClick = UpClick;
        UIEventListener.Get(_downClick).onClick = DownClick;

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
            item.GetComponent<UINormalCard>().SetData(card,this);
            item.GetComponent<UINormalCard>().DeckOrKaku = UINormalCard.deck_or_kaku.deck;
            item.transform.parent = deckGrid.transform;
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);

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
            item.GetComponent<UINormalCard>().SetData(card,this);
            item.GetComponent<UINormalCard>().DeckOrKaku = UINormalCard.deck_or_kaku.kaku;
            item.transform.parent = kakuGrid.transform;
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);

        }
        kakuGrid.repositionNow = true;

    }
    public void MoveCardFromToKaku(UINormalCard card)
    {
        foreach (BattleCardData i in Game.DataManager.MyPlayerData.CardList)
          {
              if (i.CardId == card.cardData.CardId)
              {
                  Game.DataManager.MyPlayerData.CardList.Remove(i);
                break;
              }
          }
          Game.DataManager.Kaku.Add(card.cardData);

             /*  
          deckGrid.RemoveChild(card.transform);
          Destroy(card.gameObject);

          GameObject item = Instantiate(battleCard);
          int id = card.cardData.CardId;
          item.name = "Card" + id;
          item.GetComponent<UINormalCard>().SetData(card.cardData, this);
          item.GetComponent<UINormalCard>().DeckOrKaku = UINormalCard.deck_or_kaku.kaku;
          item.transform.parent = kakuGrid.transform;
          item.transform.localPosition = new Vector3();
          item.transform.localScale = new Vector3(1, 1, 1);
          item.SetActive(true);
          deckGrid.repositionNow = true;*/

        card.transform.SetParent(kakuGrid.transform,false);
        card.DeckOrKaku = UINormalCard.deck_or_kaku.kaku;
        kakuGrid.repositionNow = true;
        deckGrid.repositionNow = true;


    }
    public void MoveKakuFromToDeck(UINormalCard card)
    {
        foreach (BattleCardData i in Game.DataManager.Kaku)
        {
            if (i.CardId == card.cardData.CardId)
            {
                Game.DataManager.Kaku.Remove(i);
                break;
            }
        }
        Game.DataManager.MyPlayerData.CardList.Add(card.cardData);

        card.transform.SetParent(deckGrid.transform, false);
        card.DeckOrKaku = UINormalCard.deck_or_kaku.deck;
        kakuGrid.repositionNow = true;
        deckGrid.repositionNow = true;


    }
    private void UpClick(GameObject btn)
    {

        print("UpClick!");
        Vector3 pos = kakuGrid.transform.localPosition;
        if (pos.y <= _gridPosY)
            return;
        pos.y -= _cellHeight * 2;
        kakuGrid.transform.localPosition = pos;
    }
    private void DownClick(GameObject btn)
    {
        print("DownClick!");

        Vector3 pos = kakuGrid.transform.localPosition;
        int count = kakuGrid.GetChildList().Count;
        
        if (pos.y + _cellHeight * 2 >= _gridPosY + _cellHeight * count / 5)
            return;
        pos.y += _cellHeight * 2;
        kakuGrid.transform.localPosition = pos;
        
    }
}
