using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActionAddBuff : BattleActionBase
{
    public BattleActionType ActionId = BattleActionType.AddBuff;

    public override void Excute(int num)
    {
        Game.BattleManager.OppPlayerData.HP--;
    }


}
