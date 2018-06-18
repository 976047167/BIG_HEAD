using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIMPRecover : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.MPRecover; } }
        public UIMPRecover() : base()
        {
            
        }

        public override IEnumerator Excute()
        {
            throw new System.NotImplementedException(typeof(UIMPRecover).ToString());
        }
    }
}
