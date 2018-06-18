using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using AppSettings;

public partial class BattleAction
{
    public class AddOppBuff : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.AddOppBuff; } }
        public override void Excute()
        {
            target = owner.IsMe ? battleMgr.OppPlayer : battleMgr.MyPlayer;
            Create(BattleActionType.AddBuff, actionArg, actionArg2, cardData, owner, target);
        }
    }
}
