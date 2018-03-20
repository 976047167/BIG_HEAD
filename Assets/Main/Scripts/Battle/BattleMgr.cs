using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMgr
{

    //UIBattleForm battleForm;

    /// <summary>
    /// 开始战斗
    /// </summary>
    public void StartBattle(int monsterId)
    {
        Debug.LogError("StartBattle => " + monsterId);
        Game.DataManager.SetOppData(monsterId);
        Game.UI.OpenForm<UIBattleForm>();
        //预装的buff和武器、技能等上膛

    }
    /// <summary>
    /// 一回合开始
    /// </summary>
    public void RoundStart()
    {

    }

}
