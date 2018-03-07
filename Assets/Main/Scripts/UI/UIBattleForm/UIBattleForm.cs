using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBattleForm : UIFormBase
{
    [SerializeField]
    private GameObject m_BattleCardTemplate;
    [SerializeField]
    private UIGrid m_MyCardsGrid;
    [SerializeField]
    private UIGrid m_UsedCardsGrid;
    [SerializeField]
    private UIGrid m_OppCardsGrid;

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

    public void UseCard(UIBattleCard battleCard)
    {
        StartCoroutine(CoroutineUser(battleCard));
    }
    IEnumerator CoroutineUser(UIBattleCard battleCard)
    {
        battleCard.transform.SetParent(m_UsedCardsGrid.transform, false);
        m_UsedCardsGrid.repositionNow = true;
        m_MyCardsGrid.repositionNow = true;
        yield return null;
        battleCard.RevertCardPos();
    }

}
