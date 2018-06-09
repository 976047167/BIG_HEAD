using System.Collections;
using System.Collections.Generic;
using AppSettings;


public class DealEvent
{
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
                if (Game.DataManager.MyPlayer.Data.HP <= tmpEvent.CostNum)
                    return 1;
                else
                    Game.DataManager.MyPlayer.Data.HP -= tmpEvent.CostNum;
                break;

            case EventType.Food:
                if (Game.DataManager.Food < tmpEvent.CostNum)
                    return 1;
                else
                    Game.DataManager.Food -= tmpEvent.CostNum;
                break;
            case EventType.MP:
                if (Game.DataManager.MyPlayer.Data.MP < tmpEvent.CostNum)
                    return 1;
                else
                    Game.DataManager.MyPlayer.Data.MP -= tmpEvent.CostNum;
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
                    foreach (NormalCard i in Game.DataManager.MyPlayer.Data.EquipList)
                    {
                        if (i.CardId == tmpEvent.CostItemId)
                        {
                            Game.DataManager.MyPlayer.Data.EquipList.Remove(i);
                            done = true;
                            break;
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

                Game.DataManager.MyPlayer.Data.HP += tmpEvent.Num;
                if (Game.DataManager.MyPlayer.Data.HP > Game.DataManager.MyPlayer.Data.MaxHP)
                    Game.DataManager.MyPlayer.Data.HP = Game.DataManager.MyPlayer.Data.MaxHP;
                break;

            case EventType.Food:
                Game.DataManager.Food += tmpEvent.Num;
                break;
            case EventType.MP:
                Game.DataManager.MyPlayer.Data.MP += tmpEvent.Num;
                if (Game.DataManager.MyPlayer.Data.MP > Game.DataManager.MyPlayer.Data.MaxMP)
                    Game.DataManager.MyPlayer.Data.MP = Game.DataManager.MyPlayer.Data.MaxMP;
                break;
            case EventType.Coin:
                Game.DataManager.Coin += tmpEvent.Num;
                break;
            case EventType.Equip:
                for (int j = 0; j < tmpEvent.CostNum; j++)
                {
                    //Game.DataManager.MyPlayer.DetailData.EquipList.Add(new BattleCardData(tmpEvent.ItemId, Game.DataManager.MyPlayer));
          
                }
                break;


            default:
                return 2;


        }

        return 0;
    }

    // Use this for initialization

}