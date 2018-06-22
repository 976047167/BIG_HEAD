using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveShield : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.RemoveShield; } }
        public override void Excute()
        {
            Debug.LogError("用扣除buff层数代替");
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
