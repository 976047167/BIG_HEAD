using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class GetOppHandCardByAttack : BattleActionBase
    {
        public static BattleActionType ActionType { get { return BattleActionType.GetOppHandCardByAttack; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
