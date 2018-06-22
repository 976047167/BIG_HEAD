using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveSelfHP : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.RemoveSelfHP; } }
        public override void Excute()
        {
            Create(BattleActionType.RemoveHP, actionArg, actionArg2, sourceData, owner, owner).Excute();
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
