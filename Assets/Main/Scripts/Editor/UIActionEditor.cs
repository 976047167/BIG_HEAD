using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Reflection;
[CustomEditor(typeof(UIAction))]
public class UIActionEditor : Editor
{
    [MenuItem("Tools/Battle/UIBattleAction", false, 1001)]
    static void SeletEnable()
    {
        Type actionType = typeof(UIActionType);
        string[] actions = Enum.GetNames(actionType);
        foreach (var item in actions)
        {
            Type type = actionType.Assembly.GetType("UIAction+UI" + item);
            if (type == null)
            {
                CreateScript(item);
            }
        }
        AssetDatabase.Refresh();
    }
    const string scriptPath = "Main/Scripts/UI/UIBattleForm/UIAction/";
    static string Template = @"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UI#UI_ACTION_NAME : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.#UI_ACTION_NAME; } }
        public UI#UI_ACTION_NAME() : base()
        {
            
        }

        public override IEnumerator Excute()
        {
            throw new System.NotImplementedException(typeof(UI#UI_ACTION_NAME).ToString());
        }
    }
}
";
    static void CreateScript(string actionName)
    {
        string path = Path.Combine(Application.dataPath, scriptPath + "UI" + actionName + ".cs");
        StreamWriter sw = File.CreateText(path);
        Debug.LogError(path);
        sw.Write(Template.Replace("#UI_ACTION_NAME", actionName));
        sw.Close();
    }

}
