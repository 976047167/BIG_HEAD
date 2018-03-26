using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class UINormalCard : MonoBehaviour
{
    public int CardDepth = 0;
    [SerializeField]
    private bool m_Draging = false;
    [SerializeField]
    private bool m_Moved = false;
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
    public Transform Parent;
    private Transform m_Parent;
    public UIGrid Grid;

    Vector3 offsetPos;
    [HideInInspector]
    public Transform cacheChildCardTrans;
    Vector3 cacheCardPos;

    BattleCardData cardData;
    protected void Start()
    {
        m_Parent = transform.parent;
    }


    protected void Update()
    {
        if (m_Draging && UICamera.mainCamera != null)
        {
            //UICamera.current.cachedCamera.ScreenPointToRay
            //Debug.Log(UICamera.lastEventPosition.ToString());

            //Ray ray = UICamera.mainCamera.ScreenPointToRay(UICamera.lastEventPosition);
            //Physics

            //transform.position = UICamera.mainCamera.ScreenToWorldPoint(UICamera.lastWorldPosition);
            transform.position = UICamera.lastWorldPosition + offsetPos;
        }
    }

 
    protected void OnDragStart()
    {
        Vector2 delta = UICamera.currentTouch.totalDelta;
        if (Mathf.Abs(delta.x) < Mathf.Abs(delta.y)) return;
        Debug.Log("OnDragStart ：" + name);
        m_Draging = true;
        transform.parent = Parent;
        Grid.repositionNow = true;
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
                 

                }
            }
        }
        if (m_Moved == false)
        {
            transform.parent = m_Parent;
            transform.localPosition = Vector3.zero;
        }
    }
    protected void OnDrop(GameObject go)
    {
        //Debug.Log("OnDrop ：" + name);
    }
    public void SetData(BattleCardData card)
    {
        cardData = card;
        m_TexIcon.mainTexture = Resources.Load<Texture>(cardData.Data.Icon);
        m_lblName.text = cardData.Data.Name;
        if (cardData.Data.Type != 0)
        {
            m_lblAttack.text = "";
            m_lblAttackCount.text = "";
        }
        for (int i = 0; i < cardData.Data.ActionTypes.Count; i++)
        {
            switch ((BattleActionType)cardData.Data.ActionTypes[i])
            {
                case BattleActionType.None:
                    break;
                case BattleActionType.AddBuff:
                    break;
                case BattleActionType.Attack:
                    if (cardData.Data.Type == 0)
                    {
                        m_lblAttack.text = "攻击";
                        m_lblAttackCount.text = cardData.Data.ActionParams[i].ToString();
                    }
                    break;
                case BattleActionType.RecoverHP:
                    break;
                case BattleActionType.RecoverMP:
                    break;
                case BattleActionType.DrawCard:
                    break;
                default:
                    break;
            }
        }
        m_lblExpand.text = "";
        m_lblExpandCount.text = cardData.Data.Spending.ToString();


    }


    //public void RevertCardPos()
    //{
    //    //cacheChildCardTrans.position = cacheCardPos;
    //    TweenPosition.Begin(cacheChildCardTrans.gameObject, 0.5f, Vector3.zero);
    //}
}