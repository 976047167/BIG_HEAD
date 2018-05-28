using System.Collections;
using System.Collections.Generic;
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
    }

    // Update is called once per frame
    void Update () {
		
	}
}
