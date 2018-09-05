using AppSettings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIUtility
{
    /// <summary>
    /// 展示卡牌
    /// </summary>
    /// <param name="go"></param>需要添加脚本的go
    /// <param name="cardId"></param>
    public static void SetCardTips(GameObject go, int cardId, int cardNum = 1)
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

    public static void GetNormalCard(Transform parent, int cardId, int count)
    {
        GetNormalCard(parent, cardId, count, Vector3.zero, Vector3.one, Vector3.zero, null);
    }
    public static void GetNormalCard(Transform parent, int cardId, int count, Action<UINormalCard> callback)
    {
        GetNormalCard(parent, cardId, count, Vector3.zero, Vector3.one, Vector3.zero, callback);
    }
    public static void GetNormalCard(Transform parent, int cardId, int count, Vector3 eulerAngles, Vector3 scale, Vector3 pos, Action<UINormalCard> callback)
    {
        ItemTableSetting cardData = ItemTableSettings.Get(cardId);
        if (cardData == null)
        {
            Debug.LogError("非法ID:" + cardId);
            return;
        }
        UIItemTableSetting uiItem = UIItemTableSettings.Get(1);
        ResourceManager.LoadGameObject(uiItem.Path,
            (p, u, go) =>
            {
                go.transform.parent = parent;
                go.transform.localEulerAngles = eulerAngles;
                go.transform.localScale = scale;
                go.transform.position = pos;
                go.SetActive(true);
                UINormalCard normalCard = go.GetComponent<UINormalCard>();
                normalCard.SetCard(cardId, count);
                if (callback != null)
                {
                    callback(normalCard);
                }

            },
            (p, u) =>
            {
                Debug.LogError("加载失败!");
            });
    }
    public static void ShowShortTips(string tips)
    {
        float duration = 0.2f;
        float time = 1f;
        Game.UI.OpenForm<WND_Tips>(new object[] { duration, time, tips });
    }
}
