using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLogic : MonoBehaviour
{
    public MapCardBase[,] maplist;
    public GameObject playerGo;
    public MapCardPos currentPos;
    public Camera mainCamera;
    List<MapCardBase> mapCards = new List<MapCardBase>();
    MapLayerData currentMapLayerData;
    public static MapLogic Instance;
    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        MakeMap();
        MakePlayer();
        //Game.UI.OpenForm<UIMapInfo>();
        //Game.UI.OpenForm<UIMenu>();
    }

    // Update is called once per frame
    void Update()
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
                mapCards[i].X = Random.Range(0, 5);
                mapCards[i].Y = Random.Range(0, 5);

                maplist[mapCards[i].X, mapCards[i].Y] = mapCards[i];
                mapCards[i].State = MapCardBase.CardState.Behind;
                mapCards[i].SetActive(true);
                mapCards[i].SetParent(transform);
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
            mapCards[i].SetParent(transform);
        }
    }

    void MakePlayer()
    {
        ResourceManager.LoadGameObject("Character/Player/Player", LoadPlayerSuccess,
            (str, obj) => { Debug.LogError("Load player Failed!"); }
            );
    }
    void LoadPlayerSuccess(string path, object[] userData, GameObject go)
    {
        playerGo = go;
        MapCardBase mapcard = mapCards[Random.Range(0, mapCards.Count)];
        currentPos = mapcard.Pos;
        playerGo.transform.position = GetTransfromByPos(currentPos);
        mapcard.State = MapCardBase.CardState.Front;
    }

    public void OnClickMapCard(MapCardBase mapCard)
    {
        int distance = Mathf.Abs(mapCard.Pos.X - currentPos.X) + Mathf.Abs(mapCard.Y - currentPos.Y);
        if (distance == 1)
        {
            PlayerMoveTo(mapCard.Pos);
        }
    }

    void PlayerMoveTo(MapCardPos pos)
    {
        maplist[pos.X, pos.Y].OnPlayerExit();
        currentPos = pos;
        TweenPosition.Begin(playerGo, 0.5f, GetTransfromByPos(pos), true);
        MapCardBase mapcard = maplist[pos.X, pos.Y];
        if (mapcard != null)
        {
            mapcard.State = MapCardBase.CardState.Front;
            mapcard.OnPlayerEnter();
        }
    }

    Vector3 GetTransfromByPos(MapCardPos pos)
    {
        Vector3 position = new Vector3((pos.X - 2) * 2f, 0.1f, (pos.Y - 2) * 2f);
        return position;
    }
}
