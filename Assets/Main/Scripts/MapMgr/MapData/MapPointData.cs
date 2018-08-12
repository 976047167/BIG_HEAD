using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
using System;
/// <summary>
/// 坐标点结构体
/// </summary>
public class MapPointData
{
    public int X { get { return Pos.X; } }
    public int Y { get { return Pos.Y; } }
    public int NearByCount;
    public MapCardPos Pos { private set; get; }
    public MapCardType CardType { private set; get; }
    public int DataId { private set; get; }

    public MapCardBase MapCard { private set; get; }
    public MapPointData(MapCardPos cardPos, MapCardType cardType, int dataId)
    {
        Pos = cardPos;
        CardType = cardType;
        DataId = dataId;
    }
    public MapPointData(int x, int y, MapCardType cardType, int dataId) : this(new MapCardPos(x, y), cardType, dataId)
    {

    }
    public MapCardBase CreateMapCard()
    {
        ModelTableSetting model = null;
        switch (CardType)
        {
            case MapCardType.None:
                break;
            case MapCardType.Door:
                MapCard = new MapCardDoor();
                MapCard.Position = Pos;
                MapCard.State = MapCardBase.CardState.Behind;
                ResourceManager.LoadGameObject("MapCard/" + typeof(MapCardDoor).ToString(), LoadAssetSuccessess, LoadAssetFailed, MapCard);
                break;
            case MapCardType.Monster:
                BattleMonsterTableSetting battleMonster = BattleMonsterTableSettings.Get(DataId);
                model = ModelTableSettings.Get(battleMonster.ModelId);
                MapCard = new MapCardDoor();
                MapCard.Position = Pos;
                MapCard.State = MapCardBase.CardState.Behind;
                ResourceManager.LoadGameObject(model.Path, LoadAssetSuccessess, LoadAssetFailed, MapCard);
                break;
            case MapCardType.Shop:
                ShopTableSetting shopTable = ShopTableSettings.Get(DataId);
                model = ModelTableSettings.Get(shopTable.ModelId);
                MapCard = new MapCardDoor();
                MapCard.Position = Pos;
                MapCard.State = MapCardBase.CardState.Behind;
                ResourceManager.LoadGameObject(model.Path, LoadAssetSuccessess, LoadAssetFailed, MapCard);
                break;
            case MapCardType.Box:
                BoxTableSetting boxTable = BoxTableSettings.Get(DataId);
                model = ModelTableSettings.Get(boxTable.ModelId);
                MapCard = new MapCardDoor();
                MapCard.Position = Pos;
                MapCard.State = MapCardBase.CardState.Behind;
                ResourceManager.LoadGameObject(model.Path, LoadAssetSuccessess, LoadAssetFailed, MapCard);
                break;
            case MapCardType.NPC:
                NpcTableSetting npcTable = NpcTableSettings.Get(DataId);
                model = ModelTableSettings.Get(npcTable.ModelId);
                MapCard = new MapCardDoor();
                MapCard.Position = Pos;
                MapCard.State = MapCardBase.CardState.Behind;
                ResourceManager.LoadGameObject(model.Path, LoadAssetSuccessess, LoadAssetFailed, MapCard);
                break;
            default:
                break;
        }

        return MapCard;
    }
    public void Destory()
    {
        if (MapCard != null)
        {
            MapCard.Destory();
        }
    }
    private void LoadAssetFailed(string path, object[] args)
    {
        Debug.LogError("Load [" + path + "] Failed!");
    }

    private void LoadAssetSuccessess(string path, object[] args, GameObject go)
    {
        MapCardBase mapCard = args[0] as MapCardBase;
        go.AddComponent<MapCardHelper>().MapCardData = mapCard;
        mapCard.Init(go);
    }

    public int NearByMaxCount
    {
        get
        {
            int max = 4;
            if (X == 0)
            {
                max--;
            }
            if (X == ConstValue.MAP_WIDTH - 1)
            {
                max--;
            }
            if (Y == 0)
            {
                max--;
            }
            if (Y == ConstValue.MAP_HEIGHT - 1)
            {
                max--;
            }
            return max;
        }
    }
    public bool IsFullNearby
    {
        get { return NearByCount < NearByMaxCount; }
    }
    public bool HasMapCard
    {
        get { return MapCard != null; }
    }
}
