//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;
using AppSettings;

public class CGExitBattleHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CGExitBattle;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CGExitBattle data = packet as CGExitBattle;
        //处理完数据和逻辑后,发送消息通知客户端

        PBAccountData accountData = GetSavedData<PBAccountData>(ACCOUNT_DATA_KEY);
        PBMapPlayerData mapPlayerData = GetSavedData<PBMapPlayerData>(MAP_PLAYER_DATA_KEY);

        //战斗结果
        if (data.PlayerData != null)
        {
            mapPlayerData.PlayerData.Hp = data.PlayerData.Hp;
            mapPlayerData.PlayerData.MaxHp = data.PlayerData.MaxHp;
            mapPlayerData.PlayerData.Mp = data.PlayerData.Mp;
            mapPlayerData.PlayerData.MaxMp = data.PlayerData.MaxMp;
            mapPlayerData.Items.Clear();
            mapPlayerData.Items.Add(data.Items);
        }

        GCMapGetReward reward = null;
        BattleMonsterTableSetting battleMonster = BattleMonsterTableSettings.Get(data.MonsterId);
        switch (data.Reason)
        {
            case 3://对方放弃
            case 0://通关
                reward = GetRewardById(battleMonster.RewardId);
                break;
            case 1://失败
            case 2://逃跑，放弃
                reward = GetRewardById(battleMonster.LoseReward);
                break;
            default:
                break;
        }
        if (reward != null)
        {

            //存奖励
            mapPlayerData.AddedExp += reward.AddedExp;
            AddExp(mapPlayerData.PlayerData, reward.AddedExp);
            accountData.Diamonds += reward.Diamonds;
            mapPlayerData.PlayerData.Gold += reward.Gold;
            mapPlayerData.PlayerData.Food += reward.Food;
            if (reward.Cards.Count > 0)
                mapPlayerData.Cards.Add(reward.Cards);
            if (reward.Equips.Count > 0)
                mapPlayerData.Equips.Add(reward.Equips);
            if (reward.Items.Count > 0)
                mapPlayerData.Items.Add(reward.Items);
            if (reward.Buffs.Count > 0)
                mapPlayerData.Buffs.Add(reward.Items);
            for (int i = 0; i < reward.Cards.Count; i++)
            {
                if (reward.CardTemps[i]==1)
                {
                    mapPlayerData.RewardCards.Add(reward.Cards[i]);
                }
            }
            for (int i = 0; i < reward.Items.Count; i++)
            {
                if (reward.ItemTemps[i] == 1)
                {
                    mapPlayerData.RewardItems.Add(reward.Items[i]);
                }
            }
            SaveData(ACCOUNT_DATA_KEY, accountData);
            SaveData(MAP_PLAYER_DATA_KEY, mapPlayerData);

            SendToClient(MessageId_Receive.GCMapGetReward, reward);
        }

        GCExitBattle exitBattle = new GCExitBattle();
        exitBattle.MonsterId = data.MonsterId;
        exitBattle.Reason = data.Reason;
        SendToClient(MessageId_Receive.GCExitBattle, exitBattle);

        GCUpdateMapPlayerData updateMapPlayerData = new GCUpdateMapPlayerData();
        updateMapPlayerData.MapPlayerData = mapPlayerData;
        updateMapPlayerData.PlayerId = mapPlayerData.PlayerData.PlayerId;

        SendToClient(MessageId_Receive.GCUpdateMapPlayerData, updateMapPlayerData);
    }
    
}
