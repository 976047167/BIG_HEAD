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
    private int MonsterId = 0;
    private enum DialogType
    {
      Normal = 1,
      Select,
      Event,
      Battle,
      NextPass,

    }
    // Use this for initialization
    protected override void OnInit(object id)
    {
        base.OnInit(id);
        if (id.GetType() == typeof(int))
        {
            ShowDialog((int)id);
        }else if(id.GetType() == typeof(List<int>))
            {
           List<int> a = (List<int>)id;
            MonsterId = a[1];
            ShowDialog(a[0]);
        }
        
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
            UIModule.Instance.CloseForm<WND_Dialog>();
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
            imgHead.gameObject.SetActive(true);
            imgHead.Load(path);
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
        switch ((DialogType)type)
        {
            case DialogType.Normal:
                    
                int NextId = NextIds[0];

                ShowDialog(NextId);
                break;
            case DialogType.Select:
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
            case DialogType.Event:
                int result = DealEvent.deal(NextIds[0]);
               if (result == 0) 
                   ShowDialog(NextIds[1]);
               else if(result == 1)
                {
                    ShowDialog(NextIds[2]);
                }
               else
                    UIModule.Instance.CloseForm<WND_Dialog>();
                break;
            case DialogType.Battle:
                if (MonsterId != 0)
                    Game.BattleManager.StartBattle(MonsterId);
                UIModule.Instance.CloseForm<WND_Dialog>();
                break;
            default:
                
                print("Unknow type!");
                UIModule.Instance.CloseForm<WND_Dialog>();
                break;
        }
        
    }
  
}
