using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIRoundStart : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.RoundStart; } }
        public UIRoundStart() : base()
        {
            
        }

        public override IEnumerator Excute()
        {
            throw new System.NotImplementedException(typeof(UIRoundStart).ToString());
        }
    }
}
