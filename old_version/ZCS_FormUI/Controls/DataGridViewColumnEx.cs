using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZCS_FormUI.Controls
{
    public class DataGridViewColumnEx: DataGridViewColumn
    {
        public DataGridViewColumnEx() : base(new DataGridViewCellEx())
        {
            Resizable = DataGridViewTriState.False;
            //固定宽度
            Width = 120;
        }

        public override sealed DataGridViewTriState Resizable
        {
            get { return base.Resizable; }
            set { base.Resizable = value; }
        }
    }
}
