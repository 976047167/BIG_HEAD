using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedure_CreateCharacter : ProcedureBase
{
    public override void OnEnter(ProcedureBase last)
    {
        base.OnEnter(last);
        Messenger.AddListener(MessageID.UI_GAME_START, GameStart);

        Game.UI.OpenForm<WND_CreateCharacter>();
    }

    public override void OnExit(ProcedureBase next)
    {
        base.OnExit(next);
        Game.UI.CloseForm<WND_CreateCharacter>();
        Messenger.RemoveListener(MessageID.UI_GAME_START, GameStart);
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
}
