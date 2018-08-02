using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
using BigHead.protocol;

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
    public float DialogSpeed;

    public MyPlayer MyPlayer { get; private set; }
    public AccountData AccountData { get; private set; }
    public PlayerData PlayerData { get; private set; }
    public PlayerDetailData PlayerDetailData { get; private set; }

    static uint uidIndex = 1;
    /// <summary>
    /// 游戏启动时初始化数据表格
    /// </summary>
    public void OnInit()
    {
        AppSettings.SettingsManager.AllSettingsReload();


        DialogSpeed = 0.5f;
    }

    public void InitAccount(PBAccountData accountData)
    {
        AccountData = new AccountData();
        AccountData.Diamonds = accountData.Diamonds;
        AccountData.Uid = accountData.Uid;
        AccountData.VipLevel = accountData.VipLevel;
        AccountData.Recharge = accountData.Recharge;

    }

    public void InitPlayer(PBPlayerData playerData)
    {
        ClassCharacterTableSetting characterData = ClassCharacterTableSettings.Get(playerData.CharacterId);
        if (characterData == null)
        {
            return;
        }

        MyPlayer = new MyPlayer();
        PlayerData = MyPlayer.Data;
        MyPlayer.Data.Level = playerData.Level;

        MyPlayer.Data.ClassData = new ClassData(playerData.CharacterId);
        LevelTableSetting levelData = LevelTableSettings.Get(MyPlayer.Data.Level);
        if (levelData == null)
        {
            return;
        }
        MyPlayer.Data.Food  = playerData.Food;
        MyPlayer.Data.MaxFood = levelData.Food[(int)MyPlayer.Data.ClassData.Type];
        //MyPlayer.Data.Coin = AccountData.Gold;
        MyPlayer.Data.Name = playerData.Name;
        MyPlayer.Data.HP = MyPlayer.Data.MaxHP = levelData.HP[(int)MyPlayer.Data.ClassData.Type];
        MyPlayer.Data.MP = MyPlayer.Data.MaxMP = levelData.MP[(int)MyPlayer.Data.ClassData.Type];
        //MyPlayer.Data.AP = MyPlayer.Data.MaxAP = 1;

        MyPlayer.Data.HeadIcon = characterData.IconID;
        PlayerDetailData = MyPlayer.DetailData;
        PlayerDetailData.Deck = new Deck();
        for (int i = 0; i < characterData.DefaultCardList.Count; i++)
        {
            NormalCard normalCard = new NormalCard(characterData.DefaultCardList[i], uidIndex++);
            MyPlayer.Data.CardList.Add(normalCard);
            PlayerDetailData.Kaku.Add(normalCard);
            PlayerDetailData.Deck.AddCard(normalCard);
        }

    }

}
