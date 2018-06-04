using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class BuffLayerEffect : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.BuffLayerEffect; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
