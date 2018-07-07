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
    private GameObject headFrame;
    private UITexture headIcon;
    private int iconId = 0;

    // Use this for initialization
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        labName = transform.Find("background/spFrameHead/labName").GetComponent<UILabel>();
        labLevel = transform.Find("background/spFrameHead/spLevel/labLevel").GetComponent<UILabel>();
        labCoin = transform.Find("background/spFrameCoin/labCoinNum").GetComponent<UILabel>();
        labYuanBao = transform.Find("background/spFrameYuanBao/labYuanBaoNum").GetComponent<UILabel>();
        labVipLevel = transform.Find("background/spFrameHead/spVipLevel/labVipLevel").GetComponent<UILabel>();
        btnKaKu = transform.Find("background/btnKaKu").gameObject;
        btnPlot = transform.Find("background/btnPlot").gameObject;
        btnDungeon = transform.Find("background/btnDungeon").gameObject;
        headFrame = transform.Find("background/spFrameHead").gameObject;
        headIcon = headFrame.transform.Find("texHead").GetComponent<UITexture>();
        UIEventListener.Get(btnKaKu).onClick = KakuClick;
        UIEventListener.Get(btnDungeon).onClick = DungeonClick;
        UIEventListener.Get(btnPlot).onClick = Onclick_btnPlot;
        UIEventListener.Get(headFrame).onClick = OnClick_HeadFrame;
        Messenger.AddListener(MessageID.MAP_UPDATE_PLAYER_INFO, UpdatePlayerInfoPanel);
    }
    protected override void OnOpen()
    {
        base.OnOpen();
        UpdatePlayerInfoPanel();
    }
    private void UpdatePlayerInfoPanel()
    {

        labName.text = Game.DataManager.MyPlayer.Data.Name;
        labLevel.text = Game.DataManager.MyPlayer.Data.Level.ToString();
        labVipLevel.text = Game.DataManager.AccountData.VipLevel.ToString();
        labYuanBao.text = Game.DataManager.AccountData.Diamonds.ToString();
        labCoin.text = Game.DataManager.AccountData.Gold.ToString();
        if (iconId != Game.DataManager.PlayerData.HeadIcon)
        {
            iconId = Game.DataManager.PlayerData.HeadIcon;
            headIcon.Load(iconId);
        }
        
    }
    private void KakuClick(GameObject obj)
    {
        Game.UI.OpenForm<WND_Kaku>(false);
    }
    private void DungeonClick(GameObject obj)
    {
      //  UIModule.Instance.OpenForm<WND_ChoseDeck>(2);
    }

    protected void Onclick_btnPlot(GameObject go)
    {
        SceneMgr.ChangeScene(4);
    }
    private void OnClick_HeadFrame(GameObject obj)
    {
        Game.UI.OpenForm<WND_Settings>();

    }
}
