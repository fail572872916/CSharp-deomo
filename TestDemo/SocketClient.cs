using System;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace SocketStruct
{
    /// <summary>
    ///版 本 Trends Mall V5.0
    ///Copyright (c) 2003-2018 北京亿高索尔有限公司
    ///创 建：彭康
    ///日 期：2018-06-06
    ///描 述：Socket客户端
    /// </summary>
    public class SocketClient
    {
        /// <summary>
        ///     客户端
        /// </summary>
        /// <param name="Ip">服务端IP</param>
        /// <param name="port">服务端口号</param>
        public SocketClient(string Ip, int port)
        {
            this.Ip = Ip;
            this.port = port;
            ReceiveFunc = ReceiveData;
            Start = false;
            SendFunc = SocketHelper.Send;
        }
        /// <summary>
        /// 启动客户端
        /// </summary>
        public void StartClient()
        {
            ConnectServer();
            StartHeartbeat();
            Start = true;
            ReceiveFunc.BeginInvoke(null, null);
        }

        /// <summary>
        /// 数据接收
        /// </summary>
        private void ReceiveData()
        {
            try
            {
                while (true)
                {
                    byte[] receiveByteArr;
                    try
                    {
                        receiveByteArr = SocketHelper.Receive(socketClient);
                    }
                    catch
                    {
                        break;
                    }

                    if (receiveByteArr != null && receiveByteArr.Length != 0)
                    {
                        SocketData data = (SocketData)BinarySerialize.Deserialize(receiveByteArr);
                        switch (data.CmdType)
                        {
                            case CmdType.RunFunction:
                                break;
                            case CmdType.Message:
                                break;
                            case CmdType.Heartbeat:
                                break;
                        }
                    }
                    else
                    {
                        if (socketClient.Connected) socketClient.Disconnect(false);
                        socketClient.Close();
                        break;
                    }
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        ///     连接服务器
        /// </summary>
        private void ConnectServer()
        {
            try
            {
                if (socketClient == null || !socketClient.Connected)
                {
                    if (socketClient != null) socketClient.Close();
                    IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(Ip), port);
                    socketClient = new Socket(ipep.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
                    {
                        SendTimeout = 120000,
                        ReceiveTimeout = 120000,
                        SendBufferSize = 10240,
                        ReceiveBufferSize = 10240
                    };
                    socketClient.Connect(ipep);
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        ///     断开服务器
        /// </summary>
        public void DisconnectServer()
        {
            try
            {
                if (socketClient != null)
                {
                    if (socketClient.Connected) socketClient.Disconnect(false);
                    socketClient.Close();
                }
                StopHeartbeat();
            }
            catch
            {
                return;
            }
        }

        #region 属性
        /// <summary>
        /// 客户端套接字
        /// </summary>
        private Socket socketClient;
        /// <summary>
        /// 服务端IP
        /// </summary>
        private readonly string Ip;
        /// <summary>
        /// 服务端端口号
        /// </summary>
        private readonly int port;
        /// <summary>
        /// 心跳
        /// </summary>
        private static Timer heartbeatTimer;
        /// <summary>
        /// StartClient函数启动标识
        /// </summary>
        private bool Start;

        /// <summary>
        ///     数据接收事件
        /// </summary>
        private readonly Action ReceiveFunc;

        /// <summary>
        ///     数据发送
        /// </summary>
        private readonly Action<Socket, SocketData> SendFunc;

        #endregion

        #region 心跳

        public void StartHeartbeat()
        {
            heartbeatTimer = new Timer { Interval = 120000 };
            heartbeatTimer.Elapsed += (obj, eea) =>
            {
                try
                {
                    SocketData data = new SocketData
                    {
                        CmdType = CmdType.Heartbeat,
                        ClassName = string.Empty,
                        FunctionName = string.Empty
                    };
                    SendFunc(socketClient, data);
                }
                catch
                {
                    DisconnectServer();
                }
            };
            heartbeatTimer.Start();
        }


        public void StopHeartbeat()
        {
            if (heartbeatTimer != null)
                heartbeatTimer.Stop();
        }

        #endregion

        #region 请求

        public object Request(SocketData data)
        {
            try
            {
                lock (this)
                {
                    StopHeartbeat();

                    ConnectServer();

                    SendFunc(socketClient, data);

                    return Request();
                }
            }
            catch
            {
                if (socketClient.Connected) socketClient.Disconnect(false);
                throw;
            }
        }

        public object Request()
        {
            try
            {
                byte[] receiveByteArr = SocketHelper.Receive(socketClient);
                if (receiveByteArr == null || receiveByteArr.Length == 0) return Request();
                object result = BinarySerialize.Deserialize(receiveByteArr);
                if (result is string && result.ToString().IndexOf("error:", StringComparison.Ordinal) == 0)
                {
                    string errMsg = result.ToString().Split(':')[1];
                    throw new Exception(errMsg);
                }
                if (Start)
                    StopHeartbeat();
                else
                    DisconnectServer();
                return result;
            }
            catch
            {
                if (socketClient.Connected) socketClient.Disconnect(false);
                throw;
            }
        }

        #endregion
    }
}