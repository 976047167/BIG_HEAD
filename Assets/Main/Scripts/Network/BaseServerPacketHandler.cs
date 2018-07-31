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
        protected void SendToClient(MessageId_Receive messageId, IMessage message)
        {
            Game.NetworkManager.Session.OnMessage((ushort)messageId, message);
        }
        protected void SaveData(string key, IMessage data)
        {
            //Debug.LogError(key + "#" + data.GetType().ToString() + ".data");
            string file = Path.Combine(Application.persistentDataPath, key + "#" + data.GetType().Name.ToString() + ".data");
            FileStream fs = File.Create(file);
            Debug.Log(file);
            //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            data.WriteTo(fs);
            //sw.Close();
            fs.Close();
        }
        protected T GetSavedData<T>(string key) where T : class, IMessage
        {
            return GetSavedData(key, typeof(T)) as T;
        }
        protected IMessage GetSavedData(string key, Type dataType)
        {
            string file = Path.Combine(Application.persistentDataPath, key + "#" + dataType.Name.ToString() + ".data");
            if (!File.Exists(file))
            {
                return null;
            }
            if (dataType == null)
            {
                return null;
            }
            IMessage data = dataType.Assembly.CreateInstance(dataType.ToString()) as IMessage;
            FileStream fs = File.Open(file, FileMode.Open);
            Debug.Log(file);
            //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            data.MergeFrom(fs);
            //sw.Close();
            fs.Close();
            return data;
        }
    }
}
