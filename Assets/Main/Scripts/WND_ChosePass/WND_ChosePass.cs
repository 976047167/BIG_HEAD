using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WND_ChosePass : MonoBehaviour
{
    private string printString;
    private UILabel labTips;
    private GameObject btnShowAll;
    // Use this for initialization
    void Awake()
    {
        instance = this;
        labTips = transform.Find("imgBg/imgTips/labTips").GetComponent<UILabel>();
        btnShowAll = transform.Find("btnShowAll").gameObject;
        UIEventListener.Get(btnShowAll.gameObject).onClick = PrintStringAll;
    }

    void Start()
    {

        this.ShowDialog("3559999999999999999999999999999");

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void PrintStringAll(GameObject btn)
    {
        StopCoroutine("PrintStringByStep");
        print("PrintStringByStep is stop");
        labTips.text = this.printString;
        btnShowAll.SetActive(false);
    }
    static WND_ChosePass instance = null;
    public static void ShowDialog(string txt)
    {
        if (instance != null)
        {
            return;
        }
        instance.StopCoroutine("PrintStringByStep");
        instance.printString = txt;
        instance.StartCoroutine("PrintStringByStep");
        GameObject prefab = (GameObject)Resources.Load("Prefabs/UIForm/WND");
        GameObject wnd = Instantiate(prefab) as GameObject;
        wnd.transform.position = Vector3.zero;
    }
    private void OnDestroy()
    {
        instance = null;
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
        print("pintStringByStep is printing" + printString);
        for (int i = 0; i < printString.Length; i++)
        {
            labTips.text = printString.Substring(0, i);
            yield return new WaitForSeconds(0.5f);
        }
        btnShowAll.SetActive(false);
    }
}
