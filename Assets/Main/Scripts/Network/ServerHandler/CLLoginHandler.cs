//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;
using UnityEngine;

public class CLLoginHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CLLogin;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CLLogin data = packet as CLLogin;
        //处理完数据和逻辑后,发送消息通知客户端
        LCLogin login = new LCLogin();
        PBAccountData read = GetSavedData<PBAccountData>(ACCOUNT_DATA_KEY);
        if (read != null)
        {
            if (read.Uid != data.UserId)
            {
                //新玩家
                login.Result = 0;
                CreateAccount(data.UserId);
            }
            else
            {
                if (GetSavedData<PBPlayerData>(PLAYER_DATA_KEY) == null || GetSavedData<PBPlayerDetailData>(PLAYER_DETAIL_DATA) == null)
                {
                    //新玩家，没有创建角色
                    login.Result = 0;
                }
                else
                    login.Result = 1;
            }
        }
        else
        {
            CreateAccount(data.UserId);
            login.Result = 0;
        }
        //Debug.LogError(data.UserId.ToString());


        SendToClient(MessageId_Receive.LCLogin, login);
    }
    void CreateAccount(ulong uid)
    {
        PBAccountData AccountData = new PBAccountData();
        AccountData.Uid = uid;
        AccountData.Recharge = 0;
        AccountData.Diamonds = 0;
        AccountData.VipLevel = 0;
        SaveData(ACCOUNT_DATA_KEY, AccountData);
    }
}
