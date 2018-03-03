using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KEngine.Table;
public class test : MonoBehaviour {
	// Use this for initialization
	void Start () {
        TextAsset testFile = Resources.Load("1") as TextAsset;
        print(testFile.text);
        TableFile<DialogTable1> sss = new TableFile<DialogTable1>(testFile.text);
        print(sss.GetRowCount());
        DialogTable1 line = sss.GetRow(1);
        print(line.text);
    }


    // Update is called once per frame
    void Update () {


    }
}
