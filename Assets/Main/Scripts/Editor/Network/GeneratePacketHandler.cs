#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;
using System.Text;
using Debug = UnityEngine.Debug;

public class GeneratePacketHandler
{
    const string MESSAGE_ID_PATH = @".\Main\Scripts\Network\PacketHandler\";

    const string Template = @"//generate by code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class #NAMEHandler : BasePacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Receive.#NAME;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        #NAME data = packet as #NAME;
        //处理完数据和逻辑后,发送消息通知其他模块,绝对不可以直接操作UI等Unity主线程的东西!
        //此处发送消息不允许使用Messenger.BroadcastSync同步通知
        throw new System.NotImplementedException(GetType().ToString());
    }
}
";
    [MenuItem("Tools/Protobuf/Generate PacketHandler")]
    public static void CompileMessageId()
    {
        DirectoryInfo protoPath = new DirectoryInfo(Path.Combine(Application.dataPath, CompileProtoFiles.PROTO_FOLDER));
        FileInfo[] fileInfos = protoPath.GetFiles("*.proto", SearchOption.TopDirectoryOnly);
        for (int i = 0; i < fileInfos.Length; i++)
        {
            string name = fileInfos[i].Name.Replace(fileInfos[i].Extension, "");
            //第二个字母是大写的C，那就是要客户端解析的
            //1000_LCLogin
            string[] splite = name.Split('_');
            if (splite.Length < 2)
            {
                //Debug.LogError("命名缺少[_] \n" + fileInfos[i].Name);
                continue;
            }
            if ((splite[1][1] == 'C' || splite[1][1] == 'c') && name.Contains("_"))
            {
                string message = splite[1];
                ushort messageId = 0;
                if (!ushort.TryParse(splite[0], out messageId))
                {
                    Debug.LogError("编号超出ushort范围!\n" + fileInfos[i].Name);
                    continue;
                }
                if (messageId < 1000)
                {
                    Debug.LogError("编号超出规定范围，1000以内为系统预留!\n" + fileInfos[i].Name);
                    continue;
                }
                CreateScript(message);
            }
        }
        AssetDatabase.Refresh();
    }
    static void CreateScript(string actionName)
    {
        string path = Path.Combine(Application.dataPath, MESSAGE_ID_PATH + actionName + "Handler.cs");
        if (File.Exists(path))
        {
            return;
        }
        FileStream fs = File.Create(path);
        Debug.Log(path);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
        sw.Write(Template.Replace("#NAME", actionName));
        sw.Close();
        fs.Close();
    }
}
#endif
