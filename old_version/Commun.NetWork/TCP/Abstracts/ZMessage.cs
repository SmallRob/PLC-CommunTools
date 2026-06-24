using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commun.NetWork.TCP.Abstracts
{
    public abstract class ZMessage
    {
        protected ZMessage()
        {
        }

        public abstract byte[] RawData { get; }
    }

}
