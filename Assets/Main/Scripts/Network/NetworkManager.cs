using BigHead.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager
{
    public const string LobbySessionName = "Lobby";

    INetworkSession session = null;

    public INetworkSession Session
    {
        get
        {
            return session;
        }
    }

    public void Init()
    {
        //NetworkHelper.RegisterHandler();
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        DicHandler.Register();
        DicParser.Register();
    }

    public void ConnectLobby()
    {
        if (Game.Instance.Online)
        {
            session = new NetworkSession(LobbySessionName);
        }
        else
        {
            session = new OfflineNetworkSession(LobbySessionName);
        }
    }
}
