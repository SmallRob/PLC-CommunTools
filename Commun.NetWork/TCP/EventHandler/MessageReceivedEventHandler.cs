using Commun.NetWork.TCP.Abstracts;

namespace Commun.NetWork.TCP.EventHandler
{
    public delegate void MessageReceivedEventHandler(ProxySocket proxySocket, ZMessage message);
}