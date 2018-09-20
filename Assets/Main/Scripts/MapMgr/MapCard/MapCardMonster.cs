using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class MapCardMonster : MapCardBase
{
    [SerializeField]
    protected int monsterId = 0;

    protected override void OnPlayerEnter()
    {

        //进入战斗
        if (isFirstEnter)
        {
            UIUtility.ShowMapDialog(TableData.Id);
        }
        else
        {
            UIUtility.ShowMapDialog(2, TableData.Id);
        }

        Used = true;
    }

    protected override void OnInit()
    {
        int count = BattleMonsterTableSettings.GetInstance().Count;
        monsterId = Random.Range(1, count + 1);
    }
}
