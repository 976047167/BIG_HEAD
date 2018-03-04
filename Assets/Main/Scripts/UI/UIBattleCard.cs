using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBattleCard : MonoBehaviour
{
    public int CardDepth = 0;
    [SerializeField]
    private bool m_Draging = false;
    [SerializeField]
    private bool m_Used = false;
    [SerializeField]
    private UITexture m_TexIcon;
    [SerializeField]
    private UILabel m_lblDesc;
    [SerializeField]
    private UILabel m_lblExpand;
    [SerializeField]
    private UILabel m_lblExpandCount;
    [SerializeField]
    private UILabel m_lblAttack;
    [SerializeField]
    private UILabel m_lblAttackCount;

    Vector3 offsetPos;
    Transform cacheCardTrans;

    void Start()
    {
        cacheCardTrans = transform.GetChild(0);
    }


    void Update()
    {
        if (m_Draging && UICamera.mainCamera != null)
        {
            //UICamera.current.cachedCamera.ScreenPointToRay
            Debug.Log(UICamera.lastEventPosition.ToString());

            //Ray ray = UICamera.mainCamera.ScreenPointToRay(UICamera.lastEventPosition);
            //Physics

            //transform.position = UICamera.mainCamera.ScreenToWorldPoint(UICamera.lastWorldPosition);
            cacheCardTrans.position = UICamera.lastWorldPosition + offsetPos;
        }
    }

    void OnDragStart()
    {
        Debug.Log("OnDragStart ：" + name);
        m_Draging = true;
        offsetPos = transform.position - UICamera.lastWorldPosition;
    }
    void OnDrag(Vector2 delta)
    {
        //Debug.Log("OnDrag ：" + name);
        //transform.Translate(delta, Space.Self);
    }
    void OnDragOver()
    {
        Debug.Log("OnDragOver ：" + name);
    }
    void OnDragOut()
    {
        Debug.LogError("OnDragOut ：" + name);
    }
    void OnDragEnd()
    {
        Debug.Log("OnDragEnd ：" + name);
        m_Draging = false;
        Ray ray = UICamera.mainCamera.ScreenPointToRay(UICamera.lastEventPosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 20f, 1 << 8);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.name== "UsedCards")
                {
                    m_Used = true;
                    Debug.LogError("释放卡牌");
                }
            }
        }
        if (m_Used == false)
        {
            TweenPosition.Begin(cacheCardTrans.gameObject, 0.5f, Vector3.zero);
        }
    }
    void OnDrop(GameObject go)
    {
        Debug.Log("OnDrop ：" + name);
    }
}