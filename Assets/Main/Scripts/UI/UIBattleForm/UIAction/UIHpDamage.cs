using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIHpDamage : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.HpDamage; } }

        public BattlePlayer Target { get; private set; }
        public int Damage { get; private set; }
        public UIHpDamage(BattlePlayer target, int hpDamage) : base()
        {
            Target = target;
            Damage = hpDamage;
        }
        public override IEnumerator Excute()
        {
            PlayerInfoView playerInfoView = BattleForm.GetPlayerInfoViewByPlayer(Target);
            Color orginColor = playerInfoView.lblHP.color;
            playerInfoView.lblHP.color = Color.red;
            yield return null;
            TweenScale.Begin(playerInfoView.lblHP.gameObject, 0.15f, new Vector3(1.2f, 1.2f, 1.2f));
            yield return new WaitForSeconds(0.15f);
            playerInfoView.PlayerInfo.HP -= Damage;
            
            TweenScale.Begin(playerInfoView.lblHP.gameObject, 0.15f, Vector3.one);
            yield return new WaitForSeconds(0.15f);
            playerInfoView.lblHP.color = orginColor;
            //yield return BattleForm.GetPlayerInfoViewByPlayer(Target).SetHpDamage(Damage);
        }
    }
}
