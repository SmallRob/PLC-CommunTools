using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunTools.Common
{
    public class PassDataEventArgs : EventArgs
    {
        public PassDataEventArgs()
        {

        }
        public PassDataEventArgs(string refName)
        {
            this._eventstr = refName;
        }

        private string _eventstr = "";
        public string EventStr
        {
            get
            {
                return _eventstr;
            }
            set
            {
                _eventstr = value;
            }
        }
    }
}
