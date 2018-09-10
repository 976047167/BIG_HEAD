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
    List<DialogData> dialogDatas = null;
    DialogData lastData = null;
    DialogData currentData = null;

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
        if (npcTable != null
            && npcTable.HeadIcons.Count == npcTable.ShowMode.Count
            && npcTable.HeadIcons.Count == npcTable.DialogContents.Count
            && npcTable.HeadIcons.Count == npcTable.DialogAction.Count
            && npcTable.HeadIcons.Count == npcTable.ActionParam.Count)
        {
            dialogDatas = new List<DialogData>(npcTable.HeadIcons.Count);
            for (int i = 0; i < npcTable.HeadIcons.Count; i++)
            {
                DialogData data = new DialogData(i, npcTable.HeadIcons[i], npcTable.ShowMode[i], npcTable.DialogContents[i], npcTable.DialogAction[i], npcTable.ActionParam[i]);
                dialogDatas.Add(data);
            }
        }
        else
        {
            Debug.LogError("配置表错误!");
        }
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

    protected void ShowContent()
    {
        if (currentData == null)
        {
            currentData = dialogDatas[0];
        }
        int action = currentData.Action;
        switch (currentData.ShowMode)
        {
            case 0://玩家
                if (lastData == null)
                {
                    goOppInfo.SetActive(false);
                }
                goMyInfo.SetActive(true);
                lblMyContent.text = I18N.Get(currentData.Content);
                texMyIcon.Load(currentData.Head);
                break;
            case 1://怪物
                if (lastData == null)
                {
                    goMyInfo.SetActive(false);
                }
                goOppInfo.SetActive(true);
                lblOppContent.text = I18N.Get(currentData.Content);
                texOppIcon.Load(currentData.Head);
                break;
            case 2://Boss
                if (lastData == null)
                {
                    goMyInfo.SetActive(false);
                }
                goOppInfo.SetActive(true);
                lblOppContent.text = I18N.Get(currentData.Content);
                texOppIcon.Load(currentData.Head);
                break;
            default:
                break;
        }
        switch (action)
        {
            case 0://结束
                Game.UI.CloseForm(this);
                break;
            case 1:

                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

}
