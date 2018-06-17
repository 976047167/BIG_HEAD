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
    public static void SetCardTips(GameObject go, int cardId)
    {
        go.AddComponent<ShowCard>().SetShow(0, cardId);
    }
    /// <summary>
    /// 展示buff
    /// </summary>
    /// <param name="go"></param>需要添加脚本的go
    /// <param name="cardId"></param>
    public static void SetBuffTips(GameObject go, int buffId)
    {

        go.AddComponent<ShowCard>().SetShow(1, buffId);
    }
    /// <summary>
    /// 展示装备
    /// </summary>
    /// <param name="go"></param>需要添加脚本的go
    /// <param name="cardId"></param>
    public static void SetEquipTips(GameObject go, int equipId)
    {
        go.AddComponent<ShowCard>().SetShow(2, equipId);
    }
}
