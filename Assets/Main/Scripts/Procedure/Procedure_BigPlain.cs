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
                last = state;
                switch (state)
                {
                    case InitState.Start:
                        Progress = 0f;
                        state = InitState.InitMapMgr;
                        break;
                    case InitState.InitMapMgr:
                        //MapMgr.Create();
                        MapMgr.Instance.Init();
                        Progress = 0.3f;
                        state = InitState.GetMapLayerData;
                        break;
                    case InitState.GetMapLayerData:
                        Messenger.AddListener(MessageId.MAP_GET_MAP_LAYER_DATA, GetLayerData);
                        CGGetMapLayerData getMapLayerData = new CGGetMapLayerData();
                        //层数从第一层开始
                        getMapLayerData.LayerIndex = 1;
                        getMapLayerData.InstanceId = MapMgr.Instance.MyMapPlayer.InstanceId;
                        getMapLayerData.PlayerX = MapMgr.Instance.MyMapPlayer.CurPos.X;
                        getMapLayerData.PlayerY = MapMgr.Instance.MyMapPlayer.CurPos.Y;
                        Game.NetworkManager.SendToLobby(MessageId_Send.CGGetMapLayerData, getMapLayerData);
                        Progress = 0.5f;
                        break;
                    case InitState.CreateModel:
                        yield return MapMgr.Instance.MakePlayer();
                        state = InitState.Finish;
                        Progress = 0.8f;
                        break;
                    case InitState.Finish:
                        Progress = 1f;
                        yield break;
                    default:
                        break;
                }
            }

            yield return null;
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        MapMgr.Instance.Update();
    }

    void GetLayerData()
    {
        if (state == InitState.GetMapLayerData)
        {
            state = InitState.CreateModel;
        }
        Messenger.RemoveListener(MessageId.MAP_GET_MAP_LAYER_DATA, GetLayerData);
        //if (MapMgr.Inited)
        //{
        //    MapMgr.Instance.MakeMapByLayerData(mapLayerData);
        //}
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
