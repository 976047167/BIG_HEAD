using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIRoundStart : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.RoundStart; } }
        public BattlePlayer Player { get; private set; }
        public UIRoundStart(BattlePlayer playerData) : base()
        {
            Player = playerData;
        }

        public override IEnumerator Excute()
        {
            PlayerInfoView playerInfoView = BattleForm.GetPlayerInfoViewByPlayer(Player);
            playerInfoView.PlayerInfo.AP = playerInfoView.PlayerInfo.MaxAP = playerInfoView.BindPlayerData.MaxAP;
            yield return null;
        }
    }
}
