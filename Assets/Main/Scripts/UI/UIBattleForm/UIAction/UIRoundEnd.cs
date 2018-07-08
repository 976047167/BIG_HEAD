using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIRoundEnd : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.RoundEnd; } }
        public BattlePlayer Player { get; private set; }
        public UIRoundEnd(BattlePlayer playerData) : base()
        {
            Player = playerData;
        }

        public override IEnumerator Excute()
        {
            //BattleForm.ClearUsedCards();
            BattleForm.UsedCardsGrid.GetChildList().ForEach((t) => t.gameObject.SetActive(false));
            //¸üÐÂbuff
            //for (int i = 0; i < BattleForm.GetPlayerInfoViewByPlayer(Player).PlayerInfo.Buffs.Count; i++)
            //{
            //    BattleBuffData buffData = BattleForm.GetPlayerInfoViewByPlayer(Player).PlayerInfo.Buffs[i];
            //}
            yield return null;
        }
    }
}
