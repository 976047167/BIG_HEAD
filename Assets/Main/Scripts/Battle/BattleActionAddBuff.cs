using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActionAddBuff : BattleActionBase
{
    public BattleActionType ActionId()
    {
        return BattleActionType.AddBuff;
    }
    public override void GameAction(int num)
    {
        Game.BattleManager.OppPlayerData.HP--;
    }

    
}
