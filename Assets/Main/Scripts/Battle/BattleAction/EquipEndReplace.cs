using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class EquipEndReplace : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.EquipEndReplace; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
