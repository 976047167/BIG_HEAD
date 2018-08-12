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

    public MapLayerData CurrentMapLayerData = new MapLayerData(0, "", 5, 5);


    public void NextLayer()
    {
        CurrentMapLayerData = new MapLayerData(CurrentMapLayerData.LayerId + 1, "", 5, 5);
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
}
