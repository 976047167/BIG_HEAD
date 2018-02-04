using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData
{
    private static MapData _instance;
    public static MapData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MapData();
            }
            return _instance;
        }
    }
    

    public static Dictionary<int, MapLayerData> DicLayerDatas = new Dictionary<int, MapLayerData>();

    public MapLayerData CurrentMapLayerData = new MapLayerData(0);


    public void NextLayer()
    {
        CurrentMapLayerData = new MapLayerData(CurrentMapLayerData.LayerId + 1);
        //切换层表现
    }

    public void PreviousLayer()
    {
        if (CurrentMapLayerData.LayerId > 0 && DicLayerDatas.ContainsKey(CurrentMapLayerData.LayerId - 1))
        {
            CurrentMapLayerData = DicLayerDatas[CurrentMapLayerData.LayerId - 1];
            //切换层表现
        }
    }




   

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
}
