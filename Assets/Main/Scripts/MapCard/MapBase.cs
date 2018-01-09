using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 地图上的那些背景地形卡牌，山河之类的
/// </summary>
public class MapBase : MonoBehaviour
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

    public static MapBase CreateMapCard<T>(T mapType) where T : MapBase
    {
        Debug.LogError("Prefab/MapCard/" + typeof(T).ToString());
        MapBase mapCard = Resources.Load<MapBase>("Prefab/MapCard/" + typeof(T).ToString());
        return mapCard;
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
}
