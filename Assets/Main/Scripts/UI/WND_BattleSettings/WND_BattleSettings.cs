using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_BattleSettings : UIFormBase {

    // Use this for initialization

    private UISlider sliderVoice;
    private UISlider sliderMusic;
    private GameObject spExit;
    private UISprite spVoice;
    private UISprite spMusic;
    private GameObject btnChangeIcon;
    private UITexture headIcon;
    private GameObject btnGiveUp;
    private GameObject btnConfim;
    private int myIconIndex;
    private UIToggle Fast;
    private UIToggle Mid;
    private UIToggle Slow;
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        spVoice = transform.Find("bg/frame/spVoice").GetComponent<UISprite>();
        sliderVoice = spVoice.transform.Find("sliderVoice").GetComponent<UISlider>();
        spMusic = transform.Find("bg/frame/spMusic").GetComponent<UISprite>();
        sliderMusic = spMusic.transform.Find("sliderMusic").GetComponent<UISlider>();
        spExit = transform.Find("bg/frame/spExit").gameObject;
        headIcon = transform.Find("bg/frame/texHead").GetComponent<UITexture>();
        btnGiveUp = transform.Find("bg/frame/btnGiveUp").gameObject;
        btnConfim = transform.Find("bg/frame/btnConfim").gameObject;
        Fast = transform.Find("bg/frame/spDialogSpeed/toggleFast").GetComponent<UIToggle>();
        Mid = transform.Find("bg/frame/spDialogSpeed/toggleMid").GetComponent<UIToggle>();
        Slow = transform.Find("bg/frame/spDialogSpeed/toggleSlow").GetComponent<UIToggle>();


        UIEventListener.Get(btnGiveUp).onClick = GiveUpClick;

         UIEventListener.Get(spExit.gameObject).onClick = ExitClick;
        UIEventListener.Get(btnConfim).onClick = ExitClick;
        EventDelegate.Add(sliderMusic.onChange, MusicChange);
        EventDelegate.Add(sliderVoice.onChange, VoiceChange);
        EventDelegate.Add(Fast.onChange, FastChange);
        EventDelegate.Add(Mid.onChange, MidChange);
        EventDelegate.Add(Slow.onChange, SlowChange);

    }

    protected override void OnOpen()
    {
        base.OnOpen();
        
        headIcon.Load(myIconIndex);
        if (Game.DataManager.DialogSpeed == 0.1f)
            Fast.value = true;
        else if (Game.DataManager.DialogSpeed == 0.5f)
            Mid.value = true;
        else if(Game.DataManager.DialogSpeed == 1.0f)
            Slow.value = true;


    }
    private void VoiceChange()
    {

    }
    private void MusicChange()
    {

    }
    private void ExitClick(GameObject obj)
    {
        Game.UI.CloseForm<WND_BattleSettings>();

    }
    private void GiveUpClick(GameObject obj)
    {
        Game.DataManager.MyPlayer.Data.HP = Game.DataManager.MyPlayer.Data.MaxHP ;
        Game.DataManager.MyPlayer.Data.MP = Game.DataManager.MyPlayer.Data.MaxMP;
        SceneMgr.ChangeScene(2);
    }

    private void FastChange()
    {
        if(UIToggle.current.value == true)
        {
            Game.DataManager.DialogSpeed = 0.1f;
        }

    }
    private void MidChange()
    {
        if (UIToggle.current.value == true)
        {
            Game.DataManager.DialogSpeed = 0.5f;
        }

    }
    private void SlowChange()
    {
        if (UIToggle.current.value == true)
        {
            Game.DataManager.DialogSpeed =1.0f;
        }

    }
}
