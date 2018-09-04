//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;
using System;

public class CGMapPlayerMoveHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CGMapPlayerMove;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CGMapPlayerMove data = packet as CGMapPlayerMove;
        //处理完数据和逻辑后,发送消息通知客户端
        GCMapPlayerMove mapPlayerMove = new GCMapPlayerMove();
        PBMapPlayerData mapPlayerData = GetSavedData<PBMapPlayerData>(MAP_PLAYER_DATA_KEY);
        PBMapLayerData mapLayerData = GetSavedData<PBMapLayerData>(MAP_LAYER_DATA_KEY);
        mapPlayerMove.PlayerId = mapPlayerData.PlayerData.PlayerId;
        int distance = System.Math.Abs(data.TargetX - mapPlayerData.PlayerPosX) + System.Math.Abs(data.TargetY - mapPlayerData.PlayerPosY);
        if (distance != 1)
        {
            mapPlayerMove.Result = 1;
            SendToClient(MessageId_Receive.GCMapPlayerMove, mapPlayerMove);
            return;
        }
        //位置更新
        mapPlayerData.PlayerPosX = data.TargetX;
        mapPlayerData.PlayerPosY = data.TargetY;
        mapPlayerMove.X = data.TargetX;
        mapPlayerMove.Y = data.TargetY;
        //食物血量
        if (mapPlayerData.PlayerData.Food > 0)
        {
            mapPlayerData.PlayerData.Food--;
            mapPlayerData.PlayerData.Hp = Math.Min(mapPlayerData.PlayerData.Hp + 1, mapPlayerData.PlayerData.MaxHp);
        }
        else
        {
            mapPlayerData.PlayerData.Hp--;
            mapPlayerData.PlayerData.Food = 0;
            if (mapPlayerData.PlayerData.Hp <= 0)
            {
                CGExitInstanceHandler exitInstanceHandler = new CGExitInstanceHandler();
                CGExitInstance exitInstance = new CGExitInstance();
                exitInstance.AccountId = GetSavedData<PBAccountData>(ACCOUNT_DATA_KEY).Uid;
                exitInstance.PlayerId = mapPlayerData.PlayerData.PlayerId;
                exitInstance.Reason = 1;
                exitInstanceHandler.Handle(this, exitInstance);
                return;
            }

        }


        //卡片地图的逻辑触发
        if (mapLayerData.PointState[data.TargetY * mapLayerData.Width + data.TargetX] == 0)
        {
            mapLayerData.PointState[data.TargetY * mapLayerData.Width + data.TargetX] = 1;
        }
        //保存
        SaveData(MAP_PLAYER_DATA_KEY, mapPlayerData);


        mapPlayerMove.Result = 0;
        SendToClient(MessageId_Receive.GCMapPlayerMove, mapPlayerMove);
        GCUpdateMapPlayerData updateMapPlayerData = new GCUpdateMapPlayerData();
        updateMapPlayerData.PlayerId = mapPlayerData.PlayerData.PlayerId;
        updateMapPlayerData.MapPlayerData = mapPlayerData;
        SendToClient(MessageId_Receive.GCUpdateMapPlayerData, updateMapPlayerData);

    }
}
