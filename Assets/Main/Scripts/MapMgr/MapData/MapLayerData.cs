using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 一层数据体
/// </summary>
public class MapLayerData
{
    public string Name{ get; private set; }
    public int LayerId { get; private set; }

    private MapCardBase[,] MapPointDatas = null;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public MapLayerData(int layerId,string name, int width, int height)
    {
        LayerId = layerId;
        Name = name;
        MapData.DicLayerDatas[layerId] = this;
        MapPointDatas = new MapCardBase[width, height];
    }

    public MapCardBase this[int x, int y]
    {
        get { return MapPointDatas[x, y]; }
        set { MapPointDatas[x, y] = value; }
    }
    public MapCardBase this[MapCardPos pos]
    {
        get { return MapPointDatas[pos.X, pos.Y]; }
        set { MapPointDatas[pos.X, pos.Y] = value; }
    }
    public void Clean()
    {
        for (int i = 0; i < ConstValue.MAP_WIDTH; i++)
        {
            for (int j = 0; j < ConstValue.MAP_HEIGHT; j++)
            {
                //if (MapCardDatas[i, j] != null)
                //{
                //    MapCardDatas[i, j].Destory();
                //}
                MapPointDatas[i, j] = null;
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
                if (MapPointDatas[i, j] == null)
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
        if (x > 0 && MapPointDatas[x - 1, y] == null)
        {
            poss.Add(new MapCardPos(x - 1, y));
        }
        if (x < ConstValue.MAP_WIDTH - 1 && MapPointDatas[x + 1, y] == null)
        {
            poss.Add(new MapCardPos(x + 1, y));
        }
        if (y < ConstValue.MAP_HEIGHT - 1 && MapPointDatas[x, y + 1] == null)
        {
            poss.Add(new MapCardPos(x, y + 1));
        }
        if (y > 0 && MapPointDatas[x, y - 1] == null)
        {
            poss.Add(new MapCardPos(x, y - 1));
        }
        return poss;
    }
}

