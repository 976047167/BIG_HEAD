using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WND_Kaku : UIFormBase {
    private UIGrid deckGrid;
    private UIGrid cardGrid;
    private GameObject Card;
    private UIGrid kakuGrid;
    public UIPanel MovingPanel;
    private GameObject _upClick;
    private GameObject _downClick;
    private float _gridPosY;
    private float _cellHeight;
    private UIPanel KakuPanel;
    private GameObject btnExit;
    private GameObject  deckInstence;
    private GameObject btnBack;



    void Awake()
    {
        deckGrid = transform.Find("bgDeck/ScrollViewDeck/Grid").GetComponent<UIGrid>();
        cardGrid = transform.Find("bgDeck/ScrollViewCard/Grid").GetComponent<UIGrid>();
        kakuGrid = transform.Find("bgKaku/Panel/Grid").GetComponent<UIGrid>();
        KakuPanel = transform.Find("bgKaku/Panel").GetComponent<UIPanel>();
        _gridPosY = kakuGrid.transform.localPosition.y;
        _cellHeight = kakuGrid.cellHeight;
        ResourceManager.LoadGameObject("Card/NormalCard", (str, obj,go) =>{ Card = go; },(str, obj) =>{ });
        MovingPanel = transform.Find("MovingPanel").GetComponent<UIPanel>();
        _upClick = transform.Find("bgKaku/Panel/up").gameObject;
        _downClick = transform.Find("bgKaku/Panel/down").gameObject;
        UIEventListener.Get(_upClick).onClick = UpClick;
        UIEventListener.Get(_downClick).onClick = DownClick;
        btnExit = transform.Find("bgDeck/btnExit").gameObject;
        UIEventListener.Get(btnExit).onClick = ExitClick;
        deckInstence = transform.Find("bgDeck/deckInstence").gameObject;
        UIEventListener.Get(btnBack).onClick = BackClick;

    }
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
       // List<BattleCardData> deckCardList = Game.DataManager.MyPlayerData.CardList;
     List<BattleCardData> KakuCardList = Game.DataManager.PlayerDetailData.CardList;
       // LoadDeckCard((List<BattleCardData>)deckCardList);
        LoadKaKuCard(KakuCardList);
        List<Deck> DeckList = Game.DataManager.PlayerDetailData.decks;
        LoadDeckList( DeckList);
    }
    private void LoadDeckList(List<Deck> DeckList)
    {
        foreach (var deck in DeckList)
        {
            GameObject item = Instantiate(deckInstence);
            item.transform.Find("labName").GetComponent<UILabel>().text = deck.DeckName;

        }
        deckGrid.repositionNow = true;
    }

    //key为卡片id，value为卡片张数；
    private void LoadDeckCard(List<BattleCardData> cardList)
    {
        foreach (var card in cardList)
        {

            GameObject item = Instantiate(Card);
            int id = card.Data.Id;
            item.name = "Card" + id;
            item.GetComponent<UINormalCard>().SetData(card,this);
            item.GetComponent<UINormalCard>().DeckOrKaku = UINormalCard.deck_or_kaku.deck;
            item.transform.parent = cardGrid.transform;
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);

        }
        cardGrid.repositionNow = true;

    }
    private void LoadKaKuCard(List<BattleCardData> cardList)
    {
        foreach (var card in cardList)
        {

            GameObject item = Instantiate(Card);
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
        cardGrid.repositionNow = true;


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

        card.transform.SetParent(cardGrid.transform, false);
        card.DeckOrKaku = UINormalCard.deck_or_kaku.deck;
        kakuGrid.repositionNow = true;
        cardGrid.repositionNow = true;


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


    private void BackClick(GameObject btn)
    {
        
    }



    private void ExitClick(GameObject btn)
    {
        print("ExitClick");
        UIModule.Instance.CloseForm<WND_Kaku>();
     
    }
}
