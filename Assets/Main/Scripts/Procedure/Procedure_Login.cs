using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedure_Login : ProcedureBase
{
    public override void OnEnter(ProcedureBase last)
    {
        base.OnEnter(last);

        Messenger.AddListener(MessageID.UI_GAME_START, GameStart);
        Messenger.AddListener(MessageID.UI_GAME_CREATE_CHARACTER, CreateCharacter);
        Game.UI.OpenForm<WND_Login>();
    }

    public override void OnExit(ProcedureBase next)
    {
        base.OnExit(next);
        Messenger.RemoveListener(MessageID.UI_GAME_START, GameStart);
        Messenger.RemoveListener(MessageID.UI_GAME_CREATE_CHARACTER, CreateCharacter);
        Game.UI.CloseForm<WND_Login>();
        Game.DataManager.OnInit();
        Game.DataManager.InitAccount();
    }

    public override bool OnInit(object userdata = null)
    {
        return base.OnInit(userdata);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    void GameStart()
    {
        //进入主界面
        SceneMgr.ChangeScene(3);
    }
    void CreateCharacter()
    {
        //进入创角界面
        SceneMgr.ChangeScene(2);
    }
}
