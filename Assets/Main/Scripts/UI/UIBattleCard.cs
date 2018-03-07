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
    private UILabel m_lblName;
    [SerializeField]
    private UILabel m_lblExpand;
    [SerializeField]
    private UILabel m_lblExpandCount;
    [SerializeField]
    private UILabel m_lblAttack;
    [SerializeField]
    private UILabel m_lblAttackCount;

    Vector3 offsetPos;
    Transform cacheChildCardTrans;
    Vector3 cacheCardPos;

    protected void Start()
    {
        cacheChildCardTrans = transform.GetChild(0);
    }


    protected void Update()
    {
        if (m_Draging && UICamera.mainCamera != null)
        {
            //UICamera.current.cachedCamera.ScreenPointToRay
            Debug.Log(UICamera.lastEventPosition.ToString());

            //Ray ray = UICamera.mainCamera.ScreenPointToRay(UICamera.lastEventPosition);
            //Physics

            //transform.position = UICamera.mainCamera.ScreenToWorldPoint(UICamera.lastWorldPosition);
            cacheChildCardTrans.position = UICamera.lastWorldPosition + offsetPos;
        }
    }

    protected void OnDragStart()
    {
        Debug.Log("OnDragStart ：" + name);
        m_Draging = true;
        offsetPos = transform.position - UICamera.lastWorldPosition;
    }
    protected void OnDrag(Vector2 delta)
    {
        //Debug.Log("OnDrag ：" + name);
        //transform.Translate(delta, Space.Self);
    }
    protected void OnDragOver()
    {
        //Debug.Log("OnDragOver ：" + name);
    }
    protected void OnDragOut()
    {
        //Debug.LogError("OnDragOut ：" + name);
    }
    protected void OnDragEnd()
    {
        Debug.Log("OnDragEnd ：" + name);
        m_Draging = false;
        Ray ray = UICamera.mainCamera.ScreenPointToRay(UICamera.lastEventPosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 20f, 1 << 8);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.name == "UsedCards")
                {
                    m_Used = UseCard();
                    //GetComponent<BoxCollider>().enabled = false;
                    
                }
            }
        }
        if (m_Used == false)
        {
            TweenPosition.Begin(cacheChildCardTrans.gameObject, 0.5f, Vector3.zero);
        }
    }
    protected void OnDrop(GameObject go)
    {
        //Debug.Log("OnDrop ：" + name);
    }
    protected void OnHover(bool isOver)
    {
        if (m_Used)
        {
            transform.localScale = Vector3.one;
            return;
        }
        if (isOver)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    protected virtual bool UseCard()
    {
        //判断使用条件，不允许返回false
        Debug.LogError("释放卡牌");
        UIBattleForm form = UIModule.Instance.GetForm<UIBattleForm>();
        cacheCardPos = cacheChildCardTrans.position;
        form.UseCard(this);
        return true;
    }

    public void RevertCardPos()
    {
        cacheChildCardTrans.position = cacheCardPos;
        TweenPosition.Begin(cacheChildCardTrans.gameObject, 0.5f, Vector3.zero);
    }
}