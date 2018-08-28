using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.protocol;
using Random = UnityEngine.Random;

public class MapMgr
{
    public Camera mainCamera;

    int instanceId = 0;
    MapLayerData currentMapLayerData;
    MapLayerData lastMapLayerData;
    public GameObject MapCardRoot { private set; get; }


    static MapMgr m_Instance;
    static bool m_Inited = false;
    protected MapPlayer m_MyMapPlayer;

    public MapPlayer MyMapPlayer { get { return m_MyMapPlayer; } }
    public int InstanceId { get { return instanceId; } }
    public MapLayerData CurrentMapLayerData { get { return currentMapLayerData; } }
    public static bool Inited { get { return m_Instance != null && m_Inited; } }
    private MapMgr()
    {
        RegisterMessage();
    }

    public static MapMgr Instance
    {
        get
        {
            return m_Instance;
        }
    }
    public static void Create(int instanceId)
    {
        m_Instance = new MapMgr();
        m_Instance.instanceId = instanceId;
        m_Instance.m_MyMapPlayer = new MapPlayer(Game.DataManager.MyPlayer);

    }
    void RegisterMessage()
    {
        Messenger.AddListener<PBMapLayerData>(MessageId_Receive.GCGetMapLayerData, MakeMapByLayerData);
        Messenger.AddListener<RewardData>(MessageId.MAP_GET_REWARD, GetMapReward);
        Messenger.AddListener(MessageId.MAP_BACK_TO_MAINTOWN, BackToMaintown);
        Messenger.AddListener<ulong>(MessageId.MAP_PLAYER_DEAD, OnPlayerDead);
    }
    void RemoveMessage()
    {
        Messenger.RemoveListener<PBMapLayerData>(MessageId_Receive.GCGetMapLayerData, MakeMapByLayerData);
        Messenger.RemoveListener<RewardData>(MessageId.MAP_GET_REWARD, GetMapReward);
        Messenger.RemoveListener(MessageId.MAP_BACK_TO_MAINTOWN, BackToMaintown);
        Messenger.RemoveListener<ulong>(MessageId.MAP_PLAYER_DEAD, OnPlayerDead);
    }



    public void Init()
    {
        //可以显示场景了
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        MapCardRoot = new GameObject("MapMgr");
        Messenger.Broadcast(MessageId.MAP_UPDATE_PLAYER_INFO);
        //MakeMap(1);

        m_Inited = true;

    }
    public void Update()
    {
        if (m_Inited == false)
        {
            //Init();
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //NextMapLayer();
            CGExitBattle getReward = new CGExitBattle();
            getReward.MonsterId = 1;
            getReward.Reason = 0;
            Game.NetworkManager.SendToLobby(MessageId_Send.CGExitBattle, getReward);
        }
        if (UICamera.isOverUI == false && Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit[] hits = Physics.RaycastAll(mainCamera.ScreenPointToRay(Input.mousePosition), 100f, 1 << 9);
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].collider.gameObject.SendMessage("OnClick");
            }
        }
    }
    public void ExitMap()
    {
        Game.DataManager.MyPlayer.Data.Gold = m_MyMapPlayer.Data.Gold;
        Game.DataManager.MyPlayer.Data.Food = m_MyMapPlayer.Data.Food;
        SceneMgr.ChangeScene(3);
        Clear();
        m_Instance = null;
    }
    public void Clear()
    {
        m_Inited = false;

        RemoveMessage();
        m_Instance = null;
    }

    public IEnumerator MakePlayer()
    {
        //MapCardPos currentPos = new MapCardPos(Random.Range(0, ConstValue.MAP_WIDTH), Random.Range(0, ConstValue.MAP_HEIGHT));
        //m_MyMapPlayer.CreateModel(currentPos);
        m_MyMapPlayer.CreateModel();
        while (m_MyMapPlayer.LoadedModel == false)
        {
            yield return null;
        }

    }
    public void MakeMapByLayerData(PBMapLayerData layerData)
    {
        MapCardBase playerDoorCard = null;
        if (currentMapLayerData != null)
        {
            if (lastMapLayerData != null)
            {
                for (int i = 0; i < lastMapLayerData.Width; i++)
                {
                    for (int j = 0; j < lastMapLayerData.Height; j++)
                    {
                        if (lastMapLayerData[i, j] != null && lastMapLayerData[i, j] != currentMapLayerData[i, j])
                        {
                            lastMapLayerData[i, j].Destory();
                        }
                    }
                }
            }
            lastMapLayerData = currentMapLayerData;
            for (int i = 0; i < lastMapLayerData.Width; i++)
            {
                for (int j = 0; j < lastMapLayerData.Height; j++)
                {
                    //共用传送门
                    if (i == m_MyMapPlayer.CurPos.X && j == m_MyMapPlayer.CurPos.Y)
                    {
                        playerDoorCard = lastMapLayerData[i, j];
                    }
                    else if (lastMapLayerData[i, j] != null)
                    {
                        lastMapLayerData[i, j].ExitMap();
                    }
                }
            }
        }
        MapLayerData mapLayerData = new MapLayerData(layerData.Index, layerData.Width, layerData.Height);
        for (int i = 0; i < layerData.Width; i++)
        {
            for (int j = 0; j < layerData.Height; j++)
            {
                if (playerDoorCard != null && i == m_MyMapPlayer.CurPos.X && j == m_MyMapPlayer.CurPos.Y)
                {
                    mapLayerData[i, j] = playerDoorCard;
                    continue;
                }
                int index = i * layerData.Width + j;
                mapLayerData[i, j] = MapCardBase.CreateMapCard((MapCardType)layerData.PointTypes[index],
                    layerData.PointIds[index],
                    new MapCardPos(i, j));
            }
        }
        currentMapLayerData = mapLayerData;
        Messenger.BroadcastSync(MessageId.MAP_GET_MAP_LAYER_DATA);
    }
    void MakeMap(MapLayerData mapLayerData)
    {

    }
    void MakeMap(int layerId)
    {

        MapCardBase playerDoorCard = null;
        if (currentMapLayerData != null)
        {
            if (lastMapLayerData != null)
            {
                for (int i = 0; i < ConstValue.MAP_WIDTH; i++)
                {
                    for (int j = 0; j < ConstValue.MAP_HEIGHT; j++)
                    {
                        if (lastMapLayerData[i, j] != null && lastMapLayerData[i, j] != currentMapLayerData[i, j])
                        {
                            lastMapLayerData[i, j].Destory();
                        }
                    }
                }
            }
            lastMapLayerData = currentMapLayerData;
            for (int i = 0; i < ConstValue.MAP_WIDTH; i++)
            {
                for (int j = 0; j < ConstValue.MAP_HEIGHT; j++)
                {
                    //共用传送门
                    if (i == m_MyMapPlayer.CurPos.X && j == m_MyMapPlayer.CurPos.Y)
                    {
                        playerDoorCard = lastMapLayerData[i, j];
                    }
                    else if (lastMapLayerData[i, j] != null)
                    {
                        lastMapLayerData[i, j].ExitMap();
                    }
                }
            }
        }
        MapLayerData layerData = new MapLayerData(layerId, 5, 5);
        currentMapLayerData = layerData;
        List<MapCardBase> mapCards = new List<MapCardBase>();
        int cardCount = Random.Range(10, 15);
        //先确定出生点
        {
            if (playerDoorCard == null)
            {
                playerDoorCard = MapCardBase.CreateMapCard<MapCardDoor>(m_MyMapPlayer.CurPos, MapCardBase.CardState.Front);
            }
            mapCards.Add(playerDoorCard);
            layerData[playerDoorCard.X, playerDoorCard.Y] = playerDoorCard;
            playerDoorCard.SetUsed(true);
            playerDoorCard.SetActive(true);
            playerDoorCard.SetParent(MapCardRoot.transform);
        }
        for (int i = 1; i < cardCount; i++)
        {
            MapCardPos pos = mapCards[Random.Range(0, i)].Position;

            List<MapCardPos> poss = layerData.GetNearEmptyPoss(pos.X, pos.Y);

            if (poss.Count == 0)
            {
                i--;
                continue;
            }
            int count = Random.Range(0, poss.Count - 1);
            pos = poss[count];
            mapCards.Add(MapCardBase.GetRandomMapCard(pos, MapCardBase.CardState.Behind));
            layerData[mapCards[i].X, mapCards[i].Y] = mapCards[i];
            mapCards[i].SetActive(true);
            mapCards[i].SetParent(MapCardRoot.transform);
        }
        //创建出口
        {
            MapCardPos pos = mapCards[Random.Range(0, cardCount)].Position;

            List<MapCardPos> poss = layerData.GetNearEmptyPoss(pos.X, pos.Y);

            while (poss.Count == 0)
            {
                pos = mapCards[Random.Range(0, cardCount)].Position;
                poss = layerData.GetNearEmptyPoss(pos.X, pos.Y);
            }
            int count = Random.Range(0, poss.Count - 1);
            pos = poss[count];
            MapCardBase door = MapCardBase.CreateMapCard<MapCardDoor>(pos, MapCardBase.CardState.Behind);
            mapCards.Add(door);
            layerData[door.X, door.Y] = door;
            door.SetActive(true);
            door.SetParent(MapCardRoot.transform);
        }
    }


    //void LoadPlayerSuccess(string path, object[] userData, GameObject go)
    //{
    //    playerGo = go;
    //    MapCardBase mapcard = mapCards[Random.Range(0, mapCards.Count)];
    //    currentPos = mapcard.Pos;
    //    playerGo.transform.position = GetTransfromByPos(currentPos);
    //    mapcard.State = MapCardBase.CardState.Front;
    //}

    public void NextMapLayer()
    {
        //MakeMap(currentMapLayerData.LayerId + 1);
        CGGetMapLayerData getMapLayerData = new CGGetMapLayerData();
        //层数从第一层开始
        getMapLayerData.LayerIndex = currentMapLayerData.LayerId + 1;
        getMapLayerData.InstanceId = MapMgr.Instance.MyMapPlayer.InstanceId;
        getMapLayerData.PlayerX = MapMgr.Instance.MyMapPlayer.CurPos.X;
        getMapLayerData.PlayerY = MapMgr.Instance.MyMapPlayer.CurPos.Y;
        Game.NetworkManager.SendToLobby(MessageId_Send.CGGetMapLayerData, getMapLayerData);
    }
    void GetMapReward(RewardData rewardData)
    {
        if (Game.BattleManager.State == BattleMgr.BattleState.None)
        {
            Game.UI.OpenForm<WND_Reward>(rewardData);
        }
    }
    void BackToMaintown()
    {
        UIUtility.ShowMessageBox(MessageBoxType.Yes, 1003006, (result) =>
        {
            Game.UI.CloseForm<UIBattleForm>();
            //回主城
            SceneMgr.ChangeScene(3);
        });
    }
    private void OnPlayerDead(ulong playerId)
    {
        if (playerId == MyMapPlayer.Data.Id)
        {
            CGExitInstance exitInstance = new CGExitInstance();
            exitInstance.AccountId = Game.DataManager.AccountData.Uid;
            exitInstance.PlayerId = Game.DataManager.MyPlayer.Data.ID;
            exitInstance.Reason = 1;
            Game.NetworkManager.SendToLobby(MessageId_Send.CGExitInstance, exitInstance);
        }
    }
    public void OnClickMapCard(MapCardBase mapCard)
    {
        if (m_Inited == false)
        {
            return;
        }
        int distance = Mathf.Abs(mapCard.X - m_MyMapPlayer.CurPos.X) + Mathf.Abs(mapCard.Y - m_MyMapPlayer.CurPos.Y);
        if (distance == 1)
        {
            m_MyMapPlayer.MoveTo(mapCard.Position);
        }
    }

    public MapCardBase GetMapCard(int x, int y)
    {
        return currentMapLayerData[x, y];
    }

    public Vector3 GetTransfromByPos(MapCardPos pos)
    {
        Vector3 position = new Vector3((pos.X - 2) * 2f, 0.25f, (pos.Y - 2) * 2f);
        return position;
    }
}
