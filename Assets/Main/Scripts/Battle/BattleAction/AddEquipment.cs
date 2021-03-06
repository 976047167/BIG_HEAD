﻿using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public partial class BattleAction
{
    public class AddEquipment : BattleAction
    {
        public static BattleActionType ActionType { get { return BattleActionType.AddEquipment; } }
        public override void Excute()
        {
            if (owner.Data.EquipList.Count > 0 && owner.Data.EquipList.Count >= BattleMgr.MAX_EQUIP_COUNT)
            {
                owner.Data.EquipList.RemoveAt(0);
            }
            BattleEquipData battleEquipData = new BattleEquipData(actionArg, sourceData, owner);
            owner.Data.EquipList.Add(battleEquipData);
            battleMgr.AddUIAction(new UIAction.UIAddEquip(battleEquipData));
        }

        public override int Excute(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
