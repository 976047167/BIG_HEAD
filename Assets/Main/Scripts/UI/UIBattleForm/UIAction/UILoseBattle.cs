using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UILoseBattle : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.LoseBattle; } }
        public UILoseBattle() : base()
        {
            
        }

        public override IEnumerator Excute()
        {
            yield return null;
            BattleForm.LoseBattle();
        }
    }
}
