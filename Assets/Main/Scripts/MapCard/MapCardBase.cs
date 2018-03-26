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
        "MapCardShop",
        "MapCardBox",
        "MapCardNpc"
    };
    static string MapCardDoor = "MapCardDoor";
    static string MapCardPlayer = "MapCardPlayer";

    public static MapCardBase CreateMapCard<T>() where T : MapCardBase
    {
        //Debug.LogError("Prefabs/MapCard/" + typeof(T).ToString());
        GameObject prefab = Resources.Load<GameObject>("Prefabs/MapCard/" + typeof(T).ToString());
        GameObject go = Instantiate(prefab);
        MapCardBase mapCard = go.GetComponent<T>();
        return mapCard;
    }

    public static MapCardBase GetRandomMapCard()
    {
        string cardType = MapCardName[Random.Range(0, MapCardName.Length)];
        return Instantiate(Resources.Load<MapCardBase>("Prefabs/MapCard/MapCardMonster"));
        //return Instantiate(Resources.Load<MapCardBase>("Prefabs/MapCard/" + cardType));
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
    public void Init()
    {
        transform.position = new Vector3((X - 2) * 2f, 0.1f, (Y - 2) * 2f);
        if (state == CardState.Behind)
        {
            transform.localEulerAngles = new Vector3(0f, 0f, 180f);
        }
        else if (state == CardState.Front)
        {
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
        UIEventListener.Get(transform.Find("Card").gameObject).onClick = OnClick;
        OnInit();
    }

    public virtual void OnInit()
    {

    }

    void OnClick(GameObject go)
    {
        Debug.LogError(name + "  " + Pos.X + ":" + Pos.Y);
        MapLogic.Instance.OnClickMapCard(this);

    }
    #region 地图事件响应

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
    protected bool isFirstEnter = true;
    /// <summary>
    /// 玩家进入时发生
    /// </summary>
    public virtual void OnPlayerEnter()
    {
        if (isFirstEnter)
        {
            isFirstEnter = false;
        }
        Game.DataManager.Food--;
        if (Game.DataManager.MyPlayerData.HP < Game.DataManager.MyPlayerData.MaxHP)
        {
            Game.DataManager.MyPlayerData.HP++;
        }
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
    #endregion
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
        state = newState;
        switch (newState)
        {
            case CardState.Behind:
                TweenRotation.Begin(gameObject, 0.5f, transform.localRotation * Quaternion.Euler(0f, 0f, 0f));
                break;
            case CardState.Front:
                TweenRotation.Begin(gameObject, 0.5f, transform.localRotation * Quaternion.Euler(0f, 0f, 180f));
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
[SerializeField]
public struct MapCardPos
{
    [SerializeField]
    public int X;
    [SerializeField]
    public int Y;
    public MapCardPos(int x, int y)
    {
        X = x;
        Y = y;
    }
}
