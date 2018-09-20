using AppSettings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCardBoss : MapCardBase
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



    protected override void ChangeState(CardState oldState, CardState newState)
    {
        transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    }



    protected override void RefreshState()
    {
        transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    }
}
