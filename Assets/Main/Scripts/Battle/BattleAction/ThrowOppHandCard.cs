using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class ThrowOppHandCard : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.ThrowOppHandCard; } }
        public override void Excute()
        {
            int count = Mathf.Min(actionArg, target.Data.HandCardList.Count);
            List<BattleCardData> removeList = new List<BattleCardData>(count);
            List<BattleCardData> canRemoveList = new List<BattleCardData>();
            canRemoveList.AddRange(target.Data.HandCardList);
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
                target.Data.HandCardList.Remove(removeList[i]);
            }
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
