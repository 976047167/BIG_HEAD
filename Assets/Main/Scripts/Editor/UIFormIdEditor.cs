using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Reflection;
using AppSettings;
public class UIFormIdEditor
{
    [MenuItem("Tools/Battle/UIFormId", false, 1001)]
    static void SeletEnable()
    {
        IEnumerable enumerable = UIFormTableSettings.GetAll();
        string result = "";
        foreach (UIFormTableSetting form in enumerable)
        {
            result += string.Format(@"    ///<summary>{0}</summary>
    {1} = {2},
", form.Desc, form.Name, form.Id);
        }
        CreateScript(result);
        AssetDatabase.Refresh();
    }
    const string scriptPath = "Main/Scripts/UI/FormId.cs";
    static string Template = @"using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FormId : int
{
#ENUM_ITEMS
}
";
    static void CreateScript(string actionName)
    {
        string path = Path.Combine(Application.dataPath, scriptPath);
        FileStream fs = File.Create(path);
        Debug.LogError(path);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
        sw.Write(Template.Replace("#ENUM_ITEMS", actionName));
        sw.Close();
        fs.Close();
    }

}