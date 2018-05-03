using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_Reward : UIFormBase
{
    private UILabel labRewardText;
    private GameObject btnCommond;
    private UIGrid grid;
    // Use this for initialization
    void Awake()
    {
        labRewardText = transform.Find("bg/frame/labReward").GetComponent<UILabel>();
        btnCommond = transform.Find("bg/frame/btnCommond").gameObject;
        grid = transform.Find("bg/frame/scrollView/Grid").GetComponent<UIGrid>();
        UIEventListener.Get(btnCommond).onClick = exitClick;
        // OnInit(1);
    }

    // Update is called once per frame
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        int monsterId = (int)userdata;
        BattleMonsterTableSetting monster = BattleMonsterTableSettings.Get(monsterId);
        List<int> rewardList = monster.RewardIds;

        int rewardId = rewardList[Random.Range(0, rewardList.Count)];

        RewardTableSetting reward = RewardTableSettings.Get(rewardId);


        if (reward.exp != 0)
        {
            GameObject item = Instantiate(labRewardText.gameObject);
            item.GetComponent<UILabel>().text = "经验";
            item.GetComponent<UILabel>().gradientBottom = Color.green;
            item.transform.Find("labNum").GetComponent<UILabel>().text = string.Format("X{0}", reward.exp);
            item.transform.Find("labNum").GetComponent<UILabel>().gradientBottom = Color.green;
            item.transform.parent = grid.transform;
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);
        }
        if (reward.gold != 0)
        {
            GameObject item = Instantiate(labRewardText.gameObject);
            item.GetComponent<UILabel>().text = "黄金";
            item.GetComponent<UILabel>().gradientBottom = Color.yellow;
            item.transform.Find("labNum").GetComponent<UILabel>().text = string.Format("X{0}", reward.gold);
            item.transform.Find("labNum").GetComponent<UILabel>().gradientBottom = Color.yellow;
            item.transform.parent = grid.transform;
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);
        }
        if (reward.diamond != 0)
        {
            GameObject item = Instantiate(labRewardText.gameObject);
            item.GetComponent<UILabel>().text = "钻石";
            item.GetComponent<UILabel>().gradientBottom = Color.blue;
            item.transform.Find("labNum").GetComponent<UILabel>().text = string.Format("X{0}", reward.diamond);
            item.transform.Find("labNum").GetComponent<UILabel>().gradientBottom = Color.blue;
            item.transform.parent = grid.transform;
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);
        }

        foreach (var card in reward.CardList)
        {
            if (card == 0) continue;

            GameObject item = Instantiate(labRewardText.gameObject);


            BattleCardTableSetting cardData = BattleCardTableSettings.Get(card);
            if (cardData == null) continue;

            item.GetComponent<UILabel>().text = cardData.Name;
            item.transform.Find("labNum").GetComponent<UILabel>().text = "";
            Color color = new Color();
            switch (cardData.Quality)
            {
                case 0:
                    color = Color.white;
                    break;
                case 1:
                    color = Color.gray;
                    break;
                case 2:
                    color = Color.green;
                    break;
                case 3:
                    color = Color.cyan;
                    break;
                case 4:
                    color = Color.red;
                    break;
            }

            item.GetComponent<UILabel>().gradientBottom = color;
            UIEventListener.Get(item).onClick = (GameObject obj) =>
            {
                UIModule.Instance.OpenForm<WND_ShowCard>(card);
            };
            UITexture icon = item.transform.Find("texReward").GetComponent<UITexture>();
            icon.gameObject.SetActive(true);
            icon.Load(cardData.Icon);
            item.transform.parent = grid.transform;
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);
        }
        grid.repositionNow = true;
    }
    private void exitClick(GameObject obj)
    {
        Game.UI.CloseForm<UIBattleForm>();
        Game.UI.CloseForm<WND_Reward>();
    }

}
