using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppSettings;

/// <summary>
/// 一条对话的数据
/// </summary>
public class DialogData
{
    int index;
    int showMode;
    int head;
    int name;
    int content;
    int next;
    int action;
    int actionParam;
    int actionParam2;

    public int Head
    {
        get
        {
            return head;
        }
    }

    public int ShowMode
    {
        get
        {
            return showMode;
        }
    }

    public int Content
    {
        get
        {
            return content;
        }
    }

    public int Action
    {
        get
        {
            return action;
        }
    }

    public int ActionParam
    {
        get
        {
            return actionParam;
        }
    }

    public int Index
    {
        get
        {
            return index;
        }
    }

    public int Next
    {
        get
        {
            return next;
        }
    }

    public int Name
    {
        get
        {
            return name;
        }
    }

    public int ActionParam2
    {
        get
        {
            return actionParam2;
        }
    }

    public DialogData(int index, int showMode, int head, int name, int content, int next, int action, int actionParam, int actionParam2)
    {
        this.index = index;
        this.head = head;
        this.name = name;
        this.showMode = showMode;
        this.content = content;
        this.next = next;
        this.action = action;
        this.actionParam = actionParam;
        this.actionParam2 = actionParam2;
    }

    public override string ToString()
    {
        return index + "[" + I18N.Get(content) + "]  action=" + action + "[" + actionParam + "," + actionParam2 + "]  ->" + next;
    }
}
