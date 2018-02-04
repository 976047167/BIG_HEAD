using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLogic : MonoBehaviour
{
    public MapCardBase[,] maplist;
    List<MapCardBase> mapCards = new List<MapCardBase>();
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeMap()
    {
        maplist = new MapCardBase[5, 5];
        mapCards.Clear();
        int carCount = Random.Range(6, 15);
        //生成的预位置
        List<MapCardPos> poss = new List<MapCardPos>(4);
        //成功生成格子的标志位
        bool createSuccess = false;
        for (int i = 0; i < carCount; i++)
        {
            //先确定出生点
            if (i == 0)
            {
                mapCards[i] = MapCardBase.CreateMapCard<MapCardDoor>();
                mapCards[i].X = Random.Range(0, 5);
                mapCards[i].Y = Random.Range(0, 5);
                maplist[mapCards[i].X, mapCards[i].Y] = mapCards[i];
                mapCards[i].state = MapCardBase.CardState.Front;
                continue;
            }
            
            mapCards[i] = MapCardBase.GetRandomMapCard();
            poss.Clear();
            if (mapCards[0].X > 0 && mapCards[0].Y < 4)
            {
                //poss.Add(new MapCardPos(mapCards))
            }
        }
    }

}
