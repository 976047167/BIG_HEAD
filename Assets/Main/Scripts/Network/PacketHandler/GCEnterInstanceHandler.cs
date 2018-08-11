//generate by code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;
using AppSettings;

public class GCEnterInstanceHandler : BasePacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Receive.GCEnterInstance;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        GCEnterInstance data = packet as GCEnterInstance;
        //处理完数据和逻辑后,发送消息通知其他模块,绝对不可以直接操作UI等Unity主线程的东西!
        if (data.Result==0)
        {
            InstanceTableSetting instanceTable = InstanceTableSettings.Get(data.InstanceId);
            if (instanceTable==null)
            {
                Debug.LogError("待进入的场景不存在!" + data.InstanceId);
                return;
            }

            SceneMgr.ChangeScene(instanceTable.SceneId);
            MapMgr.Create();
            //MapMgr.Instance.
        }
        else
        {
            Debug.LogError("进入失败!" + data.Result);
        }
    }
}
