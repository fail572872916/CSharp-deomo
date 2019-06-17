using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace SocketStruct
{
    /// <summary>
    ///版 本 Trends Mall V5.0
    ///Copyright (c) 2003-2018 北京亿高索尔有限公司
    ///创 建：彭康
    ///日 期：2018-06-06
    ///描 述：传送数据类型
    /// </summary>
    public enum CmdType
    {
        /// <summary>
        /// 执行方法
        /// </summary>
        RunFunction = 1,
        /// <summary>
        /// 心跳
        /// </summary>
        Heartbeat = 2,
        /// <summary>
        /// 消息
        /// </summary>
        Message = 3,
    }

    /// <summary>
    ///版 本 Trends Mall V5.0
    ///Copyright (c) 2003-2018 北京亿高索尔有限公司
    ///创 建：彭康
    ///日 期：2018-06-06
    ///描 述：传送数据模型
    /// </summary>
    [Serializable]
    public class SocketData : ISerializable
    {

        public SocketData() { }

        public SocketData(SerializationInfo info, StreamingContext context)
        {
            Type t = GetType();
            PropertyInfo[] Fiekds = t.GetProperties();
            foreach (PropertyInfo Field in Fiekds)
            {
                Field.SetValue(this, info.GetValue(Field.Name, Field.PropertyType));
            }
        }

        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// 请求访问地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 传输类型
        /// </summary>
        public CmdType CmdType { get; set; }

        /// <summary>
        /// Dll名称
        /// </summary>
        public string DllName { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 方法名
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 方法参数
        /// </summary>
        public object[] FunParam { get; set; }

        public Ess.Utility.UserInfo UserInfo { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Type t = GetType();
            PropertyInfo[] Fiekds = t.GetProperties();
            foreach (PropertyInfo Field in Fiekds)
            {
                info.AddValue(Field.Name, Field.GetValue(this, null), Field.PropertyType);
            }
        }
    }

}