using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class GenerateMessageId
{
    const string MESSAGE_ID_PATH = @".\Main\Scripts\Plugins\Network\MessageId_Receive.cs";
    const string Template = @"//generate by code
using UnityEngine;

public enum MessageId_Receive : ushort
{
    None = 0,
    #MESSAGE_ID
    MAX = 65535,
}
";
    [MenuItem("Tools/Protobuf/Generate MessageId Receive")]
    public static void CompileMessageId()
    {
        DirectoryInfo protoPath = new DirectoryInfo(Path.Combine(Application.dataPath, CompileProtoFiles.PROTO_FOLDER));
        string protoFolder = protoPath.FullName;
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
                sb.Append("\n    ").Append(message).Append(" = ").Append(messageId).Append(",");
            }
        }
        CreateScript(sb.ToString());
        AssetDatabase.Refresh();
    }
    static void CreateScript(string actionName)
    {
        string path = Path.Combine(Application.dataPath, MESSAGE_ID_PATH);
        FileStream fs = File.Create(path);
        Debug.Log(path);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
        sw.Write(Template.Replace("#MESSAGE_ID", actionName));
        sw.Close();
        fs.Close();
    }
}
