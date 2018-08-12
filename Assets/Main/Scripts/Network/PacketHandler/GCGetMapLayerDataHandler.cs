//generate by code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class GCGetMapLayerDataHandler : BasePacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Receive.GCGetMapLayerData;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        GCGetMapLayerData data = packet as GCGetMapLayerData;
        //处理完数据和逻辑后,发送消息通知其他模块,绝对不可以直接操作UI等Unity主线程的东西!
        if (data.Result == 0)
        {
            if (MapMgr.Inited)
            {
                MapMgr.Instance.MakeMapByLayerData(data.LayerData);
            }
            Messenger.Broadcast(MessageId.GAME_GET_MAP_LAYER_DATA);
        }

    }
}
