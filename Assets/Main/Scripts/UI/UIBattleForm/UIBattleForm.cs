﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBattleForm : MonoBehaviour
{
    [SerializeField]
    private GameObject m_BattleCardTemplate;
    [SerializeField]
    private UIGrid m_MyCardsGrid;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject card = Instantiate<GameObject>(m_BattleCardTemplate, m_MyCardsGrid.transform);
            m_MyCardsGrid.Reposition();
        }
    }
}
