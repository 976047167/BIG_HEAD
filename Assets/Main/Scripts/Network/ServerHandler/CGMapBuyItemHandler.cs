//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class CGMapBuyItemHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CGMapBuyItem;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CGMapBuyItem data = packet as CGMapBuyItem;
        //处理完数据和逻辑后,发送消息通知客户端
        GCMapGetReward reward = new GCMapGetReward();
        PBAccountData accountData = GetSavedData<PBAccountData>(ACCOUNT_DATA_KEY);
        PBMapPlayerData mapPlayerData = GetSavedData<PBMapPlayerData>(MAP_PLAYER_DATA_KEY);
        reward.AccountId = accountData.Uid;
        reward.PlayerId = mapPlayerData.PlayerData.PlayerId;
        reward.AddedExp = 10;
        reward.OldExp = mapPlayerData.PlayerData.Exp;
        reward.OldLevel = mapPlayerData.PlayerData.Level;
        reward.Diamonds = 1;
        reward.Gold = 2;
        reward.Food = 3;
        reward.Cards.Add(1);
        reward.Equips.Add(5);
        reward.Items.Add(100000);

        SendToClient(MessageId_Receive.GCMapGetReward, reward);
    }
}
