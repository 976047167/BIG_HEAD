/*
 * 网络消息兼容
 */
#define MESSAGE_ID



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public partial class Messenger
{
#if MESSAGE_ID
    //No parameters
    static public void AddListener(MessageId messageID, Callback handler)
    {
        uint eventType = (uint)messageID;
        OnListenerAdding(eventType, handler);
        m_eventListenTable[eventType] = (Callback)m_eventListenTable[eventType] + handler;
    }

    //Single parameter
    static public void AddListener<T>(MessageId messageID, Callback<T> handler)
    {
        uint eventType = (uint)messageID;
        OnListenerAdding(eventType, handler);
        m_eventListenTable[eventType] = (Callback<T>)m_eventListenTable[eventType] + handler;
    }

    //Two parameters
    static public void AddListener<T, U>(MessageId messageID, Callback<T, U> handler)
    {
        uint eventType = (uint)messageID;
        OnListenerAdding(eventType, handler);
        m_eventListenTable[eventType] = (Callback<T, U>)m_eventListenTable[eventType] + handler;
    }

    //Three parameters
    static public void AddListener<T, U, V>(MessageId messageID, Callback<T, U, V> handler)
    {
        uint eventType = (uint)messageID;
        OnListenerAdding(eventType, handler);
        m_eventListenTable[eventType] = (Callback<T, U, V>)m_eventListenTable[eventType] + handler;
    }
    //No parameters
    static public void RemoveListener(MessageId messageID, Callback handler)
    {
        uint eventType = (uint)messageID;
        if (OnListenerRemoving(eventType, handler))
        {
            m_eventListenTable[eventType] = (Callback)m_eventListenTable[eventType] - handler;
            if (m_eventListenTable[eventType] == null)
            {
                Remove(eventType);
            }
        }
    }

    //Single parameter
    static public void RemoveListener<T>(MessageId messageID, Callback<T> handler)
    {
        uint eventType = (uint)messageID;
        if (OnListenerRemoving(eventType, handler))
        {
            m_eventListenTable[eventType] = (Callback<T>)m_eventListenTable[eventType] - handler;
            if (m_eventListenTable[eventType] == null)
            {
                Remove(eventType);
            }
        }
    }

    //Two parameters
    static public void RemoveListener<T, U>(MessageId messageID, Callback<T, U> handler)
    {
        uint eventType = (uint)messageID;
        if (OnListenerRemoving(eventType, handler))
        {
            m_eventListenTable[eventType] = (Callback<T, U>)m_eventListenTable[eventType] - handler;
            if (m_eventListenTable[eventType] == null)
            {
                Remove(eventType);
            }
        }
    }

    //Three parameters
    static public void RemoveListener<T, U, V>(MessageId messageID, Callback<T, U, V> handler)
    {
        uint eventType = (uint)messageID;
        if (OnListenerRemoving(eventType, handler))
        {
            m_eventListenTable[eventType] = (Callback<T, U, V>)m_eventListenTable[eventType] - handler;
            if (m_eventListenTable[eventType] == null)
            {
                Remove(eventType);
            }
        }
    }
    /// <summary>
    /// default is Async
    /// </summary>
    static public void Broadcast(MessageId eventType)
    {
        DoBroadcast((uint)eventType, true);
    }
    /// <summary>
    /// 异步
    /// </summary>
    static public void BroadcastAsync(MessageId eventType)
    {
        DoBroadcast((uint)eventType, true);
    }
    /// <summary>
    /// 同步
    /// </summary>
    static public void BroadcastSync(MessageId eventType)
    {
        DoBroadcast((uint)eventType, false);
    }
    //Single parameter
    /// <summary>
    /// default is Async
    /// </summary>
    static public void Broadcast<T>(MessageId eventType, T arg1)
    {
        DoBroadcast<T>((uint)eventType, arg1, true);
    }
    /// <summary>
    /// 异步
    /// </summary>
    static public void BroadcastAsync<T>(MessageId eventType, T arg1)
    {
        DoBroadcast<T>((uint)eventType, arg1, true);
    }
    /// <summary>
    /// 同步
    /// </summary>
    static public void BroadcastSync<T>(MessageId eventType, T arg1)
    {
        DoBroadcast<T>((uint)eventType, arg1, false);
    }
    //Two parameters
    /// <summary>
    /// default is Async
    /// </summary>
    static public void Broadcast<T, U>(MessageId eventType, T arg1, U arg2)
    {
        DoBroadcast<T, U>((uint)eventType, arg1, arg2, true);
    }
    /// <summary>
    /// 异步
    /// </summary>
    static public void BroadcastAsync<T, U>(MessageId eventType, T arg1, U arg2)
    {
        DoBroadcast<T, U>((uint)eventType, arg1, arg2, true);
    }
    /// <summary>
    /// 同步
    /// </summary>
    static public void BroadcastSync<T, U>(MessageId eventType, T arg1, U arg2)
    {
        DoBroadcast<T, U>((uint)eventType, arg1, arg2, false);
    }
    //Three parameters
    /// <summary>
    /// default is Async
    /// </summary>
    static public void Broadcast<T, U, V>(MessageId eventType, T arg1, U arg2, V arg3)
    {
        DoBroadcast<T, U, V>((uint)eventType, arg1, arg2, arg3, true);
    }
    /// <summary>
    /// 异步
    /// </summary>
    static public void BroadcastAsync<T, U, V>(MessageId eventType, T arg1, U arg2, V arg3)
    {
        DoBroadcast<T, U, V>((uint)eventType, arg1, arg2, arg3, true);
    }
    /// <summary>
    /// 同步
    /// </summary>
    static public void BroadcastSync<T, U, V>(MessageId eventType, T arg1, U arg2, V arg3)
    {
        DoBroadcast<T, U, V>((uint)eventType, arg1, arg2, arg3, false);
    }
#endif
}
