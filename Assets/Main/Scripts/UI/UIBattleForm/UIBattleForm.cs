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

    int monsterId = 0;

    Dictionary<BattleCardData, UIBattleCard> dicBattleCard = new Dictionary<BattleCardData, UIBattleCard>();

    public UIPanel MovingPanel { get; private set; }

    public UIGrid MyCardsGrid
    {
        get
        {
            return m_MyCardsGrid;
        }
    }

    public UIGrid UsedCardsGrid
    {
        get
        {
            return m_UsedCardsGrid;
        }
    }

    public UIGrid OppCardsGrid
    {
        get
        {
            return m_OppCardsGrid;
        }
    }


    // Use this for initialization
    void Start()
    {
        myPlayerViews = new PlayerInfoViews(Game.BattleManager.MyPlayerData);
        oppPlayerViews = new PlayerInfoViews(Game.BattleManager.OppPlayerData);
        myPlayerViews.GetUIController(transform.Find("BattleInfo/MeInfo"));
        oppPlayerViews.GetUIController(transform.Find("BattleInfo/OppInfo"));
        resultInfo = transform.Find("ResultInfo").gameObject;
        lblResultInfo = transform.Find("ResultInfo/result").GetComponent<UILabel>();
        MovingPanel = transform.Find("MovingPanel").GetComponent<UIPanel>();
        UpdateInfo();
        monsterId = Game.BattleManager.MonsterId;
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
    public void WinBattle()
    {
        resultInfo.SetActive(true);
        resultInfo.transform.Find("bg").gameObject.SetActive(false);
        lblResultInfo.gameObject.SetActive(false);
        //lblResultInfo.text = "WIN!";
        //lblResultInfo.color = new Color32(255, 0, 0, 255);
        UIModule.Instance.OpenForm<WND_Reward>(monsterId);
    }
    public void LoseBattle()
    {
        resultInfo.SetActive(true);
        resultInfo.transform.Find("bg").gameObject.SetActive(true);
        lblResultInfo.gameObject.SetActive(true);
        lblResultInfo.text = "LOSE!";
        lblResultInfo.color = new Color32(150, 150, 150, 255);
        Application.Quit();
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
    public UIBattleCard CreateBattleCard(BattleCardData cardData, UIGrid parentGrid)
    {
        GameObject newCard = GameObject.Instantiate(m_BattleCardTemplate, parentGrid.transform);
        newCard.SetActive(true);
        UIBattleCard battleCard = newCard.GetComponent<UIBattleCard>();
        battleCard.SetData(cardData, this);
        dicBattleCard.Add(cardData, battleCard);
        parentGrid.Reposition();
        return battleCard;
    }
    public UIBattleCard GetUIBattleCard(BattleCardData cardData)
    {
        if (dicBattleCard.ContainsKey(cardData))
        {
            return dicBattleCard[cardData];
        }
        return null;
    }
    public PlayerInfoViews GetPlayerInfoViewByPlayerData(BattlePlayer player)
    {
        if (player == Game.DataManager.MyPlayer)
        {
            return myPlayerViews;
        }
        else
        {
            return oppPlayerViews;
        }
    }
    public void AddUIAction(UIAction uiAction)
    {
        uiActions.Enqueue(uiAction);
    }

    Queue<UIAction> uiActions = new Queue<UIAction>();
    IEnumerator CoroutineUseCard()
    {
        while (true)
        {
            if (uiActions.Count > 0)
            {
                UIAction action = uiActions.Dequeue();
                if (action.BindActionList != null)
                {
                    for (int i = 0; i < action.BindActionList.Count; i++)
                    {
                        StartCoroutine(action.BindActionList[i].Excute());
                    }
                }
                yield return action.Excute();
                //switch (action.ActionType)
                //{
                //    case UIActionType.None:
                //        break;
                //    case UIActionType.DrawCard:
                //        cardData = action.CardData;
                //        if (cardData.Owner == Game.DataManager.MyPlayerData)
                //        {
                //            GameObject newCard = GameObject.Instantiate(m_BattleCardTemplate, m_MyCardsGrid.transform);
                //            newCard.SetActive(true);
                //            battleCard = newCard.GetComponent<UIBattleCard>();
                //            battleCard.SetData(cardData, this);
                //            dicBattleCard.Add(cardData, battleCard);
                //            m_MyCardsGrid.Reposition();
                //        }
                //        else
                //        {
                //            GameObject newCard = GameObject.Instantiate(m_BattleCardTemplate, m_OppCardsGrid.transform);
                //            newCard.SetActive(true);
                //            battleCard = newCard.GetComponent<UIBattleCard>();
                //            battleCard.SetData(cardData, this);
                //            dicBattleCard.Add(cardData, battleCard);
                //            m_OppCardsGrid.Reposition();
                //        }
                //        yield return new WaitForSeconds(0.5f);
                //        break;
                //    case UIActionType.UseCard:
                //        if (dicBattleCard.ContainsKey(cardData))
                //        {
                //            //dicBattleCard[cardData].UseCard();
                //            battleCard = dicBattleCard[action.CardData];
                //            battleCard.UseCard();
                //            //yield return new WaitForSeconds(0.5f);
                //            Vector3 cachePos = battleCard.cacheChildCardTrans.position;
                //            battleCard.transform.SetParent(m_UsedCardsGrid.transform, false);
                //            //m_UsedCardsGrid.repositionNow = true;
                //            //m_MyCardsGrid.repositionNow = true;
                //            m_UsedCardsGrid.Reposition();
                //            m_MyCardsGrid.Reposition();

                //            battleCard.cacheChildCardTrans.position = cachePos;
                //            yield return null;
                //            TweenPosition.Begin(battleCard.cacheChildCardTrans.gameObject, 0.5f, Vector3.zero, false);
                //            //yield return null;
                //            //battleCard.ApplyUseEffect();
                //            yield return new WaitForSeconds(0.5f);
                //            m_OppCardsGrid.Reposition();
                //            battleCard.RefreshDepth();
                //            yield return null;
                //        }
                //        else
                //        {
                //            Debug.LogError("不存在");

                //        }
                //        break;

                //    default:
                //        break;
                //}

            }
            else
                yield return null;
        }

    }


    [System.Serializable]
    public class PlayerInfoViews
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
        UIPlayerInfo playerInfo = new UIPlayerInfo();

        BattlePlayerData bindPlayerData;
        public PlayerInfoViews(BattlePlayerData playerData)
        {
            bindPlayerData = playerData;
            InitData();

        }
        void InitData()
        {
            this.playerInfo.HP = bindPlayerData.HP;
            this.playerInfo.MaxHP = bindPlayerData.MaxHP;
            this.playerInfo.AP = bindPlayerData.AP;
            this.playerInfo.MaxAP = bindPlayerData.MaxAP;
            this.playerInfo.CardCount = bindPlayerData.CurrentCardList.Count;
            this.playerInfo.CemeteryCount = bindPlayerData.UsedCardList.Count;
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
            lblLevel.text = playerInfo.Level.ToString();
            spHP_Progress.fillAmount = (float)playerInfo.HP / playerInfo.MaxHP;
            lblHP.text = playerInfo.HP.ToString();
            lblMaxHP.text = playerInfo.MaxHP.ToString();
            lblMP.text = playerInfo.AP.ToString();
            lblMaxMP.text = playerInfo.MaxAP.ToString();
            spMP_Progress.fillAmount = (float)playerInfo.AP / playerInfo.MaxAP;
            lblCardCount.text = playerInfo.CardCount.ToString();
            lblCemeteryCount.text = playerInfo.CemeteryCount.ToString();
            SetBuffUI();
        }
        public IEnumerator SetHpDamage(int damage)
        {
            Color orginColor = lblHP.color;
            lblHP.color = Color.red;
            yield return null;
            TweenScale.Begin(lblHP.gameObject, 0.15f, new Vector3(1.2f, 1.2f, 1.2f));
            yield return new WaitForSeconds(0.15f);
            playerInfo.HP -= damage;
            if (playerInfo.HP <= 0)
            {
                if (bindPlayerData == Game.DataManager.MyPlayer.Data)
                {
                    Game.BattleManager.BattleForm.LoseBattle();
                }
                else
                {
                    Game.BattleManager.BattleForm.WinBattle();
                }
            }
            TweenScale.Begin(lblHP.gameObject, 0.15f, Vector3.one);
            yield return new WaitForSeconds(0.15f);
            lblHP.color = orginColor;
        }
        public IEnumerator SetHpRecover(int hp)
        {
            Color orginColor = lblHP.color;
            lblHP.color = Color.green;
            yield return null;
            TweenScale.Begin(lblHP.gameObject, 0.15f, new Vector3(1.2f, 1.2f, 1.2f));
            yield return new WaitForSeconds(0.15f);
            playerInfo.HP += hp;
            if (playerInfo.HP >= playerInfo.MaxHP)
            {
                playerInfo.HP = playerInfo.MaxHP;
            }
            TweenScale.Begin(lblHP.gameObject, 0.15f, Vector3.one);
            yield return new WaitForSeconds(0.15f);
            lblHP.color = orginColor;
        }
        public IEnumerator SpendAp(int ap)
        {
            Color orginColor = lblMP.color;
            lblMP.color = Color.green;
            yield return null;
            TweenScale.Begin(lblMP.gameObject, 0.15f, new Vector3(1.2f, 1.2f, 1.2f));
            yield return new WaitForSeconds(0.15f);
            playerInfo.AP -= ap;
            TweenScale.Begin(lblMP.gameObject, 0.15f, Vector3.one);
            yield return new WaitForSeconds(0.15f);
            lblMP.color = orginColor;
        }
        void SetBuffUI()
        {
            foreach (var item in BuffIcons)
            {
                item.Value.SetActive(false);
            }
            List<BattleBuffData> removeList = new List<BattleBuffData>();
            for (int i = 0; i < playerInfo.Buffs.Count; i++)
            {
                BattleBuffData buffData = playerInfo.Buffs[i];
                if (buffData.Time <= 0)
                {
                    removeList.Add(buffData);
                    continue;
                }
                GameObject buffIcon;
                if (!BuffIcons.ContainsKey(buffData.BuffId))
                {
                    buffIcon = Instantiate(goBuffIconTemplete, gridBuffGrid.transform);
                    BuffIcons.Add(buffData.BuffId, buffIcon);
                    buffIcon.GetComponent<UITexture>().Load(buffData.Data.Icon);
                    gridBuffGrid.Reposition();
                }
                else
                    buffIcon = BuffIcons[buffData.BuffId];
                buffIcon.SetActive(true);
                buffIcon.transform.Find("Label").GetComponent<UILabel>().text = buffData.Time.ToString();
            }
            for (int i = 0; i < removeList.Count; i++)
            {
                playerInfo.Buffs.Remove(removeList[i]);
            }
        }

        public void AddBuff(BattleBuffData buffData)
        {
            playerInfo.Buffs.Add(buffData);
        }
        public void DrawCard()
        {
            playerInfo.CardCount--;
            if (playerInfo.CardCount <= 0)
            {
                playerInfo.CardCount = bindPlayerData.CurrentCardList.Count;
            }
        }
        public void UseCard(BattleCardData cardData)
        {
            playerInfo.CemeteryCount++;
            playerInfo.AP -= cardData.Data.Spending;
        }
        public void RoundStart()
        {
            playerInfo.AP = playerInfo.MaxAP = bindPlayerData.MaxAP;
        }

    }
    public class UIPlayerInfo
    {
        public int HP = 0;
        public int MaxHP = 0;
        public int AP = 0;
        public int MaxAP = 0;
        public int Level = 0;
        public int CardCount = 0;
        public int CemeteryCount = 0;
        public List<BattleBuffData> Buffs = new List<BattleBuffData>();
    }

}
