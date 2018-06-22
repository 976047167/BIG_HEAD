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
        protected int layers = 0;
        /// <summary>
        /// layers = -1就是完全移除
        /// </summary>
        /// <param name="target"></param>
        /// <param name="buffId"></param>
        /// <param name="layers">-1就是完全移除</param>
        public UIRemoveBuff(BattlePlayer target, int buffId, int layers) : base()
        {
            this.target = target;
            this.buffId = buffId;
            this.layers = layers;
        }

        public override IEnumerator Excute()
        {
            throw new System.NotImplementedException(typeof(UIRemoveBuff).ToString());
        }
    }
}
