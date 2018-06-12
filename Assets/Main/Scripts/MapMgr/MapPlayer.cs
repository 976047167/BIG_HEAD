using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class MapPlayer
{
    protected GameObject m_gameObject;
    protected MapPlayerData m_Data;
    public MapPlayerData Data { get { return m_Data; } }
    public GameObject PlayerGO { get { return m_gameObject; } }

    private Player m_Player;

    public MapCardPos CurPos;

    public MapPlayer(Player player)
    {
        m_Player = player;
        m_Data = new MapPlayerData(m_Player.Data);
    }

    public void CreateModel(MapCardPos pos)
    {
        CurPos = pos;
        ResourceManager.LoadGameObject(CharacterModelTableSettings.Get(ClassCharacterTableSettings.Get(m_Data.ClassData.CharacterID).ModelID).Path, LoadPlayerSuccess,
            (str, obj) => { Debug.LogError("Load player Failed!"); }
            );
    }
    void LoadPlayerSuccess(string path, object[] userData, GameObject go)
    {
        m_gameObject = go;
        m_gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        m_gameObject.transform.position = MapMgr.Instance.GetTransfromByPos(CurPos);
    }

    public void MoveTo(MapCardPos pos)
    {
        MapMgr.Instance.GetMapCard(CurPos.X, CurPos.Y).OnPlayerExit();
        Vector3 direction = (new Vector3(pos.X - CurPos.X, 0f, pos.Y - CurPos.Y)).normalized;
        CurPos = pos;
        TweenPosition.Begin(m_gameObject, 0.5f, MapMgr.Instance.GetTransfromByPos(pos), true);
        //Quaternion quaternion = Quaternion.FromToRotation(m_gameObject.transform.forward, direction);
        m_gameObject.transform.localRotation = Quaternion.LookRotation(direction);
        MapCardBase mapcard = MapMgr.Instance.GetMapCard(pos.X, pos.Y);
        if (mapcard != null)
        {
            mapcard.State = MapCardBase.CardState.Front;
            mapcard.OnPlayerEnter();
        }
    }

}
