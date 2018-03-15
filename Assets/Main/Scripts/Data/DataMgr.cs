using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public BattlePlayerData MyPlayerData;
    /// <summary>
    /// 游戏启动时初始化
    /// </summary>
    public void OnInit()
    {
        MyPlayerData = new BattlePlayerData();
        MyPlayerData.HP = MyPlayerData.MaxHP = 10;
        MyPlayerData.MP = MyPlayerData.MaxMP = 2;
        MyPlayerData.AP = MyPlayerData.MaxAP = 1;
        MyPlayerData.CardList.Clear();
        MyPlayerData.CardList.Add(new BattleCardData(1));
        MyPlayerData.CardList.Add(new BattleCardData(1));
        MyPlayerData.CardList.Add(new BattleCardData(1));
        MyPlayerData.CardList.Add(new BattleCardData(1));
        MyPlayerData.CardList.Add(new BattleCardData(2));
        MyPlayerData.CardList.Add(new BattleCardData(2));
        MyPlayerData.CardList.Add(new BattleCardData(3));
        MyPlayerData.CardList.Add(new BattleCardData(4));
        MyPlayerData.CardList.Add(new BattleCardData(5));
        MyPlayerData.CardList.Add(new BattleCardData(6));
        MyPlayerData.CardList.Add(new BattleCardData(7));
        Food = 20;
        Coin = 20;
    }
    
}
