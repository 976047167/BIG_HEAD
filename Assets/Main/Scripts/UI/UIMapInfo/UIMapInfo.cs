using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapInfo : UIFormBase
{
    [SerializeField]
    private UILabel m_PlayerInfo;

    string formatString = @"[FF0000]血量: {0} / {1}[-]
[00FF00]食物: {2}[-]
[FFFF00]金币: {3}[-]";
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_PlayerInfo.text = string.Format(formatString, Game.DataManager.MyPlayerData.HP, Game.DataManager.MyPlayerData.MaxHP, Game.DataManager.Food, Game.DataManager.Coin);
    }
}
