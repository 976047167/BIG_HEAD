using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveRandomBuff : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.RemoveRandomBuff; } }
        public override void Excute()
        {
            List<BattleBuffData> canRemoveList = new List<BattleBuffData>();
            for (int i = 0; i < target.Data.BuffList.Count; i++)
            {
                if (target.Data.BuffList[i].Data.DispelGrade <= actionArg2)
                {
                    canRemoveList.Add(target.Data.BuffList[i]);
                }
            }
            List<BattleBuffData> removeList = new List<BattleBuffData>();
            int count = canRemoveList.Count < actionArg ? canRemoveList.Count : actionArg;
            for (int i = 0; i < count; i++)
            {
                while (true)
                {
                    int randomIndex = UnityEngine.Random.Range(0, canRemoveList.Count);
                    if (canRemoveList[randomIndex] != null)
                    {
                        removeList.Add(canRemoveList[randomIndex]);
                        canRemoveList[randomIndex] = null;
                        break;
                    }
                }
            }
            for (int i = 0; i < removeList.Count; i++)
            {
                target.Data.BuffList.Remove(removeList[i]);
            }
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
