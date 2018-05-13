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
    private deck_or_kaku _deck_or_Kaku;
    public deck_or_kaku DeckOrKaku {
        set {
            _deck_or_Kaku = value;
            gameObject.GetComponent<UIDragScrollView>().enabled = value == deck_or_kaku.deck;
        }

        get { return _deck_or_Kaku; }
    }

    public enum deck_or_kaku : int {
        deck,
        kaku
    };
    Vector3 offsetPos;
    [HideInInspector]
    public Transform cacheChildCardTrans;
    Vector3 cacheCardPos;
    WND_Kaku cacheForm;
    public NormalCard cardData;
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

 
    protected void OnDragStart()
    {
        if (DeckOrKaku == deck_or_kaku.deck)
        {
           
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

        Debug.Log("OnDragEnd ：" + name);
        m_Draging = false;
        cacheChildCardTrans.SetParent(transform, true);
        RefreshDepth();
        Ray ray = UICamera.mainCamera.ScreenPointToRay(UICamera.lastEventPosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 20f, 1 << 8);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.name == "bgKaku" && DeckOrKaku == deck_or_kaku.deck)
                {
                    cacheChildCardTrans.transform.localPosition = Vector3.zero;

                    cacheForm.MoveCardFromDeckToKaku(this);
                    RefreshDepth();
                    return;
                }
                else if (hits[i].collider.name == "bgDeck" && DeckOrKaku == deck_or_kaku.kaku)
                {
                    cacheChildCardTrans.transform.localPosition = Vector3.zero;

                    cacheForm.MoveCardFromKakuToDeck(this);
                    RefreshDepth();
                    return;
                }
            }
        }
        cacheChildCardTrans.transform.localPosition = Vector3.zero;

    }
    protected void OnDrop(GameObject go)
    {
        //Debug.Log("OnDrop ：" + name);
    }
    public void SetData(NormalCard card, WND_Kaku form)
    {

        cardData = card;
        cacheForm = form;
             m_TexIcon.Load(cardData.CardData.Icon);
            m_lblName.text = cardData.CardData.Name;
            if (cardData.CardData.Type != 0)
            {
                m_lblAttack.text = "";
                m_lblAttackCount.text = "";
            }
            for (int i = 0; i < cardData.CardData.ActionTypes.Count; i++)
            {
                switch ((BattleActionType)cardData.CardData.ActionTypes[i])
                {
                    case BattleActionType.None:
                        break;
                    case BattleActionType.AddBuff:
                        break;
                    case BattleActionType.Attack:
                        if (cardData.CardData.Type == 0)
                        {
                            m_lblAttack.text = "攻击";
                            m_lblAttackCount.text = cardData.CardData.ActionParams[i].ToString();
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
            m_lblExpandCount.text = cardData.CardData.Spending.ToString();
        

        UIEventListener.Get(gameObject).onClick = (GameObject a) =>
        {
            UIModule.Instance.OpenForm<WND_ShowCard>(cardData.CardId);


        };

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

    //public void RevertCardPos()
    //{
    //    //cacheChildCardTrans.position = cacheCardPos;
    //    TweenPosition.Begin(cacheChildCardTrans.gameObject, 0.5f, Vector3.zero);
    //}
}