using Commun.NetWork.TCP.EventHandler;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Commun.NetWork.TCP.Abstracts
{
    public abstract class ClientSocket
    {
        private bool _connected = false;
        private ProxySocket _proxy;
        private Socket _socket;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event ConnectedEventHandler Connected;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event DisConnectedEventHandler DisConnected;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event MessageReceivedEventHandler MessageReceived;

        protected ClientSocket()
        {
        }

        private void _proxy_DisConnected(ProxySocket proxySocket)
        {
            if (this.DisConnected != null)
            {
                this.DisConnected(proxySocket);
            }
        }

        private void _proxy_MessageReceived(ProxySocket proxySocket, ZMessage message)
        {
            if (this.MessageReceived != null)
            {
                this.MessageReceived(proxySocket, message);
            }
        }

        public void Connect(string ip, int port)
        {
            if (!this._connected)
            {
                try
                {
                    if (this._socket == null)
                    {
                        this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    }
                    this._socket.BeginConnect(new IPEndPoint(IPAddress.Parse(ip), port), new AsyncCallback(this.OnConnect), null);
                }
                catch
                {
                }
            }
        }

        protected abstract ProxySocket GetProxy(Socket socket);
        private void OnConnect(IAsyncResult ar)
        {
            try
            {
                this._socket.EndConnect(ar);
                this._proxy = this.GetProxy(this._socket);
                this._proxy.DisConnected += new DisConnectedEventHandler(this._proxy_DisConnected);
                this._proxy.MessageReceived += new MessageReceivedEventHandler(this._proxy_MessageReceived);
                this._proxy.StartReceive();
                this._connected = true;
                if (this.Connected != null)
                {
                    this.Connected(this._proxy);
                }
            }
            catch
            {
                if (this.Connected != null)
                {
                    this.Connected(null);
                }
            }
        }
    }
}
