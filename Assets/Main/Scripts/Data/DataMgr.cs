using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class DataMgr
{
    public DataMgr()
    {
        
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
    public float DialogSpeed;

    public MyPlayer MyPlayer { get; private set; }
    public AccountData AccountData { get; private set; }
    public PlayerData PlayerData { get; private set; }
    public PlayerDetailData PlayerDetailData { get; private set; }

    static uint uidIndex = 1;
    /// <summary>
    /// 游戏启动时初始化
    /// </summary>
    public void OnInit()
    {
        AppSettings.SettingsManager.AllSettingsReload();
        AccountData = new AccountData();
        AccountData.Gold = 100;
        AccountData.Diamonds = 100;
        AccountData.Uid = 0x1;

        MyPlayer = new MyPlayer();
        PlayerData = MyPlayer.Data;
        MyPlayer.Data.Name = "大头";
        MyPlayer.Data.Name = "player No.1";
        MyPlayer.Data.HP = MyPlayer.Data.MaxHP = 10;
        MyPlayer.Data.MP = MyPlayer.Data.MaxMP = 2;
        //MyPlayer.Data.AP = MyPlayer.Data.MaxAP = 1;
        MyPlayer.Data.Level = 1;
        MyPlayer.Data.HeadIcon = 10008;
        MyPlayer.Data.ClassData = new ClassData(3);
        MyPlayer.Data.CardList.Add(new NormalCard(1, uidIndex++));
        MyPlayer.Data.CardList.Add(new NormalCard(1, uidIndex++));
        MyPlayer.Data.CardList.Add(new NormalCard(1, uidIndex++));
        MyPlayer.Data.CardList.Add(new NormalCard(1, uidIndex++));
        MyPlayer.Data.CardList.Add(new NormalCard(2, uidIndex++));
        MyPlayer.Data.CardList.Add(new NormalCard(2, uidIndex++));
        MyPlayer.Data.CardList.Add(new NormalCard(3, uidIndex++));
        MyPlayer.Data.CardList.Add(new NormalCard(4, uidIndex++));
        MyPlayer.Data.CardList.Add(new NormalCard(5, uidIndex++));
        MyPlayer.Data.CardList.Add(new NormalCard(6, uidIndex++));
        MyPlayer.Data.CardList.Add(new NormalCard(7, uidIndex++));




        PlayerDetailData = MyPlayer.DetailData;
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


        Deck tmpDeck = new Deck();
        tmpDeck.AddCard(1);
        tmpDeck.AddCard(1);
        tmpDeck.AddCard(2);
        tmpDeck.AddCard(3);
        PlayerDetailData.Deck=tmpDeck;









        Food = 20;
        Coin = 20;
        DialogSpeed = 0.5f;
    }

}
