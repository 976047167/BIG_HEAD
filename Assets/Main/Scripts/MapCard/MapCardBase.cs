using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG;
//using System;
/// <summary>
/// 地图上的那些背景地形卡牌，山河之类的
/// </summary>
public class MapCardBase : MonoBehaviour
{
    [SerializeField]
    private Material matOpened;
    [SerializeField]
    private Material matCurrent;
    [SerializeField]
    private Material matClosed;
    [SerializeField]
    private Material matShop;
    [SerializeField]
    private Material matMonster;
    [SerializeField]
    private Material matClick;
    public GameObject prefab;
    public TextMeshPro infoBoard;
    public int X { get { return Pos.X; } set { Pos.X = value; } }
    public int Y { get { return Pos.Y; } set { Pos.Y = value; } }
    public MapCardPos Pos;
    public CardState state = CardState.None;

    public void Destory()
    {
        GameObject.Destroy(gameObject);
    }

    static string[] MapCardName = {
        "MapCardMonster",
        "MapMountain",
        "MapCardShop",
        "MapRiver",
        "MapCardBox",
        "MapCardNpc"
    };
    static string MapCardDoor = "MapCardDoor";
    static string MapCardPlayer = "MapCardPlayer";

    public static MapCardBase CreateMapCard<T>() where T : MapCardBase
    {
        Debug.LogError("Prefab/MapCard/" + typeof(T).ToString());
        MapCardBase mapCard = Resources.Load<MapCardBase>("Prefab/MapCard/" + typeof(T).ToString());
        return mapCard;
    }

    public static MapCardBase GetRandomMapCard()
    {
        return Resources.Load<MapCardBase>("Prefab/MapCard/" + MapCardName[Random.Range(0, MapCardName.Length)]);
    }

    // Use this for initialization
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {

    }
    /// <summary>
    /// 被放置到地图里
    /// </summary>
    public virtual void OnEnter()
    {

    }
    /// <summary>
    /// 被激活
    /// </summary>
    public virtual void OnActive()
    {

    }
    /// <summary>
    /// 被移出地图时触发
    /// </summary>
    public virtual void OnExit()
    {

    }
    /// <summary>
    /// 玩家进入时发生
    /// </summary>
    public virtual void OnPlayerEnter()
    {

    }
    /// <summary>
    /// 与玩家互动时发生
    /// </summary>
    public virtual void OnPlayerInteract()
    {

    }
    /// <summary>
    /// 玩家离开该方块时发生
    /// </summary>
    public virtual void OnPlayerExit()
    {

    }
    /// <summary>
    /// 天气环境发生改变
    /// </summary>
    public virtual void OnWeatherBeginChange()
    {

    }
    /// <summary>
    /// 天气或者环境发生交互
    /// </summary>
    public virtual void OnWeatherInteract()
    {

    }
    /// <summary>
    /// 天气变化结束
    /// </summary>
    public virtual void OnWeatherEnd()
    {

    }

    public virtual void SetState(CardState newState)
    {
        if ((int)newState < 10)
        {
            return;
        }
        if (state == newState)
        {
            return;
        }
        switch (newState)
        {
            case CardState.Behind:
                break;
            case CardState.Front:
                break;
            default:
                break;
        }
    }



    public enum CardState
    {
        None = 0,
        MovingToFront = 1,
        MovingToBehind = 2,

        Changing = 9,

        Behind = 10,
        Front = 11,
    }
}

public struct MapCardPos
{
    public int X;
    public int Y;
    public MapCardPos(int x, int y)
    {
        X = x;
        Y = y;
    }
}
