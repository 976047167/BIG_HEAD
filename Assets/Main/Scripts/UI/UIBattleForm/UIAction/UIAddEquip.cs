using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIAddEquip : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.AddEquip; } }
        public UIAddEquip() : base()
        {
            
        }

        public override IEnumerator Excute()
        {
            throw new System.NotImplementedException(typeof(UIAddEquip).ToString());
        }
    }
}
