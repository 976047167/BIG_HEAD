using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer
{
    public bool UseAI { private set; get; }
    public BattlePlayerData Data { get; private set; }
    private BattlePlayerAI playerAI;
    public bool IsMe { get; private set; }

    public BattlePlayer(BattlePlayerData playerData)
    {
        Data = playerData;
        Data.CurrentCardList = new List<BattleCardData>(Data.CardList);
        Data.AP = 0;
        Data.MaxAP = 0;
        IsMe = Data == Game.BattleManager.MyPlayerData;

    }
    public void EndRound()
    {
        Game.BattleManager.RoundEnd();
    }

    public void StartAI()
    {
        if (playerAI == null)
        {
            playerAI = new BattlePlayerAI(this);
        }
        UseAI = true;
        playerAI.StartAI();
    }

    public void UpdateAI()
    {
        if (UseAI == false)
        {
            return;
        }
        playerAI.UpdateAI();
    }
    public void StopAI()
    {
        if (UseAI)
        {
            UseAI = false;
            playerAI.StopAI();
        }
    }

}
