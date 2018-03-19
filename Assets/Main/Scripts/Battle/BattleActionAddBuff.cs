using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActionAddBuff : BattleActionBase
{
    public override BattleActionType ActionId()
    {
        return BattleActionType.AddBuff;
    }
    public override void GameAction(int num)
    {
        Game.DataManager.OppPlayerData.HP--;
    }

    
}
