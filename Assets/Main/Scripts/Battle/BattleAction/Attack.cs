using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class BattleAction
{
    public class Attack : BattleActionBase
    {
        public BattleActionType ActionType { get { return BattleActionType.Attack; } }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
