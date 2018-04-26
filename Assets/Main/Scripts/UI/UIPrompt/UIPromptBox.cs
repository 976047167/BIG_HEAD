using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class UIPromptBox : MonoBehaviour {
    UITexture icon;
    UILabel describe;


    private void Awake()
    {
        icon = transform.Find("icon").GetComponent<UITexture>();
        describe = transform.Find("describe").GetComponent<UILabel>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void  SetData(int id)
    {
       PromptTableSetting prompt =   PromptTableSettings.Get(id);
       icon.Load(prompt.ImagePath) ;
        describe.text = prompt.Describe;
    }
}
