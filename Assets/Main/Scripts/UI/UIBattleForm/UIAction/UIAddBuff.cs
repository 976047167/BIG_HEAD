using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIAddBuff : UIAction
    {
        public BattleBuffData BuffData { get; private set; }
        public static UIActionType ActionType { get { return UIActionType.AddBuff; } }
        public UIAddBuff(BattleBuffData buffData) : base()
        {
            BuffData = buffData;
        }

        public override IEnumerator Excute()
        {
            BattleForm.GetPlayerInfoViewByPlayer(BuffData.Target).AddBuff(BuffData);
            yield return null;
        }
    }

}
