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
    protected UIBattleForm BattleForm;
    public UIActionType ActionType { get; protected set; }
    public UIAction(UIActionType type)
    {
        this.BattleForm = Game.BattleManager.BattleForm;
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
            BattleForm.CreateBattleCard(CardData, BattleForm.MyCardsGrid);
        }
        else
        {
            BattleForm.CreateBattleCard(CardData, BattleForm.OppCardsGrid);
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
        yield return BattleForm.GetPlayerInfoViewByPlayerData(Target).SetHpDamage(Damage);
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
        UIBattleCard battleCard = BattleForm.GetUIBattleCard(CardData);
        if (battleCard == null)
        {
            yield return null;
        }
        battleCard.UseCard();
        Vector3 cachePos = battleCard.cacheChildCardTrans.position;
        battleCard.transform.SetParent(BattleForm.UsedCardsGrid.transform, false);
        BattleForm.UsedCardsGrid.Reposition();
        BattleForm.MyCardsGrid.Reposition();

        battleCard.cacheChildCardTrans.position = cachePos;
        yield return null;
        TweenPosition.Begin(battleCard.cacheChildCardTrans.gameObject, 0.5f, Vector3.zero, false);
        yield return new WaitForSeconds(0.5f);
        BattleForm.OppCardsGrid.Reposition();
        battleCard.RefreshDepth();
        yield return null;
    }
}
