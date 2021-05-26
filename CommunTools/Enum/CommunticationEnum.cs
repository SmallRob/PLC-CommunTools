using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunTools.Enum
{
    /// <summary>
    /// TCP服务通讯状态
    /// </summary>
    public enum CommunticationEnum
    {
        /// <summary>
        /// COM接口数据发送成功
        /// </summary>
        COMEntitySendOK,

        /// <summary>
        /// COM接口数据发送错误
        /// </summary>
        COMEntitySendError,

        /// <summary>
        /// COM接口数据接收成功
        /// </summary>
        COMEntityReciveOK,

        /// <summary>
        /// COM接口数据接受错误
        /// </summary>
        COMEntityReciveError,

        /// <summary>
        /// TCP：发功成功
        /// </summary>
        TCPEntitySendOK,

        /// <summary>
        /// TCP:发送错误
        /// </summary>
        TCPEntitySendError,

        /// <summary>
        /// TCP:接收成功
        /// </summary>
        TCPEntityReciveOK,

        /// <summary>
        /// TCP:接收错误
        /// </summary>
        TCPEntityReciveError,
    }
}
