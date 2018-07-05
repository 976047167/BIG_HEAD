using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMgr
{
    public MapCardBase[,] maplist;
    public Camera mainCamera;
    List<MapCardBase> mapCards = new List<MapCardBase>();
    MapLayerData currentMapLayerData;
    public GameObject MapCardRoot { private set; get; }


    static MapMgr m_Instance;

    protected MapPlayer m_MyMapPlayer;

    public MapPlayer MyMapPlayer { get { return m_MyMapPlayer; } }

    private MapMgr() { }

    public static MapMgr Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new MapMgr();
            }
            return m_Instance;
        }
    }
    public void Init()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        MapCardRoot = new GameObject("MapMgr");
        m_MyMapPlayer = new MapPlayer(Game.DataManager.MyPlayer);
        MakePlayer();
        MakeMap();
        
    }

    public void Update()
    {
        if (UICamera.isOverUI == false && Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit[] hits = Physics.RaycastAll(mainCamera.ScreenPointToRay(Input.mousePosition), 100f, 1 << 9);
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].collider.gameObject.SendMessage("OnClick");
            }
        }
    }
    public void Clear()
    {
        m_Instance = null;
    }

    void MakeMap()
    {
        MapLayerData layerData = new MapLayerData(0);
        currentMapLayerData = layerData;
        maplist = layerData.MapCardDatas;
        mapCards.Clear();
        int carCount = Random.Range(6, 15);
        //成功生成格子的标志位
        bool createSuccess = false;
        for (int i = 0; i < carCount; i++)
        {
            //先确定出生点
            if (i == 0)
            {
                mapCards.Add(MapCardBase.CreateMapCard<MapCardDoor>());
                mapCards[i].X = Random.Range(0, ConstValue.MAP_WIDTH);
                mapCards[i].Y = Random.Range(0, ConstValue.MAP_WIDTH);

                maplist[mapCards[i].X, mapCards[i].Y] = mapCards[i];
                mapCards[i].State = MapCardBase.CardState.Behind;
                mapCards[i].SetActive(true);
                mapCards[i].SetParent(MapCardRoot.transform);
                continue;
            }
            MapCardPos pos = mapCards[Random.Range(0, i)].Pos;

            List<MapCardPos> poss = layerData.GetNearEmptyPoss(pos.X, pos.Y);
            int count = Random.Range(0, poss.Count - 1);
            if (poss.Count == 0)
            {
                i--;
                continue;
            }
            pos = poss[count];
            mapCards.Add(MapCardBase.GetRandomMapCard());
            mapCards[i].X = pos.X;
            mapCards[i].Y = pos.Y;
            maplist[mapCards[i].X, mapCards[i].Y] = mapCards[i];
            mapCards[i].State = MapCardBase.CardState.Behind;
            mapCards[i].SetActive(true);
            mapCards[i].SetParent(MapCardRoot.transform);
        }
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
    //void LoadPlayerSuccess(string path, object[] userData, GameObject go)
    //{
    //    playerGo = go;
    //    MapCardBase mapcard = mapCards[Random.Range(0, mapCards.Count)];
    //    currentPos = mapcard.Pos;
    //    playerGo.transform.position = GetTransfromByPos(currentPos);
    //    mapcard.State = MapCardBase.CardState.Front;
    //}

    public void OnClickMapCard(MapCardBase mapCard)
    {
        int distance = Mathf.Abs(mapCard.Pos.X - m_MyMapPlayer.CurPos.X) + Mathf.Abs(mapCard.Y - m_MyMapPlayer.CurPos.Y);
        if (distance == 1)
        {
            m_MyMapPlayer.MoveTo(mapCard.Pos);
        }
    }

    public MapCardBase GetMapCard(int x, int y)
    {
        return maplist[x, y];
    }

    public Vector3 GetTransfromByPos(MapCardPos pos)
    {
        Vector3 position = new Vector3((pos.X - 2) * 2f, 0.25f, (pos.Y - 2) * 2f);
        return position;
    }
}
