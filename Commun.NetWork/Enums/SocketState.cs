using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commun.NetWork.Enums
{
    /// <summary>
    /// Socket状态
    /// </summary>
    [Flags]
    public enum SocketState
    {
        /// <summary>
        /// 正在连接
        /// </summary>
        Connecting = 0,

        /// <summary>
        /// 已连接
        /// </summary>
        Connected = 1,

        /// <summary>
        /// 重新连接
        /// </summary>
        Reconnection = 2,

        /// <summary>
        /// 断开连接
        /// </summary>
        Disconnect = 3,

        /// <summary>
        /// 正在监听
        /// </summary>
        StartListening = 4,

        /// <summary>
        /// 停止监听
        /// </summary>
        StopListening = 5,

        /// <summary>
        /// 客户端上线
        /// </summary>
        ClientOnline = 6,

        /// <summary>
        /// 客户端下线
        /// </summary>
        ClientOnOff = 7
    }

    /// <summary>
    /// 发送接收命令枚举
    /// </summary>
    [Flags]
    public enum Command
    {
        /// <summary>
        /// 发送请求接收文件
        /// </summary>
        RequestSendFile = 0x000001,
        /// <summary>
        /// 响应发送请求接收文件
        /// </summary>
        ResponeSendFile = 0x100001,

        /// <summary>
        /// 请求发送文件包
        /// </summary>
        RequestSendFilePack = 0x000002,
        /// <summary>
        /// 响应发送文件包
        /// </summary>
        ResponeSendFilePack = 0x100002,

        /// <summary>
        /// 请求取消发送文件包
        /// </summary>
        RequestCancelSendFile = 0x000003,
        /// <summary>
        /// 响应取消发送文件包
        /// </summary>
        ResponeCancelSendFile = 0x100003,

        /// <summary>
        /// 请求取消接收发送文件
        /// </summary>
        RequestCancelReceiveFile = 0x000004,
        /// <summary>
        /// 响应取消接收发送文件
        /// </summary>
        ResponeCancelReceiveFile = 0x100004,
        /// <summary>
        /// 请求发送文本消息
        /// </summary>
        RequestSendTextMSg = 0x000010,
    }
}
