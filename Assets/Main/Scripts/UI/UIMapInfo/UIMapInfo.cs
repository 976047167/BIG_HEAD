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

    private void Awake()
    {
        Head = transform.Find("headFrame/texHead").GetComponent<UITexture>();
        sliderHp = transform.Find("headFrame/sliderHp").GetComponent<UIProgressBar>();
        sliderMp = transform.Find("headFrame/sliderMp").GetComponent<UIProgressBar>();
        labName  = transform.Find("headFrame/labName").GetComponent<UILabel>();
        labHp = transform.Find("headFrame/sliderHp/labHp").GetComponent<UILabel>();
        labMp = transform.Find("headFrame/sliderMp/labMp").GetComponent<UILabel>();
        labGold = transform.Find("headFrame/gold/labgold").GetComponent<UILabel>();
        labFood = transform.Find("headFrame/food/labfood").GetComponent<UILabel>();
        playerInfo = Game.DataManager.MyPlayer;
        Messenger.AddListener(MessageID.MSG_UPDATE_ROLE_INFO_PANEL, UpdatePlayerInfoPanel);

    }

    // Use this for initialization
    void Start()
    {
        
    }
    protected override void OnOpen()
    {
        base.OnOpen();
        UpdatePlayerInfoPanel();
    }
    // Update is called once per frame
    void UpdatePlayerInfoPanel()
    {

         Head.Load(playerInfo.Data.HeadIcon);
        labName.text = playerInfo.Data.Name;
        sliderHp.value = playerInfo.Data.HP / playerInfo.Data.MaxHP;
        labHp.text = string.Format("{0}/{1}", playerInfo.Data.HP, playerInfo.Data.MaxHP);
        sliderHp.value = playerInfo.Data.MP / playerInfo.Data.MaxMP;
        labMp.text = string.Format("{0}/{1}", playerInfo.Data.MP, playerInfo.Data.MaxMP);
        labFood.text = (Game.DataManager.Food.ToString());
        labGold.text = (Game.DataManager.Coin.ToString());
  
}
}
