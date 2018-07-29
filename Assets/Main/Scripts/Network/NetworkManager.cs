using BigHead.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;

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
        DicServerHandler.Register();
        
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
        if (session != null)
        {
            session.Connect("127.0.0.1", 8888);
        }
    }

    public void Send(MessageId_Send messageId, IMessage message)
    {
        if (session == null)
        {
            ConnectLobby();
        }
        if (session != null)
        {
            session.Send(messageId, message);
        }
    }
}
