using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_Reward : UIFormBase
{
    private UILabel labRewardText;
	// Use this for initialization
	void Awake () {
        labRewardText = transform.Find("bg/frame/labReward").GetComponent<UILabel>();

    }

    // Update is called once per frame
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        int rewardId =(int) userdata;
        RewardTableSetting reward = RewardTableSettings.Get(rewardId);
        labRewardText.text = reward.Text;
    }
}
