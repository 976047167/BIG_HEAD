using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WND_MainTown : UIFormBase
{

    private UILabel labName;
    private GameObject btnKaKu;
    private UILabel labLevel;
    private UILabel labVipLevel;
    private UILabel labCoin;
    private UILabel labYuanBao;
    private GameObject btnDungeon;
    private GameObject btnPlot;

    // Use this for initialization
    private void Awake()
    {
        labName = transform.Find("background/spFrameHead/labName").GetComponent<UILabel>();
        labLevel = transform.Find("background/spFrameHead/spLevel/labLevel").GetComponent<UILabel>();
        labCoin = transform.Find("background/spFrameCoin/labCoinNum").GetComponent<UILabel>();
        labYuanBao = transform.Find("background/spFrameYuanBao/labYuanBaoNum").GetComponent<UILabel>();
        labVipLevel = transform.Find("background/spFrameHead/spVipLevel/labVipLevel").GetComponent<UILabel>();
        btnKaKu = transform.Find("background/btnKaKu").gameObject;
        btnPlot = transform.Find("background/btnPlot").gameObject;
        btnDungeon = transform.Find("background/btnDungeon").gameObject;
        UIEventListener.Get(btnKaKu).onClick = KakuClick;
        UIEventListener.Get(btnDungeon).onClick = DungeonClick;
        UIEventListener.Get(btnPlot).onClick = Onclick_btnPlot;
    }

    private void Start()
    {
        labName.text = Game.DataManager.MyPlayer.Data.Name;
        labLevel.text = "" + Game.DataManager.MyPlayer.Data.Level;
        labVipLevel.text = "" + Game.DataManager.AccountData.VipLevel;
        labYuanBao.text = "" + Game.DataManager.AccountData.Diamonds;
        labCoin.text = "" + Game.DataManager.AccountData.Gold;

    }

    private void KakuClick(GameObject obj)
    {
        UIModule.Instance.OpenForm<WND_ChoseDeck>(1);
    }
    private void DungeonClick(GameObject obj)
    {
        UIModule.Instance.OpenForm<WND_ChoseDeck>(2);
    }

    protected void Onclick_btnPlot(GameObject go)
    {
        SceneMgr.ChangeScene(3);
    }
}
