using ZCS_FormUI.Function;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZCS_FormUI.Controls
{
    public partial class ComboBoxTreeEx : ComboBoxEx
    {
        private int DgvX = 0;
        private int DgvY = 0;
        private TreeView checkedTree;
        private Panel panel;
        private Panel panelfill;
        private Panel panelBottom;
        private ButtonEx btnAll;
        private ButtonEx btnCanel;
        public ButtonEx btnOk;

        private DataTable _DataSource;
        private string _DisplayMember;
        private string _ValueMember;
        private string _ValueLevel;
        private string _FartherNode;
        private int _HeightEx = 200;
        private Hashtable _SelectedCheck = new Hashtable();
        private string txtstr = string.Empty;


        /// <summary>
        /// 数据源
        /// </summary>
        [Category("Custom"), Browsable(true), Description("绑定的ComboBoxTree的数据源")]
        public DataTable DataSource
        {
            set { this._DataSource = value; }
            get { return _DataSource; }
        }

        /// <summary>
        /// 绑定的显示列
        /// </summary>
        [Category("Custom"), Browsable(true), Description("绑定的ComboBoxTree的数据源 显示列")]
        public string DisplayMember
        {
            set { this._DisplayMember = value; }
            get { return _DisplayMember; }
        }

        /// <summary>
        /// 绑定的实际值
        /// </summary>
        [Category("Custom"), Browsable(true), Description("绑定的ComboBoxTree的数据源 值列")]
        public string ValueMember
        {
            set { this._ValueMember = value; }
            get { return _ValueMember; }
        }

        /// <summary>
        /// 绑定的级别值
        /// </summary>
        [Category("Custom"), Browsable(true), Description("绑定的ComboBoxTree的数据源 父级节点列")]
        public string FartherNode
        {
            set { this._FartherNode = value; }
            get { return _FartherNode; }
        }

        /// <summary>
        /// 绑定的级别值
        /// </summary>
        [Category("Custom"), Browsable(true), Description("绑定的ComboBoxTree的数据源 级别列")]
        public string ValueLevel
        {
            set { this._ValueLevel = value; }
            get { return _ValueLevel; }
        }

        [Category("Custom"), Browsable(true), Description("设置的ComboBoxTree的HeightEx列高度")]
        public int HeightEx
        {
            set { this._HeightEx = value; }
            get { return _HeightEx; }
        }

        [Category("Custom"), Browsable(true), Description("获得的ComboBoxTree选中集合")]
        public Hashtable SelectedCheck
        {
            set
            {
                this._SelectedCheck = value;

                if (SelectedCheck.Count > 0)
                {
                    SetSelected();
                }
            }
            get { return _SelectedCheck; }
        }

        private void SetSelected()
        {
            ComboBoxTreeEx_DropDown(null, null);
            for (int i = 0, cnt = checkedTree.Nodes.Count; i < cnt; i++)
            {
                TreeNode selectNodes = checkedTree.Nodes[i];
                TreeNode childNode = null;          //根节点的被选中的子节点

                childNode = selectNodes.FirstNode;
                while (childNode != null)           //找该节点的所有被选择的子节点
                {
                    string txt = childNode.Text.ToString();
                    string val = (DataSource.Select(DisplayMember + "='" + txt + "'"))[0][ValueMember].ToString();
                    foreach (DictionaryEntry de in SelectedCheck)
                    {
                        if (de.Value.ToString() == val)
                        {
                            childNode.Checked = true;
                            break;
                        }
                    }
                    childNode = childNode.NextNode;
                }
                continue;
            }
            this.Text = SelectedCheck.Count == 0 ? "" : "已选择" + SelectedCheck.Count.ToString() + "项";
            txtstr = this.Text;
        }

        public ComboBoxTreeEx()
        {
            InitializeComponent();
        }

        public ComboBoxTreeEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            if (SelectedCheck != null) SelectedCheck.Clear();
            this.Text = "";

            Init();
            GC.Collect();
            this.Refresh();
        }

        private void Init()
        {
            this.DoubleBuffered = true;

            panel = new Panel();

            panelfill = new Panel();
            panelBottom = new Panel();

            btnAll = new ButtonEx();
            btnAll.Text = "全选";
            btnAll.Width = 38;
            btnCanel = new ButtonEx();
            btnCanel.Text = "取消";
            btnCanel.Width = 38;

            btnOk = new ButtonEx();
            btnOk.Text = "确定";
            btnOk.Width = 38;

            btnAll.Click += new EventHandler(btnAll_Click);
            btnCanel.Click += new EventHandler(btnCanel_Click);
            btnOk.Click += new EventHandler(btnOk_Click);

            checkedTree = new TreeView();
            checkedTree.CheckBoxes = true;
            checkedTree.AfterCheck += new TreeViewEventHandler(checkedTree_AfterCheck);
            checkedTree.LostFocus += new EventHandler(checkedTree_LostFocus);
            checkedTree.Leave += new EventHandler(checkedTree_LostFocus);
            checkedTree.KeyDown += new KeyEventHandler(checkedTree_KeyDown);

            this.DropDown += new EventHandler(ComboBoxTreeEx_DropDown);
            this.DropDownClosed += new EventHandler(ComboBoxTreeEx_DropDownClosed);
            this.LostFocus += new EventHandler(ComboBoxTreeEx_LostFocus);
            this.checkedTree.MouseEnter += new EventHandler(checkedTree_MouseEnter);

            this.KeyUp += new KeyEventHandler(ComboBoxTreeEx_KeyUp);
            this.Leave += new EventHandler(ComboBoxTreeEx_LostFocus);
        }

        private void ComboBoxTreeEx_DropDown(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.DropDownHeight = 1;

            if (checkedTree.Nodes.Count == 0)
            {
                DgvX = 0;
                DgvY = 0;
                controlParent(this);

                //panelfill.Height = HeightEx - 30;
                panelfill.Margin = new Padding(0);
                panelfill.Dock = DockStyle.Fill;
                panelBottom.Margin = new Padding(0);
                panelBottom.Height = 30;
                panelBottom.Dock = DockStyle.Bottom;

                btnAll.Location = new Point(1, 2);
                btnAll.Margin = new Padding(0);
                btnAll.Parent = panelBottom;
                btnCanel.Location = new Point(41, 2);
                btnCanel.Margin = new Padding(0);
                btnCanel.Parent = panelBottom;

                btnOk.Location = new Point(81, 2);
                btnOk.Margin = new Padding(0);
                btnOk.Parent = panelBottom;

                panel.Width = this.Width;
                panel.Height = HeightEx;
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.Parent = this.FindForm();
                panel.Dock = DockStyle.None;
                panel.Location = new Point(DgvX, DgvY + this.Height);
                panel.BringToFront();

                checkedTree.Width = this.Width;
                checkedTree.Font = new System.Drawing.Font("宋体", 10);
                checkedTree.BorderStyle = BorderStyle.None;
                checkedTree.Margin = new Padding(0);
                checkedTree.Parent = this.panelfill;
                checkedTree.Dock = DockStyle.Fill;
                checkedTree.Nodes.Clear();

                RefreshTreeNode();

                panel.Controls.Add(panelfill);
                panel.Controls.Add(panelBottom);
            }
            panel.Show();
            panel.BringToFront();

            checkedTree.BringToFront();
            checkedTree.Focus();
            checkedTree.Select();

            this.Parent.MouseClick += new MouseEventHandler(Parent_MouseClick);
            this.FindForm().MouseClick += new MouseEventHandler(checkedTree_MouseClick);
            this.Parent.KeyDown -= new KeyEventHandler(checkedTree_KeyDown);
            this.Parent.KeyDown += new KeyEventHandler(checkedTree_KeyDown);

            panel.Focus();
            this.Cursor = Cursors.Default;
            this.Refresh();
        }

        private void RefreshTreeNode()
        {
            if (DataSource != null)
            {
                DataSet dsGroup = DataSetFunc.GroupDataTable(DataSource, FartherNode);
                foreach (DataTable item in dsGroup.Tables)
                {
                    bool ischecked = false;
                    if (item.Rows.Count > 0)
                    {
                        DataSet dsEach = DataSetFunc.GroupDataTable(item, ValueLevel);
                        TreeNode FartherNode = new TreeNode();

                        for (int i = 0, cnt = dsEach.Tables.Count; i < cnt; i++)
                        {
                            DataTable dtEach = dsEach.Tables[i];
                            if (i == 0)
                            {
                                DataRow dr = dtEach.Rows[0];

                                TreeNode node = new TreeNode();
                                node.Text = dr[DisplayMember].ToString();
                                node.Tag = dr[ValueMember].ToString();

                                checkedTree.Nodes.Add(node);
                                FartherNode = node;
                            }
                            else
                            {
                                for (int K = 0, cnk = dtEach.Rows.Count; K < cnk; K++)
                                {
                                    TreeNode childNode = new TreeNode();
                                    childNode.Text = dtEach.Rows[K][DisplayMember].ToString();
                                    childNode.Tag = dtEach.Rows[K][ValueMember].ToString();
                                    FartherNode.Nodes.Add(childNode);

                                    if (K + 1 == cnk) FartherNode = childNode;
                                }
                            }
                        }
                        checkedTree.Update();
                    }
                }
            }
        }

        private void GetSelected(TreeViewEventArgs e)
        {
            string s = string.Empty;
            string ss = string.Empty;

            string txt = e.Node.Text.ToString();
            string val = (DataSource.Select(DisplayMember + "='" + txt + "'"))[0][ValueMember].ToString();

            if (e.Node.Checked)
            {
                if (!SelectedCheck.Contains(txt))
                {
                    SelectedCheck.Add(txt, val);
                }
            }
            else
            {
                if (SelectedCheck.Contains(txt))
                {
                    SelectedCheck.Remove(txt);
                }
            }

            this.Text = SelectedCheck.Count == 0 ? "" : "已选择" + SelectedCheck.Count.ToString() + "项";
            txtstr = this.Text;

            if (ComboBoxTreeEx_Selected != null)
            {
                ComboBoxTreeEx_Selected(this, e);
            }
        }

        #region 事件
        private void ComboBoxTreeEx_DropDownClosed(object sender, EventArgs e)
        {
            checkedTree.Focus();
            checkedTree.Select();
        }

        private void checkedTree_MouseClick(object sender, MouseEventArgs e)
        {
            this.FindForm().MouseClick -= new MouseEventHandler(checkedTree_MouseClick);
            this.panel.Hide();
        }

        private void Parent_MouseClick(object sender, MouseEventArgs e)
        {
            this.Parent.MouseClick -= new MouseEventHandler(Parent_MouseClick);
            this.panel.Hide();
        }


        private void checkedTree_MouseEnter(object sender, EventArgs e)
        {
            checkedTree.Select();
            checkedTree.Focus();
        }

        private void checkedTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.panel.Hide();
            }
        }

        private void ComboBoxTreeEx_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
            }
            else
            {
                this.Text = txtstr;
            }
        }

        private void ComboBoxTreeEx_LostFocus(object sender, EventArgs e)
        {
            if (this.Controls != null && !checkedTree.IsDisposed)
            {
                if (!this.Focused && !checkedTree.Focused
                    && !this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position)
                    && !this.panel.RectangleToScreen(this.panel.ClientRectangle).Contains(Cursor.Position))
                {
                    panel.Hide();
                }
            }
        }

        private void checkedTree_LostFocus(object sender, EventArgs e)
        {
            if (checkedTree != null && !checkedTree.IsDisposed)
            {
                try
                {
                    if (!this.Focused && !checkedTree.Focused
                        && !this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position)
                        && !this.panel.RectangleToScreen(this.panel.ClientRectangle).Contains(Cursor.Position))
                    {
                        panel.Hide();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void checkedTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeViewCheck.CheckControl(e);

            if (e.Node != null && e.Node.Index >= 0)
                GetSelected(e);
        }
        #endregion

        public delegate void ComboBoxTreeEx_SelectedEventHandler(object sender, EventArgs e);
        /// <summary>
        /// 选中完成事件
        /// </summary>
        [Category("Custom"), Description("自定义选中完成事件")]
        public event ComboBoxTreeEx_SelectedEventHandler ComboBoxTreeEx_Selected;


        public delegate void btnOkClick(object sender, EventArgs e);

        /// <summary>
        /// 自定义点击确定按钮事件
        /// </summary>
        [Category("Custom"), Description("自定义点击确定按钮事件")]//用于界面获取此控件点击了确定按钮
        public event btnOkClick ComBoxConfirm;

        #region 按钮
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (checkedTree != null && checkedTree.Nodes.Count > 0)
            {
                TreeNode selectNodes = checkedTree.Nodes[0];
                while (selectNodes != null)
                {
                    TreeNode childNode = null;          //根节点的被选中的子节点

                    childNode = selectNodes.FirstNode;
                    while (childNode != null)           //找该节点的所有被选择的子节点
                    {
                        if (childNode.Checked)
                        {
                            if (!_SelectedCheck.ContainsKey(childNode.Text))
                            {
                                //处理数据
                                _SelectedCheck.Add(childNode.Text, childNode.Tag.ToString());
                            }
                        }
                        childNode = childNode.NextNode;
                    }
                    selectNodes = selectNodes.NextNode;          //下一个被选中的节点
                }
            }

            this.panel.Hide();
            if (ComBoxConfirm != null)
            {
                ComBoxConfirm(this, e);
            }
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            SetTreeViewCheck(false);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            SetTreeViewCheck(true);
        }

        #endregion

        /// <summary>
        /// 判断控件的父容器，取Location值
        /// </summary>
        /// <param name="con"></param>
        private void controlParent(Control con)
        {
            if (con != null && con.Name != this.FindForm().Name)
            {
                DgvX += con.Location.X;
                DgvY += con.Location.Y;

                controlParent(con.Parent);
            }
        }

        private void SetTreeViewCheck(bool isCheck)
        {
            for (int i = 0, cnt = checkedTree.Nodes.Count; i < cnt; i++)
            {
                TreeNode node = checkedTree.Nodes[i];
                if (node != null && !Convert.IsDBNull(node))
                {
                    TreeViewCheck.CheckParentNode(node);

                    if (node.Parent == null)
                    {
                        node.Checked = isCheck;
                    }
                    if (node.Nodes.Count > 0)
                    {
                        TreeViewCheck.CheckAllChildNodes(node, isCheck);
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
