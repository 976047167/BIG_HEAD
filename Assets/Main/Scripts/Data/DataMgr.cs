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
    public void UpdateAccount(PBAccountData accountData)
    {
        if (accountData == null)
        {
            return;
        }
        if (AccountData.Uid != accountData.Uid)
        {
            return;
        }
        AccountData.Diamonds = accountData.Diamonds;
        AccountData.VipLevel = accountData.VipLevel;
        AccountData.Recharge = accountData.Recharge;

    }
    public void InitPlayer(PBPlayerData playerData, PBPlayerDetailData playerDetailData)
    {
        ClassCharacterTableSetting characterData = ClassCharacterTableSettings.Get(playerData.CharacterId);
        if (characterData == null)
        {
            return;
        }

        MyPlayer = new MyPlayer();
        PlayerData = MyPlayer.Data;
        MyPlayer.Data.Update(playerData);
        MyPlayer.DetailData.Update(playerDetailData);
        PlayerDetailData = MyPlayer.DetailData;


    }
    public void UpdatePlayer(PBPlayerData playerData, PBPlayerDetailData playerDetailData)
    {
        ClassCharacterTableSetting characterData = ClassCharacterTableSettings.Get(playerData.CharacterId);
        if (characterData == null)
        {
            return;
        }
        if (playerData != null)
        {
            MyPlayer.Data.Update(playerData);
        }
        if (playerDetailData != null)
        {
            MyPlayer.DetailData.Update(playerDetailData);
        }


    }

}
