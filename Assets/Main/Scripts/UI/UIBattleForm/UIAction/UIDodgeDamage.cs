using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIDodgeDamage : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.DodgeDamage; } }
        public UIDodgeDamage() : base()
        {
            
        }

        public override IEnumerator Excute()
        {
            Debug.LogError("…¡±‹∂Øª≠");
            yield return null;
        }
    }
}
