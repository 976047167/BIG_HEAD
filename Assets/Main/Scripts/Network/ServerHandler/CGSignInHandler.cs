//generate by code
using BigHead.Net;
using Google.Protobuf;
using BigHead.protocol;

public class CGSignInHandler : BaseServerPacketHandler
{
    public override ushort OpCode
    {
        get
        {
            return (ushort)MessageId_Send.CGSignIn;
        }
    }

    public override void Handle(object sender, IMessage packet)
    {
        base.Handle(sender, packet);
        CGSignIn data = packet as CGSignIn;
        //处理完数据和逻辑后,发送消息通知客户端
        GCSignIn userData = new GCSignIn();
        userData.Uid = data.UserId;
        userData.AccountData = GetSavedData<PBAccountData>(ACCOUNT_DATA_KEY);
        if (userData.AccountData == null)
        {
            UnityEngine.Debug.LogError("没有创建账号!");
            return;
        }
        userData.PlayerData = GetSavedData<PBPlayerData>(PLAYER_DATA_KEY);
        if (userData.PlayerData == null)
        {
            UnityEngine.Debug.LogError("没有创建角色!");
            return;
        }
        userData.PlayerDetailData = GetSavedData<PBPlayerDetailData>(PLAYER_DETAIL_DATA);
        if (userData.PlayerDetailData == null)
        {
            UnityEngine.Debug.LogError("没有创建角色详细信息!");
            return;
        }
        SendToClient(MessageId_Receive.GCSignIn, userData);
    }
}
