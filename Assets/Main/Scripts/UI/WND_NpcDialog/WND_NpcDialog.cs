using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

public class WND_NpcDialog : UIFormBase
{

    GameObject btnMask;
    GameObject goOppInfo;
    GameObject goMyInfo;
    UITexture texOppIcon;
    UITexture texMyIcon;
    UILabel lblOppContent;
    UILabel lblMyContent;
    UIGrid gridSelectItems;
    GameObject[] goSelects;

    int npcId = 0;
    NpcTableSetting npcTable = null;

    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);

        btnMask = transform.Find("mask").gameObject;
        goOppInfo = transform.Find("Opp").gameObject;
        goMyInfo = transform.Find("Me").gameObject;
        texOppIcon = goOppInfo.transform.Find("head").GetComponent<UITexture>();
        texMyIcon = goMyInfo.transform.Find("head").GetComponent<UITexture>();
        lblMyContent = goMyInfo.transform.Find("content").GetComponent<UILabel>();
        lblOppContent = goOppInfo.transform.Find("content").GetComponent<UILabel>();
        gridSelectItems = transform.Find("selectGrid").GetComponent<UIGrid>();
        goSelects = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            goSelects[i] = gridSelectItems.transform.Find(i.ToString()).gameObject;
        }

        npcId = (int)userdata;
        npcTable = NpcTableSettings.Get(npcId);

    }

    protected override void OnOpen()
    {
        base.OnOpen();

    }
    protected override void OnClose()
    {
        base.OnClose();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

    }



}
