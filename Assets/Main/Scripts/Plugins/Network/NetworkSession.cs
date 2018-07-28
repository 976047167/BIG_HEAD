using cocosocket4unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KEngine;
using System;
using Google.Protobuf;
using System.Net;
using BigHead.protocol;
using CodedOutputStream = Google.Protobuf.CodedOutputStream;

namespace BigHead.Net
{
    /// <summary>
    /// 负责网络模块的顶层操作，如连接、断开、重连及在UI线程内分发回调
    /// </summary>
    public class NetworkSession : INetworkSession
    {
        protected string channelName;
        private USocket socket;
        private Protocol protocol;
        private USocketSender sender;
        private USocketListener listener;
        private NetState netState = NetState.None;

        public NetworkSession(string channelName)
        {
            this.channelName = channelName;
            this.Init();
        }
        public NetState State { get { return netState; } }
        void Init()
        {
            protocol = new LVProtocol();
            listener = new USocketListener(OnOpen, OnClose, OnMessage);
            socket = new USocket(listener, protocol);
            sender = new USocketSender(socket);
            netState = NetState.Inited;
        }
        public void Connect(string ip, int port)
        {
            IPAddress address;
            if (!IPAddress.TryParse(ip, out address))
            {
                Log.Error("ip address is invalid!");
                return;
            }
            socket.Connect(ip, port, address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6);
            netState = NetState.Connecting;
        }

        public void Send(MessageId_Send msgId, IMessage msg)
        {
            sender.Send(msgId, msg);
        }

        void OnOpen()
        {
            netState = NetState.Connected;
            Messenger.BroadcastAsync(MessageId.NetworkConnect);
        }
        void OnClose()
        {
            netState = NetState.Closed;
            if (socket != null)
            {
                socket.Close();
            }
            Messenger.BroadcastAsync(MessageId.NetworkDisconnect);
        }
        void OnMessage(ushort msgId, IMessage data)
        {
            DicHandler.Dic[msgId].Handle(this, data);
        }
        public void Reset()
        {
            if (socket != null)
            {
                socket.Close();
            }
            socket = new USocket(listener, protocol);
            sender = new USocketSender(socket);
            netState = NetState.Inited;
        }
        public void Close()
        {
            if (socket != null)
            {
                socket.Close();
            }
            socket = null;
            sender = null;
            netState = NetState.Closed;
        }
        public enum NetState
        {
            None = 0,
            Inited,
            Connecting,
            Connected,
            Closed,
        }
    }
    /// <summary>
    /// 负责发送消息
    /// </summary>
    internal class USocketSender
    {
        private USocket socket;
        public USocketSender(USocket socket)
        {
            this.socket = socket;
        }
        public void Send(MessageId_Send msgId, IMessage msg)
        {
            //if (MessageMap.GetMsgType(msgId) != msg.GetType())
            //{
            //    Log.Error("消息号和数据不匹配！");
            //    return;
            //}
            byte[] data = msg.ToByteArray();
            Frame frame = new Frame(data.Length + 4);
            frame.PutShort((short)(data.Length + 2));//写入数据长度
            frame.PutShort((short)msgId);//写入协议号
            frame.PutBytes(data);//写入数据
            socket.Send(frame);
        }
    }
    /// <summary>
    /// 负责转发接收的消息,处理这几个事件
    /// </summary>
    internal class USocketListener : SocketListener
    {
        Action onOpen, onClose = null;
        Action<ushort, IMessage> onMessage = null;
        public USocketListener(Action onOpen, Action onClose, Action<ushort, IMessage> onMessage)
        {
            this.onOpen = onOpen;
            this.onClose = onClose;
            this.onMessage = onMessage;
        }
        public override void OnClose(USocket us, bool fromRemote)
        {
            Log.Error("connection is closed, fromRemote=" + fromRemote);
            if (onClose != null)
                onClose();
        }

        public override void OnError(USocket us, string err)
        {
            Log.Error("connection occured an error, err=" + err);
        }

        public override void OnIdle(USocket us)
        {

        }

        public override void OnMessage(USocket us, ByteBuf bb)
        {

            int len = bb.ReadShort();

            if (bb.ReadableBytes() != len)
            {
                Log.Error("数据长度不对!");
                return;
            }

            ushort msgId = (ushort)bb.ReadShort();
            byte[] data = bb.ReadBytes(len - 2);
            IMessage message = DicParser.Dic[msgId].ParseFrom(data);
            if (onMessage != null)
                onMessage(msgId, message);
        }

        public override void OnOpen(USocket us)
        {
            if (onOpen != null)
                onOpen();
        }
    }
}