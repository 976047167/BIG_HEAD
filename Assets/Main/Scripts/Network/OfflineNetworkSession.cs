using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;
using System.Threading;
/// <summary>
/// 离线模式逻辑，按照联网游戏的逻辑，写他妈的一个服务器
/// </summary>
public sealed class OfflineNetworkSession : INetworkSession
{
    string name = "";
    NetState netState = NetState.None;
    Thread excuteThread;
    Queue<needHandleMessage> waitHandleMessage = new Queue<needHandleMessage>();
    public OfflineNetworkSession(string name)
    {
        this.name = name;
        excuteThread = new Thread(HandleMessageThreadMethod);
        BaseServerPacketHandler.Save_Data_Path = Application.persistentDataPath;
    }

    public void Connect(string ip, int port)
    {
        netState = NetState.Connected;
        Messenger.BroadcastAsync(MessageId.NetworkConnect, name);
        excuteThread.Start();
    }

    public void Send(MessageId_Send msgId, IMessage msg)
    {
        Debug.Log("Send -> " + msgId.ToString());
        DicServerHandler.Dic[(ushort)msgId].Handle(this, msg);
    }
    public void OnMessage(ushort msgId, IMessage data)
    {
        waitHandleMessage.Enqueue(new needHandleMessage(msgId, data));

    }
    void HandleMessageThreadMethod()
    {
        System.Random random = new System.Random();
        while (true)
        {
            while (waitHandleMessage.Count > 0)
            {
                needHandleMessage message = waitHandleMessage.Dequeue();
                new Thread(() =>
                {

                    Thread.Sleep(random.Next(1, 50));
                    DicHandler.Dic[(ushort)message.msgId].Handle(this, message.data);
                }).Start();

            }
            Thread.Sleep(50);
        }
    }
    public void Close()
    {
        excuteThread.Abort();
    }

    class needHandleMessage
    {
        public ushort msgId { get; private set; }
        public IMessage data { get; private set; }
        public needHandleMessage(ushort msgId, IMessage msg)
        {
            this.msgId = msgId;
            this.data = msg;
        }
    }
}
