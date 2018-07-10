using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class TansferBuffs : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.TansferBuffs; } }
        public override void Excute()
        {
            List<BattleBuffData> canRemoveList = new List<BattleBuffData>();
            for (int i = 0; i < owner.Data.BuffList.Count; i++)
            {
                if (owner.Data.BuffList[i].Data.DispelGrade <= actionArg)
                {
                    canRemoveList.Add(owner.Data.BuffList[i]);
                }
            }

            for (int i = 0; i < canRemoveList.Count; i++)
            {
                //owner.Data.BuffList.Remove(canRemoveList[i]);
                Create(BattleActionType.RemoveBuff, canRemoveList[i].BuffId, 0, sourceData, owner, owner, canRemoveList[i]).Excute();
                Create(BattleActionType.AddBuff, canRemoveList[i].BuffId, canRemoveList[i].Layer, sourceData, target, target, canRemoveList[i]).Excute();
            }
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
