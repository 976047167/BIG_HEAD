//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;
using AppSettings;

public class CGCreatePlayerHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CGCreatePlayer;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CGCreatePlayer data = packet as CGCreatePlayer;
        //处理完数据和逻辑后,发送消息通知客户端
        GCGetUserData userData = new GCGetUserData();
        userData.Uid = data.UserId;
        ClassCharacterTableSetting characterData = ClassCharacterTableSettings.Get(data.CharacterId);
        if (characterData == null)
        {
            return;
        }
        userData.AccountData = GetSavedData<PBAccountData>(ACCOUNT_DATA_KEY);
        if (userData.AccountData == null)
        {
            userData.AccountData = new PBAccountData();
            userData.AccountData.Uid = data.UserId;
            userData.AccountData.Recharge = 0;
            userData.AccountData.Diamonds = 0;
            userData.AccountData.VipLevel = 0;
            SaveData(ACCOUNT_DATA_KEY, userData.AccountData);
        }
        userData.PlayerData = GetSavedData<PBPlayerData>(PLAYER_DATA_KEY);
        if (userData.PlayerData == null)
        {
            userData.PlayerData = new PBPlayerData();
            userData.PlayerData.PlayerId = 1;
            userData.PlayerData.Level = 1;
            userData.PlayerData.CharacterId = data.CharacterId;
            ClassData classData = new ClassData(data.CharacterId);
            LevelTableSetting levelData = LevelTableSettings.Get(userData.PlayerData.Level);
            if (levelData == null)
            {
                return;
            }
            userData.PlayerData.Food = 0;
            userData.PlayerData.Gold = 0;
            //userData.PlayerData.Name = data.Name;
            userData.PlayerData.Name = I18N.Get(characterData.Name);
            userData.PlayerData.Hp = userData.PlayerData.MaxHp = levelData.HP[(int)classData.Type];
            userData.PlayerData.Mp = userData.PlayerData.MaxMp = levelData.MP[(int)classData.Type];
            userData.PlayerData.HeadIcon = characterData.IconID;
            SaveData(PLAYER_DATA_KEY, userData.PlayerData);

        }
        userData.PlayerDetailData = GetSavedData<PBPlayerDetailData>(PLAYER_DETAIL_DATA);
        if (userData.PlayerDetailData == null)
        {
            //PlayerDetailData = MyPlayer.DetailData;
            //PlayerDetailData.Deck = new Deck();
            //for (int i = 0; i < characterData.DefaultCardList.Count; i++)
            //{
            //    NormalCard normalCard = new NormalCard(characterData.DefaultCardList[i], uidIndex++);
            //    MyPlayer.Data.CardList.Add(normalCard);
            //    PlayerDetailData.Kaku.Add(normalCard);
            //    PlayerDetailData.Deck.AddCard(normalCard);
            //}
            userData.PlayerDetailData = new PBPlayerDetailData();
            userData.PlayerDetailData.Cards.AddRange(characterData.DefaultCardList);
            PBDeck deck = new PBDeck();
            deck.Cards.AddRange(characterData.DefaultCardList);
            userData.PlayerDetailData.UsingDeckIndex = deck.Index = 1;
            deck.MaxCount = 10;
            deck.Name = "默认卡组";
            userData.PlayerDetailData.Decks.Add(deck);

            SaveData(PLAYER_DETAIL_DATA, userData.PlayerDetailData);
        }
        SendToClient(MessageId_Receive.GCGetUserData, userData);
    }
}
