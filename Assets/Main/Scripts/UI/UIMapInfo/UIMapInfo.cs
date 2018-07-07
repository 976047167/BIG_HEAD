using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class UIMapInfo : UIFormBase
{

    private UITexture Head;
    private UIProgressBar sliderHp;
    private UIProgressBar sliderMp;
    private UILabel labName;
    private UILabel labHp;
    private UILabel labMp;
    private UILabel labFood;
    private UILabel labGold;
    private Player playerInfo = new Player();
    private int iconId = 0;



    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        Head = transform.Find("headFrame/texHead").GetComponent<UITexture>();
        sliderHp = transform.Find("headFrame/sliderHp").GetComponent<UIProgressBar>();
        sliderMp = transform.Find("headFrame/sliderMp").GetComponent<UIProgressBar>();
        labName = transform.Find("headFrame/labName").GetComponent<UILabel>();
        labHp = transform.Find("headFrame/sliderHp/labHp").GetComponent<UILabel>();
        labMp = transform.Find("headFrame/sliderMp/labMp").GetComponent<UILabel>();
        labGold = transform.Find("headFrame/gold/labgold").GetComponent<UILabel>();
        labFood = transform.Find("headFrame/food/labfood").GetComponent<UILabel>();
        playerInfo = Game.DataManager.MyPlayer;
        Messenger.AddListener(MessageID.MSG_UPDATE_ROLE_INFO_PANEL, UpdatePlayerInfoPanel);

    }


    protected override void OnOpen()
    {
        base.OnOpen();
        UpdatePlayerInfoPanel();
    }
    protected override void OnClose()
    {
        base.OnClose();
        Messenger.RemoveListener(MessageID.MSG_UPDATE_ROLE_INFO_PANEL, UpdatePlayerInfoPanel);
    }
    // Update is called once per frame
    void UpdatePlayerInfoPanel()
    {
        if (iconId != playerInfo.Data.HeadIcon)
        {
            iconId = playerInfo.Data.HeadIcon;
            Head.Load(iconId);
        }

        labName.text = playerInfo.Data.Name;
        sliderHp.value = playerInfo.Data.HP / playerInfo.Data.MaxHP;
        labHp.text = string.Format("{0}/{1}", playerInfo.Data.HP, playerInfo.Data.MaxHP);
        sliderHp.value = playerInfo.Data.MP / playerInfo.Data.MaxMP;
        labMp.text = string.Format("{0}/{1}", playerInfo.Data.MP, playerInfo.Data.MaxMP);
        if (MapMgr.Inited == false)
        {
            labFood.text = "";
            labGold.text = "";
            return;
        }
        labFood.text = (MapMgr.Instance.MyMapPlayer.Data.Food.ToString());
        labGold.text = (MapMgr.Instance.MyMapPlayer.Data.Coin.ToString());

    }
}
