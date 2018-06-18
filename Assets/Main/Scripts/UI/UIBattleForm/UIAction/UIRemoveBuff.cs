using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIRemoveBuff : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.RemoveBuff; } }
        protected BattlePlayer target;
        protected int buffId = 0;
        public UIRemoveBuff(BattlePlayer target,int buffId) : base()
        {
            this.target = target;
            this.buffId = buffId;
        }

        public override IEnumerator Excute()
        {
            throw new System.NotImplementedException(typeof(UIRemoveBuff).ToString());
        }
    }
}
