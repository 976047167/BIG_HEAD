using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private BattlePlayerData playerInfo = new BattlePlayerData();

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
        playerInfo = Game.DataManager.MyPlayerData;
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Head.mainTexture == null || Head.mainTexture.name != playerInfo.HeadIcon)
        {
            Head.Load(playerInfo.HeadIcon);
        }
        labName.text = playerInfo.Name;
        sliderHp.value = playerInfo.HP / playerInfo.MaxHP;
        labHp.text = string.Format("{0}/{1}", playerInfo.HP, playerInfo.MaxHP);
        sliderHp.value = playerInfo.MP / playerInfo.MaxMP;
        labMp.text = string.Format("{0}/{1}", playerInfo.MP, playerInfo.MaxMP);
        labFood.text = (Game.DataManager.Food.ToString());
        labGold.text = (Game.DataManager.Coin.ToString());
       }
}
