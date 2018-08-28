using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIWinBattle : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.WinBattle; } }

        public UIWinBattle() : base()
        {
        }

        public override IEnumerator Excute()
        {
            yield return null;
            BattleForm.WinBattle();
        }
    }
}
