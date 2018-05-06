using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WND_Settings : UIFormBase {

    // Use this for initialization

    private UISlider sliderVoice;
    private UISlider sliderMusic;
    private UISprite spExit;
    private UISprite spVoice;
    private UISprite spMusic;
    private void Awake()
    {
        spVoice = transform.Find("bg/frame/spVoice").GetComponent<UISprite>();
        sliderVoice = spVoice.transform.Find("sliderVoice").GetComponent<UISlider>();
        spMusic = transform.Find("bg/frame/spMusic").GetComponent<UISprite>();
        sliderMusic = spVoice.transform.Find("sliderMusic").GetComponent<UISlider>();
        spExit = transform.Find("bg/frame/spExit").GetComponent<UISprite>();

        UIEventListener.Get(spExit.gameObject).onClick = exitClick;
        EventDelegate.Add(sliderMusic.onChange, MusicChange);
        EventDelegate.Add(sliderVoice.onChange, VoiceChange);
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void VoiceChange()
    {

    }
    private void MusicChange()
    {

    }
    private void exitClick(GameObject obj)
    {
        UIModule.Instance.CloseForm<WND_Settings>();

    }
}
