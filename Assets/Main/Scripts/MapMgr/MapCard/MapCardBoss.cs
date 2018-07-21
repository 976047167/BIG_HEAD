using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCardBoss : MapCardMonster
{
    protected override void ChangeState(CardState oldState, CardState newState)
    {
        transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    

    protected override void RefreshState()
    {
        transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    }
}
