using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPromptHover : UIFormBase {

    // Use this for initialization
    public int PromptId;
    private GameObject PromptBox;
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        //PromptBox = Resources.Load("Prefabs/PromptBox/PromptBox") as GameObject;
        ResourceManager.LoadGameObject("UIForm/UIPromptBox",(str,obj,go)=>{ PromptBox = go;
            PromptBox.transform.SetParent(transform);
            PromptBox.transform.localPosition = new Vector3(0, 100, 0);
            PromptBox.transform.localScale = new Vector3(1, 1, 1);
            //PromptBox.GetComponent<UIPromptBox>().SetData(PromptId);

        },(str,obj)=> { });
    }


    static public void AddPrompt(GameObject obj, int promptid)
    {
        obj.AddComponent<UIPromptHover>().PromptId = promptid; ;
    }

	// Update is called once per frame
    protected void OnHover(bool isOn)
    {
        Debug.Log("11111111{0}");
        if (isOn)
        {
            StartCoroutine("CountTime");
        }
        else
        {
            StopCoroutine("CountTime");
            if (PromptBox != null)
                PromptBox.SetActive(false);
        }
    }
    private IEnumerator CountTime()
    {
        yield return new WaitForSeconds(2f);
        PromptBox.SetActive(true);
    }

}
