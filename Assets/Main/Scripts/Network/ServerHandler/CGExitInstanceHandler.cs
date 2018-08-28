//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class CGExitInstanceHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CGExitInstance;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CGExitInstance data = packet as CGExitInstance;
        //处理完数据和逻辑后,发送消息通知客户端
        PBAccountData accountData = GetSavedData<PBAccountData>(ACCOUNT_DATA_KEY);
        PBMapPlayerData mapPlayerData = GetSavedData<PBMapPlayerData>(MAP_PLAYER_DATA_KEY);
        PBPlayerData playerData = GetSavedData<PBPlayerData>(PLAYER_DATA_KEY);
        PBPlayerDetailData playerDetailData = GetSavedData<PBPlayerDetailData>(PLAYER_DETAIL_DATA);

        AddExp(playerData, mapPlayerData.AddedExp);
        playerDetailData.Items.Clear();
        playerDetailData.Items.Add(mapPlayerData.Items);
        playerData.Gold += mapPlayerData.PlayerData.Gold;
        playerData.Food += mapPlayerData.PlayerData.Food;

        SaveData(MAP_PLAYER_DATA_KEY, null);
        SaveData(PLAYER_DATA_KEY, playerData);
        SaveData(PLAYER_DETAIL_DATA, playerDetailData);


    }
}
