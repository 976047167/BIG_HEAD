using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIWinBattle : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.WinBattle; } }

        public int RewardId { private set; get; }
        public UIWinBattle(int rewardId) : base()
        {
            RewardId = rewardId;
        }

        public override IEnumerator Excute()
        {
            yield return null;
            BattleForm.WinBattle();
            UIModule.Instance.OpenForm<WND_Reward>(RewardId);
        }
    }
}
