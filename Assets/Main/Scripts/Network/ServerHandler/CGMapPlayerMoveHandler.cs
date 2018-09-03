//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

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
        //食物血量

        //卡片地图的逻辑触发

        //保存
        SaveData(MAP_PLAYER_DATA_KEY, mapPlayerData);


        mapPlayerMove.Result = 0;
        SendToClient(MessageId_Receive.GCMapPlayerMove, mapPlayerMove);
    }
}
