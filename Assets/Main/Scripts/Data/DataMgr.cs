using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : MonoBehaviour
{
    private DataMgr()
    {

    }
    static DataMgr instance = null;
    public static DataMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataMgr();
                instance.OnInit();
            }
            return instance;
        }

    }
    public int MaxBlood;
    public int Blood;
    public int Food;
    public int Coin;

    public void OnInit()
    {
        MaxBlood = 20;
        Blood = MaxBlood;
        Food = 20;
        Coin = 20;
    }
}
