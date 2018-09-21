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
    public GameObject goEquipTemplete;
    Dictionary<int, GameObject> buffIcons = new Dictionary<int, GameObject>();
    Dictionary<int, GameObject> equipIcons = new Dictionary<int, GameObject>();
    protected UIPlayerInfo playerInfo = new UIPlayerInfo();
    public UIPlayerInfo PlayerInfo { get { IsDirty = true; return playerInfo; } }
    public BattlePlayerData bindPlayerData = null;
    public BattlePlayerData BindPlayerData { get { IsDirty = false; return bindPlayerData; } }
    public Dictionary<int, GameObject> BuffIcons { get { return buffIcons; } }
    public Dictionary<int, GameObject> EquipIcons { get { return equipIcons; } }

    protected bool IsDirty = false;
    public void InitData(BattlePlayerData playerData)
    {
        bindPlayerData = playerData;
        this.playerInfo.HP = bindPlayerData.HP;
        this.playerInfo.MaxHP = bindPlayerData.MaxHP;
        this.playerInfo.AP = bindPlayerData.AP;
        this.playerInfo.MaxAP = bindPlayerData.MaxAP;
        this.playerInfo.MP = bindPlayerData.MP;
        this.playerInfo.MaxMP = bindPlayerData.MaxMP;
        this.playerInfo.CardCount = bindPlayerData.CurrentCardList.Count;
        this.playerInfo.CemeteryCount = bindPlayerData.UsedCardList.Count;
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
        goEquipTemplete = gridEquipGrid.transform.Find("BattleCardEquip").gameObject;
    }
    public void UpdateInfo()
    {
        if (utHeadIcon.mainTexture == null)
        {
            utHeadIcon.Load(bindPlayerData.HeadIcon);
        }
        lblLevel.text = playerInfo.Level.ToString();
        spHP_Progress.fillAmount = (float)playerInfo.HP / playerInfo.MaxHP;
        lblHP.text = playerInfo.HP.ToString();
        lblMaxHP.text = playerInfo.MaxHP.ToString();
        lblMP.text = playerInfo.MP.ToString();
        lblMaxMP.text = playerInfo.MaxMP.ToString();
        spMP_Progress.fillAmount = (float)playerInfo.MP / playerInfo.MaxMP;
        lblCardCount.text = playerInfo.CardCount.ToString();
        lblCemeteryCount.text = playerInfo.CemeteryCount.ToString();
        SetBuffUI();
        IsDirty = false;
    }

    void SetBuffUI()
    {
        foreach (var item in buffIcons)
        {
            item.Value.SetActive(false);
        }
        List<BattleBuffData> removeList = new List<BattleBuffData>();
        for (int i = 0; i < playerInfo.Buffs.Count; i++)
        {
            BattleBuffData buffData = playerInfo.Buffs[i];
            //if (buffData.Time == 0)
            //{
            //    removeList.Add(buffData);
            //    continue;
            //}
            GameObject buffIcon;
            if (!buffIcons.ContainsKey(buffData.BuffId))
            {
                buffIcon = Instantiate(goBuffIconTemplete, gridBuffGrid.transform);
                buffIcon.name = buffData.BuffId.ToString();
                buffIcons.Add(buffData.BuffId, buffIcon);
                buffIcon.transform.Find("icon").GetComponent<UITexture>().Load(buffData.Data.IconID);
                UIUtility.SetBuffTips(buffIcon, buffData.BuffId);
            }
            else
                buffIcon = buffIcons[buffData.BuffId];
            buffIcon.SetActive(true);
            buffIcon.transform.Find("Label").GetComponent<UILabel>().text = buffData.Layer > 1 ? buffData.Layer.ToString() : "";
            buffIcon.transform.Find("border").GetComponent<UITexture>().fillAmount = buffData.Time == -1 ? 1f : ((float)buffData.Time / (float)buffData.Data.Time);
        }
        for (int i = 0; i < removeList.Count; i++)
        {
            playerInfo.Buffs.Remove(removeList[i]);
        }
        //if (removeList.Count > 0)
        {
            gridBuffGrid.Reposition();
        }

    }


    public void AddBuff(BattleBuffData buffData)
    {
        playerInfo.Buffs.Add(buffData);
    }
    public void RemoveBuff(int buffId)
    {
        for (int i = 0; i < playerInfo.Buffs.Count; i++)
        {
            if (playerInfo.Buffs[i].BuffId == buffId)
            {
                playerInfo.Buffs.RemoveAt(i);
                break;
            }
        }
    }
    //public void SetEquipIcon(BattleEquipData equipData)
    //{
    //    BattleEquipData removedEquip = null;
    //    if (playerInfo.Equips.Count > 0 && playerInfo.Equips.Count >= BattleMgr.MAX_EQUIP_COUNT)
    //    {
    //        removedEquip = playerInfo.Equips[0];
    //        playerInfo.Equips.RemoveAt(0);
    //    }
    //    playerInfo.Equips.Add(equipData);
    //    GameObject goEquip = null;
    //    if (equipIcons.ContainsKey(equipData.EquipId))
    //    {
    //        goEquip = equipIcons[equipData.EquipId];
    //    }
    //    if (goEquip == null)
    //    {
    //        goEquip = Instantiate(goEquipTemplete);
    //    }
    //    goEquip.name = equipData.EquipId.ToString();
    //    UIUtility.SetEquipTips(goEquip, equipData.EquipId);
    //    goEquip.transform.Find("Icon").GetComponent<UITexture>().Load(equipData.Data.IconID);
    //}

}