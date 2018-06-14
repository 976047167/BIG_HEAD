using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract partial class UIAction
{
    protected UIBattleForm BattleForm;
    /// <summary>
    /// 多个ui表现
    /// </summary>
    public List<UIAction> BindActionList { get; protected set; }

    
    public UIAction()
    {
        this.BattleForm = Game.BattleManager.BattleForm;
    }
    public void AddBindUIAction(UIAction childUIAction)
    {
        if (BindActionList == null)
        {
            BindActionList = new List<UIAction>(1);
        }
        BindActionList.Add(childUIAction);
    }


    public abstract IEnumerator Excute();
   
}