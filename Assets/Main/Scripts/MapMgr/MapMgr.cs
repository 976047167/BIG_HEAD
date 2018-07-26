using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMgr
{
    public Camera mainCamera;

    MapLayerData currentMapLayerData;
    MapLayerData lastMapLayerData;
    public GameObject MapCardRoot { private set; get; }


    static MapMgr m_Instance;
    static bool m_Inited = false;
    protected MapPlayer m_MyMapPlayer;

    public MapPlayer MyMapPlayer { get { return m_MyMapPlayer; } }
    public static bool Inited { get { return m_Instance != null && m_Inited; } }

    private MapMgr()
    {

    }

    public static MapMgr Instance
    {
        get
        {
            return m_Instance;
        }
    }
    public static void Create()
    {
        m_Instance = new MapMgr();

    }
    public void Init()
    {
        //可以显示场景了
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        MapCardRoot = new GameObject("MapMgr");
        m_MyMapPlayer = new MapPlayer(Game.DataManager.MyPlayer);
        MakePlayer();
        MakeMap(1);
        m_Inited = true;
        Messenger.Broadcast(MessageId.MAP_UPDATE_PLAYER_INFO);
    }
    public void Update()
    {
        if (m_Inited == false)
        {
            Init();
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            NextMapLayer();
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
        Game.DataManager.MyPlayer.Data.Coin = m_MyMapPlayer.Data.Coin;
        Game.DataManager.MyPlayer.Data.Food = m_MyMapPlayer.Data.Food;
        SceneMgr.ChangeScene(3);
        Clear();
        m_Instance = null;
    }
    public void Clear()
    {
        m_Inited = false;
    }

    void MakePlayer()
    {
        //MapCardBase mapcard = mapCards[Random.Range(0, mapCards.Count)];
        //MapCardPos currentPos = new MapCardPos(mapcard.X, mapcard.Y);
        //m_MyMapPlayer.CreateModel(currentPos);
        //mapcard.State = MapCardBase.CardState.Front;
        MapCardPos currentPos = new MapCardPos(Random.Range(0, ConstValue.MAP_WIDTH), Random.Range(0, ConstValue.MAP_HEIGHT));
        m_MyMapPlayer.CreateModel(currentPos);

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
        MapLayerData layerData = new MapLayerData(layerId);
        currentMapLayerData = layerData;
        MapCardBase[,] maplist = layerData.MapCardDatas;
        List<MapCardBase> mapCards = new List<MapCardBase>();
        int cardCount = Random.Range(10, 15);
        //先确定出生点
        {
            if (playerDoorCard == null)
            {
                playerDoorCard = MapCardBase.CreateMapCard<MapCardDoor>(m_MyMapPlayer.CurPos, MapCardBase.CardState.Front);
            }
            mapCards.Add(playerDoorCard);
            maplist[playerDoorCard.X, playerDoorCard.Y] = playerDoorCard;
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
            maplist[mapCards[i].X, mapCards[i].Y] = mapCards[i];
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
            maplist[door.X, door.Y] = door;
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
        MakeMap(currentMapLayerData.LayerId + 1);
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
        return currentMapLayerData.MapCardDatas[x, y];
    }

    public Vector3 GetTransfromByPos(MapCardPos pos)
    {
        Vector3 position = new Vector3((pos.X - 2) * 2f, 0.25f, (pos.Y - 2) * 2f);
        return position;
    }
}
