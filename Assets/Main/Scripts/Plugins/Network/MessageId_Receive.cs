//generate by code
using UnityEngine;

public enum MessageId_Receive : ushort
{
    None = 0,
    
    GCLogin = 1000,
    GCSignIn = 1002,
    GCUpdatePlayerData = 1004,
    GCEnterInstance = 2000,
    GCGetMapLayerData = 2001,
    GCMapGetReward = 2002,
    GCExitInstance = 2003,
    GCMapCardInteraction = 2004,
    GCMapPlayerMove = 2005,
    GCMapBuyItem = 2006,
    GCMapChangeDeck = 2007,
    GCMapUseItem = 2008,
    GCUpdateMapPlayerData = 2009,
    GCMapApplyEffect = 2010,
    GCEnterBattle = 3000,
    GCExitBattle = 3001,
    MAX = 65535,
}
