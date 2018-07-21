using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIAction
{
    public class UIAddEquip : UIAction
    {
        public static UIActionType ActionType { get { return UIActionType.AddEquip; } }
        public BattleEquipData EquipData { get; private set; }
        public UIAddEquip(BattleEquipData battleCardData) : base()
        {
            EquipData = battleCardData;
        }

        public override IEnumerator Excute()
        {
            yield return null;
            BattleEquipData removedEquip = null;
            PlayerInfoView playerInfoView = BattleForm.GetPlayerInfoViewByPlayer(EquipData.Owner);
            if (playerInfoView.PlayerInfo.Equips.Count > 0 && playerInfoView.PlayerInfo.Equips.Count >= BattleMgr.MAX_EQUIP_COUNT)
            {
                removedEquip = playerInfoView.PlayerInfo.Equips[0];
                playerInfoView.PlayerInfo.Equips.RemoveAt(0);
            }
            playerInfoView.PlayerInfo.Equips.Add(EquipData);
            GameObject goEquip = null;
            if (playerInfoView.EquipIcons.ContainsKey(EquipData.EquipId))
            {
                goEquip = playerInfoView.EquipIcons[EquipData.EquipId];
            }
            if (goEquip == null)
            {
                goEquip = GameObject.Instantiate(playerInfoView.goEquipTemplete);
                playerInfoView.EquipIcons.Add(EquipData.EquipId, goEquip);
                goEquip.transform.parent = playerInfoView.gridEquipGrid.transform;
                goEquip.transform.localScale = Vector3.one;

            }
            goEquip.SetActive(true);
            goEquip.name = EquipData.EquipId.ToString();
            UIUtility.SetEquipTips(goEquip, EquipData.EquipId);
            goEquip.transform.Find("Icon").GetComponent<UITexture>().Load(EquipData.Data.IconID);
            yield return null;
            playerInfoView.gridEquipGrid.Reposition();
        }
    }
}
