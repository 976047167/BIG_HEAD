using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIModule
{
    static Dictionary<Type, UIConfig> DicUIConfig = new Dictionary<Type, UIConfig>()
    {
        {typeof(UIBattleForm),new UIConfig("Instance/WND_BattleForm") },
        {typeof(UIMapInfo),new UIConfig("WND_MapInfo") },
        {typeof(WND_Dialog),new UIConfig("WND_Dialog") },
        {typeof(WND_Bag),new UIConfig("WND_Bag") },
        {typeof(WND_ShowCard),new UIConfig("WND_ShowCard") },
        {typeof(WND_Kaku),new UIConfig("WND_Kaku") },
        {typeof(UIMenu),new UIConfig("WND_Menu") },
        {typeof(WND_Reward),new UIConfig("WND_Reward") },
        {typeof(WND_MainTown),new UIConfig("Lobby/WND_MainTown") },
        {typeof(WND_ChoseDeck),new UIConfig("WND_ChoseDeck") },
        {typeof(WND_Loading),new UIConfig("Internal/WND_Loading") },
        {typeof(WND_Launch),new UIConfig("Internal/WND_Launch") },
        {typeof(WND_Login),new UIConfig("Internal/WND_Login") },
    };
}