using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 坐标点结构体
/// </summary>
public class MapPointData
{
    public int X;
    public int Y;
    public int NearByCount;
    public MapCardBase MapCard;
    public int NearByMaxCount
    {
        get
        {
            int max = 4;
            if (X == 0)
            {
                max--;
            }
            if (X == ConstValue.MAP_WIDTH - 1)
            {
                max--;
            }
            if (Y == 0)
            {
                max--;
            }
            if (Y == ConstValue.MAP_HEIGHT - 1)
            {
                max--;
            }
            return max;
        }
    }
    public bool IsFullNearby
    {
        get { return NearByCount < NearByMaxCount; }
    }
    public bool HasMapCard
    {
        get { return MapCard != null; }
    }
}
