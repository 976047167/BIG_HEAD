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
        myPlayerViews = new PlayerInfoViews();
        oppPlayerViews = new PlayerInfoViews();
        myPlayerViews.GetUIController(transform.Find("BattleInfo/MeInfo"));
        oppPlayerViews.GetUIController(transform.Find("BattleInfo/OppInfo"));
        resultInfo = transform.Find("ResultInfo").gameObject;
        lblResultInfo = transform.Find("ResultInfo/result").GetComponent<UILabel>();
        MovingPanel = transform.Find("MovingPanel").GetComponent<UIPanel>();
        UpdateInfo();
        Game.BattleManager.ReadyStart(this);
        UIEventListener.Get(transform.Find("btnRoundEnd").gameObject).onClick = OnClick_RoundEnd;
        UIEventListener.Get(transform.Find("ResultInfo/mask").gameObject).onClick = Onclick_CloseUI;
    }
    // Update is called once per frame
    void Update()
    {
        Game.BattleManager.UpdateScope();
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        myPlayerViews.UpdateInfo(Game.DataManager.MyPlayerData);
        oppPlayerViews.UpdateInfo(Game.DataManager.OppPlayerData);
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
        m_UsedCardsGrid.GetChildList().ForEach((t) => Destroy(t.gameObject));
    }
    /// <summary>
    /// 添加卡牌至手牌，要做成列表，显示抽牌动画
    /// </summary>
    /// <param name="cardId"></param>
    public void AddHandCard(BattleCardData cardData)
    {
        if (cardData.Owner == Game.DataManager.MyPlayerData)
        {
            GameObject newCard = GameObject.Instantiate(m_BattleCardTemplate, m_MyCardsGrid.transform);
            newCard.SetActive(true);
            UIBattleCard battleCard = newCard.GetComponent<UIBattleCard>();
            battleCard.SetData(cardData, this);
            dicBattleCard.Add(cardData, battleCard);
            m_MyCardsGrid.Reposition();
        }
        else
        {
            GameObject newCard = GameObject.Instantiate(m_BattleCardTemplate, m_OppCardsGrid.transform);
            newCard.SetActive(true);
            UIBattleCard battleCard = newCard.GetComponent<UIBattleCard>();
            battleCard.SetData(cardData, this);
            dicBattleCard.Add(cardData, battleCard);
            m_OppCardsGrid.Reposition();
        }

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
        if (dicBattleCard.ContainsKey(battleCardData))
        {
            dicBattleCard[battleCardData].UseCard();
        }
        else
        {
            Debug.LogError("不存在");
        }


    }

    public void ApplyUseCard(UIBattleCard battleCard)
    {
        StartCoroutine(CoroutineUseCard(battleCard));
    }
    IEnumerator CoroutineUseCard(UIBattleCard battleCard)
    {
        Vector3 cachePos = battleCard.cacheChildCardTrans.position;
        battleCard.transform.SetParent(m_UsedCardsGrid.transform, false);
        //m_UsedCardsGrid.repositionNow = true;
        //m_MyCardsGrid.repositionNow = true;
        m_UsedCardsGrid.Reposition();
        m_MyCardsGrid.Reposition();
        battleCard.cacheChildCardTrans.position = cachePos;
        yield return null;
        TweenPosition.Begin(battleCard.cacheChildCardTrans.gameObject, 0.5f, Vector3.zero, false);
        yield return null;
        battleCard.ApplyUseEffect();
        yield return new WaitForSeconds(0.5f);
        battleCard.RefreshDepth();
    }

    [System.Serializable]
    class PlayerInfoViews
    {
        public UILabel HP;
        public UILabel MaxHP;
        public UISprite HP_Progress;
        public UILabel MP;
        public UILabel MaxMP;
        public UISprite MP_Progress;
        public UILabel Level;
        public UITexture HeadIcon;
        public UILabel CardCount;
        public UIGrid EquipGrid;
        /// <summary>
        /// 墓地
        /// </summary>
        public UILabel CemeteryCount;
        public UIGrid BuffGrid;
        public GameObject BuffIconTemplete;
        Dictionary<int, GameObject> BuffIcons = new Dictionary<int, GameObject>();

        public void GetUIController(Transform transInfo)
        {
            HeadIcon = transInfo.Find("HeadIcon").GetComponent<UITexture>();
            Level = transInfo.Find("lblLevel").GetComponent<UILabel>();
            HP_Progress = transInfo.Find("progressBlood").GetComponent<UISprite>();
            HP = transInfo.Find("progressBlood/HP").GetComponent<UILabel>();
            MaxHP = transInfo.Find("progressBlood/MaxHP").GetComponent<UILabel>();
            MP_Progress = transInfo.Find("progressMP").GetComponent<UISprite>();
            MP = transInfo.Find("progressMP/MP").GetComponent<UILabel>();
            MaxMP = transInfo.Find("progressMP/MaxMP").GetComponent<UILabel>();
            CardCount = transInfo.Find("CardCount/CardCount").GetComponent<UILabel>();
            EquipGrid = transInfo.Find("EquipGrid").GetComponent<UIGrid>();
            CemeteryCount = transInfo.Find("Cemetery/CardCount").GetComponent<UILabel>();
            BuffGrid = transInfo.Find("BuffGrid").GetComponent<UIGrid>();
            BuffIconTemplete = BuffGrid.transform.Find("buff").gameObject;
        }

        public void UpdateInfo(BattlePlayerData playerData)
        {
            if (HeadIcon.mainTexture == null || HeadIcon.mainTexture.name != playerData.HeadIcon)
            {
                HeadIcon.Load(playerData.HeadIcon);
            }
            Level.text = playerData.Level.ToString();
            HP_Progress.fillAmount = (float)playerData.HP / playerData.MaxHP;
            HP.text = playerData.HP.ToString();
            MaxHP.text = playerData.MaxHP.ToString();
            MP.text = playerData.AP.ToString();
            MaxMP.text = playerData.MaxAP.ToString();
            MP_Progress.fillAmount = (float)playerData.AP / playerData.MaxAP;
            CardCount.text = playerData.CardList.Count.ToString();
            CemeteryCount.text = playerData.UsedCardList.Count.ToString();
            foreach (var item in BuffIcons)
            {
                item.Value.SetActive(false);
            }
            for (int i = 0; i < playerData.BuffList.Count; i++)
            {
                BattleBuffData buffData = playerData.BuffList[i];
                GameObject buffIcon;
                if (!BuffIcons.ContainsKey(buffData.BuffId))
                {
                    buffIcon = Instantiate(BuffIconTemplete, BuffGrid.transform);
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
