using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_Reward : UIFormBase
{
    private UILabel labRewardText;
    private GameObject btnCommond;
    private UIGrid grid;
    protected RewardData rewardData;
    private GameObject frame;

    protected override void OnInit(object userdata)
    {
        labRewardText = transform.Find("bg/frame/labReward").GetComponent<UILabel>();
        btnCommond = transform.Find("bg/frame/btnCommond").gameObject;
        frame = transform.Find("bg/frame").gameObject;
        grid = transform.Find("bg/frame/scrollView/Grid").GetComponent<UIGrid>();
        UIEventListener.Get(btnCommond).onClick = exitClick;
        base.OnInit(userdata);


        rewardData = (RewardData)userdata;

    }

    protected override void OnOpen()
    {
        base.OnOpen();

        LoadRewardList();
        PlayShowAnime();

    }

    private void PlayShowAnime()
    {
        frame.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        TweenScale.Begin(frame, 0.1f, Vector3.one);
    }


    private void LoadRewardList()
    {
        if (rewardData == null)
        {
            Debug.LogError("没有奖励!");
            return;
        }

        if (rewardData.Exp != 0)
        {
            GameObject item = Instantiate(labRewardText.gameObject);
            item.GetComponent<UILabel>().text = I18N.Get(1002003);
            item.GetComponent<UILabel>().color = Color.green;
            //item.transform.Find("labNum").GetComponent<UILabel>().text = string.Format("X{0}", reward.exp);
            UILabel label = item.transform.Find("labNum").GetComponent<UILabel>();
            NumberJumpAnime(label, rewardData.Exp);
            item.transform.Find("labNum").GetComponent<UILabel>().color = Color.green;
            item.transform.SetParent(grid.transform, false);
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);
        }
        if (rewardData.Gold != 0)
        {
            GameObject item = Instantiate(labRewardText.gameObject);
            item.GetComponent<UILabel>().text = I18N.Get(1002001);
            item.GetComponent<UILabel>().color = Color.yellow;
            //item.transform.Find("labNum").GetComponent<UILabel>().text = string.Format("X{0}", reward.gold);
            UILabel label = item.transform.Find("labNum").GetComponent<UILabel>();
            NumberJumpAnime(label, rewardData.Gold);
            item.transform.Find("labNum").GetComponent<UILabel>().color = Color.yellow;
            item.transform.SetParent(grid.transform, false);
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);
        }
        if (rewardData.Diamond != 0)
        {
            GameObject item = Instantiate(labRewardText.gameObject);
            item.GetComponent<UILabel>().text = I18N.Get(1002002);
            item.GetComponent<UILabel>().color = Color.blue;
            //item.transform.Find("labNum").GetComponent<UILabel>().text = string.Format("X{0}", reward.diamond);
            UILabel label = item.transform.Find("labNum").GetComponent<UILabel>();
            NumberJumpAnime(label, rewardData.Diamond);
            item.transform.Find("labNum").GetComponent<UILabel>().color = Color.blue;
            item.transform.SetParent(grid.transform, false);
            item.transform.localPosition = new Vector3();
            item.transform.localScale = new Vector3(1, 1, 1);
            item.SetActive(true);
        }

        foreach (var card in rewardData.Items)
        {
            if (card == 0) continue;
            ItemTableSetting cardData = ItemTableSettings.Get(card);
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
