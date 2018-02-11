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
        MakeMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeMap()
    {
        MapLayerData layerData = new MapLayerData(0);
        maplist = layerData.MapCardDatas;
        mapCards.Clear();
        int carCount = Random.Range(6, 15);
        //成功生成格子的标志位
        bool createSuccess = false;
        for (int i = 0; i < carCount; i++)
        {
            //先确定出生点
            if (i == 0)
            {
                mapCards.Add(MapCardBase.CreateMapCard<MapCardDoor>());
                mapCards[i].X = Random.Range(0, 5);
                mapCards[i].Y = Random.Range(0, 5);

                maplist[mapCards[i].X, mapCards[i].Y] = mapCards[i];
                mapCards[i].state = MapCardBase.CardState.Front;
                mapCards[i].Init();
                mapCards[i].gameObject.SetActive(true);
                continue;
            }
            MapCardPos pos = mapCards[Random.Range(0, i)].Pos;
            
            List<MapCardPos> poss = layerData.GetNearEmptyPoss(pos.X, pos.Y);
            int count = Random.Range(0, poss.Count - 1);
            if (poss.Count==0)
            {
                i--;
                continue;
            }
            try
            {
                pos = poss[count];
            }
            catch (System.Exception ex)
            {
                Debug.LogError(pos.X + "  " + pos.Y + "  " + count);
                throw;
            }
            mapCards.Add(MapCardBase.GetRandomMapCard());
            mapCards[i].X = pos.X;
            mapCards[i].Y = pos.Y;
            maplist[mapCards[i].X, mapCards[i].Y] = mapCards[i];
            mapCards[i].state = MapCardBase.CardState.Behind;
            mapCards[i].Init();
            mapCards[i].gameObject.SetActive(true);
            Debug.LogError(mapCards[i].name);
        }
    }

}
