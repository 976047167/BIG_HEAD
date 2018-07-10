using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class EquipEndReplace : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.EquipEndReplace; } }
        public override void Excute()
        {
            if (actionArg > 0)
            {
                Create(BattleActionType.AddEquipment, actionArg, actionArg2, sourceData, owner, owner, null).Excute();
            }
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
