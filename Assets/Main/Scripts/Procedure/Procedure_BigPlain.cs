using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.protocol;


public class Procedure_BigPlain : ProcedureBase
{
    protected InitState state = InitState.Start;
    public override void OnEnter(ProcedureBase last)
    {
        base.OnEnter(last);
        Game.UI.OpenForm<UIMapInfo>();
        Game.UI.OpenForm<UIMenu>();

        Messenger.AddListener<int>(MessageId.GAME_ENTER_BATTLE, EnterBattle);

    }

    public override void OnExit(ProcedureBase next)
    {
        base.OnExit(next);
        Game.UI.CloseForm<UIMapInfo>();
        Game.UI.CloseForm<UIMenu>();
        MapMgr.Instance.MyMapPlayer.Save();
        MapMgr.Instance.Clear();
    }

    public override IEnumerator OnInit(object userdata = null)
    {
        InitState last = InitState.None;
        while (true)
        {
            if (last != state)
            {
                switch (state)
                {
                    case InitState.Start:
                        Progress = 0f;
                        state = InitState.InitMapMgr;
                        break;
                    case InitState.InitMapMgr:
                        MapMgr.Create();
                        Progress = 0.1f;
                        state = InitState.GetMapLayerData;
                        break;
                    case InitState.GetMapLayerData:
                        Messenger.AddListener<PBMapLayerData>(MessageId_Receive.GCGetMapLayerData, GetLayerData);
                        CGGetMapLayerData getMapLayerData = new CGGetMapLayerData();
                        getMapLayerData.LayerIndex = 0;
                        Game.NetworkManager.SendToLobby(MessageId_Send.CGGetMapLayerData, getMapLayerData);
                        break;
                    case InitState.CreateModel:
                        break;
                    case InitState.Finish:
                        break;
                    default:
                        break;
                }
                last = state;
            }

            yield return null;
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        MapMgr.Instance.Update();
    }

    void GetLayerData(PBMapLayerData mapLayerData)
    {
        state = InitState.CreateModel;
        Messenger.RemoveListener<PBMapLayerData>(MessageId_Receive.GCGetMapLayerData, GetLayerData);

    }
    /// <summary>
    /// 进入战斗
    /// </summary>
    /// <param name="monsterId"></param>
    void EnterBattle(int monsterId)
    {
        Game.BattleManager.StartBattle(monsterId);
        Game.UI.CloseForm<WND_Dialog>();
    }
    public enum InitState
    {
        None = 0,
        Start,
        InitMapMgr,
        GetMapLayerData,
        CreateModel,
        Finish,
    }
}
