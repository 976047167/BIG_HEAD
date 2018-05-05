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
    public BattlePlayerData MyPlayerData { get; private set; }
    public List<BattleCardData> Kaku = new List<BattleCardData>();
    /// <summary>
    /// 游戏启动时初始化
    /// </summary>
    public void OnInit()
    {
        MyPlayerData = new BattlePlayerData();
        MyPlayerData.Name = "player No.1";
        MyPlayerData.HP = MyPlayerData.MaxHP = 10;
        MyPlayerData.MP = MyPlayerData.MaxMP = 2;
        MyPlayerData.AP = MyPlayerData.MaxAP = 1;
        MyPlayerData.Level = 1;
        MyPlayerData.SkillId = 0;
        MyPlayerData.HeadIcon = "Head/npc_009";
        MyPlayerData.CardList.Clear();
        MyPlayerData.CardList.Add(new BattleCardData(1, MyPlayerData));
        MyPlayerData.CardList.Add(new BattleCardData(1, MyPlayerData));
        MyPlayerData.CardList.Add(new BattleCardData(1, MyPlayerData));
        MyPlayerData.CardList.Add(new BattleCardData(1, MyPlayerData));
        MyPlayerData.CardList.Add(new BattleCardData(2, MyPlayerData));
        MyPlayerData.CardList.Add(new BattleCardData(2, MyPlayerData));
        MyPlayerData.CardList.Add(new BattleCardData(3, MyPlayerData));
        MyPlayerData.CardList.Add(new BattleCardData(4, MyPlayerData));
        MyPlayerData.CardList.Add(new BattleCardData(5, MyPlayerData));
        MyPlayerData.CardList.Add(new BattleCardData(6, MyPlayerData));
        MyPlayerData.CardList.Add(new BattleCardData(7, MyPlayerData));
        Kaku.Add(new BattleCardData(1, MyPlayerData));
        Kaku.Add(new BattleCardData(3, MyPlayerData));
        Kaku.Add(new BattleCardData(3, MyPlayerData));
        Kaku.Add(new BattleCardData(3, MyPlayerData));
        Kaku.Add(new BattleCardData(2, MyPlayerData));
        Kaku.Add(new BattleCardData(4, MyPlayerData));
        Kaku.Add(new BattleCardData(5, MyPlayerData));
        Kaku.Add(new BattleCardData(6, MyPlayerData));
        Kaku.Add(new BattleCardData(7, MyPlayerData));
        Kaku.Add(new BattleCardData(7, MyPlayerData));
        Kaku.Add(new BattleCardData(7, MyPlayerData));
        Food = 20;
        Coin = 20;
    }
    
}
