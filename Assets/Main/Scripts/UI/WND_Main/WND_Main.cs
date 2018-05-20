using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WND_Main : UIFormBase {

    private UILabel labName;


    // Use this for initialization
    private void Awake()
    {
        labName = transform.Find("background/spFrameHead/labName").GetComponent<UILabel>();




    }
}
