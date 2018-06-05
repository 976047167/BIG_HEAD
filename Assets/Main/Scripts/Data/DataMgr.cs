using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class DataMgr
{
    public DataMgr()
    {
        AppSettings.SettingsManager.AllSettingsReload();
    }
    //static DataMgr instance = null;
    //public static DataMgr Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = new DataMgr();
    //            //instance.OnInit();
    //        }
    //        return instance;
    //    }

    //}
    public int Food;
    public int Coin;
    public BattlePlayer MyPlayer { get; private set; }
    public AccountData AccountData { get; private set; }

    public PlayerDetailData PlayerDetailData { get; private set; }
 
    /// <summary>
    /// 游戏启动时初始化
    /// </summary>
    public void OnInit()
    {
        AccountData = new AccountData();
        AccountData.Gold = 100;
        AccountData.Diamonds = 100;
        AccountData.Uid = 0x1;





        PlayerDetailData = new PlayerDetailData();
        PlayerDetailData.Kaku.Add(new NormalCard(1));
        PlayerDetailData.Kaku.Add(new NormalCard(2));
        PlayerDetailData.Kaku.Add(new NormalCard(2));
        PlayerDetailData.Kaku.Add(new NormalCard(3));
        PlayerDetailData.Kaku.Add(new NormalCard(3));
        PlayerDetailData.Kaku.Add(new NormalCard(4));
        PlayerDetailData.Kaku.Add(new NormalCard(4));
        PlayerDetailData.Kaku.Add(new NormalCard(5));
        PlayerDetailData.Kaku.Add(new NormalCard(5));
        PlayerDetailData.Kaku.Add(new NormalCard(7));
        PlayerDetailData.Kaku.Add(new NormalCard(7));


        uint uid = 124111315;
        Deck tmpDeck = new Deck(uid);
        tmpDeck.SetDeckName("卡组1");
        tmpDeck.SetClassType(ClassType.Warriop);
        tmpDeck.AddCard(1);
        tmpDeck.AddCard(1);
        tmpDeck.AddCard(2);
        tmpDeck.AddCard(3);
        PlayerDetailData.Decks.Add(uid, tmpDeck);









        Food = 20;
        Coin = 20;
    }

}
