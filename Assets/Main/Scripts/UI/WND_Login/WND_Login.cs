using BigHead.protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WND_Login : UIFormBase
{
    UIButton btnGameStart = null;

    protected override void OnClose()
    {
        base.OnClose();
    }

    protected override void OnHide()
    {
        base.OnHide();
    }

    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        btnGameStart = transform.Find("imgBg/btn").GetComponent<UIButton>();


        btnGameStart.onClick.Add(new EventDelegate(OnClick_GameStart));
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        Messenger.AddListener<string>(MessageId.NetworkConnect, OnCnnectServerSuccess);
    }

    protected override void OnShow()
    {
        base.OnShow();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }


    void OnClick_GameStart()
    {
        //Messenger.Broadcast(MessageID.UI_GAME_START);
        Game.NetworkManager.ConnectLobby();
    }
    void OnCnnectServerSuccess(string serverName)
    {
        if (serverName == NetworkManager.LobbySessionName)
        {
            CLLogin login = new CLLogin();
            login.UserId = 1;
            Game.NetworkManager.SendToLobby(MessageId_Send.CLLogin, login);
        }
    }
}
