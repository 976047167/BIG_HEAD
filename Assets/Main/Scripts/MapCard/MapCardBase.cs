using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG;
using System.Reflection;
using Type = System.Type;
/// <summary>
/// 地图上的那些背景地形卡牌，山河之类的
/// </summary>
[System.Serializable]
public class MapCardBase
{
    public TextMeshPro infoBoard;
    public int X { get { return Pos.X; } set { Pos.X = value; RefreshPos(); } }
    public int Y { get { return Pos.Y; } set { Pos.Y = value; RefreshPos(); } }
    public GameObject gameObject { protected set; get; }
    public Transform transform { protected set; get; }
    public Transform parent { get; protected set; }
    public MapCardPos Pos;
    private CardState state;
    public bool Active { get; private set; }

    public CardState State
    {
        get
        {
            return state;
        }

        set
        {
            if ((int)value < 10)
            {
                return;
            }
            if (state == value)
            {
                return;
            }
            ChangeState(state, value);
            state = value;
        }
    }

    public MapCardBase()
    {
        Active = true;
        state = CardState.None;
    }
    public void Destory()
    {
        GameObject.Destroy(gameObject);
        gameObject = null;
        transform = null;
        parent = null;
    }

    static string[] MapCardName = {
        "MapCardMonster",
        "MapCardShop",
        "MapCardBox",
        "MapCardNpc"
    };
    static string MapCardDoor = "MapCardDoor";
    static string MapCardPlayer = "MapCardPlayer";

    public static MapCardBase CreateMapCard<T>() where T : MapCardBase, new()
    {

        MapCardBase mapCard = new T();

        ResourceManager.LoadGameObject("MapCard/" + typeof(T).ToString(), LoadAssetSuccessess, LoadAssetFailed, mapCard);

        return mapCard;
    }

    public static MapCardBase GetRandomMapCard()
    {
        string cardType = MapCardName[Random.Range(0, MapCardName.Length)];
        if (Random.Range(0, 100) % 3 == 0)
        {
            cardType = "MapCardMonster";
        }
        //Type type = Type.GetType("MapCardMonster");
        MapCardBase mapCard = Assembly.GetExecutingAssembly().CreateInstance(cardType) as MapCardBase;
        ResourceManager.LoadGameObject("MapCard/" + cardType, LoadAssetSuccessess, LoadAssetFailed, mapCard);
        //return CreateMapCard<MapCardMonster>();
        return mapCard;
    }

    static void LoadAssetSuccessess(string path, object[] args, GameObject go)
    {
        MapCardBase mapCard = args[0] as MapCardBase;
        go.AddComponent<MapCardHelper>().MapCardData = mapCard;
        mapCard.Init(go);

    }
    static void LoadAssetFailed(string path, object[] args)
    {
        Debug.LogError("Load [" + path + "] Failed!");
        //ResourceManager.Load<GameObject>(path, args, LoadAssetScuess, LoadAssetFailed);
    }
    public void SetActive(bool active)
    {
        Active = active;
        if (gameObject != null)
        {
            gameObject.SetActive(active);
        }
    }
    public void SetParent(Transform parent)
    {
        if (this.parent != parent)
        {
            this.parent = parent;
        }
        if (gameObject != null)
        {
            transform.SetParent(parent);
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public bool Init(GameObject go)
    {
        gameObject = go;
        if (gameObject == null)
        {
            return false;
        }
        transform = gameObject.transform;
        gameObject.SetActive(Active);
        transform.SetParent(parent);
        RefreshPos();
        RefreshState();
        UIEventListener.Get(transform.Find("Card").gameObject).onClick = OnClick;
        OnInit();
        return true;
    }

    void RefreshPos()
    {
        if (gameObject == null)
        {
            return;
        }
        transform.position = new Vector3((X - 2) * 2f, 0.1f, (Y - 2) * 2f);
    }
    /// <summary>
    /// 刷新当前状态
    /// </summary>
    protected virtual void RefreshState()
    {
        switch (state)
        {
            case CardState.Behind:
                transform.localEulerAngles = new Vector3(0f, 0f, 180f);
                break;
            case CardState.Front:
                transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 状态发生改变
    /// </summary>
    /// <param name="oldState"></param>
    /// <param name="newState"></param>
    protected virtual void ChangeState(CardState oldState, CardState newState)
    {
        if (gameObject == null)
        {
            return;
        }
        switch (state)
        {
            case CardState.Behind:
                TweenRotation.Begin(gameObject, 0.5f, transform.localRotation * Quaternion.Euler(0f, 0f, 180f));
                break;
            case CardState.Front:
                TweenRotation.Begin(gameObject, 0.5f, transform.localRotation * Quaternion.Euler(0f, 0f, 180f));
                break;
            default:
                break;
        }
    }
    public virtual void OnInit()
    {

    }

    void OnClick(GameObject go)
    {
        Debug.Log(gameObject.name + "  " + Pos.X + ":" + Pos.Y);
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
        if (Game.DataManager.MyPlayer.Data.HP < Game.DataManager.MyPlayer.Data.MaxHP)
        {
            Game.DataManager.MyPlayer.Data.HP++;
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

