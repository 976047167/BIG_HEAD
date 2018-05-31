using System.Collections;
using System.Collections.Generic;
using System;
using AppSettings;
using UnityEngine;

public class WND_ChoseDeck : UIFormBase {
    private GameObject btnCommond;
    private GameObject btnExit;
    private GameObject deckInstence;
    private GameObject classTypeInstence;
    private UIGrid deckGrid;
    private UIGrid classTypeGrid;
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
                        item.name = "" + classType;
                        item.GetComponent<UITexture>().Load(chararcter.Image);
                        item.transform.Find("CheckMark").GetComponent<UITexture>().Load(chararcter.Image);
                        EventDelegate.Add(item.GetComponent<UIToggle>().onChange, OnClassTypeChose);
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
    private void OnClassTypeChose()
    {
       if( UIToggle.current.value == true)
        {
            int classType = 0;
            int.TryParse(UIToggle.current.name, out classType);
            if (classType == 0)
                return;
            LoadDeckList(classType);
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
            //    item.transform.Find("labClassType").GetComponent<UILabel>().text = deck.ClassType;
            item.name = "Deck" + deck.Uid;
            item.transform.SetParent(deckGrid.transform, false);
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.AddComponent<UIDragScrollView>();
            UIEventListener.Get(item).onClick = (GameObject obj) => {
              
            };

            item.SetActive(true);
        }
        deckGrid.repositionNow = true;
    }

private void ExitClick (GameObject obj)
{
       print("ExitClick");
        UIModule.Instance.CloseForm<WND_ChoseDeck>();
}

}
