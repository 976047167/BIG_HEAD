using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG;
using System.Reflection;
using Type = System.Type;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
/// <summary>
/// 地图上的那些背景地形卡牌，山河之类的
/// </summary>
[System.Serializable]
public class MapCardBase
{
    public TextMeshPro infoBoard;
    public int X { get { return pos.X; } set { pos.X = value; RefreshPos(); } }
    public int Y { get { return pos.Y; } set { pos.Y = value; RefreshPos(); } }
    public GameObject gameObject { protected set; get; }
    public Transform transform { protected set; get; }
    public Transform parent { get; protected set; }
    private MapCardPos pos = new MapCardPos(0, 0);
    public MapCardPos Position { get { return pos; } }
    private CardState state;
    /// <summary>
    /// gameObject的状态
    /// </summary>
    public bool Active { get; protected set; }
    /// <summary>
    /// 是否激活，处在可交互状态
    /// </summary>
    public bool Activating { get; protected set; }
    protected bool isFirstEnter = true;
    public bool Used { get; protected set; }

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
        Activating = false;
        state = CardState.None;
        Used = false;
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

    public static MapCardBase CreateMapCard<T>(MapCardPos pos = null, CardState defaultState = CardState.None) where T : MapCardBase, new()
    {

        MapCardBase mapCard = new T();
        if (pos != null)
        {
            mapCard.pos = new MapCardPos(pos.X, pos.Y);
        }
        if (defaultState!= CardState.None)
        {
            mapCard.state = defaultState;
        }
        ResourceManager.LoadGameObject("MapCard/" + typeof(T).ToString(), LoadAssetSuccessess, LoadAssetFailed, mapCard);

        return mapCard;
    }

    public static MapCardBase GetRandomMapCard(MapCardPos pos = null, CardState defaultState = CardState.None)
    {
        string cardType = MapCardName[Random.Range(0, MapCardName.Length)];
        if (Random.Range(0, 100) % 3 == 0)
        {
            cardType = "MapCardMonster";
        }
        MapCardBase mapCard = Assembly.GetExecutingAssembly().CreateInstance(cardType) as MapCardBase;
        if (pos != null)
        {
            mapCard.pos = new MapCardPos(pos.X, pos.Y);
        }
        if (defaultState != CardState.None)
        {
            mapCard.state = defaultState;
        }
        ResourceManager.LoadGameObject("MapCard/" + cardType, LoadAssetSuccessess, LoadAssetFailed, mapCard);
        return mapCard;
    }

    static void LoadAssetSuccessess(string path, object[] args, GameObject go )
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
    public void SetUsed(bool used)
    {
        Used = used;
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
        EnterMap();
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
    protected virtual void OnInit()
    {

    }

    void OnClick(GameObject go)
    {
        Debug.Log(gameObject.name + "  " + pos.X + ":" + pos.Y);
        if (Activating)
        {
            MapMgr.Instance.OnClickMapCard(this);
        }
    }
    #region 事件调用


    /// <summary>
    /// 被放置到地图里
    /// </summary>
    public void EnterMap()
    {
        //Debug.LogError(gameObject.name + "[" + pos.X + "," + pos.Y + "]" + "=>" + transform.position.ToString());
        isFirstEnter = true;
        Vector3 target = transform.position;
        transform.position = target + new Vector3(0f, 20f, 0f);
        DOTween.To(() => transform.position, (x) => transform.position = x, target, Random.Range(0.4f, 2f))
            .OnComplete(() => { Activate(); });
    }
    /// <summary>
    /// 被激活
    /// </summary>
    public void Activate()
    {
        Activating = true;
        OnActivate();
    }
    /// <summary>
    /// 被冻结
    /// </summary>
    public void Freeze()
    {
        Activating = false;
        OnFreeze();
    }
    /// <summary>
    /// 被移出地图时触发
    /// </summary>
    public void ExitMap()
    {
        isFirstEnter = false;
        Freeze();
        Vector3 target = transform.position + new Vector3(0f, 20f, 0f);
        DOTween.To(() => transform.position, (x) => transform.position = x, target, Random.Range(0.4f, 0.5f))
            .OnComplete(() => { OnExitMap(); });
    }
    /// <summary>
    /// 玩家进入时发生
    /// </summary>
    public void PlayerEnter()
    {
        if (Used == false)
        {
            MapMgr.Instance.MyMapPlayer.Data.Food--;
            if (MapMgr.Instance.MyMapPlayer.Data.HP < MapMgr.Instance.MyMapPlayer.Data.MaxHP)
            {
                MapMgr.Instance.MyMapPlayer.Data.HP++;
            }
            Messenger.Broadcast(MessageID.MAP_UPDATE_PLAYER_INFO);
            OnPlayerEnter();
        }
        if (isFirstEnter)
        {
            isFirstEnter = false;
        }
    }
    /// <summary>
    /// 与玩家互动时发生
    /// </summary>
    public void PlayerInteract()
    {

    }
    /// <summary>
    /// 玩家离开该方块时发生
    /// </summary>
    public void PlayerExit()
    {

    }
    /// <summary>
    /// 天气环境发生改变
    /// </summary>
    public void WeatherBeginChange()
    {

    }
    /// <summary>
    /// 天气或者环境发生交互
    /// </summary>
    public void WeatherInteract()
    {

    }
    /// <summary>
    /// 天气变化结束
    /// </summary>
    public void WeatherEnd()
    {

    }
    #endregion
    #region 地图事件响应

    /// <summary>
    /// 被放置到地图里
    /// </summary>
    protected virtual void OnEnterMap()
    {

    }
    /// <summary>
    /// 被激活
    /// </summary>
    protected virtual void OnActivate()
    {

    }
    /// <summary>
    /// 被冻结
    /// </summary>
    protected virtual void OnFreeze()
    {

    }
    /// <summary>
    /// 被移出地图时触发
    /// </summary>
    protected virtual void OnExitMap()
    {
        SetActive(false);
    }


    /// <summary>
    /// 玩家进入时发生
    /// </summary>
    protected virtual void OnPlayerEnter()
    {
        
    }
    /// <summary>
    /// 与玩家互动时发生
    /// </summary>
    protected virtual void OnPlayerInteract()
    {

    }
    /// <summary>
    /// 玩家离开该方块时发生
    /// </summary>
    protected virtual void OnPlayerExit()
    {

    }
    /// <summary>
    /// 天气环境发生改变
    /// </summary>
    protected virtual void OnWeatherBeginChange()
    {

    }
    /// <summary>
    /// 天气或者环境发生交互
    /// </summary>
    protected virtual void OnWeatherInteract()
    {

    }
    /// <summary>
    /// 天气变化结束
    /// </summary>
    protected virtual void OnWeatherEnd()
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
[System.Serializable]
public class MapCardPos
{
    public int X;
    public int Y;
    public MapCardPos(int x, int y)
    {
        X = x;
        Y = y;
    }
}

