using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUtility
{
    /// <summary>
    /// 展示卡牌
    /// </summary>
    /// <param name="go"></param>需要添加脚本的go
    /// <param name="cardId"></param>
    public static void SetCardTips(GameObject go, int cardId,int cardNum =1)
    {
        ShowCard ShowCard = go.GetComponent<ShowCard>();
        if (ShowCard == null)
        {
            go.AddComponent<ShowCard>().SetShow(0, cardId, cardNum);
        }
       else
        {
            ShowCard.SetShow(0, cardId, cardNum);
        }
    }
    /// <summary>
    /// 展示buff
    /// </summary>
    /// <param name="go"></param>需要添加脚本的go
    /// <param name="cardId"></param>
    public static void SetBuffTips(GameObject go, int buffId, int cardNum = 1)
    {
        ShowCard ShowCard = go.GetComponent<ShowCard>();
        if (ShowCard == null)
        {
            go.AddComponent<ShowCard>().SetShow(0, buffId, cardNum);
        }
        else
        {
            ShowCard.SetShow(0, buffId, cardNum);
        }
    }
    /// <summary>
    /// 展示装备
    /// </summary>
    /// <param name="go"></param>需要添加脚本的go
    /// <param name="cardId"></param>
    public static void SetEquipTips(GameObject go, int equipId, int cardNum = 1)
    {
        ShowCard ShowCard = go.GetComponent<ShowCard>();
        if (ShowCard == null)
        {
            go.AddComponent<ShowCard>().SetShow(0, equipId, cardNum);
        }
        else
        {
            ShowCard.SetShow(0, equipId, cardNum);
        }
    }
    public static void ShowMessageBox(MessageBoxType messageBoxType, int contentId, WND_MessageBox.MessageBoxCallback messageBoxCallback)
    {
        ShowMessageBox(messageBoxType, I18N.Get(contentId), messageBoxCallback);
    }
    public static void ShowMessageBox(MessageBoxType messageBoxType, string content, WND_MessageBox.MessageBoxCallback messageBoxCallback)
    {
        UIModule.Instance.OpenForm<WND_MessageBox>(new object[] { messageBoxType, content, messageBoxCallback });
    }
}
