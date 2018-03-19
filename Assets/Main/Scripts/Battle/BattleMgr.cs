using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMgr
{



    /// <summary>
    /// 开始战斗
    /// </summary>
    public void StartBattle(int monsterId)
    {
        Game.DataManager.SetOppData(monsterId);
        Game.UI.OpenForm<UIBattleForm>();
    }

}
