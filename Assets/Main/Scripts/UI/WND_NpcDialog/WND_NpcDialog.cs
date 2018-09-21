using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;
using BigHead.protocol;

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

    int dialogId = 0;
    int mapCardId = 0;
    MapCardTableSetting mapCardTable = null;
    DialogTableSetting dialogTable = null;
    List<DialogData> dialogDatas = null;
    DialogData myData = null;
    DialogData oppData = null;
    DialogData currentDialogData = null;

    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);

        btnMask = transform.Find("maskPanel/mask").gameObject;
        goOppInfo = transform.Find("Opp").gameObject;
        goMyInfo = transform.Find("Me").gameObject;
        texOppIcon = goOppInfo.transform.Find("head").GetComponent<UITexture>();
        texMyIcon = goMyInfo.transform.Find("head").GetComponent<UITexture>();
        Transform scroll = goMyInfo.transform.Find("Scroll").transform;
        lblMyContent = goMyInfo.transform.Find("Scroll/content").GetComponent<UILabel>();
        lblOppContent = goOppInfo.transform.Find("Scroll/content").GetComponent<UILabel>();
        gridSelectItems = transform.Find("selectGrid").GetComponent<UIGrid>();
        goSelects = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            goSelects[i] = gridSelectItems.transform.Find(i.ToString()).gameObject;
            goSelects[i].SetActive(false);
            UIEventListener.Get(goSelects[i]).onClick = OnClick_Selected;
        }

        int[] datas = (int[])userdata;
        dialogId = datas[0];
        mapCardId = datas[1];
        //MapCardTableSetting mapCardTable = MapCardTableSettings.Get(mapCardId);
        //if (mapCardTable == null)
        //{
        //    return;
        //}
        dialogTable = DialogTableSettings.Get(dialogId);
        if (dialogTable != null
            && dialogTable.HeadIcons.Count == dialogTable.ShowMode.Count
            && dialogTable.HeadIcons.Count == dialogTable.DialogContents.Count
            && dialogTable.HeadIcons.Count == dialogTable.DialogAction.Count
            && dialogTable.HeadIcons.Count == dialogTable.ActionParam.Count
            && dialogTable.HeadIcons.Count == dialogTable.NextIndexs.Count
            && dialogTable.HeadIcons.Count == dialogTable.ShowNames.Count
            && dialogTable.HeadIcons.Count == dialogTable.ActionParam2.Count
            )
        {
            dialogDatas = new List<DialogData>(dialogTable.HeadIcons.Count);
            for (int i = 0; i < dialogTable.HeadIcons.Count; i++)
            {
                DialogData data = new DialogData(i, dialogTable.ShowMode[i], dialogTable.HeadIcons[i], dialogTable.ShowNames[i],
                    dialogTable.DialogContents[i], dialogTable.NextIndexs[i], dialogTable.DialogAction[i],
                    dialogTable.ActionParam[i], dialogTable.ActionParam2[i]);
                dialogDatas.Add(data);
            }

        }
        else
        {
            Debug.LogError("配置表错误!");
        }

        UIEventListener.Get(btnMask).onClick = OnClick_btnMask;
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        Messenger.AddListener(MessageId.MAP_APPLY_EFFECT, OnEffectApplyed);
        StartDialog(0);
    }
    protected override void OnClose()
    {
        base.OnClose();
        Messenger.RemoveListener(MessageId.MAP_APPLY_EFFECT, OnEffectApplyed);
    }

    //    private void Start()
    //    {
    //        OnInit(1);
    //    }

    //    private void Update()
    //    {
    //        if (Input.GetKeyDown(KeyCode.A))
    //        {
    //            lblMyContent.text = "目的　[n]探索激光显微技术(laser capture microdissection system, LCM)捕获口腔上皮细胞，" +
    //                "并进行STR-DNA分型检测的方法。方法　用VERITAS显微切割仪红外低能激光显微捕获一定数量口腔上皮细胞，" +
    //                "进行Profiler Plus试剂盒STR复合扩增，检测DNA基因型。" +
    //                "结果　7~8个口腔上皮细胞能成功获得STR-DNA分型。3~4个口腔上皮细胞不能成功获得STR-DNA分型。" +
    //                "结论　激光显微捕获作为一种分离单个细胞的新技术，对于微量口腔上皮细胞的STR-DNA分型是可行的。特异性高\n" +
    //@"首次报导的PCR所用的DNA聚合酶是大肠杆菌的DNAPolymerase I的Klenow大片段，其酶活性在90℃会变性失活，需每次PCR循环都要重新加入Klenow大片段，同时引物是在37℃延伸（聚合）易产生模板—引物之间的碱基错配、致特异性较差，1988年Saiki等从温泉水中分离到的水生嗜热杆菌中提取的热稳定的Taq DNA聚合酶，在热变性处理时不被灭活，不必在每次循环扩增中再加入新酶，可以在较高温度下连续反应，显著地提高PCR产物的特异性，序列分析证明其扩增的DNA序列与原模板DNA一致。
    //扩增过程中，单核苷酸的错误参入程度很低、其错配率一般只有约万分之一，足可以提供特异性分析，选用各型病毒相对的特异寡核苷酸引物。PCR能一次确定病毒的多重感染。如用HPV11和HPV16型病毒引物检测病妇宫颈刮片细胞可以发现部分病人存在HPV11和HPV16两型的双重感染。
    //高度敏感
    //理论上PCR可以按2n倍数扩增DNA十亿倍以上，实际应用已证实可以将极微量的靶DNA成百万倍以上地扩增到足够检测分析量的DNA。能从100万个细胞中检出一个靶细胞，或对诸如病人口液等只含一个感染细胞的标本或仅含0.01pg的感染细胞的特异性片段样品中均可检测。
    //无放射性
    //一般在2小时内约可完成30次以上的循环扩增，加上用电泳分析。只需3 - 4小时便可完成，不用分离提纯病毒，DNA粗制品及总RNA均可作为反应起始物，可直接用临床标本如血液、体液、尿液、洗液、脱落毛发、细胞、活体组织等粗制的DNA的提取液来扩增检测，省去费时繁杂的提纯程序，扩增产物用一般电泳分析即可，不一定用同位素，无放射性易于推广。
    //简便
    //扩增产物可直接供作序列分析和分子克隆，摆脱繁琐的基因方法，可直接从RNA或染色体DNA中或部分DNA已降解的样品中分离目的基因，省去常规方法中须先进行克隆后再作序列分析的冗繁程序。已固定的和包埋的组织或切片亦可检测。如在PCR引物端事先构建一个内切酶位点，扩增的靶DNA可直接克隆到M13，PUC19等相应酶切位点的载体中。
    //可扩增物质
    //先按通常方法用寡脱氧胸苷引物和逆转录酶将mRNA转变成单链cDNA，再将得到的单链cDNA进行PCR扩增，即使mRNA转录片段只有100ngcDNA中的0.01 %，也能经PCR扩增1ng有242碱基对长度的特异片段，有些外显子分散在一段很长的DNA中，难以将整段DNA大分子扩增和做序列分析。若以mRNA作模板，则可将外显子集中，用PCR一次便完成对外显子的扩增并进行序列分析。";
    //            StartTypewriting(lblMyContent);
    //        }
    //        OnUpdate();
    //    }
    UILabel writingLabel = null;
    UIScrollView writingScrollView = null;
    TypewriterEffect writingEffect = null;
    float lastHeight = 0f;
    protected override void OnUpdate()
    {
        base.OnUpdate();



        if (writingLabel != null && lastHeight != writingLabel.height)
        {
            writingScrollView.ResetPosition();
            if (writingLabel.height < 180f)
            {
                writingScrollView.SetDragAmount(0f, 0f, false);
            }
            else
            {
                writingScrollView.SetDragAmount(0f, 1f, false);
            }
            lastHeight = writingLabel.height;
        }


    }

    void StartDialog(int index)
    {
        if (index < 0 || index >= dialogDatas.Count)
        {
            Game.UI.CloseForm(this);
        }
        DialogData dialogData = dialogDatas[index];
        currentDialogData = dialogData;
        switch (dialogData.ShowMode)
        {
            case 0:
                myData = dialogData;
                break;
            case 1:
                oppData = dialogData;
                break;
            default:
                break;
        }
        ShowContent(dialogData);
    }

    void StartTypewriting(UILabel label)
    {
        if (writingLabel != null)
        {
            writingEffect.Finish();
        }
        lastHeight = 0f;
        writingLabel = label;
        writingScrollView = writingLabel.transform.parent.GetComponent<UIScrollView>();
        writingEffect = writingLabel.GetComponent<TypewriterEffect>();
        writingEffect.ResetToBeginning();
        btnMask.SetActive(true);
        EventDelegate.Add(writingEffect.onFinished, OnEndTypewriting, true);
    }

    void EndTypewriting()
    {
        if (writingEffect == null)
        {
            return;
        }
        UIScrollView tempScrollView = writingScrollView;
        UILabel tempLabel = writingLabel;
        writingEffect.Finish();
        tempScrollView.ResetPosition();
        if (tempLabel.height < 180f)
            tempScrollView.SetDragAmount(0f, 0f, false);
        else
            tempScrollView.SetDragAmount(0f, 1f, false);
        btnMask.SetActive(false);
        //OnEndTypewriting();
    }
    void OnEndTypewriting()
    {
        btnMask.SetActive(false);
        writingLabel = null;
        writingScrollView = null;
        writingEffect = null;
        lastHeight = 0f;
        //下一句是需要点击才会出现的
        if (currentDialogData.Action <= 1)
        {
            btnMask.SetActive(true);

        }
        else
        {
            ApplyDialogAction(currentDialogData.Index, currentDialogData.Action, currentDialogData.ActionParam, currentDialogData.ActionParam2, currentDialogData.Next);
        }

    }
    protected void ShowContent(DialogData dialogData)
    {
        if (dialogData == null)
        {
            Debug.LogError("null");
        }
        for (int i = 0; i < goSelects.Length; i++)
        {
            goSelects[i].SetActive(false);
        }
        int action = dialogData.Action;
        switch (dialogData.ShowMode)
        {
            case 0://玩家
                if (oppData == null)
                {
                    goOppInfo.SetActive(false);
                }
                goMyInfo.SetActive(true);
                lblMyContent.text = I18N.Get(dialogData.Content);
                StartTypewriting(lblMyContent);
                texMyIcon.Load(dialogData.Head);
                break;
            case 1://怪物
                if (myData == null)
                {
                    goMyInfo.SetActive(false);
                }
                goOppInfo.SetActive(true);
                lblOppContent.text = I18N.Get(dialogData.Content);
                StartTypewriting(lblOppContent);
                texOppIcon.Load(dialogData.Head);
                break;
            case 2://Boss
                if (myData == null)
                {
                    goMyInfo.SetActive(false);
                }
                goOppInfo.SetActive(true);
                lblOppContent.text = I18N.Get(dialogData.Content);
                StartTypewriting(lblOppContent);
                texOppIcon.Load(dialogData.Head);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 0结束
    ///1下一个
    ///2选择
    ///3进入战斗
    ///4回血
    ///5回魔
    ///6回粮食
    ///7进入商店
    ///8打开宝箱
    /// </summary>
    void ApplyDialogAction(int index, int action, int param, int param2, int next)
    {
        Debug.Log(index + "  action=" + action + "[" + param + "," + param2 + "]  ->" + next);
        switch (action)
        {
            case 0://结束
                Game.UI.CloseForm(this);
                break;
            case 1://下一个2选择
                StartDialog(next);
                break;
            case 2://选择
                StartSelect(index + 1, param);
                break;
            case 9://下一层
                MapMgr.Instance.NextMapLayer();
                break;
            default:
                if (action > 2)
                {
                    CGMapApplyEffect mapApplyEffect = new CGMapApplyEffect();
                    mapApplyEffect.PlayerId = MapMgr.Instance.MyMapPlayer.Data.Id;
                    mapApplyEffect.Action = action;
                    //使用外部参数
                    if (param == -1)
                    {
                        MapCardTableSetting mapCardTable = MapCardTableSettings.Get(mapCardId);
                        if (mapCardTable == null)
                        {
                            mapApplyEffect.Param = param;
                        }
                        else
                        {
                            mapApplyEffect.Param = mapCardTable.DataId;
                        }
                    }
                    else
                        mapApplyEffect.Param = param;
                    mapApplyEffect.Param2 = param2;
                    mapApplyEffect.MapCardId = mapCardId;
                    Game.NetworkManager.SendToLobby(MessageId_Send.CGMapApplyEffect, mapApplyEffect);
                    waitingNext = next;
                }

                break;
        }
    }
    int waitingNext = -1;

    void OnEffectApplyed()
    {
        if (waitingNext >= 0)
        {
            StartDialog(waitingNext);
        }
        else
        {
            Game.UI.CloseForm(this);
        }
    }

    void StartSelect(int startIndex, int length)
    {
        if (length > 4)
        {
            Debug.LogError("数量过多!");
        }
        if (startIndex + length >= dialogDatas.Count)
        {
            Debug.LogError("超出界限!");
        }
        //List<DialogData> datas = new List<DialogData>(length);
        //for (int i = 0; i < length; i++)
        //{
        //    datas.Add(dialogDatas[startIndex + i]);
        //}
        for (int i = 0; i < length; i++)
        {
            DialogData data = dialogDatas[startIndex + i];
            GameObject go = goSelects[i];
            go.name = data.Index.ToString();
            go.SetActive(true);
            go.transform.Find("content").GetComponent<UILabel>().text = I18N.Get(data.Content);
        }
        gridSelectItems.Reposition();
    }
    void OnClick_Selected(GameObject go)
    {
        DialogData data = dialogDatas[int.Parse(go.name)];
        currentDialogData = data;
        ApplyDialogAction(data.Index, data.Action, data.ActionParam, data.ActionParam2, data.Next);
    }
    void OnClick_btnMask(GameObject go)
    {
        EndTypewriting();
        //下一句是需要点击才会出现的
        if (currentDialogData.Action <= 1)
        {
            ApplyDialogAction(currentDialogData.Index, currentDialogData.Action, currentDialogData.ActionParam, currentDialogData.ActionParam2, currentDialogData.Next);
        }

    }
}

public enum MapDialogType : int
{
    None = 0,
    Door,
    NPC,
    Shop,
    Box,
    Monster,
    Boss,
}