using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Reflection;
[CustomEditor(typeof(BattleAction))]
public class CardActionEditor : Editor
{
    [MenuItem("Tools/Battle/BattleAction", false, 1000)]
    static void SeletEnable()
    {
        Type actionType = typeof(BattleActionType);
        string[] actions = Enum.GetNames(actionType);
        foreach (var item in actions)
        {
            Type type = actionType.Assembly.GetType("BattleAction+" + item);
            if (type == null)
            {
                CreateScript(item);
            }
        }
        AssetDatabase.Refresh();
    }
    const string scriptPath = "Main/Scripts/Battle/BattleAction/";
    static string Template = @"using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class #ACTION_NAME : BattleActionBase
    {
        public static BattleActionType ActionType { get { return BattleActionType.#ACTION_NAME; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
";
    static void CreateScript(string actionName)
    {
        string path = Path.Combine(Application.dataPath, scriptPath + actionName + ".cs");
        FileStream fs = File.Create(path);
        Debug.LogError(path);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
        sw.Write(Template.Replace("#ACTION_NAME", actionName));
        sw.Close();
        fs.Close();
    }

}
