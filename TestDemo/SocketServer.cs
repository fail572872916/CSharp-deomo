using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketStruct
{
    /// <summary>
    ///版 本 Trends Mall V5.0
    ///Copyright (c) 2003-2018 北京亿高索尔有限公司
    ///创 建：彭康
    ///日 期：2018-06-06
    ///描 述：Socket服务端
    /// </summary>
    public class SocketServer
    {
        #region 属性
        /// <summary>
        /// 服务套接字
        /// </summary>
        private Socket socketServer;

        /// <summary>
        /// 客户端列表
        /// </summary>
        private static readonly List<Socket> clientList = new List<Socket>();

        /// <summary>
        /// 服务端口号
        /// </summary>
        public readonly int port;

        /// <summary>
        /// 服务最大监听数
        /// </summary>
        public readonly int backlong;

        /// <summary>
        /// 心跳
        /// </summary>
        private DateTime lastHeartbeat;

        #endregion

        #region 委托事件

        /// <summary>
        /// 监听事件
        /// </summary>
        private readonly Action ListenAction;

        /// <summary>
        /// 数据接收事件
        /// </summary>
        private readonly Action<Socket> ReceiveFunc;

        /// <summary>
        /// 心跳
        /// </summary>
        private readonly Action<Socket> HeartbeatAction;

        /// <summary>
        /// 数据处理
        /// </summary>
        private readonly Func<object, object> RunFunction;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">服务端口号</param>
        /// <param name="backlong">服务最大监听数</param>
        /// <param name="RunFunction">数据处理函数</param>
        public SocketServer(int port, int backlong, Func<object, object> RunFunction=null)
        {
            this.port = port;
            this.backlong = backlong;
            ListenAction = Listen;
            ReceiveFunc = ReceiveData;
            HeartbeatAction = Heartbeat;
            this.RunFunction = RunFunction;
            StartServer();
        }

        /// <summary>
        /// 启动服务端
        /// </summary>
        private void StartServer()
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, port);
            socketServer = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socketServer.Bind(ipEndPoint);
            socketServer.Listen(backlong);
            ListenAction.BeginInvoke(null, null);
        }

        /// <summary>
        /// 监听
        /// </summary>
        private void Listen()
        {
            while (true)
            {
                Socket client;
                try
                {
                    client = socketServer.Accept();
                    client.SendTimeout = 2000;
                    client.ReceiveTimeout = 2000;
                    client.SendBufferSize = 10240;
                    client.ReceiveBufferSize = 10240;
                    lastHeartbeat = DateTime.Now;
                    lock (clientList)
                    {
                        if (!clientList.Contains(client))
                        {
                            clientList.Add(client);
                        }
                    }
                }
                catch
                {
                    continue;
                }

                ReceiveFunc.BeginInvoke(client, null, null);

                HeartbeatAction.BeginInvoke(client, null, null);
            }
        }

        /// <summary>
        /// 接收并解析数据调用数据处理函数
        /// </summary>
        /// <param name="client"></param>
        private void ReceiveData(Socket client)
        {
            try
            {
                while (true)
                {
                    byte[] receiveByteArr;
                    try
                    {
                        receiveByteArr = SocketHelper.Receive(client);
                    }
                    catch
                    {
                        break;
                    }

                    if (receiveByteArr != null)
                    {
                        if (receiveByteArr.Length == 0) continue;
                        SocketData data = (SocketData) BinarySerialize.Deserialize(receiveByteArr);
                        switch (data.CmdType)
                        {
                            case CmdType.RunFunction:
                                object obj = null;
                                try
                                {
                                    if (RunFunction == null)
                                        obj = false;
                                    else obj = RunFunction(data);
                                }
                                catch
                                {
                                    SocketHelper.Send(client, "error:执行服务端方法出错");
                                }

                                SocketHelper.Send(client, obj);
                                break;
                            case CmdType.Message:
                                break;
                            case CmdType.Heartbeat:
                                lastHeartbeat = DateTime.Now;
                                break;
                        }
                    }
                    else
                    {
                        lock (client)
                        {
                            clientList.Remove(client);
                            if (client.Connected) client.Disconnect(false);
                            client.Close();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SocketHelper.Send(client, "error:" + ex.Message);
            }
        }

        /// <summary>
        /// 检测心跳
        /// </summary>
        /// <param name="client"></param>
        private void Heartbeat(Socket client)
        {
            try
            {
                while (true)
                {
                    DateTime now = DateTime.Now;
                    if (now.Subtract(lastHeartbeat).TotalSeconds > 20)
                    {
                        lock (clientList)
                        {
                            clientList.Remove(client);
                            if (client.Connected) client.Disconnect(false);
                            client.Close();
                            break;
                        }
                    }
                    Thread.Sleep(500);
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        ///     停止服务
        /// </summary>
        public void StopServer()
        {
            try
            {
                lock (clientList)
                {
                    foreach (Socket socket in clientList)
                    {
                        if (socket.Connected) socket.Disconnect(false);
                        socket.Close();
                        socket.Dispose();
                    }

                    clientList.Clear();
                }
                if (socketServer != null)
                {
                    if (socketServer.Connected) socketServer.Disconnect(false);
                    socketServer.Close();
                }
            }
            catch
            {
                return;
            }
        }
    }
}
