using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_Reward : UIFormBase
{
    private UILabel labRewardText;
    private GameObject btnCommond;
    // Use this for initialization
    void Awake () {
        labRewardText = transform.Find("bg/frame/labReward").GetComponent<UILabel>();
        btnCommond = transform.Find("bg/frame/btnCommond").gameObject;
        UIEventListener.Get(btnCommond).onClick = exitClick;
    }

    // Update is called once per frame
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        int rewardId =(int) userdata;
        RewardTableSetting reward = RewardTableSettings.Get(rewardId);
        labRewardText.text = reward.Text;
    }
    private void exitClick(GameObject obj)
    {
        Destroy(gameObject);
    }
}
