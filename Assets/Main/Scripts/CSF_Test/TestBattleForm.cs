using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattleForm : MonoBehaviour
{
    public UIBattleForm form;
    // Use this for initialization
    void Start()
    {
        form.gameObject.SetActive(false);
        UIModule.Instance.OpenForm<UIBattleForm>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
