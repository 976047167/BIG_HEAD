using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class GetCard : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.GetCard; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
