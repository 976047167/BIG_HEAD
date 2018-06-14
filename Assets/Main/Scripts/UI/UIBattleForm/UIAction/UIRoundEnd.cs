using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIRoundEnd : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.RoundEnd; } }
        public UIRoundEnd() : base()
        {
            
        }

        public override IEnumerator Excute()
        {
            throw new System.NotImplementedException(typeof(UIRoundEnd).ToString());
        }
    }
}
