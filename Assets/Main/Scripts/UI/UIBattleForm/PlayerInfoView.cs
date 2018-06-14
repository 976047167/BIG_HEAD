using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoView : MonoBehaviour
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
    protected UIPlayerInfo playerInfo = new UIPlayerInfo();
    public UIPlayerInfo PlayerInfo { get { return playerInfo; } }

    public BattlePlayerData BindPlayerData { private set; get; }
    public void InitData(BattlePlayerData playerData)
    {
        BindPlayerData = playerData;
        this.playerInfo.HP = BindPlayerData.HP;
        this.playerInfo.MaxHP = BindPlayerData.MaxHP;
        this.playerInfo.AP = BindPlayerData.AP;
        this.playerInfo.MaxAP = BindPlayerData.MaxAP;
        this.playerInfo.CardCount = BindPlayerData.CurrentCardList.Count;
        this.playerInfo.CemeteryCount = BindPlayerData.UsedCardList.Count;
        GetUIController(transform);
    }
    void GetUIController(Transform transform)
    {
        utHeadIcon = transform.Find("HeadIcon").GetComponent<UITexture>();
        lblLevel = transform.Find("lblLevel").GetComponent<UILabel>();
        spHP_Progress = transform.Find("progressBlood").GetComponent<UISprite>();
        lblHP = transform.Find("progressBlood/HP").GetComponent<UILabel>();
        lblMaxHP = transform.Find("progressBlood/MaxHP").GetComponent<UILabel>();
        spMP_Progress = transform.Find("progressMP").GetComponent<UISprite>();
        lblMP = transform.Find("progressMP/MP").GetComponent<UILabel>();
        lblMaxMP = transform.Find("progressMP/MaxMP").GetComponent<UILabel>();
        lblCardCount = transform.Find("CardCount/CardCount").GetComponent<UILabel>();
        gridEquipGrid = transform.Find("EquipGrid").GetComponent<UIGrid>();
        lblCemeteryCount = transform.Find("Cemetery/CardCount").GetComponent<UILabel>();
        gridBuffGrid = transform.Find("BuffGrid").GetComponent<UIGrid>();
        goBuffIconTemplete = gridBuffGrid.transform.Find("buff").gameObject;
    }
    public void UpdateInfo()
    {
        if (utHeadIcon.mainTexture == null)
        {
            utHeadIcon.Load(BindPlayerData.HeadIcon);
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
            if (BindPlayerData == Game.BattleManager.MyPlayer.Data)
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
                buffIcon.GetComponent<UITexture>().Load(buffData.Data.IconID);
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
            playerInfo.CardCount = BindPlayerData.CurrentCardList.Count;
        }
    }
    public void UseCard(BattleCardData cardData)
    {
        playerInfo.CemeteryCount++;
        playerInfo.AP -= cardData.Data.Spending;
    }
    public void RoundStart()
    {
        playerInfo.AP = playerInfo.MaxAP = BindPlayerData.MaxAP;
    }

}