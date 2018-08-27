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
            //战斗结果
            if (data.PlayerData!=null)
            {
                mapPlayerData.PlayerData.Hp = data.PlayerData.Hp;
                mapPlayerData.PlayerData.MaxHp = data.PlayerData.MaxHp;
                mapPlayerData.PlayerData.Mp = data.PlayerData.Mp;
                mapPlayerData.PlayerData.MaxMp = data.PlayerData.MaxMp;
            }
            //存奖励
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
    void AddExp(PBPlayerData playerData, int exp)
    {
        LevelTableSetting levelTable = LevelTableSettings.Get(playerData.Level);
        if (levelTable == null)
        {
            return;
        }
        exp = exp + playerData.Exp;
        int maxExp = levelTable.Exp[ClassCharacterTableSettings.Get(playerData.CharacterId).ClassType];
        while (exp >= maxExp)
        {
            //level Up!
            playerData.Level++;
            exp = exp - maxExp;
            levelTable = LevelTableSettings.Get(playerData.Level);
            if (levelTable == null)
            {
                break;
            }
            playerData.Hp = levelTable.HP[(int)MapMgr.Instance.MyMapPlayer.Data.ClassData.Type];
            playerData.MaxHp = levelTable.HP[(int)MapMgr.Instance.MyMapPlayer.Data.ClassData.Type];
            playerData.Mp = levelTable.MP[(int)MapMgr.Instance.MyMapPlayer.Data.ClassData.Type];
            playerData.MaxMp = levelTable.MP[(int)MapMgr.Instance.MyMapPlayer.Data.ClassData.Type];
            maxExp = levelTable.Exp[(int)MapMgr.Instance.MyMapPlayer.Data.ClassData.Type];
        }
        playerData.Exp = exp;
    }
    GCMapGetReward GetRewardById(int rewardId)
    {
        GCMapGetReward reward = new GCMapGetReward();
        PBAccountData accountData = GetSavedData<PBAccountData>(ACCOUNT_DATA_KEY);
        PBMapPlayerData mapPlayerData = GetSavedData<PBMapPlayerData>(MAP_PLAYER_DATA_KEY);
        reward.AccountId = accountData.Uid;
        reward.PlayerId = mapPlayerData.PlayerData.PlayerId;
        reward.OldExp = mapPlayerData.PlayerData.Exp;
        reward.OldLevel = mapPlayerData.PlayerData.Level;
        RewardTableSetting rewardTable = RewardTableSettings.Get(rewardId);
        reward.AddedExp = rewardTable.exp;
        reward.Diamonds = rewardTable.diamond;
        reward.Gold = rewardTable.gold;
        reward.Food = rewardTable.food;
        for (int i = 0; i < rewardTable.ItemList.Count; i++)
        {
            if (UnityEngine.Random.Range(0, 10000) < rewardTable.RewardProbability[i])
            {
                ItemTableSetting itemTable = ItemTableSettings.Get(rewardTable.ItemList[i]);
                if (itemTable == null)
                {
                    continue;
                }
                switch ((ItemType)itemTable.Type)
                {
                    case ItemType.Card:
                        reward.Cards.Add(rewardTable.ItemList[i]);
                        break;
                    case ItemType.Equip:
                        reward.Equips.Add(rewardTable.ItemList[i]);
                        break;
                    case ItemType.Skill:
                        //要改
                        reward.Buffs.Add(rewardTable.ItemList[i]);
                        break;
                    case ItemType.Consumable:
                        reward.Items.Add(rewardTable.ItemList[i]);
                        break;
                    default:
                        break;
                }
            }
        }
        return reward;
    }
}
