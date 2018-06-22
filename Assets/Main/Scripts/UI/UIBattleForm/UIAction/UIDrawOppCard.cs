using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIDrawOppCard : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.DrawOppCard; } }
        public BattleCardData DrawedCard { get; private set; }
        public UIDrawOppCard(BattleCardData cardData) : base()
        {
            DrawedCard = cardData;
        }

        public override IEnumerator Excute()
        {
            throw new System.NotImplementedException(typeof(UIDrawOppCard).ToString());
        }
    }
}
