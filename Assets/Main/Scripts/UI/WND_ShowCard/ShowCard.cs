using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCard : MonoBehaviour {
    private int Type;
    private int Id;

    public void SetShow(int type,int id)
    {
        Type = type;
        Id = id;
        UIEventListener.Get(gameObject).onClick += Show;
    }
    private void Show(GameObject obj)
    {
        int[] args = new int[2];
        args[0] = Type;
        args[1] = Id;
        Game.UI.OpenForm<WND_ShowCard>(args);
    }

}
