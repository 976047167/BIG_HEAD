using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
using System;

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
    private MapPlayer playerInfo;
    private UISprite spExp;
    private UILabel lblLevel;
    private UILabel lblMapName;
    private UILabel lblMapLayerName;

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
        labGold = transform.Find("gold/num").GetComponent<UILabel>();
        labFood = transform.Find("food/num").GetComponent<UILabel>();
        spExp = transform.Find("headFrame/spExp").GetComponent<UISprite>();
        lblLevel = transform.Find("headFrame/spExp/lblLevel").GetComponent<UILabel>();
        lblMapName = transform.Find("mapName").GetComponent<UILabel>();
        lblMapLayerName = transform.Find("mapLayerName").GetComponent<UILabel>();
        Messenger.AddListener(MessageId.MAP_UPDATE_PLAYER_INFO, UpdatePlayerInfoPanel);
        Messenger.AddListener(MessageId.GAME_GET_MAP_LAYER_DATA, UpdateMapInfo);
    }



    protected override void OnOpen()
    {
        base.OnOpen();
        UpdatePlayerInfoPanel();
        UpdateMapInfo();
    }
    protected override void OnClose()
    {
        base.OnClose();
        Messenger.RemoveListener(MessageId.MAP_UPDATE_PLAYER_INFO, UpdatePlayerInfoPanel);
    }
    // Update is called once per frame
    void UpdatePlayerInfoPanel()
    {
        if (MapMgr.Inited == false)
        {
            return;
        }
        playerInfo = MapMgr.Instance.MyMapPlayer;
        if (iconId != playerInfo.Data.HeadIcon)
        {
            iconId = playerInfo.Data.HeadIcon;
            Head.Load(iconId);
        }

        labName.text = playerInfo.Data.Name;
        sliderHp.value = (float)playerInfo.Data.HP / (float)playerInfo.Data.MaxHP;
        labHp.text = string.Format("{0}/{1}", playerInfo.Data.HP, playerInfo.Data.MaxHP);
        sliderMp.value = (float)playerInfo.Data.MP / (float)playerInfo.Data.MaxMP;
        labMp.text = string.Format("{0}/{1}", playerInfo.Data.MP, playerInfo.Data.MaxMP);
        labFood.text = (MapMgr.Instance.MyMapPlayer.Data.Food.ToString());
        labGold.text = (MapMgr.Instance.MyMapPlayer.Data.Gold.ToString());
        spExp.fillAmount = (float)playerInfo.Data.Exp / (float)playerInfo.Data.MaxExp;
        lblLevel.text = playerInfo.Data.Level.ToString();
    }
    private void UpdateMapInfo()
    {
        if (MapMgr.Inited == false)
        {
            return;
        }
        InstanceTableSetting instanceTable = InstanceTableSettings.Get(MapMgr.Instance.InstanceId);
        lblMapName.text = instanceTable.Name;
        lblMapLayerName.text = I18N.Get(1005001, MapMgr.Instance.CurrentMapLayerData.LayerId);
    }
}
