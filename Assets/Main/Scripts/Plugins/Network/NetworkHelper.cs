using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead;
using System;
using Google.Protobuf;

namespace BigHead.Net
{
    public class NetworkHelper
    {
        static Dictionary<ushort, BasePacketHandler> dicHandler;
        public static void RegisterHandler()
        {
            dicHandler = new Dictionary<ushort, BasePacketHandler>();
            Type baseType = typeof(BasePacketHandler);
            Type[] types = Utility.Assembly.GetTypes();
            foreach (var type in types)
            {
                if (baseType != type && baseType.IsAssignableFrom(type))
                {
                    BasePacketHandler handler = type.Assembly.CreateInstance(type.ToString()) as BasePacketHandler;
                    dicHandler.Add(handler.OpCode, handler);
                }
            }


        }

        
    }
}