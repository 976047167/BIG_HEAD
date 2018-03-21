using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_Dialog : UIFormBase
{
    private string printString;
    private UILabel labTips;
    private GameObject btnShowAll;
    private UITexture imgHead;
    private UIButton btnSelect;
    private int preserntIndex;
    private bool isPrinting;
    private bool isChosing;
    private UIGrid grid;
    // Use this for initialization
    protected override void OnInit(object id)
    {
        base.OnInit(id);
        ShowDialog((int)id);
    }
    
    void Awake()
    {
        labTips = transform.Find("imgTips/labTips").GetComponent<UILabel>();
        imgHead = transform.Find("imgTips/imgHead").GetComponent<UITexture>();
        btnShowAll = transform.Find("btnShowAll").gameObject;
        UIEventListener.Get(btnShowAll).onClick = PrintStringAll;
        btnSelect = transform.Find("btnSelect").GetComponent<UIButton>();
        
        grid = transform.Find("Container/Grid").GetComponent<UIGrid>();


    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //public static void ShowDialog(int Id)
    //{
    //    if (instance == null)
    //    {
    //        GameObject prefab = (GameObject)Resources.Load("Prefabs/UIForm/WND");
    //        GameObject wnd =
    //        wnd.transform.position = Vector3.zero;
    //    }

    //    instance.showDialog(Id);

    //}
    private void OnDestroy()
    {
        
    }
    private void ShowDialog(int Id)
    {
        if (Id == 0)
        {
            btnShowAll.SetActive(false);
            Destroy(gameObject);
            return;
        }
        preserntIndex = Id;
        StopCoroutine("PrintStringByStep");
        

         printString = DialogTableSettings.Get(Id).Text;
        string path = DialogTableSettings.Get(Id).ImagePath;
        if (path == "")
            imgHead.gameObject.SetActive(false);
        else
        {
            imgHead.gameObject.SetActive(transform);
            imgHead.mainTexture = Resources.Load(path) as Texture2D;
        }
           

        // printString = "你好5555555";
        StartCoroutine("PrintStringByStep");
    }
    private void ClearGrid()
    {
        List < Transform > list = grid.GetChildList();
        foreach(Transform i in list)
        {
            Destroy(i.gameObject);

        }
        grid.repositionNow = true;
    }
    private IEnumerator PrintStringByStep()
    {
        string printString = string.Copy(this.printString);
        if (printString.Length == 0)
        {
            print("pintStringByStep has no String to print!");
            yield break;
        }

        isPrinting = true;
        print("pintStringByStep is printing" + printString);
        for (int i = 1; i <= printString.Length; i++)
        {
            labTips.text = printString.Substring(0, i);
            yield return new WaitForSeconds(0.5f);
        }
        isPrinting = false;
        PrintStringAll(new GameObject());
    }
    private void PrintStringAll(GameObject btn)
    {
        if (isPrinting)
        {
            StopCoroutine("PrintStringByStep");
            print("PrintStringByStep is stop");
            labTips.text = this.printString;
            isPrinting = false;
            return;
        }
      
       
        int type = DialogTableSettings.Get(preserntIndex).Type;
        List<int> NextIds = DialogTableSettings.Get(preserntIndex).NextIds;
        switch (type)
        {
            case 1:
                    
                int NextId = NextIds[0];

                ShowDialog(NextId);
                break;
            case 2:
                if (isChosing) {
                    return;
                }
                isChosing = true;
                int nums = NextIds.Count;
                ClearGrid();
                for(int i= 0; i<nums; i++)
                {
                    GameObject item = Instantiate(btnSelect.gameObject);
                    item.name = "option" + i;
                    item.transform.Find("imgNum/labSelectNum").GetComponent<UILabel>().text = ""+(i+1);
                    item.transform.Find("labSelectString").GetComponent<UILabel>().text = DialogTableSettings.Get(NextIds[i]).Text;
                    int nextId = NextIds[i];
                    UIEventListener.Get(item.gameObject).onClick = (GameObject a)=> {
                        isChosing = false;
                        ClearGrid();
                        ShowDialog(nextId);
                  
                    };

                    item.transform.parent = grid.transform;
                    item.transform.localPosition = new Vector3();
                    item.transform.localScale = new Vector3(1, 1, 1);
                    item.SetActive(true);
                }
                grid.repositionNow = true;
                break;
            case 3:
                int result = DealEvent(NextIds[0]);
               if (result == 0) 
                   ShowDialog(NextIds[1]);
               else if(result == 1)
                {
                    ShowDialog(NextIds[2]);
                }
               else
                    Destroy(gameObject);
                break;
            case 4:
                Game.BattleManager.StartBattle(NextIds[0]);
                Destroy(gameObject);
                break;
            default:
                print("Unknow type!");
                break;
        }
        
    }
    //返回值为错误码，0表示成功
    private int DealEvent(int eventId)
    {
  
        EventTableSetting tmpEvent = EventTableSettings.Get(eventId);
        int costType = tmpEvent.CostType;
        switch (costType)
        {
            case 0:
                break;
            case 1:
                if (Game.DataManager.MyPlayerData.HP <= tmpEvent.CostNum)
                    return 1;
                else
                    Game.DataManager.MyPlayerData.HP -= tmpEvent.CostNum;
                break;
       
            case 2:
                if (Game.DataManager.Food < tmpEvent.CostNum)
                    return 1;
                else
                    Game.DataManager.Food -= tmpEvent.CostNum;
                break;
            case 3:
                if (Game.DataManager.MyPlayerData.MP < tmpEvent.CostNum)
                    return 1;
                else
                    Game.DataManager.MyPlayerData.MP -= tmpEvent.CostNum;
                break;
            case 4:
                if (Game.DataManager.Coin < tmpEvent.CostNum)
                    return 1;
                else
                    Game.DataManager.Coin -= tmpEvent.CostNum;
                break;
            case 6:
                for (int j= 0; j<tmpEvent.CostNum; j++)
                {
                    bool done = false;
                    foreach (BattleCardData i in Game.DataManager.MyPlayerData.EquipList)
                    {
                        if (i.CardId == tmpEvent.CostItemId)
                        {
                            Game.DataManager.MyPlayerData.EquipList.Remove(i);
                            done = true;
                        }
                    }
                    if (done == false)
                        return 1;
                }
                break;
               

            default:
                return 2;
                
        }
        switch(tmpEvent.Type) {
            case 0:
                break;
            case 1:

                Game.DataManager.MyPlayerData.HP += tmpEvent.Num;
                if (Game.DataManager.MyPlayerData.HP > Game.DataManager.MyPlayerData.MaxHP)
                    Game.DataManager.MyPlayerData.HP = Game.DataManager.MyPlayerData.MaxHP;
                break;

            case 2:
                Game.DataManager.Food += tmpEvent.Num;
                break;
            case 3:
                Game.DataManager.MyPlayerData.MP += tmpEvent.Num;
                if (Game.DataManager.MyPlayerData.MP > Game.DataManager.MyPlayerData.MaxMP)
                    Game.DataManager.MyPlayerData.MP = Game.DataManager.MyPlayerData.MaxMP;
                break;
            case 4:
                Game.DataManager.Coin += tmpEvent.Num;
                break;
            case 6:
                for (int j = 0; j < tmpEvent.CostNum; j++)
                {

                    Game.DataManager.MyPlayerData.EquipList.Add(new BattleCardData(tmpEvent.ItemId));
                 
                }
                break;


            default:
                return 2;


        }

        return 0;
    }
    
}
