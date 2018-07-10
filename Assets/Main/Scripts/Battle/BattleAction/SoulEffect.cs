using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class SoulEffect : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.SoulEffect; } }
        public override void Excute()
        {
            int count = 0;
            for (int i = 0; i < owner.Data.BuffList.Count; i++)
            {
                BattleBuffData buff = owner.Data.BuffList[i];
                if (buff.BuffId == actionArg)
                {
                    count = buff.Layer;
                    break;
                }
            }
            if (count < 3)
            {
                owner.Data.MP += 5;
                if (owner.Data.MP > owner.Data.MaxMP)
                {
                    owner.Data.MP = owner.Data.MaxMP;
                }
            }
            if (count >= 3)
            {
                owner.Data.MP += 5;
                if (owner.Data.MP > owner.Data.MaxMP)
                {
                    owner.Data.MP = owner.Data.MaxMP;
                }
                Create(BattleActionType.Attack, 1, 0, sourceData, owner, target, null).Excute();
            }
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
