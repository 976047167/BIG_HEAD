using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIRoundEnd : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.RoundEnd; } }
        public BattlePlayer PlayerData { get; private set; }
        public UIRoundEnd(BattlePlayer playerData) : base()
        {
            PlayerData = playerData;
        }

        public override IEnumerator Excute()
        {
            //BattleForm.ClearUsedCards();
            BattleForm.UsedCardsGrid.GetChildList().ForEach((t) => t.gameObject.SetActive(false));
            yield return null;
        }
    }
}
