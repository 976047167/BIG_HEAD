//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;
using UnityEngine;

public class CGLoginHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CGLogin;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CGLogin data = packet as CGLogin;
        //处理完数据和逻辑后,发送消息通知客户端
        GCLogin login = new GCLogin();
        PBAccountData read = GetSavedData<PBAccountData>(ACCOUNT_DATA_KEY);
        if (read != null)
        {
            if (read.Username != data.Username)
            {
                //新玩家
                login.Result = 0;
                login.AccountData = CreateAccount(data.Username);
            }
            else
            {
                login.AccountData = GetSavedData<PBAccountData>(ACCOUNT_DATA_KEY);
                if (GetSavedData<PBPlayerData>(PLAYER_DATA_KEY) == null || GetSavedData<PBPlayerDetailData>(PLAYER_DETAIL_DATA_KEY) == null)
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
            login.AccountData = CreateAccount(data.Username);
            login.Result = 0;
        }
        //Debug.LogError(data.UserId.ToString());


        SendToClient(MessageId_Receive.GCLogin, login);
    }
    PBAccountData CreateAccount(string username)
    {
        PBAccountData AccountData = new PBAccountData();
        AccountData.Uid = 1;
        AccountData.Recharge = 0;
        AccountData.Diamonds = 0;
        AccountData.VipLevel = 0;
        AccountData.Username = username;
        SaveData(ACCOUNT_DATA_KEY, AccountData);
        return AccountData;
    }
}
