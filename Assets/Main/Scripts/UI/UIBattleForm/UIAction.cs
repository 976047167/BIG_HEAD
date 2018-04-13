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
public abstract class UIAction
{
    public UIActionType ActionType { get; protected set; }
    public UIAction(UIActionType type)
    {
        ActionType = type;
    }
    public abstract IEnumerator Excute();


}
public class UIAction_DrawCard : UIAction
{
    public BattleCardData CardData { get; private set; }
    public UIAction_DrawCard(UIActionType type, BattleCardData data) : base(type)
    {
        ActionType = type;
        CardData = data;

    }

    public override IEnumerator Excute()
    {
        yield return null;
    }
}

public class UIAction_Damage : UIAction
{
    public UIAction_Damage(UIActionType type, BattleCardData data) : base(type)
    {

    }
    public override IEnumerator Excute()
    {
        yield return null;
    }
}
