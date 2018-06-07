using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_Kaku : UIFormBase
{
    private UIGrid deckGrid;
    private UIGrid cardGrid;
    private GameObject CardInstence;
    private UIGrid kakuGrid;
    private UIPanel MovingPanel;
    private GameObject btnCreateDeck;
    private GameObject btnExit;
    private GameObject deckInstence;
    private bool isEditor;
    private KaKu KaKu;
    private Dictionary<int, List<NormalCard>>  tempKaKuCardsDic;
    private List<Deck> Decks;
    private Deck tempDeck;
    private uint editorDeckUid;
    private Vector3 offsetPos;
    private GameObject dragObj;
    private bool isDraging = false;
    private Vector3 cardScale = new Vector3(0.5f, 0.5f, 0.5f);
    private UIGrid startDragGrid;
    private UIToggle toggleSkill;
    private UIToggle toggleEquip;
    private UIToggle toggleItem;
    private UITexture charaterIcon;


    void Awake()
    {
        deckGrid = transform.Find("ScrollViewDeck/Grid").GetComponent<UIGrid>();
        cardGrid = transform.Find("bgDeck/ScrollViewCard/Grid").GetComponent<UIGrid>();
        kakuGrid = transform.Find("bgKaku/ScrollViewKaku/Grid").GetComponent<UIGrid>();
        btnCreateDeck = deckGrid.transform.Find("btnCreateDeck").gameObject;
        UIEventListener.Get(btnCreateDeck).onClick = CreateDeckClick;
        ResourceManager.LoadGameObject("Card/NormalCard", (str, obj, go) => { CardInstence = go; }, (str, obj) => { });
        MovingPanel = transform.Find("MovingPanel").GetComponent<UIPanel>();
        btnExit = transform.Find("btnExit").gameObject;
        charaterIcon = transform.Find("texClassCharacter").GetComponent<UITexture>();
        toggleSkill = transform.Find("bgKaku/toggleSkill").GetComponent<UIToggle>();
        toggleEquip = transform.Find("bgKaku/toggleEquip").GetComponent<UIToggle>();
        toggleItem = transform.Find("bgKaku/toggleItem").GetComponent<UIToggle>();
        EventDelegate.Add(toggleSkill.onChange,SkillChose);
        EventDelegate.Add(toggleEquip.onChange,EquipChose);
        EventDelegate.Add(toggleItem.onChange,ItemChose);

        UIEventListener.Get(btnExit).onClick = ExitClick;
        deckInstence = transform.Find("deckInstence").gameObject;
        KaKu = Game.DataManager.PlayerDetailData.Kaku;
        Decks = Game.DataManager.PlayerDetailData.Decks;
        isEditor = false;
    }
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        // List<BattleCardData> deckCardList = Game.DataManager.MyPlayerData.CardList; 
        // LoadDeckCard((List<BattleCardData>)deckCardList);
      
        Deck usingDeck = Decks.Find((deck) =>deck.Uid ==   Game.DataManager.PlayerData.UsingDeck);
        ClassCharacterTableSetting character = ClassCharacterTableSettings.Get(Game.DataManager.PlayerData.UsingCharacter);
        SetCharacterIcon(character.Image);
        if (usingDeck != null && usingDeck.ClassType ==(ClassType) character.ClassType)
        {
            
          //  LoadDeckList(Decks.FindAll((deck) => deck.ClassType == usingDeck.ClassType));
            ChoseDeck(usingDeck.Uid);
        }
         else
        {
           // LoadDeckList(Decks.FindAll((deck) => deck.ClassType == (ClassType)character.ClassType));
            LoadKaKuCard(KaKu.GetDicCards(KaKu.GetClassTypeCards((ClassType)character.ClassType, true)));
        }
       


    }
    private void SetCharacterIcon(int icon)
    {
        charaterIcon.Load(icon);
    }
    /*
    private void LoadDeckList(List<Deck> decks)
    {
        foreach (var trans in deckGrid.GetChildList())
        {
            if (trans == btnCreateDeck.transform)
                continue;
            Destroy(trans.gameObject);
        }
        foreach (var deck in decks)
        {
            GameObject item = Instantiate(deckInstence);
            item.transform.Find("labName").GetComponent<UILabel>().text = deck.DeckName;
            //    item.transform.Find("labClassType").GetComponent<UILabel>().text = deck.ClassType;
            item.name = "Deck" + deck.Uid;
            item.transform.SetParent(deckGrid.transform, false);
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.AddComponent<UIDragScrollView>();
            UIEventListener.Get(item).onClick =(GameObject obj) => {
                ChoseDeck(deck.Uid);
                };

            item.SetActive(true);
        }
        deckGrid.repositionNow = true;
    }
    */

    //key为卡片id，value为卡片张数；
    private void LoadDeckCard(Dictionary<int, List<NormalCard>> cardsDic)
    {
        foreach (var trans in cardGrid.GetChildList())
        {
            Destroy(trans.gameObject);
        }
        foreach (var cardList in cardsDic)
        {

            GameObject item = Instantiate(CardInstence);
            int id = cardList.Key;
            item.name = "" + id;
            item.GetComponent<UINormalCard>().SetCard(id);
            item.GetComponent<UINormalCard>().CardNum = cardList.Value.Count;
            item.AddComponent<UIDragScrollView>();
            UIEventListener.Get(item).onDragStart = OnCardDragStart;
            UIEventListener.Get(item).onDrag = OnCardDrag;
            UIEventListener.Get(item).onDragEnd = OnCardDragEnd;
            item.transform.SetParent(cardGrid.transform, false);
            item.transform.localPosition = new Vector3();
            item.transform.localScale = cardScale;
            item.SetActive(true);

        }
        cardGrid.repositionNow = true;

    }
    private void LoadKaKuCard(Dictionary<int, List<NormalCard>> cardsDic)
    {
        foreach (var trans in kakuGrid.GetChildList())
        {
            Destroy(trans.gameObject);
        }
        foreach (var cardList in cardsDic)
        {

            GameObject item = Instantiate(CardInstence);
            int id = cardList.Key;

            item.GetComponent<UINormalCard>().SetCard(id);
            item.GetComponent<UINormalCard>().CardNum = cardList.Value.Count;
            item.name = "" + id;
            item.AddComponent<UIDragScrollView>();
            UIEventListener.Get(item).onDragStart = OnCardDragStart;
            UIEventListener.Get(item).onDrag = OnCardDrag;
            UIEventListener.Get(item).onDragEnd = OnCardDragEnd;
            item.transform.SetParent(kakuGrid.transform, false);
            item.transform.localPosition = new Vector3();
            item.transform.localScale = cardScale;
            item.SetActive(true);

        }
        kakuGrid.repositionNow = true;

    }

    private void SkillChose()
    {
  
        foreach (var trans in kakuGrid.GetChildList())
        {
            if (trans.GetComponent<UINormalCard>().CardData.Type != 2 )
                trans.gameObject.SetActive(!UIToggle.current.value);
        }
        kakuGrid.repositionNow = true;
    }

    private void EquipChose()
    {
        foreach (var trans in kakuGrid.GetChildList())
        {
            if (trans.GetComponent<UINormalCard>().CardData.Type != 1)
                trans.gameObject.SetActive(!UIToggle.current.value);
        }
        kakuGrid.repositionNow = true;
    }
    private void ItemChose()
    {
        foreach (var trans in kakuGrid.GetChildList())
        {
            if (trans.GetComponent<UINormalCard>().CardData.Type != 3)
                trans.gameObject.SetActive(!UIToggle.current.value);
        }
        kakuGrid.repositionNow = true;
    }






    private void MoveCardFromDeckToKaKu(int cardId)
    {

        if (isEditor)
        {
            UINormalCard cardFromDeck =  cardGrid.transform.Find("" + cardId).GetComponent<UINormalCard>();
            if (cardFromDeck.CardNum <= 0 ){
                Debug.LogError("Move failed, the Card has wrong num!");
                return;
            }
            cardFromDeck.CardNum -= 1;
            if (cardFromDeck.CardNum == 0 )
                Destroy(cardFromDeck.gameObject);
            UINormalCard cardToKaKu =  kakuGrid.transform.Find("" + cardId).GetComponent<UINormalCard>();
            cardToKaKu.gameObject.SetActive(true);
            cardToKaKu.CardNum += 1;
            kakuGrid.repositionNow = true;
            cardGrid.repositionNow = true;
            tempKaKuCardsDic[cardId].Add(tempDeck.RemoveCard(cardId));

        }



    }
    private void MoveCardFromKaKuToDeck(int cardId)
    {

        if (isEditor)
        {
            UINormalCard cardFromKaKu =  kakuGrid.transform.Find("" + cardId).GetComponent<UINormalCard>();
            if (cardFromKaKu.CardNum <= 0 ){
                Debug.LogError("Move failed, the Card has wrong num!");
                return;
            }
            cardFromKaKu.CardNum -= 1;

            if (tempDeck.Cards.Find((item) => item.CardId == cardId) == null)
            {

                GameObject item = Instantiate(CardInstence);
                int id = cardId;
                item.name = "" + id;
                item.AddComponent<UIDragScrollView>();
                item.GetComponent<UINormalCard>().SetCard(id);
                UIEventListener.Get(item).onDragStart = OnCardDragStart;
                UIEventListener.Get(item).onDrag = OnCardDrag;
                UIEventListener.Get(item).onDragEnd = OnCardDragEnd;
                item.transform.SetParent(cardGrid.transform, false);
                item.transform.localPosition = new Vector3();
                item.transform.localScale = cardScale;
                item.SetActive(true);
                
            }
            else
            {
                UINormalCard cardToDeck =  cardGrid.transform.Find("" + cardId).GetComponent<UINormalCard>();
                cardToDeck.CardNum += 1;
            }


            kakuGrid.repositionNow = true;
            cardGrid.repositionNow = true;
            tempDeck.Cards.Add(tempKaKuCardsDic[cardId][0]);
            tempKaKuCardsDic[cardId].RemoveAt(0);

        }



    }


    private void ChoseDeck(uint uid)
    {
        if (!isEditor)
        {
            editorDeckUid = uid;
            tempDeck = Decks.Find((deck)=>deck.Uid == uid).CloneSelf();
            cardGrid.gameObject.SetActive(true);
            LoadDeckCard(tempDeck.GetDicCards());
            tempKaKuCardsDic = KaKu.GetDicCards(KaKu.GetCardsWithDeck(tempDeck));
            LoadKaKuCard(tempKaKuCardsDic);
            isEditor = true;
        }
    }

    private void BackClick(GameObject btn)
    {
        if (isEditor)
        {
            foreach (Transform decktransform in deckGrid.GetChildList())
            {
                decktransform.Find("btnBack").gameObject.SetActive(false);
                decktransform.Find("btnDelete").gameObject.SetActive(false);
                decktransform.gameObject.SetActive(true);
            }
            cardGrid.gameObject.SetActive(false);

            deckGrid.repositionNow = true;
            LoadDeckCard(new Dictionary<int, List<NormalCard>>());
            LoadKaKuCard(KaKu.GetDicCards());
            SaveDeck();
            isEditor = false;
        }

    }


    private void OnCardDragStart(GameObject obj)
    {
        Debug.Log("OnDragStart ：" + obj.name);
        
        offsetPos = obj.transform.position - UICamera.lastWorldPosition;
        Vector2 delta =  UICamera.currentTouch.totalDelta;
        if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
        {
            if (obj.GetComponent<UINormalCard>().CardNum == 0)
                return;

            Debug.Log("OnDragStart ：is Drang Card");
            isDraging = true;
            startDragGrid =  obj.GetComponentInParent<UIGrid>();
            obj.GetComponent<UIDragScrollView>().enabled = false;
            
            dragObj = Instantiate(obj);

            dragObj.GetComponent<UINormalCard>().SetCard(obj.GetComponent<UINormalCard>().CardId);
            dragObj.transform.SetParent(MovingPanel.transform, true);
            dragObj.transform.localScale = cardScale;
            RefreshDepth(dragObj.transform);
        }
        else
        {
            Debug.Log("OnDragStart ：is Drang ScrollView");
            isDraging = false;
            obj.GetComponent<UIDragScrollView>().enabled = true;
        }


    }
    protected void OnCardDragEnd(GameObject obj)
    {
        if (isDraging)
        {
            Debug.Log("OnDragEnd ：" + name);
            isDraging = false;
            obj.GetComponent<UIDragScrollView>().enabled = true;

            Ray ray = UICamera.mainCamera.ScreenPointToRay(UICamera.lastEventPosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, 20f, 1 << 8);
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.name == "bgKaku" && startDragGrid == cardGrid)
                    {

                        MoveCardFromDeckToKaKu(dragObj.GetComponent<UINormalCard>().CardId);
                        break;
                    }
                    else if (hits[i].collider.name == "bgDeck" && startDragGrid == kakuGrid)
                    {
                        MoveCardFromKaKuToDeck(dragObj.GetComponent<UINormalCard>().CardId);
                        break;

                    }
                }
            }
            Destroy(dragObj);
            RefreshDepth();
        }
   

    }
    protected void OnCardDrag(GameObject obj, Vector2 delta)
    {
        if (UICamera.mainCamera != null && isDraging)
        {

            dragObj.transform.position = UICamera.lastWorldPosition + offsetPos;
        }
    }

    private void RefreshDepth()
    {
        RefreshDepth(transform);
    }
    private void RefreshDepth(Transform trans)
    {
        UIWidget[] widgets = trans.GetComponentsInChildren<UIWidget>(true);
        foreach (var item in widgets)
        {
            if (item.enabled)
            {
                item.Refresh();
            }

        }
    }
    private void CreateDeckClick(GameObject obj)
    {

    }
    private void SaveDeck()
    {
        Deck deck = Decks.Find((item) => item.Uid == editorDeckUid);
        deck = tempDeck.CloneSelf() ;
    }



    private void ExitClick(GameObject btn)
    {
        SaveDeck();
        Destroy(CardInstence);
        print("ExitClick");
        UIModule.Instance.CloseForm<WND_Kaku>();

    }
}
