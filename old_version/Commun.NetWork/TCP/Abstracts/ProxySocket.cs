using Commun.NetWork.TCP.EventHandler;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Commun.NetWork.TCP.Abstracts
{
    public abstract class ProxySocket
    {
        private byte[] _buffer_received = new byte[0x400];
        private Socket _socket;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        internal event DisConnectedEventHandler DisConnected;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        internal event MessageReceivedEventHandler MessageReceived;

        public ProxySocket(Socket socket)
        {
            this._socket = socket;
        }

        protected abstract ZDataBuffer GetDataBuffer();
        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                int count = this._socket.EndReceive(ar);
                if (count > 0)
                {
                    ZDataBuffer dataBuffer = this.GetDataBuffer();
                    byte[] asyncState = ar.AsyncState as byte[];
                    if (asyncState != null)
                    {
                        dataBuffer.WriteBytes(asyncState, 0, asyncState.Length);
                    }
                    dataBuffer.WriteBytes(this._buffer_received, 0, count);
                    ZMessage message = null;
                    while ((message = dataBuffer.TryReadMessage()) != null)
                    {
                        if (MessageReceived != null)
                        {
                            this.MessageReceived.BeginInvoke(this, message, null, null);
                        }
                    }
                    this._socket.BeginReceive(this._buffer_received, 0, 0x400, SocketFlags.None, new AsyncCallback(this.OnReceive), dataBuffer.UnCompelete);
                }
                else if (DisConnected != null)
                {
                    this.DisConnected(this);
                }
            }
            catch
            {
                if (DisConnected != null)
                {
                    this.DisConnected(this);
                }
            }
        }

        public void SendMessage(ZMessage message)
        {
            try
            {
                this._socket.Send(message.RawData);
            }
            catch
            {
            }
        }

        internal void StartReceive()
        {
            if (this._socket != null)
            {
                this._socket.BeginReceive(this._buffer_received, 0, 0x400, SocketFlags.None, new AsyncCallback(this.OnReceive), null);
            }
        }

        public string RemoteIP
        {
            get
            {
                try
                {
                    return (this._socket.RemoteEndPoint as IPEndPoint).Address.ToString();
                }
                catch
                {
                    return null;
                }
            }
        }

        public int RemotePort
        {
            get
            {
                try
                {
                    return (this._socket.RemoteEndPoint as IPEndPoint).Port;
                }
                catch
                {
                    return -1;
                }
            }
        }
    }
}