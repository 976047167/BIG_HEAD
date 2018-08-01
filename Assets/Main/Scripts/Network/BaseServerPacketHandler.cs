using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using System.IO;
using System.Text;
namespace BigHead.Net
{
    public abstract class BaseServerPacketHandler : BasePacketHandler
    {
        const char SPLITE = '#';
        const string SUFFIX = ".data";

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
            string file = Path.Combine(Application.persistentDataPath, key + SPLITE + data.GetType().ToString() + SUFFIX);
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
            string[] files = Directory.GetFiles(Application.persistentDataPath);
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                string[] splites = file.Split(SPLITE);
                if (splites[0] == key)
                {
                    if (splites.Length < 2 || splites[1] == null)
                    {
                        return null;
                    }
                    string type = splites[1].Replace(SUFFIX, "");
                    IMessage data = GetType().Assembly.CreateInstance(type) as IMessage;
                    FileStream fs = File.Open(file, FileMode.Open);
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
    }
}
