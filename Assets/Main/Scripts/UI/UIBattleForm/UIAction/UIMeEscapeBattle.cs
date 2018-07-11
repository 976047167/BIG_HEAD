using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIMeEscapeBattle : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.MeEscapeBattle; } }
        public UIMeEscapeBattle() : base()
        {
            
        }

        public override IEnumerator Excute()
        {
            yield return null;
            BattleForm.MeEscapeBattle();
        }
    }
}
