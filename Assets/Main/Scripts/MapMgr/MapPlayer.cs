using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayer
{
    protected GameObject m_gameObject;
    protected MapPlayerData m_Data;
    public MapPlayerData Data { get { return m_Data; } }
    public GameObject PlayerGO { get { return m_gameObject; } }

    private Player m_Player;

    public MapPlayer(Player player)
    {
        m_Player = player;
        m_Data = new MapPlayerData(m_Player.Data);
    }

    public void CreateModel()
    {
        ResourceManager.LoadGameObject("Character/Player/Player", LoadPlayerSuccess,
            (str, obj) => { Debug.LogError("Load player Failed!"); }
            );
    }
    void LoadPlayerSuccess(string path, object[] userData, GameObject go)
    {
        //playerGo = go;
        //MapCardBase mapcard = mapCards[Random.Range(0, mapCards.Count)];
        //currentPos = mapcard.Pos;
        //playerGo.transform.position = GetTransfromByPos(currentPos);
        //mapcard.State = MapCardBase.CardState.Front;
    }

}
