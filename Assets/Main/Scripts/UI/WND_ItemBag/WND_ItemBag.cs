using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_ItemBag : UIFormBase {
    bool isInLobby;
    GameObject itemInstence;
    GameObject bg;
    UISprite itemBg; 
    UIGrid grid;

    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        isInLobby = (bool)userdata;
        bg = transform.Find("bg").gameObject;
        itemBg = bg.transform.Find("itemBg").GetComponent<UISprite>();
        itemInstence = bg.transform.Find("itemInstence").gameObject;
        grid = itemBg.transform.Find("ScrollView/Grid").GetComponent<UIGrid>();
    }
    protected override void OnOpen()
    {
        base.OnOpen();
        int num = ItemTableSettings.GetInstance().Count;
        for(int i = 0; i < num; i++)
        {
            GameObject item = Instantiate(itemInstence);
            item.transform.SetParent(grid.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;
        }


    }
    protected override void OnShow()
    {
        base.OnShow();
        Vector3 orginPos =itemBg.transform.localPosition;
      //  itemBg.transform.localPosition
        //TweenPosition.Begin(itemBg.gameObject，0.1f,)
    }


}
