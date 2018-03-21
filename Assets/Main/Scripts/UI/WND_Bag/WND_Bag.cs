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
        List<BattleCardData> cardList = Game.DataManager.MyPlayerData.CardList;
        LoadCard((List<BattleCardData>)cardList);
    }
    //key为卡片id，value为卡片张数；
    private void LoadCard(List<BattleCardData> cardList)
    {
        foreach (var card in cardList)
        {

                GameObject item = Instantiate(battleCard);
                item.name = "Card" + card.CardId;
                item.transform.parent = grid.transform;
                item.transform.localPosition = new Vector3();
                item.transform.localScale = new Vector3(1, 1, 1);
                item.SetActive(true);
            UIEventListener.Get(item).onClick = (GameObject a) =>
            {
                UIModule.Instance.OpenForm<WND_ShowCard>(card.CardId);


            };
        }
        grid.repositionNow = true;

    }
}
