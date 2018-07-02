using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using AppSettings;

public class WND_CreateCharacter : UIFormBase
{
    private GameObject mClassItemTemplate;
    private UIGrid mGridClassType;


    Dictionary<string, GameObject> dicClassesGo = new Dictionary<string, GameObject>();
    string currentSelect = "";

    protected override void OnClose()
    {
        base.OnClose();
    }

    protected override void OnHide()
    {
        base.OnHide();
    }

    protected override void OnInit(object userdata)
    {
        base.OnInit(userdata);
        mClassItemTemplate = transform.Find("Grid/Template").gameObject;
        mGridClassType = transform.Find("Grid").GetComponent<UIGrid>();
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        string[] classes = Enum.GetNames(typeof(ClassType));
        for (int i = 0; i < classes.Length; i++)
        {
            if (classes[i] == "None")
            {
                continue;
            }
            GameObject go = Instantiate(mClassItemTemplate) as GameObject;
            go.name = classes[i];
            go.transform.parent = mGridClassType.transform;
            go.transform.localScale = Vector3.one;
            go.SetActive(true);
            ClassTableSetting classData = ClassTableSettings.Get((int)Enum.Parse(typeof(ClassType), classes[i]));
            go.transform.Find("icon").GetComponent<UITexture>().Load(classData.Image);
            dicClassesGo.Add(classes[i], go);
            UIEventListener.Get(go).onClick = OnClick_ClassItem;
        }
        mGridClassType.Reposition();
    }

    protected override void OnShow()
    {
        base.OnShow();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }


    private void OnClick_ClassItem(GameObject go)
    {
        //Debug.LogError(go.name);

        if (currentSelect != go.name)
        {
            Transform icon = go.transform.Find("icon");
            DOTween.To(() => icon.localPosition, (v) => icon.localPosition = v, new Vector3(0f, -200f, 0f), 0.3f);
            DOTween.To(() => icon.localScale, (v) => icon.localScale = v, new Vector3(1.4f, 1.4f, 1.4f), 0.3f);
            if (!string.IsNullOrEmpty(currentSelect))
            {
                Transform lastIcon = dicClassesGo[currentSelect].transform.Find("icon");
                DOTween.To(() => lastIcon.localPosition, (v) => lastIcon.localPosition = v, new Vector3(0f, 0f, 0f), 0.3f);
                DOTween.To(() => lastIcon.localScale, (v) => lastIcon.localScale = v, new Vector3(1f, 1f, 1f), 0.3f);
            }
            currentSelect = go.name;
        }
    }
}
