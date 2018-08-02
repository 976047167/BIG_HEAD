using System.Collections;
using System.Collections.Generic;
using AppSettings;


public class Trade
{
    //返回值为错误码，0表示成功
    enum TradeType
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

        TradeTableSetting tmpEvent = TradeTableSettings.Get(eventId);
        TradeType costType = (TradeType)tmpEvent.CostType;
        switch (costType)
        {
            case TradeType.Unknow:
                break;
            case TradeType.HP:
                if (MapMgr.Instance.MyMapPlayer.Data.HP <= tmpEvent.CostNum)
                    return 1;
                else
                    Game.DataManager.MyPlayer.Data.HP -= tmpEvent.CostNum;
                break;

            case TradeType.Food:
                if (MapMgr.Instance.MyMapPlayer.Data.Food < tmpEvent.CostNum)
                    return 1;
                else
                    MapMgr.Instance.MyMapPlayer.Data.Food -= tmpEvent.CostNum;
                break;
            case TradeType.MP:
                if (Game.DataManager.MyPlayer.Data.MP < tmpEvent.CostNum)
                    return 1;
                else
                    Game.DataManager.MyPlayer.Data.MP -= tmpEvent.CostNum;
                break;
            case TradeType.Coin:
                if (MapMgr.Instance.MyMapPlayer.Data.Gold < tmpEvent.CostNum)
                    return 1;
                else
                    MapMgr.Instance.MyMapPlayer.Data.Gold -= tmpEvent.CostNum;
                break;
            case TradeType.Equip:
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
        switch ((TradeType)tmpEvent.Type)
        {
            case TradeType.Unknow:
                break;
            case TradeType.HP:

                Game.DataManager.MyPlayer.Data.HP += tmpEvent.Num;
                if (Game.DataManager.MyPlayer.Data.HP > Game.DataManager.MyPlayer.Data.MaxHP)
                    Game.DataManager.MyPlayer.Data.HP = Game.DataManager.MyPlayer.Data.MaxHP;
                break;

            case TradeType.Food:
                MapMgr.Instance.MyMapPlayer.Data.Food += tmpEvent.Num;
                break;
            case TradeType.MP:
                Game.DataManager.MyPlayer.Data.MP += tmpEvent.Num;
                if (Game.DataManager.MyPlayer.Data.MP > Game.DataManager.MyPlayer.Data.MaxMP)
                    Game.DataManager.MyPlayer.Data.MP = Game.DataManager.MyPlayer.Data.MaxMP;
                break;
            case TradeType.Coin:
                MapMgr.Instance.MyMapPlayer.Data.Gold += tmpEvent.Num;
                break;
            case TradeType.Equip:
                for (int j = 0; j < tmpEvent.CostNum; j++)
                {
                    //Game.DataManager.MyPlayer.DetailData.EquipList.Add(new BattleCardData(tmpEvent.ItemId, Game.DataManager.MyPlayer));

                }
                break;


            default:
                return 2;


        }
        Messenger.Broadcast(MessageId.MAP_UPDATE_PLAYER_INFO);
        return 0;
    }

    // Use this for initialization

}