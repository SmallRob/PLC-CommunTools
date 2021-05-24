using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZCS_FormUI.Controls
{
    /// <summary>
    /// 子类节点
    /// Implements the <see cref="System.Windows.Forms.UserControl" />
    /// Implements the <see cref="ZCS_FormUI.Controls.IMenuItem" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    /// <seealso cref="ZCS_FormUI.Controls.IMenuItem" />
    [ToolboxItem(false)]
    public partial class UCMenuChildrenItem : UserControl, IMenuItem
    {
        /// <summary>
        /// Occurs when [selected item].
        /// </summary>
        public event EventHandler SelectedItem;

        /// <summary>
        /// The m data source
        /// </summary>
        private MenuItemEntity m_dataSource;
        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public MenuItemEntity DataSource
        {
            get
            {
                return m_dataSource;
            }
            set
            {
                m_dataSource = value;
                if (value != null)
                {
                    lblTitle.Text = value.Text;
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UCMenuChildrenItem" /> class.
        /// </summary>
        public UCMenuChildrenItem()
        {
            InitializeComponent();
            this.lblTitle.MouseDown += lblTitle_MouseDown;
        }

        /// <summary>
        /// Handles the MouseDown event of the lblTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedItem != null)
            {
                SelectedItem(this, null);
            }
        }

        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="styles">key:属性名称,value:属性值</param>
        /// <exception cref="System.Exception">菜单元素设置样式异常</exception>
        /// <exception cref="Exception">菜单元素设置样式异常</exception>
        public void SetStyle(Dictionary<string, object> styles)
        {
            Type t = this.GetType();
            foreach (var item in styles)
            {
                var pro = t.GetProperty(item.Key);
                if (pro != null && pro.CanWrite)
                {
                    try
                    {
                        pro.SetValue(this, item.Value, null);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("菜单元素设置样式异常", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the selected style.
        /// </summary>
        /// <param name="blnSelected">if set to <c>true</c> [BLN selected].</param>
        public void SetSelectedStyle(bool? blnSelected)
        {

        }
    }
}
