using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;


public class I18N
{
    private static LanguageType language = LanguageType.None;
    public static LanguageType Language { get { return language; } }

    public static void SetLanguage(LanguageType languageType)
    {
        //使用系统默认语言
        if (languageType == LanguageType.None)
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    languageType = LanguageType.ChineseSimplified;
                    break;
                case SystemLanguage.ChineseTraditional:
                    languageType = LanguageType.ChineseTraditional;
                    break;
                case SystemLanguage.English:
                    languageType = LanguageType.English;
                    break;
                default:
                    languageType = LanguageType.English;
                    break;
            }
        }
        if (language != languageType)
        {
            //切换语言，刷新所有label
        }
        language = languageType;
    }

    public static string Get(int stringId, params object[] args)
    {
        LocalizationTableSetting localizationTable = null;
        string result = "";
        switch (language)
        {
            case LanguageType.None:
                SetLanguage(LanguageType.None);
                result = Get(stringId, args);
                break;
            case LanguageType.ChineseSimplified:
                localizationTable = LocalizationTableSettings.Get(stringId);
                if (localizationTable == null)
                {
                    Debug.LogError("无法找到匹配的字符串[" + stringId + "]");
                    break;
                }
                result = localizationTable.ChineseSimplified;
                break;
            case LanguageType.ChineseTraditional:
                localizationTable = LocalizationTableSettings.Get(stringId);
                if (localizationTable == null)
                {
                    Debug.LogError("无法找到匹配的字符串[" + stringId + "]");
                    break;
                }
                result = localizationTable.ChineseTraditional;
                break;
            case LanguageType.English:
                localizationTable = LocalizationTableSettings.Get(stringId);
                if (localizationTable == null)
                {
                    Debug.LogError("无法找到匹配的字符串[" + stringId + "]");
                    break;
                }
                result = localizationTable.English;
                break;
            default:
                localizationTable = LocalizationTableSettings.Get(stringId);
                if (localizationTable == null)
                {
                    Debug.LogError("无法找到匹配的字符串[" + stringId + "]");
                    break;
                }
                result = localizationTable.ChineseSimplified;
                break;
        }
        if (args == null)
        {
            return result;
        }
        else
        {
            return string.Format(result, args);
        }
    }
}



