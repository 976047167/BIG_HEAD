using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using System.IO;
using System.Text;
using AppSettings;
using BigHead.protocol;

namespace BigHead.Net
{
    public abstract class BaseServerPacketHandler : BasePacketHandler
    {
        const char SPLITE = '#';
        const string SUFFIX = ".data";
        public static string Save_Data_Path = "c:\\";

        protected const string ACCOUNT_DATA_KEY = "accountData";
        protected const string PLAYER_DATA_KEY = "playerData";
        protected const string MAP_PLAYER_DATA_KEY = "mapPlayerData";
        protected const string PLAYER_DETAIL_DATA = "detailPlayerData";

        protected void SendToClient(MessageId_Receive messageId, IMessage message)
        {
            Game.NetworkManager.Session.OnMessage((ushort)messageId, message);
        }
        protected void SaveData(string key, IMessage data)
        {
            //Debug.LogError(key + SPLITE + data.GetType().ToString() + ".data");
            
            string file = Path.Combine(Save_Data_Path, key + SPLITE + data.GetType().ToString() + SUFFIX);
            if (data == null)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                return;
            }
            FileStream fs = File.Create(file);
            Debug.Log(file);
            //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            data.WriteTo(fs);
            //sw.Close();
            fs.Close();
        }
        protected T GetSavedData<T>(string key) where T : class, IMessage
        {
            return GetSavedData(key) as T;
        }
        protected IMessage GetSavedData(string key)
        {
            string[] files = Directory.GetFiles(Save_Data_Path);
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = new FileInfo(files[i]);
                string[] splites = file.Name.Split(SPLITE);
                if (splites[0] == key)
                {
                    if (splites.Length < 2 || splites[1] == null)
                    {
                        return null;
                    }
                    string type = splites[1].Replace(SUFFIX, "");
                    IMessage data = GetType().Assembly.CreateInstance(type) as IMessage;
                    FileStream fs = file.Open(FileMode.Open);
                    Debug.Log(file);
                    //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                    data.MergeFrom(fs);
                    //sw.Close();
                    fs.Close();
                    return data;
                }
            }

            return null;
        }
        #region Common Function
        protected void AddExp(PBPlayerData playerData, int exp)
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
        protected GCMapGetReward GetRewardById(int rewardId)
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
        #endregion
    }

}
