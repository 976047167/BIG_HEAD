using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_Reward : UIFormBase
{
    private UILabel labRewardText;
    private GameObject btnCommond;
    private UIGrid grid;
    private int monsterId;
    private GameObject frame;

    protected override void OnInit(object userdata)
    {
        labRewardText = transform.Find("bg/frame/labReward").GetComponent<UILabel>();
        btnCommond = transform.Find("bg/frame/btnCommond").gameObject;
        frame = transform.Find("bg/frame").gameObject;
        grid = transform.Find("bg/frame/scrollView/Grid").GetComponent<UIGrid>();
        UIEventListener.Get(btnCommond).onClick = exitClick;
        base.OnInit(userdata);


        monsterId = (int)userdata;

    }

    protected override void OnOpen()
    {
        base.OnOpen();
        BattleMonsterTableSetting monster = BattleMonsterTableSettings.Get(monsterId);
        List<int> rewardList = monster.RewardIds;

        int rewardId = rewardList[Random.Range(0, rewardList.Count)];
        LoadRewardList(rewardId);
        PlayShowAnime();

    }

    private void PlayShowAnime()
    {
        frame.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        TweenScale.Begin(frame, 0.1f, Vector3.one);
    }


    private void LoadRewardList(int rewardId)
    {
        RewardTableSetting reward = RewardTableSettings.Get(rewardId);


        if (reward.exp != 0)
        {
            GameObject item = Instantiate(labRewardText.gameObject);
            item.GetComponent<UILabel>().text = I18N.Get(1002003);
            item.GetComponent<UILabel>().color = Color.green;
            //item.transform.Find("labNum").GetComponent<UILabel>().text = string.Format("X{0}", reward.exp);
            UILabel label = item.transform.Find("labNum").GetComponent<UILabel>();
            NumberJumpAnime(label, reward.exp);
            item.transform.Find("labNum").GetComponent<UILabel>().color = Color.green;
            item.transform.SetParent(grid.transform, false);
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);
        }
        if (reward.gold != 0)
        {
            GameObject item = Instantiate(labRewardText.gameObject);
            item.GetComponent<UILabel>().text = I18N.Get(1002001);
            item.GetComponent<UILabel>().color = Color.yellow;
            //item.transform.Find("labNum").GetComponent<UILabel>().text = string.Format("X{0}", reward.gold);
            UILabel label = item.transform.Find("labNum").GetComponent<UILabel>();
            NumberJumpAnime(label, reward.gold);
            item.transform.Find("labNum").GetComponent<UILabel>().color = Color.yellow;
            item.transform.SetParent(grid.transform, false);
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);
        }
        if (reward.diamond != 0)
        {
            GameObject item = Instantiate(labRewardText.gameObject);
            item.GetComponent<UILabel>().text = I18N.Get(1002002);
            item.GetComponent<UILabel>().color = Color.blue;
            //item.transform.Find("labNum").GetComponent<UILabel>().text = string.Format("X{0}", reward.diamond);
            UILabel label = item.transform.Find("labNum").GetComponent<UILabel>();
            NumberJumpAnime(label, reward.diamond);
            item.transform.Find("labNum").GetComponent<UILabel>().color = Color.blue;
            item.transform.SetParent(grid.transform, false);
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);
        }

        foreach (var card in reward.ItemList)
        {
            if (card == 0) continue;
            BattleCardTableSetting cardData = BattleCardTableSettings.Get(card);
            if (cardData == null) continue;
            GameObject item = Instantiate(labRewardText.gameObject);




            item.GetComponent<UILabel>().text = I18N.Get(cardData.Name);
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

            item.GetComponent<UILabel>().color = color;
            UIUtility.SetCardTips(item, cardData.Id);
            UITexture icon = item.transform.Find("texReward").GetComponent<UITexture>();
            icon.gameObject.SetActive(true);
            icon.Load(cardData.ShowID);
            item.transform.SetParent(grid.transform, false);
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);
        }
        grid.repositionNow = true;
    }
    private void NumberJumpAnime(UILabel label, int num)
    {

        IEnumerator ie = JumpNum(label, num);
        StartCoroutine(ie);
    }
    private IEnumerator JumpNum(UILabel label, int num)
    {
        int perNum = 20;
        if (num / 15 > perNum)
            perNum = num / 15;

        for (int i = 0; i < num; i += perNum)
        {
            yield return new WaitForSeconds(0.1f);
            label.text = i.ToString();
        }
        label.text = num.ToString();
    }

    private void exitClick(GameObject obj)
    {
        Game.UI.CloseForm<UIBattleForm>();
        Game.UI.CloseForm<WND_Reward>();
    }

}
