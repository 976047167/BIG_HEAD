//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;
using AppSettings;
using System.Collections.Generic;
using UnityEngine;

public class CGGetMapLayerDataHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CGGetMapLayerData;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CGGetMapLayerData data = packet as CGGetMapLayerData;
        //处理完数据和逻辑后,发送消息通知客户端
        GCGetMapLayerData response = new GCGetMapLayerData();
        InstanceTableSetting instanceTable = InstanceTableSettings.Get(data.InstanceId);
        //层数从第一层开始
        InstanceLayerTableSetting layerTableSetting = InstanceLayerTableSettings.Get(instanceTable.Layers[data.LayerIndex - 1]);


        MapLayerData layerData = new MapLayerData(data.LayerIndex, "", instanceTable.Width, instanceTable.Height);
        int[,] mapType = new int[instanceTable.Width, instanceTable.Height];
        int[,] mapID = new int[instanceTable.Width, instanceTable.Height];
        List<MapCardPos> mapCards = new List<MapCardPos>();
        int cardCount = Random.Range(layerTableSetting.MinCount, layerTableSetting.MaxCount + 1);
        //先确定出生点
        {
            MapCardPos playerDoorCard = new MapCardPos(data.PlayerX, data.PlayerY);
            mapCards.Add(playerDoorCard);
            layerData[playerDoorCard] = new MapCardBase();
            mapType[playerDoorCard.X, playerDoorCard.Y] = (int)MapCardType.Door;
        }
        for (int i = 1; i < cardCount; i++)
        {
            MapCardPos pos = mapCards[Random.Range(0, i)];

            List<MapCardPos> poss = layerData.GetNearEmptyPoss(pos.X, pos.Y);

            if (poss.Count == 0)
            {
                i--;
                continue;
            }
            int count = Random.Range(0, poss.Count - 1);
            pos = poss[count];
            mapCards.Add(pos);
            layerData[pos] = new MapCardBase();
            mapType[pos.X, pos.Y] = Random.Range((int)MapCardType.Monster, (int)MapCardType.NPC + 1);

        }
        //创建出口
        {
            MapCardPos pos = mapCards[Random.Range(0, cardCount)];

            List<MapCardPos> poss = layerData.GetNearEmptyPoss(pos.X, pos.Y);

            while (poss.Count == 0)
            {
                pos = mapCards[Random.Range(0, cardCount)];
                poss = layerData.GetNearEmptyPoss(pos.X, pos.Y);
            }
            int count = Random.Range(0, poss.Count - 1);
            pos = poss[count];
            mapCards.Add(pos);
            layerData[pos] = new MapCardBase();
            mapType[pos.X, pos.Y] = (int)MapCardType.Door;
        }
        for (int i = 0; i < mapCards.Count; i++)
        {
            MapCardPos pos = new MapCardPos(mapCards[i].X, mapCards[i].Y);
            switch ((MapCardType)mapType[pos.X, pos.Y])
            {
                case MapCardType.None:
                    mapID[pos.X, pos.Y] = 0;
                    break;
                case MapCardType.Door:
                    mapID[pos.X, pos.Y] = 0;
                    break;
                case MapCardType.Monster:
                    mapID[pos.X, pos.Y] = layerTableSetting.Monsters[Random.Range(0, layerTableSetting.Monsters.Count)];
                    break;
                case MapCardType.Shop:
                    mapID[pos.X, pos.Y] = layerTableSetting.Shop[Random.Range(0, layerTableSetting.Shop.Count)];
                    break;
                case MapCardType.Box:
                    mapID[pos.X, pos.Y] = layerTableSetting.Box[Random.Range(0, layerTableSetting.Box.Count)];
                    break;
                case MapCardType.NPC:
                    mapID[pos.X, pos.Y] = layerTableSetting.NPC[Random.Range(0, layerTableSetting.NPC.Count)];
                    break;
                default:
                    break;
            }
        }
        response.Result = 0;
        response.LayerData = new PBMapLayerData();
        response.LayerData.Height = instanceTable.Width;
        response.LayerData.Width = instanceTable.Height;
        response.LayerData.Name = string.Format("{0} 第{1}层", instanceTable.Name, data.LayerIndex);
        for (int i = 0; i < instanceTable.Width; i++)
        {
            for (int j = 0; j < instanceTable.Height; j++)
            {
                response.LayerData.PointTypes.Add(mapType[i, j]);
                response.LayerData.PointIds.Add(mapID[i, j]);
            }
        }
        SendToClient(MessageId_Receive.GCGetMapLayerData, response);
    }
}
