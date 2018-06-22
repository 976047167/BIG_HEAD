using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIGetCard : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.GetCard; } }
        public BattleCardData CardData { get; private set; }
        public UIGetCard(BattleCardData cardData) : base()
        {
            CardData = cardData;
        }

        public override IEnumerator Excute()
        {
            throw new System.NotImplementedException(typeof(UIGetCard).ToString());
        }
    }
}
