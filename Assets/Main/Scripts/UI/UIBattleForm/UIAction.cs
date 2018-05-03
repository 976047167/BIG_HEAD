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
    RoundStart,
    RoundEnd,
}
public abstract class UIAction
{
    protected UIBattleForm BattleForm;
    public UIActionType ActionType { get; protected set; }
    public List<UIAction> BindActionList { get; protected set; }
    public UIAction(UIActionType type)
    {
        this.BattleForm = Game.BattleManager.BattleForm;
        ActionType = type;
    }
    public void AddBindUIAction(UIAction childUIAction)
    {
        if (BindActionList==null)
        {
            BindActionList = new List<UIAction>(1);
        }
        BindActionList.Add(childUIAction);
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
        if (CardData.Owner == Game.BattleManager.MyPlayerData)
        {
            BattleForm.CreateBattleCard(CardData, BattleForm.MyCardsGrid);
        }
        else
        {
            BattleForm.CreateBattleCard(CardData, BattleForm.OppCardsGrid);
        }
        BattleForm.GetPlayerInfoViewByPlayerData(CardData.Owner).DrawCard();
        yield return new WaitForSeconds(0.5f);
    }
}

public class UIAction_HPDamage : UIAction
{
    public BattlePlayerData Target { get; private set; }
    public int Damage { get; private set; }
    public UIAction_HPDamage(BattlePlayerData target, int hpDamage) : base(UIActionType.HpDamage)
    {
        Target = target;
        Damage = hpDamage;
    }
    public override IEnumerator Excute()
    {
        yield return BattleForm.GetPlayerInfoViewByPlayerData(Target).SetHpDamage(Damage);
    }
}
public class UIAction_HpRecover : UIAction
{
    public BattlePlayerData Target { get; private set; }
    public int HpRecover { get; private set; }
    public UIAction_HpRecover(BattlePlayerData target, int hpRecover) : base(UIActionType.HpDamage)
    {
        Target = target;
        HpRecover = hpRecover;
    }
    public override IEnumerator Excute()
    {
        yield return BattleForm.GetPlayerInfoViewByPlayerData(Target).SetHpRecover(HpRecover);
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
        BattleForm.GetPlayerInfoViewByPlayerData(CardData.Owner).UseCard(battleCard.CardData);
        yield return new WaitForSeconds(0.5f);
        BattleForm.OppCardsGrid.Reposition();
        battleCard.RefreshDepth();
        yield return null;
    }
}
public class UIAction_AddBuff : UIAction
{
    public BattleBuffData BuffData { get; private set; }
    public UIAction_AddBuff(BattleBuffData buffData) : base(UIActionType.AddBuff)
    {
        BuffData = buffData;
    }

    public override IEnumerator Excute()
    {
        BattleForm.GetPlayerInfoViewByPlayerData(BuffData.TargetPlayerData).AddBuff(BuffData);
        yield return null;
    }
}
public class UIAction_ApSpend : UIAction
{
    public BattlePlayerData PlayerData { get; private set; }
    public int SpentAp { get; private set; }
    public UIAction_ApSpend(BattlePlayerData playerData, int spentAp) : base(UIActionType.ApSpend)
    {
        PlayerData = playerData;
        SpentAp = spentAp;
    }

    public override IEnumerator Excute()
    {
        BattleForm.GetPlayerInfoViewByPlayerData(PlayerData).SpendAp(SpentAp);
        yield return null;
    }
}
public class UIAction_RoundStart : UIAction
{
    public BattlePlayerData PlayerData { get; private set; }
    public UIAction_RoundStart(BattlePlayerData playerData) : base(UIActionType.RoundStart)
    {
        PlayerData = playerData;
    }

    public override IEnumerator Excute()
    {
        BattleForm.GetPlayerInfoViewByPlayerData(PlayerData).RoundStart();
        yield return null;
    }
}
public class UIAction_RoundEnd : UIAction
{
    public BattlePlayerData PlayerData { get; private set; }
    public UIAction_RoundEnd(BattlePlayerData playerData) : base(UIActionType.RoundEnd)
    {
        PlayerData = playerData;
    }

    public override IEnumerator Excute()
    {
        BattleForm.ClearUsedCards();
        yield return null;
    }
}
