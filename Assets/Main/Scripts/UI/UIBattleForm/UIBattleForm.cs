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
    private PlayerInfoView myPlayerViews;
    [SerializeField]
    private PlayerInfoView oppPlayerViews;
    [SerializeField]
    private GameObject resultInfo;
    [SerializeField]
    private UILabel lblResultInfo;
    [SerializeField]
    private UILabel lblRoundCount;
    [SerializeField]
    private GameObject btnBattleSetting;


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
    /// <summary>
    /// 根据可否使用卡牌，判断可否结束回合
    /// </summary>
    public bool CanUseCard { get; set; }


    // Use this for initialization
    protected override void OnOpen()
    {
        myPlayerViews = transform.Find("BattleInfo/MeInfo").gameObject.AddComponent<PlayerInfoView>();
        myPlayerViews.InitData(Game.BattleManager.MyPlayer.Data);
        oppPlayerViews = transform.Find("BattleInfo/OppInfo").gameObject.AddComponent<PlayerInfoView>();
        oppPlayerViews.InitData(Game.BattleManager.OppPlayer.Data);
        //myPlayerViews.GetUIController();
        //oppPlayerViews.GetUIController();
        resultInfo = transform.Find("ResultInfo").gameObject;
        lblResultInfo = transform.Find("ResultInfo/result").GetComponent<UILabel>();
        MovingPanel = transform.Find("MovingPanel").GetComponent<UIPanel>();
        lblRoundCount = transform.Find("FlowPanel/split/lblRoundCount").GetComponent<UILabel>();
        btnBattleSetting = transform.Find("FlowPanel/btnSetting").gameObject;
        UpdateInfo();
        monsterId = Game.BattleManager.MonsterId;
        Game.BattleManager.ReadyStart(this);
        UIEventListener.Get(transform.Find("FlowPanel/btnRoundEnd").gameObject).onClick = OnClick_RoundEnd;
        UIEventListener.Get(transform.Find("ResultInfo/mask").gameObject).onClick = Onclick_CloseUI;

        StartCoroutine(CoroutineUseCard());
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
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
        

    }
    public void LoseBattle()
    {
        resultInfo.SetActive(true);
        resultInfo.transform.Find("bg").gameObject.SetActive(true);
        lblResultInfo.gameObject.SetActive(true);
        lblResultInfo.text = "LOSE!";
        lblResultInfo.color = new Color32(150, 150, 150, 255);

    }
    public void MeEscapeBattle()
    {
        resultInfo.SetActive(true);
        resultInfo.transform.Find("bg").gameObject.SetActive(true);
        lblResultInfo.gameObject.SetActive(true);
        lblResultInfo.text = "我跑!";
        lblResultInfo.color = new Color32(150, 150, 150, 255);

    }
    public void OppEscapeBattle()
    {
        resultInfo.SetActive(true);
        resultInfo.transform.Find("bg").gameObject.SetActive(true);
        lblResultInfo.gameObject.SetActive(true);
        lblResultInfo.text = "快追!";
        lblResultInfo.color = new Color32(150, 150, 150, 255);

    }
    public void UpdateRoundCount(int roundCount)
    {
        lblRoundCount.text = roundCount.ToString();
    }
    /// <summary>
    /// 结束当前回合的按钮
    /// </summary>
    /// <param name="go"></param>
    void OnClick_RoundEnd(GameObject go)
    {
        if (CanUseCard)
        {
            CanUseCard = false;
            Game.BattleManager.RoundEnd();
        }
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
    public PlayerInfoView GetPlayerInfoViewByPlayer(BattlePlayer player)
    {
        if (player == Game.BattleManager.MyPlayer)
        {
            return myPlayerViews;
        }
        else
        {
            return oppPlayerViews;
        }
    }


    IEnumerator CoroutineUseCard()
    {
        while (true)
        {
            UIAction action = Game.BattleManager.GetTopUIAction();
            if (action != null)
            {
                if (action.BindActionList != null)
                {
                    for (int i = 0; i < action.BindActionList.Count; i++)
                    {
                        StartCoroutine(action.BindActionList[i].Excute());
                    }
                }
                yield return action.Excute();

            }
            else
                yield return null;
        }

    }

    protected override void OnClose()
    {
        base.OnClose();
        Game.BattleManager.Clear();
    }

    
}

public class UIPlayerInfo
{
    public int HP = 0;
    public int MaxHP = 0;
    public int MP = 0;
    public int MaxMP = 0;
    public int AP = 0;
    public int MaxAP = 0;
    public int Level = 0;
    public int CardCount = 0;
    public int CemeteryCount = 0;
    public List<BattleBuffData> Buffs = new List<BattleBuffData>();
    public List<BattleEquipData> Equips = new List<BattleEquipData>();
}


