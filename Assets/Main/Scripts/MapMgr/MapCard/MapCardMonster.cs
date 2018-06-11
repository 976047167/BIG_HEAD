using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class MapCardMonster : MapCardBase
{
    [SerializeField]
    int monsterId = 0;

    public override void OnPlayerEnter()
    {
        
        //进入战斗
        if (isFirstEnter)
        {
            
            int DialogId = BattleMonsterTableSettings.Get(monsterId).DialogId;
            List<int> a = new List<int>();
            a.Add( DialogId);
            a.Add(monsterId);
            UIModule.Instance.OpenForm<WND_Dialog>(a);
        }
        base.OnPlayerEnter();
    }

    public override void OnInit()
    {
        int count = BattleMonsterTableSettings.GetInstance().Count;
        monsterId = Random.Range(1, count + 1);
    }
}
