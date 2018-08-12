using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 一层数据体
/// </summary>
public class MapLayerData
{

    public int LayerId { get; private set; }

    public MapCardBase[,] MapCardDatas = null;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public MapLayerData(int layerId, int width, int height)
    {
        LayerId = layerId;
        MapData.DicLayerDatas[layerId] = this;
        MapCardDatas = new MapCardBase[width, height];
    }

    public MapCardBase this[int x, int y]
    {
        get { return MapCardDatas[x, y]; }
    }
    public void Clean()
    {
        for (int i = 0; i < ConstValue.MAP_WIDTH; i++)
        {
            for (int j = 0; j < ConstValue.MAP_HEIGHT; j++)
            {
                if (MapCardDatas[i, j] != null)
                {
                    MapCardDatas[i, j].Destory();
                }
                MapCardDatas[i, j] = null;
            }
        }
    }
    public List<MapCardPos> GetEmptyPoss()
    {
        List<MapCardPos> poss = new List<MapCardPos>();
        for (int i = 0; i < ConstValue.MAP_WIDTH; i++)
        {
            for (int j = 0; j < ConstValue.MAP_HEIGHT; j++)
            {
                if (MapCardDatas[i, j] == null)
                {
                    poss.Add(new MapCardPos(i, j));
                }
            }
        }
        return poss;
    }
    public List<MapCardPos> GetNearEmptyPoss(int x, int y)
    {
        List<MapCardPos> poss = new List<MapCardPos>();
        if (x > 0 && MapCardDatas[x - 1, y] == null)
        {
            poss.Add(new MapCardPos(x - 1, y));
        }
        if (x < ConstValue.MAP_WIDTH - 1 && MapCardDatas[x + 1, y] == null)
        {
            poss.Add(new MapCardPos(x + 1, y));
        }
        if (y < ConstValue.MAP_HEIGHT - 1 && MapCardDatas[x, y + 1] == null)
        {
            poss.Add(new MapCardPos(x, y + 1));
        }
        if (y > 0 && MapCardDatas[x, y - 1] == null)
        {
            poss.Add(new MapCardPos(x, y - 1));
        }
        return poss;
    }
}

