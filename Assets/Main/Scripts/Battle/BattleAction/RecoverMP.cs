using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RecoverMP : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.RecoverMP; } }
        public override void Excute()
        {
            owner.Data.MP += actionArg;
            if (owner.Data.MP> owner.Data.MaxMP)
            {
                owner.Data.MP = owner.Data.MaxMP;
            }
        }
    }
}
