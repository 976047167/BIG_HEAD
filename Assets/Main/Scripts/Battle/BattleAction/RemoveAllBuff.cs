using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class RemoveAllBuff : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.RemoveAllBuff; } }
        public override void Excute()
        {
            BattlePlayer player = actionArg == 0 ? owner : target;
            List<BattleBuffData> removeList = new List<BattleBuffData>();
            for (int i = 0; i < player.Data.BuffList.Count; i++)
            {
                BattleBuffData buff = player.Data.BuffList[i];
                if (buff.Data.DispelGrade <= actionArg2)
                {
                    removeList.Add(buff);
                }
            }
            for (int i = 0; i < removeList.Count; i++)
            {
                player.Data.BuffList.Remove(removeList[i]);
            }
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
