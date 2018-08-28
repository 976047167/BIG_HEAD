/*
 * Advanced C# messenger by Ilya Suzdalnitski. V1.0
 * 
 * Based on Rod Hyde's "CSharpMessenger" and Magnus Wolffelt's "CSharpMessenger Extended".
 * 
 * Features:
 	* Prevents a MissingReferenceException because of a reference to a destroyed message handler.
 	* Option to log all messages
 	* Extensive error detection, preventing silent bugs
 * 
 * Usage examples:
 	1. Messenger.AddListener<GameObject>("prop collected", PropCollected);
 	   Messenger.Broadcast<GameObject>("prop collected", prop);
 	2. Messenger.AddListener<float>("speed changed", SpeedChanged);
 	   Messenger.Broadcast<float>("speed changed", 0.5f);
 * 
 * Messenger cleans up its evenTable automatically upon loading of a new level.
 * 
 * Don't forget that the messages that should survive the cleanup, should be marked with Messenger.MarkAsPermanent(string)
 * 
 */

using System;
using System.Collections.Generic;

public class Lua2csMessenger
{
    public delegate void MsgCallback1(object msg);
    public delegate void MsgCallback2(object arg0, object arg1);
    public delegate void MsgCallback3(object arg0, object arg1, object arg2);
    static private Lua2csMessenger m_Instance = null;
    static public Lua2csMessenger Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new Lua2csMessenger();
            }
            return m_Instance;
        }
        set { m_Instance = value; }

    }
    /// <summary> default is Async </summary>
    public void Broadcast(uint eventType)
    {
        Messenger.Broadcast(eventType);
    }
    /// <summary> default is Async </summary>
    public void Broadcast(uint eventType, Object para1)
    {
        Messenger.Broadcast<Object>(eventType, para1);
    }
    /// <summary> default is Async </summary>
    public void Broadcast(uint eventType, Object para1, Object para2)
    {
        Messenger.Broadcast<Object, Object>(eventType, para1, para2);
    }
    /// <summary> default is Async </summary>
    public void Broadcast(uint eventType, Object para1, Object para2, Object para3)
    {
        Messenger.Broadcast<Object, Object, Object>(eventType, para1, para2, para3);
    }
    public void BroadcastSync(uint eventType)
    {
        Messenger.BroadcastSync(eventType);
    }

    public void BroadcastSync(uint eventType, Object para1)
    {
        Messenger.BroadcastSync<Object>(eventType, para1);
    }

    public void BroadcastSync(uint eventType, Object para1, Object para2)
    {
        Messenger.BroadcastSync<Object, Object>(eventType, para1, para2);
    }

    public void BroadcastSync(uint eventType, Object para1, Object para2, Object para3)
    {
        Messenger.BroadcastSync<Object, Object, Object>(eventType, para1, para2, para3);
    }

    public void BroadcastAsync(uint eventType)
    {
        Messenger.BroadcastAsync(eventType);
    }

    public void BroadcastAsync(uint eventType, Object para1)
    {
        Messenger.BroadcastAsync<Object>(eventType, para1);
    }

    public void BroadcastAsync(uint eventType, System.Object para1, System.Object para2)
    {
        Messenger.BroadcastAsync<Object, Object>(eventType, para1, para2);
    }

    public void BroadcastAsync(uint eventType, System.Object para1, System.Object para2, System.Object para3)
    {
        Messenger.BroadcastAsync<Object, Object, Object>(eventType, para1, para2, para3);
    }
    public void AddListener(uint eventType, Callback callback)
    {
        Messenger.AddListener(eventType, callback);
    }
    public void AddListener1(uint eventType, MsgCallback1 callback)
    {
        Callback<object> call = (obj) =>
        {
            if (callback != null)
            {
                callback(obj);
            }
        };
        Messenger.AddListener<object>(eventType, call);
    }
    public void AddListener2(uint eventType, MsgCallback2 callback)
    {
        Callback<object, object> call = (arg0, arg1) =>
         {
             if (callback != null)
             {
                 callback(arg0, arg1);
             }
         };
        Messenger.AddListener<object, object>(eventType, call);
    }
    public void AddListener3(uint eventType, MsgCallback3 callback)
    {
        Callback<object, object, object> call = (arg0, arg1, arg2) =>
         {
             if (callback != null)
             {
                 callback(arg0, arg1, arg2);
             }
         };
        Messenger.AddListener<object, object, object>(eventType, call);
    }
}
