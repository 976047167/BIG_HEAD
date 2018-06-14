using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract partial class UIAction
{
    protected UIBattleForm BattleForm;
    /// <summary>
    /// 多个ui表现
    /// </summary>
    public List<UIAction> BindActionList { get; protected set; }

    
    public UIAction()
    {
        this.BattleForm = Game.BattleManager.BattleForm;
    }
    public void AddBindUIAction(UIAction childUIAction)
    {
        if (BindActionList == null)
        {
            BindActionList = new List<UIAction>(1);
        }
        BindActionList.Add(childUIAction);
    }


    public abstract IEnumerator Excute();
    //static Dictionary<UIAction, Type> dicActionType = null;
    //static void Init()
    //{
    //    Type baseType = typeof(UIAction);
    //    Type[] types = Assembly.GetExecutingAssembly().GetExportedTypes();
    //    dicActionType = new Dictionary<UIAction, Type>(Enum.GetNames(typeof(UIActionType)).Length);
    //    List<string> tableNames = new List<string>();
    //    Type type = null;
    //    for (int i = 0; i < types.Length; i++)
    //    {
    //        type = types[i];
    //        if (baseType != type && baseType.IsAssignableFrom(type))
    //        {

    //            dicActionType.Add((UIAction)type.GetProperty("UIAction", BindingFlags.Static | BindingFlags.Public).GetValue(null, null), type);
    //        }
    //    }
    //}

    //public static BattleAction Create(BattleActionType actionType, int actionArg, int actionArg2, BattleCardData cardData, BattlePlayer owner, BattlePlayer target)
    //{
    //    if (dicActionType == null)
    //    {
    //        Init();
    //    }
    //    BattleAction battleAction = Activator.CreateInstance(dicActionType[actionType]) as BattleAction;
    //    battleAction.actionArg = actionArg;
    //    battleAction.actionArg2 = actionArg2;
    //    battleAction.cardData = cardData;
    //    battleAction.owner = owner;
    //    battleAction.target = target;
    //    battleAction.battleMgr = Game.BattleManager;
    //    return battleAction;
    //}



    //public class DrawCard : UIAction
    //{
    //    public BattleCardData CardData { get; private set; }

    //    public DrawCard(BattleCardData data) : base()
    //    {
    //        CardData = data;
    //    }

    //    public override IEnumerator Excute()
    //    {
    //        yield return null;
    //        if (CardData.Owner == Game.BattleManager.MyPlayer)
    //        {
    //            BattleForm.CreateBattleCard(CardData, BattleForm.MyCardsGrid);
    //        }
    //        else
    //        {
    //            BattleForm.CreateBattleCard(CardData, BattleForm.OppCardsGrid);
    //        }
    //        BattleForm.GetPlayerInfoViewByPlayer(CardData.Owner).DrawCard();
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //}

    //public class HPDamage : UIAction
    //{
    //    public BattlePlayer Target { get; private set; }
    //    public int Damage { get; private set; }
    //    public HPDamage(BattlePlayer target, int hpDamage) : base()
    //    {
    //        Target = target;
    //        Damage = hpDamage;
    //    }
    //    public override IEnumerator Excute()
    //    {
    //        yield return BattleForm.GetPlayerInfoViewByPlayer(Target).SetHpDamage(Damage);
    //    }
    //}
    public class HpRecover : UIAction
    {
        public BattlePlayer Target { get; private set; }
        public int RecoverdHp { get; private set; }
        public HpRecover(BattlePlayer target, int hpRecover) : base()
        {
            Target = target;
            RecoverdHp = hpRecover;
        }
        public override IEnumerator Excute()
        {
            yield return BattleForm.GetPlayerInfoViewByPlayer(Target).SetHpRecover(RecoverdHp);
        }
    }
    public class UseCard : UIAction
    {
        public BattleCardData CardData { get; private set; }
        public UseCard(BattleCardData cardData) : base()
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
            BattleForm.GetPlayerInfoViewByPlayer(CardData.Owner).UseCard(battleCard.CardData);
            yield return new WaitForSeconds(0.5f);
            BattleForm.OppCardsGrid.Reposition();
            battleCard.RefreshDepth();
            yield return null;
        }
    }
    //public class AddBuff : UIAction
    //{
    //    public BattleBuffData BuffData { get; private set; }
    //    public AddBuff(BattleBuffData buffData) : base()
    //    {
    //        BuffData = buffData;
    //    }

    //    public override IEnumerator Excute()
    //    {
    //        BattleForm.GetPlayerInfoViewByPlayer(BuffData.TargetPlayerData).AddBuff(BuffData);
    //        yield return null;
    //    }
    //}
    //public class ApSpend : UIAction
    //{
    //    public BattlePlayer Player { get; private set; }
    //    public int SpentAp { get; private set; }
    //    public ApSpend(BattlePlayer player, int spentAp) : base()
    //    {
    //        Player = player;
    //        SpentAp = spentAp;
    //    }

    //    public override IEnumerator Excute()
    //    {
    //        BattleForm.GetPlayerInfoViewByPlayer(Player).SpendAp(SpentAp);
    //        yield return null;
    //    }
    //}
    public class RoundStart : UIAction
    {
        public BattlePlayer Player { get; private set; }
        public RoundStart(BattlePlayer playerData) : base()
        {
            Player = playerData;
        }

        public override IEnumerator Excute()
        {
            BattleForm.GetPlayerInfoViewByPlayer(Player).RoundStart();
            yield return null;
        }
    }
    public class RoundEnd : UIAction
    {
        public BattlePlayer PlayerData { get; private set; }
        public RoundEnd(BattlePlayer playerData) : base()
        {
            PlayerData = playerData;
        }

        public override IEnumerator Excute()
        {
            BattleForm.ClearUsedCards();
            yield return null;
        }
    }
}