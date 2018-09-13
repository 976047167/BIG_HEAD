using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
using System;
using BigHead.protocol;

public class MapPlayer
{
    protected GameObject m_gameObject;
    protected MapPlayerData m_Data;
    protected int m_InstanceId;
    public MapPlayerData Data { get { return m_Data; } }
    public GameObject PlayerGO { get { return m_gameObject; } }
    /// <summary>
    /// 场景ID
    /// </summary>
    public int InstanceId { get { return m_InstanceId; } }
    private Player m_Player;
    public Player Player { get { return m_Player; } }
    public MapCardPos CurPos { protected set; get; }

    public bool LoadedModel { get; protected set; }

    public MapPlayer(Player player)
    {
        m_Player = player;
        LoadedModel = false;
        if (m_Player is MyPlayer)
        {
            m_Data = new MapPlayerData(m_Player.Data, (m_Player as MyPlayer).DetailData);
        }
        else
            m_Data = new MapPlayerData(m_Player.Data, null);
    }
    public void Update(PBMapPlayerData mapPlayerData)
    {
        CurPos = new MapCardPos(mapPlayerData.PlayerPosX, mapPlayerData.PlayerPosY);
        if (m_Data.Id == MapMgr.Instance.MyMapPlayer.Data.Id && m_Data.HP > mapPlayerData.PlayerData.Hp && mapPlayerData.PlayerData.Food == 0)
        {
            //食物没有了，扣血
            Messenger.Broadcast(MessageId.MAP_PLAYER_NO_FOOD_DAMAGE);
        }
        m_InstanceId = mapPlayerData.InstanceId;
        m_Data.Update(mapPlayerData);
        if (m_Data.HP <= 0)
        {
            Messenger.BroadcastAsync<ulong>(MessageId.MAP_PLAYER_DEAD, m_Data.Id);
        }
    }
    public void CreateModel(MapCardPos pos = null)
    {
        if (CurPos == null)
        {
            CurPos = pos;
        }
        ResourceManager.LoadGameObject(ModelTableSettings.Get(ClassCharacterTableSettings.Get(m_Data.ClassData.CharacterID).ModelID).Path, LoadPlayerSuccess,
            (str, obj) => { Debug.LogError("Load player Failed!"); }
            );
    }
    void LoadPlayerSuccess(string path, object[] userData, GameObject go)
    {
        m_gameObject = go;
        m_gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        m_gameObject.transform.position = MapMgr.Instance.GetTransfromByPos(CurPos);
        LoadedModel = true;
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
            Data.Gold += rewardTable.gold;
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
        Messenger.Broadcast(MessageId.MAP_UPDATE_PLAYER_INFO);
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
        Data.MaxExp = levelTable.Exp[(int)Data.ClassData.Type];
        Messenger.Broadcast(MessageId.MAP_UPDATE_PLAYER_EXP);
    }
    /// <summary>
    /// 保存到全局角色
    /// </summary>
    public void Save()
    {
        m_Player.Data.Level = Data.Level;
        m_Player.Data.Exp = Data.Exp;
        m_Player.Data.MaxExp = Data.MaxExp;
    }
}
