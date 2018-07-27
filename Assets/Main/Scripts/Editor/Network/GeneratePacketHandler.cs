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
    const string PROTO_FOLDER = @"..\Proto\proto";
    const string MESSAGE_ID_PATH = @".\Main\Scripts\Network\PacketHandler\";

    const string Template = @"//generate by code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;

public class #NAMEHandler : BasePacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)NetworkMessageId.#NAME;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        throw new System.NotImplementedException(GetType().ToString());
    }
}
";
    [MenuItem("Tools/Protobuf/Generate PacketHandler")]
    public static void CompileMessageId()
    {
        DirectoryInfo protoPath = new DirectoryInfo(Path.Combine(Application.dataPath, PROTO_FOLDER));
        FileInfo[] fileInfos = protoPath.GetFiles("*.proto", SearchOption.TopDirectoryOnly);
        for (int i = 0; i < fileInfos.Length; i++)
        {
            string name = fileInfos[i].Name.Replace(fileInfos[i].Extension, "");
            //第二个字母是大写的C，那就是要客户端解析的
            //LCLogin_1000
            if ((name[1] == 'C' || name[1] == 'c') && name.Contains("_"))
            {
                string[] splite = name.Split('_');
                if (splite.Length < 2)
                {
                    Debug.LogError("命名缺少[_] \n" + fileInfos[i].Name);
                    continue;
                }
                string message = splite[0];
                ushort messageId = 0;
                if (!ushort.TryParse(splite[1], out messageId))
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
        Debug.LogError(path);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
        sw.Write(Template.Replace("#NAME", actionName));
        sw.Close();
        fs.Close();
    }
}
#endif
