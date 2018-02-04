using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDialog : MonoBehaviour {
    private int index;
    private string text;
    private int type;
    private int nextIndex;
    private int imageIndex;

    public DataDialog(int index,string text ,int type,int nextIndex ,int imageIndex)
    {
        this.index = index;
        this.text = text;
        this.type = type;
        this.nextIndex = nextIndex;
        this.imageIndex = imageIndex;
    }


}
