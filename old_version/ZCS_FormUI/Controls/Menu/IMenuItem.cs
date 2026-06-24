using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZCS_FormUI.Controls
{
    /// <summary>
    /// Interface IMenuItem
    /// </summary>
    public interface IMenuItem
    {
        /// <summary>
        /// Occurs when [selected item].
        /// </summary>
        event EventHandler SelectedItem;
        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        MenuItemEntity DataSource { get; set; }
        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="styles">key:属性名称,value:属性值</param>
        void SetStyle(Dictionary<string, object> styles);
        /// <summary>
        /// 设置选中样式
        /// </summary>
        /// <param name="blnSelected">是否选中</param>
        void SetSelectedStyle(bool? blnSelected);
    }
}
