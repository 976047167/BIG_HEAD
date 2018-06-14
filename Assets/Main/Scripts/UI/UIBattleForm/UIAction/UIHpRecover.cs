using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIHpRecover : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.HpRecover; } }
        public UIHpRecover() : base()
        {
            
        }

        public override IEnumerator Excute()
        {
            throw new System.NotImplementedException(typeof(UIHpRecover).ToString());
        }
    }
}
