using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveBuffLayers : BattleActionBase
    {
        public static BattleActionType ActionType { get { return BattleActionType.RemoveBuffLayers; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
