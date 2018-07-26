using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_Settings : UIFormBase {

    // Use this for initialization

    private UISlider sliderVoice;
    private UISlider sliderMusic;
    private UISprite spExit;
    private UISprite spVoice;
    private UISprite spMusic;
    private UIGrid IconGrid;
    private GameObject btnChangeIcon;
    private UITexture headIcon;
    private GameObject IconMaskBg;
    private GameObject IconInstence;
    private GameObject btnCommand;
    private int myIconIndex;
    private bool haveLoading; 
    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        spVoice = transform.Find("bg/frame/spVoice").GetComponent<UISprite>();
        sliderVoice = spVoice.transform.Find("sliderVoice").GetComponent<UISlider>();
        spMusic = transform.Find("bg/frame/spMusic").GetComponent<UISprite>();
        sliderMusic = spMusic.transform.Find("sliderMusic").GetComponent<UISlider>();
        spExit = transform.Find("bg/frame/spExit").GetComponent<UISprite>();
        headIcon = transform.Find("bg/frame/texHead").GetComponent<UITexture>();
        btnChangeIcon = headIcon.transform.Find("spChangeHead").gameObject;
        IconMaskBg = transform.Find("bg/iconBgMask").gameObject;
        IconGrid = IconMaskBg.transform.Find("headIconBg/ScrollView/Grid").GetComponent<UIGrid>();
        IconInstence = transform.Find("headIconInstence").gameObject;
        btnCommand = IconMaskBg.transform.Find("headIconBg/btnCommand").gameObject;

        myIconIndex = Game.DataManager.PlayerData.HeadIcon;
        UIEventListener.Get(spExit.gameObject).onClick = ExitClick;
        UIEventListener.Get(btnChangeIcon).onClick = ChangeIconClick;
        UIEventListener.Get(IconMaskBg).onClick = CanceClick;
        UIEventListener.Get(btnCommand).onClick = CommandClick;
        EventDelegate.Add(sliderMusic.onChange, MusicChange);
        EventDelegate.Add(sliderVoice.onChange, VoiceChange);
        EventDelegate.Add(IconInstence.GetComponent<UIToggle>().onChange,IconChose);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        
        headIcon.Load(myIconIndex);
    }
    private void VoiceChange()
    {

    }
    private void MusicChange()
    {

    }
    private void ExitClick(GameObject obj)
    {
        Game.UI.CloseForm<WND_Settings>();

    }
    private void ChangeIconClick(GameObject obj)
    {
        IconMaskBg.SetActive(true);
        if (!haveLoading)
        {
            haveLoading = true;
            LoadHeadIconList();
        }
    }
    private void LoadHeadIconList()
    {
        List<int> iconIndexList = new List<int>();
        foreach(TextureTableSetting texSetting in TextureTableSettings.GetAll())
        {
            if (texSetting.Id >= 10000 && texSetting.Id < 20000)
                iconIndexList.Add(texSetting.Id);
        }
         for(int i = 0; i<iconIndexList.Count; i++) {
            int iconIndex = iconIndexList[i];
            GameObject item = Instantiate(IconInstence);
            item.SetActive(true);
            item.name = iconIndex.ToString();
            item.transform.Find("Texture").GetComponent<UITexture>().Load(iconIndex);
            if (iconIndex == myIconIndex)
                item.GetComponent<UIToggle>().value = true;
            item.transform.SetParent(IconGrid.transform, false);
            item.transform.localScale = new Vector3(1, 1, 1);
            item.transform.localPosition = Vector3.zero;
        }
        IconGrid.repositionNow = true;

    }
    private void IconChose()
    {
        if (UIToggle.current.value == true)
        {
            int.TryParse(UIToggle.current.name,out myIconIndex);
        }
    }
    private void CommandClick(GameObject obj)
    {
        Game.DataManager.PlayerData.HeadIcon = myIconIndex;
        Messenger.Broadcast(MessageId.MAP_UPDATE_PLAYER_INFO);
        IconMaskBg.SetActive(false);
        headIcon.Load(myIconIndex);
    }
    private void CanceClick(GameObject obj)
    {
        IconMaskBg.SetActive(false);
    }
}
