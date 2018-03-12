using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_ChosePass : UIFormBase
{
    private string printString;
    private UILabel labTips;
    private GameObject btnShowAll;
    private int preserntIndex;
    private bool isPrinting;
    // Use this for initialization
    protected override void OnInit(object id)
    {
        showDialog((int)id);
    }
    
    void Awake()
    {
        labTips = transform.Find("imgBg/imgTips/labTips").GetComponent<UILabel>();
        btnShowAll = transform.Find("btnShowAll").gameObject;
        UIEventListener.Get(btnShowAll.gameObject).onClick = PrintStringAll;
        
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
        preserntIndex = Id;
        StopCoroutine("PrintStringByStep");
        

         printString = DialogTableSettings.Get(Id).Text;
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

        btnShowAll.SetActive(true);
        isPrinting = true;
        print("pintStringByStep is printing" + printString);
        for (int i = 1; i <= printString.Length; i++)
        {
            labTips.text = printString.Substring(0, i);
            print(labTips.text);
            yield return new WaitForSeconds(0.5f);
        }
        isPrinting = false;
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
        else
        {
            int type = DialogTableSettings.Get(preserntIndex).Type;
            switch (type)
            {
                case 1:
                    List<int> NextIds = DialogTableSettings.Get(preserntIndex).NextIds;
                    int NextId = NextIds[0];
                    if (NextId == 0)
                    {
                        btnShowAll.SetActive(false);
                        Destroy(gameObject);
                        return;
                    }
                    showDialog(NextId);
                    break;
                default:
                    print("Unknow type!");
                    break;
            }

        }
     

    }
}
