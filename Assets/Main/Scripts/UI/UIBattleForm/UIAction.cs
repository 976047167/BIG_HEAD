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
    protected UIBattleForm battleForm;
    public UIActionType ActionType { get; protected set; }
    public UIAction(UIActionType type)
    {
        this.battleForm = Game.BattleManager.BattleForm;
        ActionType = type;
    }
    public abstract IEnumerator Excute();


}
public class UIAction_DrawCard : UIAction
{


    public BattleCardData CardData { get; private set; }

    public UIAction_DrawCard(BattleCardData data) : base(UIActionType.DrawCard)
    {
        CardData = data;
    }

    public override IEnumerator Excute()
    {
        yield return null;
        if (CardData.Owner == Game.DataManager.MyPlayerData)
        {
            battleForm.CreateBattleCard(CardData, battleForm.MyCardsGrid);
        }
        else
        {
            battleForm.CreateBattleCard(CardData, battleForm.OppCardsGrid);
        }
        yield return new WaitForSeconds(0.5f);
    }
}

public class UIAction_Damage : UIAction
{
    public BattlePlayerData Target { get; private set; }
    public int Damage { get; private set; }
    public UIAction_Damage(BattlePlayerData target, int hpDamage) : base(UIActionType.HpDamage)
    {
        Target = target;
        Damage = hpDamage;
    }
    public override IEnumerator Excute()
    {
        yield return null;
    }
}

public class UIAction_UseCard : UIAction
{
    public BattleCardData CardData { get; private set; }
    public UIAction_UseCard(BattleCardData cardData) : base(UIActionType.UseCard)
    {
        CardData = cardData;
    }

    public override IEnumerator Excute()
    {
        throw new System.NotImplementedException();
    }
}
