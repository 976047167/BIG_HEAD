using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class GetOppTopCards : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.GetOppTopCards; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
