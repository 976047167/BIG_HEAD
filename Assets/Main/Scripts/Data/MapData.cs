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
            if (_instance==null)
            {
                _instance = new MapData();
            }
            return _instance;
        }
    }

    

}
