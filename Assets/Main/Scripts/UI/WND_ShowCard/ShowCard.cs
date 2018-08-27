using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCard : MonoBehaviour {
    private int Type;
    private int Id;
    private int Num;

    public void SetShow(int type,int id,int num)
    {
        Type = type;
        Id = id;
        Num = num;
        UIEventListener.Get(gameObject).onClick += Show;
    }
    private void Show(GameObject obj)
    {
        int[] args = new int[3];
        args[0] = Type;
        args[1] = Id;
        args[2] = Num;
        Game.UI.OpenForm<WND_ShowCard>(args);
    }

}
