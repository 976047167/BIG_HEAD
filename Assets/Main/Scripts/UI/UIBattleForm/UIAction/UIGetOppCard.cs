using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIGetOppCard : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.GetOppCard; } }
        public BattleCardData OriginCard { get; private set; }
        public BattleCardData NowCard { get; private set; }
        public UIGetOppCard(BattleCardData originCard, BattleCardData nowCard) : base()
        {
            OriginCard = originCard;
            NowCard = nowCard;
        }

        public override IEnumerator Excute()
        {
            throw new System.NotImplementedException(typeof(UIGetOppCard).ToString());
        }
    }
}
