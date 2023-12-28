using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Lidgren.Network;
using Thresh.Core.Config.INI;
using Thresh.Core.Data;
using Thresh.Core.Utility;
using Thresh.Core.Variant;
using Thresh.Network;
using Thresh.Unity.Databind;
using Thresh.Unity.Global;
using UnityEngine;

namespace Thresh.Unity.App
{
    public class NetworkApp : AppBase
    {
        public string Address;
        public int Port;
        public string IpAddress;
        private string _Ident;
        private NetClient _Client;
        private NetConnection _Connection;
        private Queue<MsgSender> _SendMessages = new Queue<MsgSender>();
        private Queue<MsgSender> TmpSendMessages = new Queue<MsgSender>();
        public string FileVersion;
        public string FileUrl;

        public NetClient Client
        {
            get { return _Client; }
        }

        public int Ping
        {
            get { return (int)_Connection.AverageRoundtripTime * Constant.SECOND; }
        }

        public bool Connected
        {
            get { return _Connection.Status != NetConnectionStatus.Disconnected; }
        }

        public override void OnAwake(Configuration app_ini)
        {
            try
            {
                Address = "192.168.1.185";
                Port = app_ini["proxy_net"]["port"].IntValue;
                _Ident = app_ini["proxy_net"]["ident"].StringValue;
                FileVersion = app_ini["version"]["version"].StringValue;
                FileUrl = app_ini["version"]["url"].StringValue;
            }
            catch (Exception e)
            {
                throw new INIException("NetworkApp read ini fail", e);
            }
        }

        public override void OnOpen()
        {
            
        }

        public void Connect()
        {
            AppEngine.Instance.ConnectionSuccess = false;
            LogAssert.Util.Debug("NetUtility.CheckIPV6 " + Address);
            NetUtility.CheckIPV6(Address);

            NetPeerConfiguration config = new NetPeerConfiguration(_Ident);
#if UNITY_EDITOR
            config.ConnectionTimeout = 60;
#endif
            config.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            _Client = new NetClient(config);
            _Client.Start();
            LogAssert.Util.Debug("_Client Start");

            if (NetUtility.IsIPV6)
            {
                var addresses = Dns.GetHostAddresses(Address);
                LogAssert.Util.Debug("Dns.GetHostAddresses" + addresses);
                if (addresses == null)
                {
                    _Connection = _Client.Connect(IpAddress, Port);
                    return;
                }

                if (!Socket.OSSupportsIPv6)
                {
                    return;
                }
                LogAssert.Util.Debug("Dns.GetHostAddress "+addresses);

                foreach (var address in addresses)
                {
                    if (address.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        IPEndPoint iep = new IPEndPoint(address, Port);
                        _Connection = _Client.Connect(iep);
                        LogAssert.Util.Debug("_Client.Connect "+address);
                        break;
                    }
                }
                LogAssert.Util.Debug("NetUtility.IsIPV6 End ");
            }
            else
            {
                _Connection = _Client.Connect(IpAddress, Port);
                LogAssert.Util.Debug("_Client.Connect ipv4"+ IpAddress);
            }
        }

        private int tmp = 0;

        private void ProcessMessage()
        {
            NetIncomingMessage msg = null;
            while ((_Client != null) && (msg = _Client.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        string text = msg.ReadString();
                        Logger.Trace(text);
                        break;
                    
                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)msg.ReadByte();
                        Logger.Debug("Connect {0} Status Changed,Now is {1}",msg.SenderEndPoint.ToString(),status);
                        break;
                    
                    case NetIncomingMessageType.Data:
                        if (msg.LengthBytes > 0)
                        {
                            int length = msg.ReadInt32();
                            byte[] msg_data = msg.ReadBytes(length);
                            tmp += length;

                            VariantList recv_msg = DataUtil.BytesToObject<VariantList>(msg_data);
                            int opcode = recv_msg.SubtractInt();
                            Logger.Info("DispatchMessage msg={0},data={1}",OpcodeUtil.ToName<SystemOpcode.SERVER>(opcode),recv_msg);
                            AppEngine.Instance.Dispatch.DispatchMessage(opcode, recv_msg);
                        }
                        else
                        {
                            Logger.Warn("Empty NetIncomingMessageType.Data message from {0}",msg.SenderEndPoint);
                        }

                        break;
                    
                    default:
                        break;
                }
            }
        }

        IEnumerator DelayRecycle(int opcode, VariantList recv_msg)
        {
            yield break;
        }

        public override void OnLoop()
        {
            if (_SendMessages == null)
            {
                return;
            }
            ProcessMessage();
            
            TmpSendMessages.Clear();
            while (_SendMessages.Count > 0 && _Client.ConnectionStatus == NetConnectionStatus.Connected)
            {
                MsgSender client_msg = _SendMessages.Dequeue();
                //if(!AppEngine.Instance.ConnectionSuccess && (client_msg.Opcode == SystemOpcode.CLIENT.))
            }

            while (TmpSendMessages.Count > 0)
            {
                _SendMessages.Enqueue(TmpSendMessages.Dequeue());
            }
        }

        public void SendSystemMsg(VariantList send_msg)
        {
            byte[] send_bytes = DataUtil.ObjectToBytes(send_msg);

            NetOutgoingMessage om = _Client.CreateMessage();
            om.Write(send_bytes.Length);
            om.Write(send_bytes);

            _Client.SendMessage(om, NetDeliveryMethod.ReliableOrdered, (int)ProxyChannel.Game);
            
            send_msg.Recycle();
        }

        public MsgSender SendSystemMsg(int opcode)
        {
            MsgSender handler = MsgSender.New(opcode);
            _SendMessages.Enqueue(handler);

            return handler;
        }

        public MsgSender SendCustomMsg(int custom)
        {
            MsgSender handler = MsgSender.New(SystemOpcode.CLIENT.CUSTOM_MSG_REQUEST);
            handler.Msg.Append(custom);
            
            _SendMessages.Enqueue(handler);

            return handler;
        }

        public override void OnClose()
        {
            _Client.Disconnect("LidgrenNetworkApp shutdown");
        }
    }
}