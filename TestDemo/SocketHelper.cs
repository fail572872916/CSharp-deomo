using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
//using SocketStruct.Log;

namespace SocketStruct
{
    /// <summary>
    ///版 本 Trends Mall V5.0
    ///Copyright (c) 2003-2018 北京亿高索尔有限公司
    ///创 建：彭康
    ///日 期：2018-06-06
    ///描 述：Socket帮助类
    /// </summary>
    public static class SocketHelper
    {

        #region 接收数据

        /// <summary>
        /// 接收数据
        /// </summary>
        public static byte[] Receive(Socket socket)
        {
            lock (socket)
            {
                try
                {
                    const int block = 4;
                    byte[] buffer = new byte[block];
                    int receiveCount = socket.Receive(buffer, 0, block, SocketFlags.None);
                    if (receiveCount == 0)
                    {
                        return null;
                    }
                    while (receiveCount < block)
                    {
                        int revCount = socket.Receive(buffer, receiveCount, buffer.Length - receiveCount,
                            SocketFlags.None);
                        receiveCount += revCount;
                        Thread.Sleep(1);
                    }
                    int dataLength = BitConverter.ToInt32(buffer, 0);
                    //block = 10240;
                    receiveCount = 0;
                    byte[] result = new byte[dataLength];
                    while (receiveCount < dataLength)
                    {
                        int revCount = socket.Receive(result, receiveCount, result.Length - receiveCount,
                            SocketFlags.None);
                        receiveCount += revCount;
                        Thread.Sleep(1);
                    }
                    try
                    {
                        BinarySerialize.Deserialize(result);
                    }
                    catch
                    {
                        return result;
                    }
                    return result;
                }
                catch
                {
                    return new byte[0];
                }
            }
        }

        #endregion

        #region 发送数据

        /// <summary>
        /// 发送数据
        /// </summary>
        public static void Send(Socket socket, object objdata)
        {
            lock (socket)
            {
                try
                {
                    byte[] data = BinarySerialize.Serialize(objdata);
                    byte[] lenArr = BitConverter.GetBytes(data.Length);
                    int sendTotal = 0;
                    while (sendTotal < lenArr.Length)
                    {
                        int sendOnce = socket.Send(lenArr, sendTotal, lenArr.Length - sendTotal, SocketFlags.None);
                        sendTotal += sendOnce;
                        Thread.Sleep(1);
                    }

                    Thread.Sleep(1);

                    int block = 10240;
                    int count = (data.Length - 1) / block + 1;
                    for (int i = 0; i < count; i++)
                    {
                        int currentBlock = block;
                        if (i == count - 1)
                        {
                            currentBlock = data.Length - block * i;
                        }

                        sendTotal = 0;
                        while (sendTotal < currentBlock)
                        {
                            int sendOnce = socket.Send(data, i * block + sendTotal, currentBlock - sendTotal,
                                SocketFlags.None);
                            sendTotal += sendOnce;
                            Thread.Sleep(1);
                        }

                        Thread.Sleep(1);
                    }
                }
                catch
                {
                    return;
                }
            }
        }

        #endregion

    }
    /// <summary>
    ///版 本 Trends Mall V5.0
    ///Copyright (c) 2003-2018 北京亿高索尔有限公司
    ///创 建：彭康
    ///日 期：2018-06-06
    ///描 述：序列化帮助类
    /// </summary>
    public class BinarySerialize
    {
        #region  属性定义
        private static readonly IFormatter SerializeUtil = new BinaryFormatter();

        internal static MemoryStream SendDate;
        #endregion


        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="date">序列化数据</param>
        /// <returns></returns>
        public static byte[] Serialize(object date)
        {
            SendDate = new MemoryStream();
            SerializeUtil.Serialize(SendDate, date);
            return SendDate.GetBuffer();
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="Dedate">反序列化数据</param>
        /// <returns></returns>
        public static object Deserialize(byte[] Dedate)
        {
            return SerializeUtil.Deserialize(new MemoryStream(Dedate));
        }
    }
}