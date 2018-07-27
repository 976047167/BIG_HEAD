//generate by code
using UnityEngine;

public enum NetworkMessageId : ushort
{
    None = 0,
    //添加内置事件,需要前往CompileProtoFiles.cs修改模板
    NetworkConnect,
    NetworkDisconnect,
    NetworkReconnect,
    
    LCLogin = 1000,
    LCLogout = 1001,
    MAX = 65535,
}
