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
    public BattlePlayerData OppPlayerData { get; private set; }
    public List<BattleCardData> Kaku = new List<BattleCardData>();
    /// <summary>
    /// 游戏启动时初始化
    /// </summary>
    public void OnInit()
    {
        MyPlayerData = new BattlePlayerData();
        MyPlayerData.HP = MyPlayerData.MaxHP = 10;
        MyPlayerData.MP = MyPlayerData.MaxMP = 2;
        MyPlayerData.AP = MyPlayerData.MaxAP = 1;
        MyPlayerData.Level = 1;
        MyPlayerData.SkillId = 0;
        MyPlayerData.HeadIcon = "UITexture/Head/npc_009";
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
        Kaku.Add(new BattleCardData((1)));
        Kaku.Add(new BattleCardData((3)));
        Kaku.Add(new BattleCardData((3)));
        Kaku.Add(new BattleCardData((3)));
        Kaku.Add(new BattleCardData((2)));
        Kaku.Add(new BattleCardData((4)));
        Kaku.Add(new BattleCardData((5)));
        Kaku.Add(new BattleCardData((6)));
        Kaku.Add(new BattleCardData((7)));
        Kaku.Add(new BattleCardData((7)));
        Kaku.Add(new BattleCardData((7)));
        Food = 20;
        Coin = 20;
    }
    public void SetOppData(int monsterId)
    {
        BattleMonsterTableSetting monster = BattleMonsterTableSettings.Get(monsterId);
        if (monster==null)
        {
            Debug.LogError("怪物表格配置错误");
            return;
        }
        OppPlayerData = new BattlePlayerData();
        OppPlayerData.HP = monster.HP;
        OppPlayerData.MaxHP = monster.MaxHp;
        OppPlayerData.MP = monster.MP;
        OppPlayerData.MaxMP = monster.MaxMP;
        OppPlayerData.AP = monster.AP;
        OppPlayerData.MaxAP = monster.MaxAP;
        OppPlayerData.Level = monster.Level;
        OppPlayerData.HeadIcon = monster.Icon;
        for (int i = 0; i < monster.BattleCards.Count; i++)
        {
            OppPlayerData.CardList.Add(new BattleCardData(monster.BattleCards[i]));
        }
        //TODO: Buff Equip
    }
}
