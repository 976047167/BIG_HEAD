//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;
using AppSettings;

public class CLGetUserDataHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CLGetUserData;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        //处理完数据和逻辑后,发送消息通知客户端
        CLGetUserData data = packet as CLGetUserData;
        LCGetUserData userData = new LCGetUserData();
        userData.Uid = data.UserId;
        userData.PlayerData = new PBPlayerData();
        userData.PlayerData.Level = 1;
        LevelTableSetting levelData = LevelTableSettings.Get(userData.PlayerData.Level);
        if (levelData == null)
        {
            return;
        }
        userData.PlayerData.Food = userData.PlayerData.MaxFood = levelData.Food[MyPlayer.Data.Level];
        MyPlayer.Data.Coin = AccountData.Gold;
        MyPlayer.Data.Name = I18N.Get(characterData.Name);
        MyPlayer.Data.HP = MyPlayer.Data.MaxHP = levelData.HP[(int)MyPlayer.Data.ClassData.Type];
        MyPlayer.Data.MP = MyPlayer.Data.MaxMP = levelData.MP[(int)MyPlayer.Data.ClassData.Type];
        //MyPlayer.Data.AP = MyPlayer.Data.MaxAP = 1;

        MyPlayer.Data.HeadIcon = characterData.IconID;
        PlayerDetailData = MyPlayer.DetailData;
        PlayerDetailData.Deck = new Deck();
        for (int i = 0; i < characterData.DefaultCardList.Count; i++)
        {
            NormalCard normalCard = new NormalCard(characterData.DefaultCardList[i], uidIndex++);
            MyPlayer.Data.CardList.Add(normalCard);
            PlayerDetailData.Kaku.Add(normalCard);
            PlayerDetailData.Deck.AddCard(normalCard);
        }
        SendToClient(MessageId_Receive.LCGetUserData, userData);
    }
}
