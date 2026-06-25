using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZCS_FormUI.Controls
{
    /// <summary>
    /// Class DataGridViewCellEntity.
    /// </summary>
    public class DataGridViewCellEntity
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width { get; set; }
        /// <summary>
        /// Gets or sets the type of the width.
        /// </summary>
        /// <value>The type of the width.</value>
        public System.Windows.Forms.SizeType WidthType { get; set; }

    }
}
