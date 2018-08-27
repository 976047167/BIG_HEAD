//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;
using AppSettings;
using UnityEngine;

public class CGEnterInstanceHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CGEnterInstance;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CGEnterInstance data = packet as CGEnterInstance;
        //处理完数据和逻辑后,发送消息通知客户端
        GCEnterInstance response = new GCEnterInstance();
        InstanceTableSetting instanceTable = InstanceTableSettings.Get(data.InstanceId);
        if (instanceTable == null)
        {
            response.Result = 1;
            SendToClient(MessageId_Receive.GCEnterInstance, response);
            return;
        }
        response.InstanceId = data.InstanceId;
        response.Result = 0;
        response.MapPlayerData = new PBMapPlayerData();
        response.MapPlayerData.InstanceId = data.InstanceId;
        response.MapPlayerData.PlayerPosX = Random.Range(0, instanceTable.Width);
        response.MapPlayerData.PlayerPosY = Random.Range(0, instanceTable.Height);
        response.MapPlayerData.PlayerData = GetSavedData<PBPlayerData>(PLAYER_DATA_KEY);
        response.MapPlayerData.PlayerModelId = ClassCharacterTableSettings.Get(response.MapPlayerData.PlayerData.CharacterId).ModelID;
        PBPlayerDetailData playerDetailData = GetSavedData<PBPlayerDetailData>(PLAYER_DETAIL_DATA);
        for (int i = 0; i < playerDetailData.Decks.Count; i++)
        {
            if (playerDetailData.Decks[i].Index == data.UsingDeck)
            {
                response.MapPlayerData.Deck = playerDetailData.Decks[i];
                break;
            }
        }
        response.MapPlayerData.Items.AddRange(playerDetailData.Items);
        response.MapPlayerData.Equips.AddRange(playerDetailData.Equips);
        response.MapPlayerData.Buffs.AddRange(playerDetailData.Buffs);
        SaveData(MAP_PLAYER_DATA_KEY, response.MapPlayerData);
        SendToClient(MessageId_Receive.GCEnterInstance, response);
    }
}
