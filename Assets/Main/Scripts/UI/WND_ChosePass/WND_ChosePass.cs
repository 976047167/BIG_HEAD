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
    void Awake()
    {
        labTips = transform.Find("imgBg/imgTips/labTips").GetComponent<UILabel>();
        btnShowAll = transform.Find("btnShowAll").gameObject;
        UIEventListener.Get(btnShowAll.gameObject).onClick = PrintStringAll;
        showDialog(1);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //public static void ShowDialog(int index)
    //{
    //    if (instance == null)
    //    {
    //        GameObject prefab = (GameObject)Resources.Load("Prefabs/UIForm/WND");
    //        GameObject wnd =
    //        wnd.transform.position = Vector3.zero;
    //    }

    //    instance.showDialog(index);

    //}
    private void OnDestroy()
    {
        
    }
    private void showDialog(int index)
    {
        preserntIndex = index;
        StopCoroutine("PrintStringByStep");
        

        printString = DialogTable.getInstance().getDialogString(index);
        printString = DialogSettings.Get(index).text;
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
        for (int i = 0; i < printString.Length; i++)
        {
            labTips.text = printString.Substring(0, i);
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
            int type = DialogTable.getInstance().getDialogType(preserntIndex);
            type = DialogSettings.Get(preserntIndex).type;
            switch (type)
            {
                case 1:
                    List<int> nextIndices = DialogTable.getInstance().getDialogNextIndices(preserntIndex);
                    nextIndices = DialogSettings.Get(preserntIndex).nextIndices;
                    int nextIndex = nextIndices[0];
                    if (nextIndex == 0)
                    {
                        btnShowAll.SetActive(false);
                        Destroy(gameObject);
                        return;
                    }
                    showDialog(nextIndex);
                    break;
                default:
                    print("Unknow type!");
                    break;
            }

        }
     

    }
}
