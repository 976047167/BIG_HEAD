using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : UIFormBase
{
    GameObject btnKaku;

    void Awake()
    {
        btnKaku = transform.Find("btnKaku").gameObject;
        UIEventListener.Get(btnKaku).onClick = btnMenuClick;

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void btnMenuClick(GameObject btn)
    {
        UIModule.Instance.OpenForm<WND_Kaku>();
    }
}
