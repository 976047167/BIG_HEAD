using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UINone : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.None; } }
        public UINone() : base()
        {
            
        }

        public override IEnumerator Excute()
        {
            throw new System.NotImplementedException(typeof(UINone).ToString());
        }
    }
}
