using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_Bag : UIFormBase
{
    private UIGrid grid;
    private GameObject battleCard;
    void Awake()
    {
        grid = transform.Find("ScrollView/Grid").GetComponent<UIGrid>();
        battleCard = Resources.Load("Prefabs/Card/BattleCard") as GameObject;

    }
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
    }

    private void LoadCard(Dictionary<int,int> dicCard)
    {
        foreach (var card in dicCard)
        {
            for (int i=0; i <card.Value;i++)
            {
                GameObject item = Instantiate(battleCard);
                item.name = "Card" + card.Key;
                item.transform.parent = grid.transform;
                item.transform.localPosition = new Vector3();
                item.transform.localScale = new Vector3(1, 1, 1);
                item.SetActive(true);
            }
        }
        grid.repositionNow = true;

    }
}
