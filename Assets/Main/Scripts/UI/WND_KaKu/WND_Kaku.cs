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
    private List<NormalCard> tempDeck = new List<NormalCard>();
    private KaKu ExtraKakuCards;
    private Deck ExtraDeckCards;
    private Vector3 offsetPos;
    private GameObject dragObj;
    private bool isDraging = false;
    private Vector3 cardScale = new Vector3(0.5f, 0.5f, 0.5f);
    private UIGrid startDragGrid;
    private UIToggle toggleSkill;
    private UIToggle toggleEquip;
    private UIToggle toggleItem;
    private UITexture charaterIcon;
    private KaKu KaKu;
    private Deck Deck;

    private bool isInBattle = false;


    /// <summary>
    /// 加载
    /// </summary>
    /// <param name="userdata"></param>目前为空
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        isInBattle = (bool)userdata;




        deckGrid = transform.Find("ScrollViewDeck/Grid").GetComponent<UIGrid>();
        cardGrid = transform.Find("bgDeck/ScrollViewCard/Grid").GetComponent<UIGrid>();
        kakuGrid = transform.Find("bgKaku/ScrollViewKaku/Grid").GetComponent<UIGrid>();
        btnCreateDeck = deckGrid.transform.Find("btnCreateDeck").gameObject;
        MovingPanel = transform.Find("MovingPanel").GetComponent<UIPanel>();
        btnExit = transform.Find("btnExit").gameObject;
        charaterIcon = transform.Find("texClassCharacter").GetComponent<UITexture>();
        toggleSkill = transform.Find("bgKaku/toggleSkill").GetComponent<UIToggle>();
        toggleEquip = transform.Find("bgKaku/toggleEquip").GetComponent<UIToggle>();
        toggleItem = transform.Find("bgKaku/toggleItem").GetComponent<UIToggle>();
        EventDelegate.Add(toggleSkill.onChange, SkillChose);
        EventDelegate.Add(toggleEquip.onChange, EquipChose);
        EventDelegate.Add(toggleItem.onChange, ItemChose);

        UIEventListener.Get(btnExit).onClick = ExitClick;
        deckInstence = transform.Find("deckInstence").gameObject;
        KaKu = Game.DataManager.PlayerDetailData.Kaku;
        Deck = Game.DataManager.PlayerDetailData.Deck;
        if (!isInBattle)
        {

            ExtraKakuCards = Game.DataManager.PlayerDetailData.ExtraKakuCards;
            ExtraDeckCards = Game.DataManager.PlayerDetailData.ExtraDeckCards;
            for (int i = 0; i < Deck.Count; i++)
            {
                tempDeck.Add(Deck[i]);
            }
           // tempDeck.AddCards(ExtraDeckCards.Cards);
        }
        else
        {
            Deck Deck = Game.DataManager.PlayerDetailData.Deck;
            ExtraDeckCards = Game.DataManager.PlayerDetailData.ExtraDeckCards;
            List<NormalCard> kakuCards = MapMgr.Instance.MyMapPlayer.Data.MapCardList;
            for (int i = 0; i < Deck.Count; i++)
            {
                tempDeck.Add(Deck[i]);
            }
        }


        
 

       


    }
    protected override void OnOpen()
    {
        base.OnOpen();
        ResourceManager.LoadGameObject("UI/Item/UINormalCard", LoadSucess, (str, obj) => { });
         

    }

    private void LoadSucess(string str,object[] obj,GameObject go)
    {
        CardInstence = go;
        CardInstence.transform.SetParent(transform);
        CardInstence.transform.localScale = new Vector3(1, 1, 1);
        CardInstence.SetActive(false);
        LoadDeckCard();

        LoadKaKuCard();
    }

    private void SetCharacterIcon(int icon)
    {
        charaterIcon.Load(icon);
    }

    private void LoadDeckCard()
    {
        for (int i = 0; i < cardGrid.transform.childCount; i++)
        {
            Transform trans = cardGrid.transform.GetChild(i);
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < tempDeck.Count; i++)
        {


            int cardId = tempDeck[i].CardId;
            if (cardGrid.transform.Find(cardId.ToString()) != null)
            {
                GameObject item = cardGrid.transform.Find(cardId.ToString()).gameObject;
                item.GetComponent<UINormalCard>().CardNum++;
            }
            else
            {
                GameObject item = Instantiate(CardInstence);
                item.SetActive(true);
                int id = cardId;
                item.name = "" + id;
                item.GetComponent<UINormalCard>().SetCard(id);
                item.GetComponent<UINormalCard>().CardNum = 1;
                item.AddComponent<UIDragScrollView>();
                UIEventListener.Get(item).onDragStart = OnCardDragStart;
                UIEventListener.Get(item).onDrag = OnCardDrag;
                UIEventListener.Get(item).onDragEnd = OnCardDragEnd;
                item.transform.SetParent(cardGrid.transform, false);
                item.transform.localPosition = new Vector3();
                item.transform.localScale = cardScale;


            }
        }
        cardGrid.repositionNow = true;

    }
    private void LoadKaKuCard()
    {
        for (int i = 0; i < kakuGrid.transform.childCount; i++)
        {
            Transform trans = kakuGrid.transform.GetChild(i);
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < KaKu.Count;i++)
        {
            int cardId = KaKu[i].CardId;
            if (kakuGrid.transform.Find(cardId.ToString()) == null)
            {
                GameObject item = Instantiate(CardInstence);
                item.SetActive(true);
                int id = cardId;

                item.GetComponent<UINormalCard>().SetCard(id);
                item.GetComponent<UINormalCard>().CardNum = 0;
                item.name = "" + id;
                item.AddComponent<UIDragScrollView>();
                UIEventListener.Get(item).onDragStart = OnCardDragStart;
                UIEventListener.Get(item).onDrag = OnCardDrag;
                UIEventListener.Get(item).onDragEnd = OnCardDragEnd;
                item.transform.SetParent(kakuGrid.transform, false);
                item.transform.localPosition = new Vector3();
                item.transform.localScale = cardScale;
            }
            bool isInDeck = false;
            for(int j = 0; j < tempDeck.Count; j++)
            {
                if (KaKu[i] == tempDeck[j])
                {
                    isInDeck = true;
                    break;
                }
                   
            }
            if (isInDeck)
                continue;



           

            GameObject itemCard = kakuGrid.transform.Find(cardId.ToString()).gameObject;
            itemCard.GetComponent<UINormalCard>().CardNum++ ;


        }

        kakuGrid.repositionNow = true;

    }

    private void SkillChose()
    {
        //直接用Grid的GetChild会出问题。改用transfom的
        for (int i = 0;i<kakuGrid.transform.childCount;i++)
        {
            Transform trans = kakuGrid.transform.GetChild(i);
            if (trans.GetComponent<UINormalCard>().CardData.Type != 2)
                trans.gameObject.SetActive(!UIToggle.current.value);
        }
    
       
        kakuGrid.repositionNow = true;
    }

    private void EquipChose()
    {
        for (int i = 0; i < kakuGrid.transform.childCount; i++)
        {
            Transform trans = kakuGrid.transform.GetChild(i);
            if (trans.GetComponent<UINormalCard>().CardData.Type != 1)
                trans.gameObject.SetActive(!UIToggle.current.value);
        }
        kakuGrid.repositionNow = true;
    }
    private void ItemChose()
    {
        for (int i = 0; i < kakuGrid.transform.childCount; i++)
        {
            Transform trans = kakuGrid.transform.GetChild(i);
            if (trans.GetComponent<UINormalCard>().CardData.Type != 3)
                trans.gameObject.SetActive(!UIToggle.current.value);
        }
        kakuGrid.repositionNow = true;
    }






    private void MoveCardFromDeckToKaKu(int cardId)
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
            cardToKaKu.CardNum += 1;
            kakuGrid.repositionNow = true;
            cardGrid.repositionNow = true;
            for (int i = 0; i < tempDeck.Count; i++)
            {
                if(tempDeck[i].CardId == cardId)
                 {
                tempDeck.RemoveAt(i);
                 break;
                 }
            }




    }
    private void MoveCardFromKaKuToDeck(int cardId)
    {

 
            UINormalCard cardFromKaKu =  kakuGrid.transform.Find("" + cardId).GetComponent<UINormalCard>();
            if (cardFromKaKu.CardNum <= 0 ){
                Debug.LogError("Move failed, the Card has wrong num!");
                return;
            }
            cardFromKaKu.CardNum -= 1;
            bool isFoud = false;
            for (int i = 0; i < tempDeck.Count; i++)
            {
                if (tempDeck[i].CardId == cardId)
                {
                    isFoud = true;
                    break;
                }
            }
            if (isFoud == false)
            {

                GameObject item = Instantiate(CardInstence);
                item.SetActive(true);
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

                
            }
            else
            {
                UINormalCard cardToDeck =  cardGrid.transform.Find("" + cardId).GetComponent<UINormalCard>();
                cardToDeck.CardNum += 1;
            }


            kakuGrid.repositionNow = true;
            cardGrid.repositionNow = true;
            for(int i = 0; i < tempDeck.Count; i++)
            {
                bool isFind = false;
                for (int j = 0; j < KaKu.Count; j++)
                {
                    if (KaKu[j].CardId ==cardId && KaKu[j] != tempDeck[i])
                    {
                        isFind = true;
                        tempDeck.Add(KaKu[j]);
                        break;
                    }
                }
                if (isFind)
                {
                    break;
                }
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
            dragObj.SetActive(true);
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
            Debug.Log("OnDragEnd ：" + obj.name);
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

    private void SaveDeck()
    {
        if (isInBattle)
        {
            MapMgr.Instance.MyMapPlayer.Data.MapCardList.Clear();
            for (int i = 0; i < tempDeck.Count; i++)
            {
                MapMgr.Instance.MyMapPlayer.Data.MapCardList.Add(tempDeck[i]);
            }
        }
        else
        {
            Game.DataManager.PlayerDetailData.Deck.Clear();
            Game.DataManager.PlayerDetailData.Deck.AddCards(tempDeck);

        }




    }



    private void ExitClick(GameObject btn)
    {
   
        print("ExitClick");
        UIModule.Instance.CloseForm<WND_Kaku>();

    }
    protected override void OnClose()
    {
        base.OnClose();

        SaveDeck();
        Destroy(CardInstence);
    }
}
