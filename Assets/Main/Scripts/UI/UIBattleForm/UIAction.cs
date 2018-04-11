using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIActionType
{
    None = 0,
    DrawCard,
    UseCard,
    HpDamage,
    ApSpend,
    HpRecover,
    AddBuff,
}

public class UIAction
{
    public UIActionType ActionType { get; private set; }
    public BattleCardData CardData { get; private set; }
    public UIAction(UIActionType type, BattleCardData data)
    {
        ActionType = type;
        CardData = data;
    }
    
}
//public class UIAction_
