using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 当前客户端玩家逻辑
/// </summary>
[Serializable]
public class MyPlayer : Player
{
    
    protected PlayerDetailData m_playerDetialData;

    public PlayerDetailData DetailData { get { return m_playerDetialData; } }
    public MyPlayer()
    {
        m_Data = new PlayerData();
        m_playerDetialData = new PlayerDetailData();
    }
}
