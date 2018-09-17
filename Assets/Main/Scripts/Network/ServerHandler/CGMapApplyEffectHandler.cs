//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;
using AppSettings;

public class CGMapApplyEffectHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CGMapApplyEffect;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CGMapApplyEffect data = packet as CGMapApplyEffect;
        //处理完数据和逻辑后,发送消息通知客户端
        PBAccountData accountData = GetSavedData<PBAccountData>(ACCOUNT_DATA_KEY);
        PBMapPlayerData mapPlayerData = GetSavedData<PBMapPlayerData>(MAP_PLAYER_DATA_KEY);
        PBPlayerData playerData = GetSavedData<PBPlayerData>(PLAYER_DATA_KEY);
        PBPlayerDetailData playerDetailData = GetSavedData<PBPlayerDetailData>(PLAYER_DETAIL_DATA_KEY);

        GCMapApplyEffect applyEffect = new GCMapApplyEffect();
        applyEffect.PlayerId = playerData.PlayerId;

        if (data.PlayerId != playerData.PlayerId)
        {
            applyEffect.Result = 1;
            SendToClient(MessageId_Receive.GCMapApplyEffect, applyEffect);
            return;
        }

        switch (data.Action)
        {
            //0结束
            case 0:
                break;
            //1下一个
            case 1:
                break;
            //2选择
            case 2:
                break;
            //3进入战斗
            case 3:
                GCEnterBattle enterBattle = new GCEnterBattle();
                enterBattle.MonsterId = data.Param;
                SendToClient(MessageId_Receive.GCEnterBattle, enterBattle);
                break;
            //4回血
            case 4:
                mapPlayerData.PlayerData.Hp = System.Math.Min(mapPlayerData.PlayerData.Hp + data.Param, mapPlayerData.PlayerData.MaxHp);
                GCUpdateMapPlayerData hpUpdate = new GCUpdateMapPlayerData();
                hpUpdate.PlayerId = mapPlayerData.PlayerData.PlayerId;
                hpUpdate.MapPlayerData = mapPlayerData;
                SaveData(MAP_PLAYER_DATA_KEY, mapPlayerData);
                SendToClient(MessageId_Receive.GCUpdateMapPlayerData, hpUpdate);
                break;
            //5回魔
            case 5:
                mapPlayerData.PlayerData.Mp = System.Math.Min(mapPlayerData.PlayerData.Mp + data.Param, mapPlayerData.PlayerData.MaxMp);
                GCUpdateMapPlayerData mpUpdate = new GCUpdateMapPlayerData();
                mpUpdate.PlayerId = mapPlayerData.PlayerData.PlayerId;
                mpUpdate.MapPlayerData = mapPlayerData;
                SaveData(MAP_PLAYER_DATA_KEY, mapPlayerData);
                SendToClient(MessageId_Receive.GCUpdateMapPlayerData, mpUpdate);
                break;
            //6回粮食
            case 6:
                mapPlayerData.PlayerData.Food = System.Math.Min(mapPlayerData.PlayerData.Food + data.Param, mapPlayerData.PlayerData.MaxFood);
                GCUpdateMapPlayerData foodUpdate = new GCUpdateMapPlayerData();
                foodUpdate.PlayerId = mapPlayerData.PlayerData.PlayerId;
                foodUpdate.MapPlayerData = mapPlayerData;
                SaveData(MAP_PLAYER_DATA_KEY, mapPlayerData);
                SendToClient(MessageId_Receive.GCUpdateMapPlayerData, foodUpdate);
                break;
            //7进入商店
            case 7:
                GCMapEnterShop mapEnterShop = new GCMapEnterShop();
                mapEnterShop.Result = 0;
                mapEnterShop.ShopId = data.Param;
                SendToClient(MessageId_Receive.GCMapEnterShop, mapEnterShop);
                break;
            //8打开宝箱
            case 8:
                GCMapOpenBox openBox = new GCMapOpenBox();
                openBox.Result = 0;
                openBox.BoxId = data.Param;
                SendToClient(MessageId_Receive.GCMapOpenBox, openBox);
                BoxTableSetting boxTable = BoxTableSettings.Get(data.Param);
                //switch (boxTable.RewardId)
                //{
                //    default:
                //        break;
                //}
                break;
            default:
                break;
        }



        applyEffect.Result = 0;
        SendToClient(MessageId_Receive.GCMapApplyEffect, applyEffect);
    }
}
