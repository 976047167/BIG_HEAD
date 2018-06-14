using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIUseCard : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.UseCard; } }
        public UIUseCard() : base()
        {
            
        }

        public override IEnumerator Excute()
        {
            throw new System.NotImplementedException(typeof(UIUseCard).ToString());
        }
    }
}
