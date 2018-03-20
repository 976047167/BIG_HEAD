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

    // Use this for initialization
    void Start()
    {
        myPlayerViews = new PlayerInfoViews();
        oppPlayerViews = new PlayerInfoViews();
        myPlayerViews.GetUIController(transform.Find("BattleInfo/MeInfo"));
        oppPlayerViews.GetUIController(transform.Find("BattleInfo/OppInfo"));
        UpdateInfo();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    GameObject card = Instantiate<GameObject>(m_BattleCardTemplate, m_MyCardsGrid.transform);
        //    card.SetActive(true);
        //    m_MyCardsGrid.Reposition();
        //}
    }

    public void UpdateInfo()
    {
        myPlayerViews.UpdateInfo(Game.DataManager.MyPlayerData);
        oppPlayerViews.UpdateInfo(Game.DataManager.OppPlayerData);
    }

    public void UseCard(UIBattleCard battleCard)
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
        TweenPosition.Begin(battleCard.gameObject, 0.5f, Vector3.zero);
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
        }

        public void UpdateInfo(BattlePlayerData playerData)
        {
            if (HeadIcon.mainTexture==null || HeadIcon.mainTexture.name!=playerData.HeadIcon)
            {
                HeadIcon.Load(playerData.HeadIcon);
            }
            Level.text = playerData.Level.ToString();
            HP_Progress.fillAmount = playerData.HP / playerData.MaxHP;
            HP.text = playerData.HP.ToString();
            MaxHP.text = playerData.MaxHP.ToString();
            MP.text = playerData.MP.ToString();
            MaxMP.text = playerData.MaxMP.ToString();
            MP_Progress.fillAmount = playerData.MP / playerData.MaxMP;
            CardCount.text = playerData.CardList.Count.ToString();
            CemeteryCount.text = playerData.UsedCardList.Count.ToString();
        }
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
