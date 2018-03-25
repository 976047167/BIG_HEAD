using System.Collections;
using System.Collections.Generic;
using AppSettings;


public class DealEvent  {
    //返回值为错误码，0表示成功
    enum EventType
    {
        Unknow,
        HP,
        Food,
        MP,
        Coin,
        Card,
        Equip,

    }

    static public int deal(int eventId)
    {

        EventTableSetting tmpEvent = EventTableSettings.Get(eventId);
        EventType costType = (EventType)tmpEvent.CostType;
        switch (costType)
        {
            case EventType.Unknow:
                break;
            case EventType.HP:
                if (Game.DataManager.MyPlayerData.HP <= tmpEvent.CostNum)
                    return 1;
                else
                    Game.DataManager.MyPlayerData.HP -= tmpEvent.CostNum;
                break;

            case EventType.Food:
                if (Game.DataManager.Food < tmpEvent.CostNum)
                    return 1;
                else
                    Game.DataManager.Food -= tmpEvent.CostNum;
                break;
            case EventType.MP:
                if (Game.DataManager.MyPlayerData.MP < tmpEvent.CostNum)
                    return 1;
                else
                    Game.DataManager.MyPlayerData.MP -= tmpEvent.CostNum;
                break;
            case EventType.Coin:
                if (Game.DataManager.Coin < tmpEvent.CostNum)
                    return 1;
                else
                    Game.DataManager.Coin -= tmpEvent.CostNum;
                break;
            case EventType.Equip:
                for (int j = 0; j < tmpEvent.CostNum; j++)
                {
                    bool done = false;
                    foreach (BattleCardData i in Game.DataManager.MyPlayerData.EquipList)
                    {
                        if (i.CardId == tmpEvent.CostItemId)
                        {
                            Game.DataManager.MyPlayerData.EquipList.Remove(i);
                            done = true;
                        }
                    }
                    if (done == false)
                        return 1;
                }
                break;


            default:
                return 2;

        }
        switch ((EventType)tmpEvent.Type)
        {
            case EventType.Unknow:
                break;
            case EventType.HP:

                Game.DataManager.MyPlayerData.HP += tmpEvent.Num;
                if (Game.DataManager.MyPlayerData.HP > Game.DataManager.MyPlayerData.MaxHP)
                    Game.DataManager.MyPlayerData.HP = Game.DataManager.MyPlayerData.MaxHP;
                break;

            case EventType.Food:
                Game.DataManager.Food += tmpEvent.Num;
                break;
            case EventType.MP:
                Game.DataManager.MyPlayerData.MP += tmpEvent.Num;
                if (Game.DataManager.MyPlayerData.MP > Game.DataManager.MyPlayerData.MaxMP)
                    Game.DataManager.MyPlayerData.MP = Game.DataManager.MyPlayerData.MaxMP;
                break;
            case EventType.Coin:
                Game.DataManager.Coin += tmpEvent.Num;
                break;
            case EventType.Equip:
                for (int j = 0; j < tmpEvent.CostNum; j++)
                {

                    Game.DataManager.MyPlayerData.EquipList.Add(new BattleCardData(tmpEvent.ItemId));

                }
                break;


            default:
                return 2;


        }

        return 0;
    }

    // Use this for initialization

}