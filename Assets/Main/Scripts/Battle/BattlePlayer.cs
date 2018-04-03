using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer
{
    public bool UseAI { private set; get; }
    public BattlePlayerData Data { get; private set; }
    private BattlePlayerAI playerAI;
    public bool IsMe()
    {
        return false;
    }

    public void UseCard(BattleCardData cardData)
    {

    }
    public void EndRound()
    {

    }

}
