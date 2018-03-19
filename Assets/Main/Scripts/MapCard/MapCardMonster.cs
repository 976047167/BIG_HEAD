using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class MapCardMonster : MapCardBase
{
    int monsterId = 0;

    public override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        //进入战斗
        Game.BattleManager.StartBattle(monsterId);
    }

    public override void Init()
    {
        int count = BattleMonsterTableSettings.GetInstance().Count;
        monsterId = Random.Range(0, count);
    }
}
