using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class DrawCard : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.DrawCard; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
