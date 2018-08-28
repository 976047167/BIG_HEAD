using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
using DG.Tweening;

public class WND_Reward : UIFormBase
{
    private UILabel labRewardText;
    private GameObject btnCommond;
    private UIGrid gridItems;
    protected RewardData rewardData;
    private GameObject frame;

    GameObject goExp;
    UILabel lblLevel;
    UILabel lblExp;
    UISlider sliderExp;
    UIGrid gridGold;
    GameObject goGold;
    GameObject goDiamond;
    GameObject goFood;
    UILabel lblGold;
    UILabel lblDiamond;
    UILabel lblFood;



    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        labRewardText = transform.Find("frame/labReward").GetComponent<UILabel>();
        btnCommond = transform.Find("frame/btnCommond").gameObject;
        frame = transform.Find("frame").gameObject;
        gridItems = transform.Find("frame/scrollView/Grid").GetComponent<UIGrid>();

        goExp = transform.Find("frame/exp").gameObject;
        lblLevel = transform.Find("frame/exp/level").GetComponent<UILabel>();
        lblExp = transform.Find("frame/exp/exp").GetComponent<UILabel>();
        sliderExp = transform.Find("frame/exp").GetComponent<UISlider>();
        goGold = transform.Find("frame/grid/gold").gameObject;
        goDiamond = transform.Find("frame/grid/diamond").gameObject;
        goFood = transform.Find("frame/grid/food").gameObject;
        lblGold = transform.Find("frame/grid/gold/Label").GetComponent<UILabel>();
        lblDiamond = transform.Find("frame/grid/diamond/Label").GetComponent<UILabel>();
        lblFood = transform.Find("frame/grid/food/Label").GetComponent<UILabel>();
        gridGold = transform.Find("frame/grid").GetComponent<UIGrid>();

        UIEventListener.Get(btnCommond).onClick = exitClick;

        rewardData = (RewardData)userdata;
        PlayShowAnime();
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        //PlayShowAnime();
        if (rewardData == null)
        {
            Debug.LogError("没有奖励!");
            return;
        }
        LoadRewardList();


    }

    private void PlayShowAnime()
    {
        //frame.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        //TweenScale.Begin(frame, 0.1f, Vector3.one);
        DOTween.To(() => Vector3.zero, (v3) => frame.transform.localScale = v3, Vector3.one, 0.1f);
    }


    private void LoadRewardList()
    {
        if (rewardData.AddedExp > 0)
        {
            goExp.SetActive(true);

            int maxExp = 0;
            int level = rewardData.OldLevel;
            int exp = rewardData.OldExp + rewardData.AddedExp;
            LevelTableSetting levelTable = LevelTableSettings.Get(level);
            if (levelTable == null)
            {
                return;
            }
            maxExp = levelTable.Exp[(int)MapMgr.Instance.MyMapPlayer.Data.ClassData.Type];
            while (exp >= maxExp)
            {
                //level Up!
                level++;
                exp = exp - maxExp;
                levelTable = LevelTableSettings.Get(level);
                if (levelTable == null)
                {
                    break;
                }
                maxExp = levelTable.Exp[(int)MapMgr.Instance.MyMapPlayer.Data.ClassData.Type];
            }
            lblLevel.text = level.ToString();
            lblExp.text = exp + " / " + maxExp;
            sliderExp.value = Mathf.Max(0.001f, (float)exp / (float)maxExp);
        }
        else
        {
            goExp.SetActive(false);
        }

        if (rewardData.Gold != 0)
        {
            goGold.SetActive(true);
            lblGold.text = rewardData.Gold.ToString();
        }
        else
        {
            goGold.SetActive(false);
        }

        if (rewardData.Diamond != 0)
        {
            goDiamond.SetActive(true);
            lblDiamond.text = rewardData.Diamond.ToString();
        }
        else
        {
            goDiamond.SetActive(false);
        }

        if (rewardData.Food != 0)
        {
            goFood.SetActive(true);
            lblFood.text = rewardData.Food.ToString();
        }
        else
        {
            goFood.SetActive(false);
        }
        gridGold.Reposition();

        foreach (var card in rewardData.Items)
        {
            if (card == 0) continue;
            UIUtility.GetNormalCard(gridItems.transform, card, 1);
        }
        gridItems.repositionNow = true;
    }
    private void NumberJumpAnime(UILabel label, int num)
    {

        StartCoroutine(JumpNum(label, num));
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
        //Game.UI.CloseForm<UIBattleForm>();
        Game.UI.CloseForm<WND_Reward>();
    }

    protected override void OnClose()
    {
        base.OnClose();
        if (Game.BattleManager.State!= BattleMgr.BattleState.None)
        {
            
            Game.UI.CloseForm<UIBattleForm>();
        }
        
    }
}
