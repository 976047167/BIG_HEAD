using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIOppEscapeBattle : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.OppEscapeBattle; } }
        public UIOppEscapeBattle() : base()
        {

        }

        public override IEnumerator Excute()
        {
            yield return null;
            BattleForm.OppEscapeBattle();
        }
    }
}
