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

        PBPlayerData playerData = GetSavedData<PBPlayerData>(PLAYER_DATA_KEY);
        response.MapPlayerData.PlayerData = new PBPlayerData();
        response.MapPlayerData.PlayerData.PlayerId = playerData.PlayerId;
        response.MapPlayerData.PlayerData.Hp = playerData.Hp;
        response.MapPlayerData.PlayerData.MaxHp = playerData.MaxHp;
        response.MapPlayerData.PlayerData.Mp = playerData.Mp;
        response.MapPlayerData.PlayerData.MaxMp = playerData.MaxMp;
        response.MapPlayerData.PlayerData.Gold = 0;
        response.MapPlayerData.PlayerData.Exp = 0;
        response.MapPlayerData.PlayerData.Level = 1;
        response.MapPlayerData.PlayerData.CharacterId = playerData.CharacterId;
        response.MapPlayerData.PlayerData.HeadIcon = playerData.HeadIcon;
        response.MapPlayerData.PlayerData.MapSkillId = playerData.MapSkillId;
        response.MapPlayerData.PlayerData.BattleSkillId = playerData.BattleSkillId;
        //response.MapPlayerData.PlayerData.Equips
        if (playerData.Food > instanceTable.FoodMax)
        {
            response.MapPlayerData.PlayerData.Food = response.MapPlayerData.PlayerData.MaxFood = instanceTable.FoodMax;
            playerData.Food = playerData.Food - instanceTable.FoodMax;
        }
        else
        {
            response.MapPlayerData.PlayerData.Food = response.MapPlayerData.PlayerData.MaxFood = playerData.Food;
            playerData.Food = 0;
        }



        //response.MapPlayerData.PlayerData
        response.MapPlayerData.PlayerModelId = ClassCharacterTableSettings.Get(response.MapPlayerData.PlayerData.CharacterId).ModelID;
        PBPlayerDetailData playerDetailData = GetSavedData<PBPlayerDetailData>(PLAYER_DETAIL_DATA);
        bool exist = false;
        for (int i = 0; i < playerDetailData.Decks.Count; i++)
        {
            if (playerDetailData.Decks[i].Index == data.Deck.Index)
            {
                //更新已有的卡组
                playerDetailData.Decks[i] = data.Deck;
                exist = true;
                break;
            }
        }
        //添加新卡组
        if (exist == false)
        {
            playerDetailData.Decks.Add(data.Deck);
        }
        response.MapPlayerData.Deck = data.Deck;
        response.MapPlayerData.Items.Add(data.Items);
        response.MapPlayerData.Equips.AddRange(data.Equips);
        response.MapPlayerData.Buffs.AddRange(data.Buffs);
        for (int i = 0; i < data.Items.Count; i++)
        {
            playerDetailData.Items.Remove(data.Items[i]);
        }

        SaveData(PLAYER_DATA_KEY, playerData);
        SaveData(PLAYER_DETAIL_DATA, playerDetailData);
        SaveData(MAP_PLAYER_DATA_KEY, response.MapPlayerData);
        SendToClient(MessageId_Receive.GCEnterInstance, response);
    }
}
