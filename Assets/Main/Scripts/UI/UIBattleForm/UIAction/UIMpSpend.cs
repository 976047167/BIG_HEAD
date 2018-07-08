using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIMpSpend : UIAction
    {
        public BattlePlayer Player { get; private set; }
        public int SpentAp { get; private set; }
        public UIMpSpend(BattlePlayer player, int spentAp) : base()
        {
            Player = player;
            SpentAp = spentAp;
        }

        public override IEnumerator Excute()
        {
            PlayerInfoView playerInfoView= BattleForm.GetPlayerInfoViewByPlayer(Player);
            Color orginColor = playerInfoView.lblMP.color;
            playerInfoView.lblMP.color = Color.green;
            yield return null;
            TweenScale.Begin(playerInfoView.lblMP.gameObject, 0.15f, new Vector3(1.2f, 1.2f, 1.2f));
            yield return new WaitForSeconds(0.15f);
            playerInfoView.PlayerInfo.MP -= SpentAp;
            TweenScale.Begin(playerInfoView.lblMP.gameObject, 0.15f, Vector3.one);
            yield return new WaitForSeconds(0.15f);
            playerInfoView.lblMP.color = orginColor;
            yield return null;
        }
    }
}
