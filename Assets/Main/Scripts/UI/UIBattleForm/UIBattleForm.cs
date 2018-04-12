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
    [SerializeField]
    private PlayerInfoViews myPlayerViews;
    [SerializeField]
    private PlayerInfoViews oppPlayerViews;
    [SerializeField]
    private GameObject resultInfo;
    [SerializeField]
    private UILabel lblResultInfo;

    Dictionary<BattleCardData, UIBattleCard> dicBattleCard = new Dictionary<BattleCardData, UIBattleCard>();

    public UIPanel MovingPanel { get; private set; }


    // Use this for initialization
    void Start()
    {
        myPlayerViews = new PlayerInfoViews(Game.DataManager.MyPlayerData);
        oppPlayerViews = new PlayerInfoViews(Game.DataManager.MyPlayerData);
        myPlayerViews.GetUIController(transform.Find("BattleInfo/MeInfo"));
        oppPlayerViews.GetUIController(transform.Find("BattleInfo/OppInfo"));
        resultInfo = transform.Find("ResultInfo").gameObject;
        lblResultInfo = transform.Find("ResultInfo/result").GetComponent<UILabel>();
        MovingPanel = transform.Find("MovingPanel").GetComponent<UIPanel>();
        UpdateInfo();
        Game.BattleManager.ReadyStart(this);
        UIEventListener.Get(transform.Find("btnRoundEnd").gameObject).onClick = OnClick_RoundEnd;
        UIEventListener.Get(transform.Find("ResultInfo/mask").gameObject).onClick = Onclick_CloseUI;

        StartCoroutine(CoroutineUseCard());
    }
    // Update is called once per frame
    void Update()
    {
        Game.BattleManager.UpdateScope();
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        myPlayerViews.UpdateInfo();
        oppPlayerViews.UpdateInfo();
    }
    public void UpdateBuffIcons()
    {

    }
    public void WinBattle()
    {
        resultInfo.SetActive(true);
        lblResultInfo.text = "WIN!";
        lblResultInfo.color = new Color32(255, 0, 0, 255);
    }
    public void LoseBattle()
    {
        resultInfo.SetActive(true);
        lblResultInfo.text = "LOSE!";
        lblResultInfo.color = new Color32(150, 150, 150, 255);
    }
    /// <summary>
    /// 结束当前回合的按钮
    /// </summary>
    /// <param name="go"></param>
    void OnClick_RoundEnd(GameObject go)
    {
        Game.BattleManager.RoundEnd();
    }
    void Onclick_CloseUI(GameObject go)
    {
        Game.UI.CloseForm<UIBattleForm>();
    }
    public void ClearUsedCards()
    {
        m_UsedCardsGrid.GetChildList().ForEach((t) => t.gameObject.SetActive(false));
    }
    /// <summary>
    /// 添加卡牌至手牌，要做成列表，显示抽牌动画
    /// </summary>
    /// <param name="cardId"></param>
    public void AddHandCard(BattleCardData cardData)
    {
        uiActions.Enqueue(new UIAction(UIActionType.DrawCard, cardData));

    }


    /// <summary>
    /// 添加卡牌到牌库
    /// </summary>
    /// <param name="cardId"></param>
    public void AddCurrentList(int cardId)
    {

    }
    /// <summary>
    /// 使用卡牌
    /// </summary>
    /// <param name="battleCard"></param>
    public void UseCard(BattleCardData battleCardData)
    {
        uiActions.Enqueue(new UIAction(UIActionType.UseCard, battleCardData));


    }

    Queue<UIAction> uiActions = new Queue<UIAction>();
    public void ApplyUseCard(UIBattleCard battleCard)
    {

    }
    IEnumerator CoroutineUseCard()
    {
        while (true)
        {
            if (uiActions.Count > 0)
            {
                UIAction action = uiActions.Dequeue();
                BattleCardData cardData = action.CardData;
                UIBattleCard battleCard = null;
                switch (action.ActionType)
                {
                    case UIActionType.None:
                        break;
                    case UIActionType.DrawCard:
                        cardData = action.CardData;
                        if (cardData.Owner == Game.DataManager.MyPlayerData)
                        {
                            GameObject newCard = GameObject.Instantiate(m_BattleCardTemplate, m_MyCardsGrid.transform);
                            newCard.SetActive(true);
                            battleCard = newCard.GetComponent<UIBattleCard>();
                            battleCard.SetData(cardData, this);
                            dicBattleCard.Add(cardData, battleCard);
                            m_MyCardsGrid.Reposition();
                        }
                        else
                        {
                            GameObject newCard = GameObject.Instantiate(m_BattleCardTemplate, m_OppCardsGrid.transform);
                            newCard.SetActive(true);
                            battleCard = newCard.GetComponent<UIBattleCard>();
                            battleCard.SetData(cardData, this);
                            dicBattleCard.Add(cardData, battleCard);
                            m_OppCardsGrid.Reposition();
                        }
                        yield return new WaitForSeconds(0.5f);
                        break;
                    case UIActionType.UseCard:
                        if (dicBattleCard.ContainsKey(cardData))
                        {
                            //dicBattleCard[cardData].UseCard();
                            battleCard = dicBattleCard[action.CardData];
                            battleCard.UseCard();
                            //yield return new WaitForSeconds(0.5f);
                            Vector3 cachePos = battleCard.cacheChildCardTrans.position;
                            battleCard.transform.SetParent(m_UsedCardsGrid.transform, false);
                            //m_UsedCardsGrid.repositionNow = true;
                            //m_MyCardsGrid.repositionNow = true;
                            m_UsedCardsGrid.Reposition();
                            m_MyCardsGrid.Reposition();

                            battleCard.cacheChildCardTrans.position = cachePos;
                            yield return null;
                            TweenPosition.Begin(battleCard.cacheChildCardTrans.gameObject, 0.5f, Vector3.zero, false);
                            //yield return null;
                            //battleCard.ApplyUseEffect();
                            yield return new WaitForSeconds(0.5f);
                            m_OppCardsGrid.Reposition();
                            battleCard.RefreshDepth();
                            yield return null;
                        }
                        else
                        {
                            Debug.LogError("不存在");

                        }
                        break;

                    default:
                        break;
                }

            }
            else
                yield return null;
        }

    }


    [System.Serializable]
    class PlayerInfoViews
    {
        public UILabel lblHP;
        public UILabel lblMaxHP;
        public UISprite spHP_Progress;
        public UILabel lblMP;
        public UILabel lblMaxMP;
        public UISprite spMP_Progress;
        public UILabel lblLevel;
        public UITexture utHeadIcon;
        public UILabel lblCardCount;
        public UIGrid gridEquipGrid;
        /// <summary>
        /// 墓地
        /// </summary>
        public UILabel lblCemeteryCount;
        public UIGrid gridBuffGrid;
        public GameObject goBuffIconTemplete;
        Dictionary<int, GameObject> BuffIcons = new Dictionary<int, GameObject>();
        int HP = 0;
        int MaxHP = 0;
        int AP = 0;
        int MaxAP = 0;
        int Level = 0;
        int CardCount = 0;
        int CemeteryCount = 0;
        List<BattleBuffData> Buffs = new List<BattleBuffData>();

        BattlePlayerData bindPlayerData;
        public PlayerInfoViews(BattlePlayerData playerData)
        {
            bindPlayerData = playerData;


        }
        public void GetUIController(Transform transInfo)
        {
            utHeadIcon = transInfo.Find("HeadIcon").GetComponent<UITexture>();
            lblLevel = transInfo.Find("lblLevel").GetComponent<UILabel>();
            spHP_Progress = transInfo.Find("progressBlood").GetComponent<UISprite>();
            lblHP = transInfo.Find("progressBlood/HP").GetComponent<UILabel>();
            lblMaxHP = transInfo.Find("progressBlood/MaxHP").GetComponent<UILabel>();
            spMP_Progress = transInfo.Find("progressMP").GetComponent<UISprite>();
            lblMP = transInfo.Find("progressMP/MP").GetComponent<UILabel>();
            lblMaxMP = transInfo.Find("progressMP/MaxMP").GetComponent<UILabel>();
            lblCardCount = transInfo.Find("CardCount/CardCount").GetComponent<UILabel>();
            gridEquipGrid = transInfo.Find("EquipGrid").GetComponent<UIGrid>();
            lblCemeteryCount = transInfo.Find("Cemetery/CardCount").GetComponent<UILabel>();
            gridBuffGrid = transInfo.Find("BuffGrid").GetComponent<UIGrid>();
            goBuffIconTemplete = gridBuffGrid.transform.Find("buff").gameObject;
        }
        public void UpdateInfo()
        {
            if (utHeadIcon.mainTexture == null || utHeadIcon.mainTexture.name != bindPlayerData.HeadIcon)
            {
                utHeadIcon.Load(bindPlayerData.HeadIcon);
            }
            lblLevel.text = Level.ToString();
            spHP_Progress.fillAmount = (float)HP / MaxHP;
            lblHP.text = HP.ToString();
            lblMaxHP.text = MaxHP.ToString();
            lblMP.text = AP.ToString();
            lblMaxMP.text = MaxAP.ToString();
            spMP_Progress.fillAmount = (float)AP / MaxAP;
            lblCardCount.text = CardCount.ToString();
            lblCemeteryCount.text = CemeteryCount.ToString();
            foreach (var item in BuffIcons)
            {
                item.Value.SetActive(false);
            }
            for (int i = 0; i < Buffs.Count; i++)
            {
                BattleBuffData buffData = Buffs[i];
                GameObject buffIcon;
                if (!BuffIcons.ContainsKey(buffData.BuffId))
                {
                    buffIcon = Instantiate(goBuffIconTemplete, gridBuffGrid.transform);
                    BuffIcons.Add(buffData.BuffId, buffIcon);
                    buffIcon.GetComponent<UITexture>().Load(buffData.Data.Icon);
                }
                else
                    buffIcon = BuffIcons[buffData.BuffId];
                buffIcon.SetActive(true);
                buffIcon.transform.Find("Label").GetComponent<UILabel>().text = buffData.Time.ToString();
            }
        }
        void SetBuffUI()
        {

        }
        //public void AddBuff(BattleBuffData buffData)
        //{

        //}

    }
    class PlayerInfo
    {
        public int HP = 0;
        public int MaxHP = 0;
        public int MP = 0;
        public int MaxMP = 0;
        public int Level = 1;
        public string HeadIcon = "";
        public List<CardInfo> CardList = new List<CardInfo>();
    }

    class CardInfo
    {

    }
}
