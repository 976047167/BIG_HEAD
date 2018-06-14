using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIHpRecover : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.HpRecover; } }

        public BattlePlayer Target { get; private set; }
        public int RecoverdHp { get; private set; }
        public UIHpRecover(BattlePlayer target, int hpRecover) : base()
        {
            Target = target;
            RecoverdHp = hpRecover;
        }
        public override IEnumerator Excute()
        {
            //yield return BattleForm.GetPlayerInfoViewByPlayer(Target).SetHpRecover(RecoverdHp);
            PlayerInfoView playerInfoView = BattleForm.GetPlayerInfoViewByPlayer(Target);
            Color orginColor = playerInfoView.lblHP.color;
            playerInfoView.lblHP.color = Color.green;
            yield return null;
            TweenScale.Begin(playerInfoView.lblHP.gameObject, 0.15f, new Vector3(1.2f, 1.2f, 1.2f));
            yield return new WaitForSeconds(0.15f);
            playerInfoView.PlayerInfo.HP += RecoverdHp;
            if (playerInfoView.PlayerInfo.HP >= playerInfoView.PlayerInfo.MaxHP)
            {
                playerInfoView.PlayerInfo.HP = playerInfoView.PlayerInfo.MaxHP;
            }
            TweenScale.Begin(playerInfoView.lblHP.gameObject, 0.15f, Vector3.one);
            yield return new WaitForSeconds(0.15f);
            playerInfoView.lblHP.color = orginColor;
        }
    }
}
