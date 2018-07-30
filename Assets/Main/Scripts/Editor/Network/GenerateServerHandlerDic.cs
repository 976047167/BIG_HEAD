#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;
using System.Text;
using Debug = UnityEngine.Debug;

public class GenerateServerHandlerDic
{
    const string PROTO_FOLDER = @"..\Proto\proto";
    const string DIC_PATH = @".\Main\Scripts\Network\DicServerHandler.cs";

    const string Template = @"//generate by code
using System.Collections.Generic;
namespace BigHead.Net
{
    public class DicServerHandler
    {
        public static Dictionary<ushort, BasePacketHandler> Dic = new Dictionary<ushort, BasePacketHandler>();
        public static void Register()
        {
            Dic.Clear();
            #NAME
        }
    }
}
";
    [MenuItem("Tools/Protobuf/Generate ServerHandler Dic")]
    public static void CompileMessageId()
    {
        DirectoryInfo protoPath = new DirectoryInfo(Path.Combine(Application.dataPath, PROTO_FOLDER));
        FileInfo[] fileInfos = protoPath.GetFiles("*.proto", SearchOption.TopDirectoryOnly);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < fileInfos.Length; i++)
        {
            string name = fileInfos[i].Name.Replace(fileInfos[i].Extension, "");
            //第二个字母是大写的C，那就是要客户端解析的
            //1000_LCLogin
            string[] splite = name.Split('_');
            if (splite.Length < 2)
            {
                Debug.LogError("命名缺少[_] \n" + fileInfos[i].Name);
                continue;
            }
            if ((splite[1][0] == 'C' || splite[1][0] == 'c') && name.Contains("_"))
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

                sb.Append("\n            Dic.Add((ushort)MessageId_Send.").Append(message).Append(", new ").Append(message).Append("Handler());");
            }
        }
        CreateScript(sb.ToString());
        AssetDatabase.Refresh();
    }
    static void CreateScript(string actionName)
    {
        string path = Path.Combine(Application.dataPath, DIC_PATH);
        //if (File.Exists(path))
        //{
        //    return;
        //}
        FileStream fs = File.Create(path);
        Debug.Log(path);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
        sw.Write(Template.Replace("#NAME", actionName));
        sw.Close();
        fs.Close();
    }
}
#endif
