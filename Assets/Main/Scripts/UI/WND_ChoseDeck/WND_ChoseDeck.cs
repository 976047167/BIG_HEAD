using System.Collections;
using System.Collections.Generic;
using System;
using AppSettings;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WND_ChoseDeck : UIFormBase {
    private GameObject btnCommond;
    private GameObject btnExit;
    private GameObject deckInstence;
    private GameObject classTypeInstence;
    private UIGrid deckGrid;
    private UIGrid classTypeGrid;
    private delegate void CallBack();
    private CallBack callbackdelegate;
    private int callBackInt;
    private int chosingDeck = 0;
    private int chosingClassCharacter;
    // Use this for initialization
     void Awake()
    {
        btnCommond = transform.Find("bg/btnCommond").gameObject;
        btnExit = transform.Find("bg/btnExit").gameObject;
        deckInstence = transform.Find("bg/deckInstence").gameObject;
        classTypeInstence = transform.Find("bg/classTypeInstence").gameObject;
        classTypeGrid = transform.Find("bg/ScrollViewClassTypes/Grid").GetComponent<UIGrid>();
        deckGrid = transform.Find("bg/ScrollViewDecks/Grid").GetComponent<UIGrid>();
        UIEventListener.Get(btnExit).onClick = ExitClick;
        UIEventListener.Get(btnCommond).onClick = CommondClick;
    }

    private void Start()
    {
        foreach(int classType in Enum.GetValues(typeof(ClassType)))
        {
            if (classType == 0)
            {
                continue;
            }
            else
            {
                
                foreach(ClassCharacterTableSetting chararcter in ClassCharacterTableSettings.GetAll())
                {
                    if (chararcter.ClassType == classType)
                    {
                        GameObject item = Instantiate(classTypeInstence);
                        item.name = "" + chararcter.Id;
                        item.GetComponent<UITexture>().Load(chararcter.Image);
                        item.transform.Find("CheckMark").GetComponent<UITexture>().Load(chararcter.Image);
                        //EventDelegate.Add(item.GetComponent<UIToggle>().onChange, OnClassCharacterChose);
                        item.transform.SetParent(classTypeGrid.transform, false);
                        item.transform.localPosition = new Vector3();
                        item.transform.localScale = new Vector3(1, 1, 1);
                        item.SetActive(true);
                        break;
                    }
                        
                }

            }
        }
        classTypeGrid.repositionNow = true;
    }
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        if (userdata != null)
            callBackInt = (int)userdata;



    }
    /*
    private void OnClassCharacterChose()
    {
       if( UIToggle.current.value == true)
        {
            int classCharacter = 0;
            int.TryParse(UIToggle.current.name, out classCharacter);
            if (classCharacter == 0)
                return;
            chosingClassCharacter = classCharacter;
            chosingDeck = 0;
            LoadDeckList(ClassCharacterTableSettings.Get(classCharacter).ClassType);
        }
    }

    private void LoadDeckList(int classType)
    {
        foreach (var trans in deckGrid.GetChildList())
        {
            Destroy(trans.gameObject);
        }
        List<Deck> decks = Game.DataManager.PlayerDetailData.Decks.FindAll((item) =>item.ClassType == (ClassType)classType);

        foreach (var deck in decks)
        {
            GameObject item = Instantiate(deckInstence);
            item.transform.Find("labName").GetComponent<UILabel>().text = deck.DeckName;
             item.name = "Deck" + deck.Uid;
            item.transform.SetParent(deckGrid.transform, false);
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.AddComponent<UIDragScrollView>();
            EventDelegate.Add(item.GetComponent<UIToggle>().onChange, ()=>{
                if (UIToggle.current.value == true)
                    chosingDeck = deck.Uid; });

            item.SetActive(true);
        }
        deckGrid.repositionNow = true;
    }
    */
    private void ExitClick (GameObject obj)
    {
           print("ExitClick");
            UIModule.Instance.CloseForm<WND_ChoseDeck>();
    }
    private void CommondClick(GameObject obj)
    {
        if (chosingDeck == 0)
        {
            Debug.Log("未选择卡组");
            if (callBackInt == 2)
                return;
        }
        else
        {
            Game.DataManager.PlayerDetailData.UsingDeck = chosingDeck;
        }
        
        if (chosingClassCharacter == 0)
        {
            Debug.Log("未选择角色");
        }
        else
        {
            Game.DataManager.PlayerData.UsingCharacter = chosingClassCharacter;
        }

        UIModule.Instance.CloseForm<WND_ChoseDeck>();
        switch (callBackInt)
        {

            case 1:
               UIModule.Instance.OpenForm<WND_Kaku>();
                break;
            case 2:
                 SceneManager.LoadScene("Main");
                break;
        }
    }
}
