using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPromptHover : MonoBehaviour {

    // Use this for initialization
    public int PromptId;
    private GameObject PromptBox;
    private GameObject instance;
    private void Awake()
    {

        //PromptBox = Resources.Load("Prefabs/PromptBox/PromptBox") as GameObject;
        ResourceManager.LoadGameObject("UIForm/PromptBox",(str,obj,go)=>{ PromptBox = go; },(str,obj)=> { });
    }

    void Start () {
		
	}
    
    static public void AddPrompt(GameObject obj, int promptid)
    {
        obj.AddComponent<UIPromptHover>();
        obj.GetComponent<UIPromptHover>().PromptId = promptid;
    }

	// Update is called once per frame
	void Update () {
       
    }
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
            if (instance != null)
              Destroy(instance);
        }
    }
    private IEnumerator CountTime()
    {
        yield return new WaitForSeconds(2f);
        instance = Instantiate(PromptBox);
        instance.transform.SetParent(transform);
        instance.transform.localPosition = new Vector3(0,100,0);
        instance.transform.localScale = new Vector3(1, 1, 1);
        instance.SetActive(true);
        instance.GetComponent<UIPromptBox>().SetData(PromptId);
    }

}
