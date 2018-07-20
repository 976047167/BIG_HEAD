using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
using System;

public class MapPlayer
{
    protected GameObject m_gameObject;
    protected MapPlayerData m_Data;
    public MapPlayerData Data { get { return m_Data; } }
    public GameObject PlayerGO { get { return m_gameObject; } }

    private Player m_Player;
    public Player Player { get { return m_Player; } }
    public MapCardPos CurPos { protected set; get; }

    public MapPlayer(Player player)
    {
        m_Player = player;
        m_Data = new MapPlayerData(m_Player.Data);
    }

    public void CreateModel(MapCardPos pos)
    {
        CurPos = pos;
        ResourceManager.LoadGameObject(CharacterModelTableSettings.Get(ClassCharacterTableSettings.Get(m_Data.ClassData.CharacterID).ModelID).Path, LoadPlayerSuccess,
            (str, obj) => { Debug.LogError("Load player Failed!"); }
            );
    }
    void LoadPlayerSuccess(string path, object[] userData, GameObject go)
    {
        m_gameObject = go;
        m_gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        m_gameObject.transform.position = MapMgr.Instance.GetTransfromByPos(CurPos);
    }

    public void MoveTo(MapCardPos pos)
    {
        if (MapMgr.Instance.GetMapCard(CurPos.X, CurPos.Y) != null)
        {
            MapMgr.Instance.GetMapCard(CurPos.X, CurPos.Y).PlayerExit();
        }
        Vector3 direction = (new Vector3(pos.X - CurPos.X, 0f, pos.Y - CurPos.Y)).normalized;
        CurPos = pos;
        TweenPosition.Begin(m_gameObject, 0.5f, MapMgr.Instance.GetTransfromByPos(pos), true);
        //Quaternion quaternion = Quaternion.FromToRotation(m_gameObject.transform.forward, direction);
        m_gameObject.transform.localRotation = Quaternion.LookRotation(direction);
        MapCardBase mapcard = MapMgr.Instance.GetMapCard(pos.X, pos.Y);
        if (mapcard != null)
        {
            mapcard.State = MapCardBase.CardState.Front;
            mapcard.PlayerEnter();
        }
    }

    public void AddReward(int rewardId)
    {
        RewardTableSetting rewardTable = RewardTableSettings.Get(rewardId);
        if (rewardTable != null)
        {
            AddExp(rewardTable.exp);
            Data.Coin += rewardTable.gold;
            Data.Diamond += rewardTable.diamond;
            for (int i = 0; i < rewardTable.ItemList.Count; i++)
            {
                //卡牌类型(攻击0,装备1,法术2,消耗品3)
                ItemTableSetting itemTable = ItemTableSettings.Get(rewardTable.ItemList[i]);
                if (itemTable == null)
                {
                    Debug.LogError("物品不存在=" + rewardTable.ItemList[i]);
                    continue;
                }
                //TODO: 瞎jb写的
                switch ((ItemType)itemTable.Type)
                {
                    case ItemType.Card:
                        Data.CardList.Add(new NormalCard(itemTable.Id));
                        break;
                    case ItemType.Equip:
                        Data.EquipList.Add(new NormalCard(itemTable.Id));
                        break;
                    case ItemType.Skill:
                        Data.BuffList.Add(new NormalCard(itemTable.Id));
                        break;
                    case ItemType.Consumable:
                        Data.ItemList.Add(new ItemData(itemTable.Id));
                        break;
                    default:
                        break;
                }

            }

        }
    }
    public void AddExp(int exp)
    {
        LevelTableSetting levelTable = LevelTableSettings.Get(Data.Level);
        Data.Exp += exp;
        while (Data.Exp >= levelTable.Exp[(int)Data.ClassData.Type])
        {
            Data.Level++;
            Data.Exp -= levelTable.Exp[(int)Data.ClassData.Type];
            levelTable = LevelTableSettings.Get(Data.Level);
        }
    }
    public void Save()
    {
        m_Player.Data.Level = Data.Level;
        m_Player.Data.Exp = Data.Exp;
    }
}
