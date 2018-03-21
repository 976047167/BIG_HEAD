using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class MapCardMonster : MapCardBase
{
    int monsterId = 0;

    public override void OnPlayerEnter()
    {
        
        //进入战斗
        if (isFirstEnter)
        {
            
            int DialogId = BattleMonsterTableSettings.Get(monsterId).DialogId;
            UIModule.Instance.OpenForm<WND_Dialog>(DialogId);
        }
        base.OnPlayerEnter();
    }

    public override void OnInit()
    {
        int count = BattleMonsterTableSettings.GetInstance().Count;
        monsterId = Random.Range(1, count + 1);
    }
}
