using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

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
    [HideInInspector]
    public Transform cacheChildCardTrans;
    Vector3 cacheCardPos;

    BattleCardData cardData;
    UIBattleForm cacheForm;

    public BattleCardData CardData
    {
        get;
        private set;
    }

    protected void Start()
    {
        cacheChildCardTrans = transform.GetChild(0);
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
            cacheChildCardTrans.position = UICamera.lastWorldPosition + offsetPos;
        }
    }

    /// <summary>
    /// 应用使用特效
    /// </summary>
    public void ApplyUseEffect()
    {
        Game.BattleManager.ApplyCardEffect(CardData);
        //cardData.Owner.AP -= cardData.Data.Spending;
        //for (int i = 0; i < cardData.Data.ActionTypes.Count; i++)
        //{
        //    switch ((BattleActionType)cardData.Data.ActionTypes[i])
        //    {
        //        case BattleActionType.None:
        //            break;
        //        case BattleActionType.AddBuff:
        //            break;
        //        case BattleActionType.Attack:
        //            if (cardData.Owner == Game.DataManager.MyPlayerData)
        //            {
        //                Game.DataManager.OppPlayerData.HP -= cardData.Data.ActionParams[i];
        //            }
        //            else if (cardData.Owner == Game.DataManager.OppPlayerData)
        //            {
        //                Game.DataManager.MyPlayerData.HP -= cardData.Data.ActionParams[i];
        //            }
        //            break;
        //        case BattleActionType.RecoverHP:
        //            break;
        //        case BattleActionType.RecoverMP:
        //            break;
        //        case BattleActionType.DrawCard:
        //            Game.BattleManager.DrawCard(cardData.Owner, cardData.Data.ActionParams[i]);
        //            break;
        //        case BattleActionType.AddEuipment:
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
    bool IsMine()
    {
        if (CardData != null && CardData.Owner == Game.DataManager.MyPlayerData)
        {
            return true;
        }
        return false;
    }
    protected void OnDragStart()
    {
        if (!Game.BattleManager.CanUseCard)
        {
            return;
        }
        if (m_Used)
        {
            return;
        }
        if (!IsMine())
        {
            return;
        }
        Debug.Log("OnDragStart ：" + name);
        m_Draging = true;
        offsetPos = transform.position - UICamera.lastWorldPosition;
        cacheChildCardTrans.SetParent(cacheForm.MovingPanel.transform, true);
        RefreshDepth(cacheChildCardTrans);

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
        if (!Game.BattleManager.CanUseCard)
        {
            return;
        }
        if (m_Used)
        {
            return;
        }
        if (!IsMine())
        {
            return;
        }
        Debug.Log("OnDragEnd ：" + name);
        cacheChildCardTrans.SetParent(transform, true);
        m_Draging = false;
        Ray ray = UICamera.mainCamera.ScreenPointToRay(UICamera.lastEventPosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 20f, 1 << 8);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.name == "UsedCardBG")
                {
                    //UseCard();
                    m_Used = Game.BattleManager.UseCard(CardData);
                }
            }
        }
        if (m_Used == false)
        {
            TweenPosition.Begin(cacheChildCardTrans.gameObject, 0.5f, Vector3.zero);
            RefreshDepth(cacheChildCardTrans);
        }
    }
    protected void OnDrop(GameObject go)
    {
        //Debug.Log("OnDrop ：" + name);
    }
    protected void OnHover(bool isOver)
    {
        if (!IsMine())
        {
            return;
        }
        if (m_Used)
        {
            //transform.localScale = Vector3.one;
            TweenScale.Begin(cacheChildCardTrans.gameObject, 0.2f, Vector3.one);
            return;
        }
        if (isOver)
        {
            //transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            TweenScale.Begin(cacheChildCardTrans.gameObject, 0.2f, new Vector3(1.5f, 1.5f, 1.5f));
        }
        else
        {
            //transform.localScale = Vector3.one;
            TweenScale.Begin(cacheChildCardTrans.gameObject, 0.2f, Vector3.one);
        }
    }
    public void SetData(BattleCardData card, UIBattleForm form)
    {
        CardData = card;
        cacheForm = form;
        m_TexIcon.Load(card.Data.Icon);
        m_lblName.text = CardData.Data.Name;
        if (CardData.Data.Type != 0)
        {
            m_lblAttack.text = "";
            m_lblAttackCount.text = "";
        }
        for (int i = 0; i < CardData.Data.ActionTypes.Count; i++)
        {
            switch ((BattleActionType)CardData.Data.ActionTypes[i])
            {
                case BattleActionType.None:
                    break;
                case BattleActionType.AddBuff:
                    break;
                case BattleActionType.Attack:
                    if (CardData.Data.Type == 0)
                    {
                        m_lblAttack.text = "攻击";
                        m_lblAttackCount.text = CardData.Data.ActionParams[i].ToString();
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
        m_lblExpandCount.text = CardData.Data.Spending.ToString();
        if (card.Owner != Game.DataManager.MyPlayerData)
        {
            m_TexIcon.gameObject.SetActive(false);
            m_lblName.gameObject.SetActive(false);
            m_lblExpand.gameObject.SetActive(false);
            m_lblExpandCount.gameObject.SetActive(false);
            m_lblAttack.gameObject.SetActive(false);
            m_lblAttackCount.gameObject.SetActive(false);
        }
    }
    public void RefreshDepth()
    {
        RefreshDepth(transform);
    }
    public void RefreshDepth(Transform trans)
    {
        UIWidget[] widgets = trans.GetComponentsInChildren<UIWidget>(true);
        foreach (var item in widgets)
        {
            if (item.enabled)
            {
                item.Refresh();
            }

        }
    }
    /// <summary>
    /// 使用卡牌的表现
    /// </summary>
    /// <returns></returns>
    public bool UseCard()
    {
        //判断使用条件，不允许返回false
        Debug.Log("释放卡牌: " + CardData.Data.Name);
        //if (CardData.Owner.AP < CardData.Data.Spending)
        //{
        //    return false;
        //}
        cacheCardPos = cacheChildCardTrans.position;
        m_Used = true;
        if (CardData.Owner != Game.DataManager.MyPlayerData)
        {
            m_TexIcon.gameObject.SetActive(true);
            m_lblName.gameObject.SetActive(true);
            m_lblExpand.gameObject.SetActive(true);
            m_lblExpandCount.gameObject.SetActive(true);
            m_lblAttack.gameObject.SetActive(true);
            m_lblAttackCount.gameObject.SetActive(true);
        }
        return true;
    }

    //public void RevertCardPos()
    //{
    //    //cacheChildCardTrans.position = cacheCardPos;
    //    TweenPosition.Begin(cacheChildCardTrans.gameObject, 0.5f, Vector3.zero);
    //}
}