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
    public int MaxFood;
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


        DialogSpeed = 0.5f;
    }

    public void InitAccount()
    {
        AccountData = new AccountData();
        AccountData.Gold = 100;
        AccountData.Diamonds = 100;
        AccountData.Uid = 0x1;

    }

    public void InitPlayer(int characterId)
    {
        ClassCharacterTableSetting characterData = ClassCharacterTableSettings.Get(characterId);
        if (characterData == null)
        {
            return;
        }

        MyPlayer = new MyPlayer();
        PlayerData = MyPlayer.Data;
        MyPlayer.Data.Level = 1;

        MyPlayer.Data.ClassData = new ClassData(characterId);
        LevelTableSetting levelData = LevelTableSettings.Get(MyPlayer.Data.Level);
        if (levelData == null)
        {
            return;
        }
        Food = MaxFood = levelData.Food[MyPlayer.Data.Level];
        Coin = AccountData.Gold;
        MyPlayer.Data.Name = I18N.Get(characterData.Name);
        MyPlayer.Data.HP = MyPlayer.Data.MaxHP = levelData.HP[(int)MyPlayer.Data.ClassData.Type];
        MyPlayer.Data.MP = MyPlayer.Data.MaxMP = levelData.MP[(int)MyPlayer.Data.ClassData.Type];
        //MyPlayer.Data.AP = MyPlayer.Data.MaxAP = 1;

        MyPlayer.Data.HeadIcon = characterData.IconID;
        PlayerDetailData = MyPlayer.DetailData;
        PlayerDetailData.Deck = new Deck();
        for (int i = 0; i < characterData.DefaultCardList.Count; i++)
        {
            MyPlayer.Data.CardList.Add(new NormalCard(characterData.DefaultCardList[i], uidIndex++));
            PlayerDetailData.Kaku.Add(new NormalCard(characterData.DefaultCardList[i]));
            PlayerDetailData.Deck.AddCard(characterData.DefaultCardList[i]);
        }

    }

}
