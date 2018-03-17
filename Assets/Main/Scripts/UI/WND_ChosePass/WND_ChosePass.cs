using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_ChosePass : UIFormBase
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
        showDialog((int)id);
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
    private void showDialog(int Id)
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
        }
      
       
        int type = DialogTableSettings.Get(preserntIndex).Type;
        List<int> NextIds = DialogTableSettings.Get(preserntIndex).NextIds;
        switch (type)
        {
            case 1:
                    
                int NextId = NextIds[0];

                showDialog(NextId);
                break;
            case 2:
                if (isChosing) {
                    return;
                }
                isChosing = true;
                int nums = NextIds.Count;
                for(int i= 0; i<nums; i++)
                {
                    GameObject item = Instantiate(btnSelect.gameObject);
                    item.name = "option" + i;
                    item.transform.Find("imgNum/labSelectNum").GetComponent<UILabel>().text = ""+(i+1);
                    item.transform.Find("labSelectString").GetComponent<UILabel>().text = DialogTableSettings.Get(NextIds[i]).Text;
                    int nextId = DialogTableSettings.Get(NextIds[i]).NextIds[0];
                    UIEventListener.Get(item.gameObject).onClick = (GameObject a)=> {
                        isChosing = false;
                        showDialog(nextId);
                  
                    };

                    item.transform.parent = grid.transform;
                    item.transform.localPosition = new Vector3();
                    item.transform.localScale = new Vector3(1, 1, 1);
                    item.SetActive(true);
                }
                grid.repositionNow = true;
                break;
            case 3:
                showDialog(NextIds[0]);
                    
                break;

            default:
                print("Unknow type!");
                break;
        }

        
     

    }

}
