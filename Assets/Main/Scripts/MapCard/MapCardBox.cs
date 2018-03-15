using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCardBox : MapCardBase
{
    public override void OnPlayerEnter()
    {

        if (isFirstEnter)
        {
            if (Random.Range(0, 1000) % 2 == 0)
                Game.DataManager.Food += 5;
            else
                Game.DataManager.Coin += 5;
        }
        base.OnPlayerEnter();
    }
}
