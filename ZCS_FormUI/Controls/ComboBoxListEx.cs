using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace ZCS_FormUI.Controls
{
    public partial class ComboBoxListEx : ComboBoxEx
    {
        private int DgvX = 0;
        private int DgvY = 0;
        private CheckedListBox mCheckedListBox;
        private Panel panel;
        private Panel panelfill;
        private Panel panelBottom;
        private ButtonEx btnAll;
        private ButtonEx btnCanel;
        public ButtonEx btnOk;

        private DataTable _DataSource;
        private string _DisplayMember;
        private string _ValueMember;
        private int _HeightEx = 200;
        private Hashtable _SelectedCheck = new Hashtable();
        private string txtstr = string.Empty;


        /// <summary>
        /// 数据源
        /// </summary>
        [Category("Custom"), Browsable(true), Description("绑定的CheckBoxList  的数据源")]
        public DataTable DataSource
        {
            set { this._DataSource = value; }
            get { return _DataSource; }
        }

        /// <summary>
        /// 绑定的显示列
        /// </summary>
        [Category("Custom"), Browsable(true), Description("绑定的CheckBoxList 的数据源 显示列")]
        public string DisplayMember
        {
            set { this._DisplayMember = value; }
            get { return _DisplayMember; }
        }

        /// <summary>
        /// 绑定的实际值
        /// </summary>
        [Category("Custom"), Browsable(true), Description("绑定的CheckBoxList 的数据源 值列")]
        public string ValueMember
        {
            set { this._ValueMember = value; }
            get { return _ValueMember; }
        }

        [Category("Custom"), Browsable(true), Description("设置的CheckBoxList 的HeightEx列高度")]
        public int HeightEx
        {
            set { this._HeightEx = value; }
            get { return _HeightEx; }
        }

        [Category("Custom"), Browsable(true), Description("获得的CheckBoxList 选中集合")]
        public Hashtable SelectedCheck
        {
            set
            {
                this._SelectedCheck = value;
                //SetSelected();
            }
            get { return _SelectedCheck; }
        }

        private bool _selectedall = false;
        public bool SelectedALL
        {
            set
            {
                this._selectedall = value;
                Hashtable ht = new Hashtable();
                if (DataSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in DataSource.Rows)
                    {
                        ht.Add(dr[ValueMember].ToString(), "");
                    }
                }
                SelectedCheck = ht;
                SetSelected();
            }
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

        public delegate void ComboBoxListEx_SelectedEventHandler(object sender, EventArgs e);
        /// <summary>
        /// 选中完成事件
        /// </summary>
        [Category("Custom"), Description("自定义选中完成事件")]
        public event ComboBoxListEx_SelectedEventHandler ComboBoxListEx_Selected;


        public delegate void btnOkClick(object sender, EventArgs e);
        /// <summary>
        /// 自定义点击确定按钮事件
        /// </summary>
        [Category("Custom"), Description("自定义点击确定按钮事件")]//用于界面获取此控件点击了确定按钮
        public event btnOkClick ComBoxConfirm;

        public ComboBoxListEx()
        {
            InitializeComponent();
        }

        public ComboBoxListEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Init();
        }

        public void Init()
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

            mCheckedListBox = new CheckedListBox();
            mCheckedListBox.CheckOnClick = true;
            mCheckedListBox.ItemCheck += new ItemCheckEventHandler(mCheckedListBox_ItemCheck);
            mCheckedListBox.LostFocus += new EventHandler(mCheckedListBox_LostFocus);
            mCheckedListBox.Leave += new EventHandler(ComboBoxListEx_LostFocus);
            mCheckedListBox.KeyDown += new KeyEventHandler(mCheckedListBox_KeyDown);

            this.DropDown += new EventHandler(ComboBoxListEx_DropDown);
            this.DropDownClosed += new EventHandler(ComboBoxListEx_DropDownClosed);
            this.LostFocus += new EventHandler(ComboBoxListEx_LostFocus);
            this.mCheckedListBox.MouseEnter += new EventHandler(mCheckedListBox_MouseEnter);

            this.KeyUp += new KeyEventHandler(ComboBoxListEx_KeyUp);
            this.Leave += new EventHandler(ComboBoxListEx_LostFocus);
        }

        void btnOk_Click(object sender, EventArgs e)
        {
            this.panel.Hide();
            if (ComBoxConfirm != null)
            {
                ComBoxConfirm(this, e);
            }
        }

        void mCheckedListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.panel.Hide();
            }
        }


        void ComboBoxListEx_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
            }
            else
            {
                this.Text = txtstr;
            }
        }

        void ComboBoxListEx_MouseClick(object sender, MouseEventArgs e)
        {
            this.FindForm().MouseClick -= new MouseEventHandler(ComboBoxListEx_MouseClick);
            this.panel.Hide();
        }

        void Parent_MouseClick(object sender, MouseEventArgs e)
        {
            this.Parent.MouseClick -= new MouseEventHandler(Parent_MouseClick);
            this.panel.Hide();
        }

        void mCheckedListBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsDisposed && mCheckedListBox != null && !mCheckedListBox.IsDisposed)
                {
                    mCheckedListBox.Select();
                    mCheckedListBox.Focus();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void mCheckedListBox_LostFocus(object sender, EventArgs e)
        {
            if (!this.IsDisposed && mCheckedListBox != null && !mCheckedListBox.IsDisposed)
            {
                try
                {
                    if (!this.Focused && !mCheckedListBox.Focused
                        && !this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position)
                        && !this.panel.RectangleToScreen(this.panel.ClientRectangle).Contains(Cursor.Position))
                    {
                        panel.Hide();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        void ComboBoxListEx_LostFocus(object sender, EventArgs e)
        {
            if (!this.IsDisposed && this.Controls != null && !mCheckedListBox.IsDisposed)
            {
                if (!this.Focused && !mCheckedListBox.Focused
                    && !this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position)
                    && !this.panel.RectangleToScreen(this.panel.ClientRectangle).Contains(Cursor.Position))
                {
                    panel.Hide();
                }
            }
        }

        void btnCanel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < mCheckedListBox.Items.Count; i++)
            {
                mCheckedListBox.SetItemChecked(i, false);
            }
            //GetSelected();
        }

        void btnAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < mCheckedListBox.Items.Count; i++)
            {
                mCheckedListBox.SetItemChecked(i, true);
            }
            //GetSelected();
        }

        void ComboBoxListEx_DropDownClosed(object sender, EventArgs e)
        {
            //mCheckedListBox.Hide();
            mCheckedListBox.Focus();
            mCheckedListBox.Select();
        }

        void ComboBoxListEx_DropDown(object sender, EventArgs e)
        {
            if (!IsDisposed || this.InvokeRequired)
            {
                this.Cursor = Cursors.WaitCursor;
                this.DropDownHeight = 1;

                if (mCheckedListBox != null && !mCheckedListBox.IsDisposed && mCheckedListBox.Items.Count == 0)
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

                    mCheckedListBox.Items.Clear();

                    mCheckedListBox.Width = this.Width;
                    mCheckedListBox.Font = new System.Drawing.Font("宋体", 10);
                    mCheckedListBox.BorderStyle = BorderStyle.None;
                    //mCheckedListBox.Location = new Point(DgvX, DgvY + this.Height);
                    mCheckedListBox.Margin = new Padding(0);
                    mCheckedListBox.Parent = this.panelfill;
                    mCheckedListBox.Dock = DockStyle.Fill;

                    if (DataSource != null)
                    {
                        this.Invoke(new Action(delegate
                        {
                            bool ischecked = false;
                            foreach (DataRow dr in DataSource.Rows)
                            {
                                if (SelectedCheck != null && SelectedCheck.Count > 0)
                                {
                                    foreach (DictionaryEntry de in SelectedCheck)
                                    {
                                        if (de.Key.ToString() == dr[ValueMember].ToString())
                                        {
                                            ischecked = true;
                                            break;
                                        }
                                    }
                                }

                                mCheckedListBox.Items.Add(dr[DisplayMember].ToString(), ischecked);
                                mCheckedListBox.Refresh();
                            }
                        }));
                    }
                    GC.KeepAlive(mCheckedListBox);
                }

                panel.Controls.Add(panelfill);
                panel.Controls.Add(panelBottom);

                panel.Show();
                panel.BringToFront();

                mCheckedListBox.BringToFront();
                mCheckedListBox.Focus();
                mCheckedListBox.Select();

                this.Parent.MouseClick += new MouseEventHandler(Parent_MouseClick);
                this.FindForm().MouseClick += new MouseEventHandler(ComboBoxListEx_MouseClick);
                this.Parent.KeyDown -= new KeyEventHandler(mCheckedListBox_KeyDown);
                this.Parent.KeyDown += new KeyEventHandler(mCheckedListBox_KeyDown);

                panel.Focus();
                this.Cursor = Cursors.Default;
                this.Refresh();
            }
        }

        /// <summary>
        /// CheckedListBox_ItemCheck事件,保存设置是否显示列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            GetSelected(e);
        }

        private void SetSelected()
        {
            this.Invoke(new Action(() =>
            {
                if (SelectedCheck.Count > 0)
                {
                    for (int i = 0; i < mCheckedListBox.Items.Count; i++)
                    {
                        string txt = mCheckedListBox.GetItemText(mCheckedListBox.Items[i]);
                        string val = (DataSource.Select(DisplayMember + "='" + txt + "'"))[0][ValueMember].ToString();
                        foreach (DictionaryEntry de in SelectedCheck)
                        {
                            if (de.Key.ToString() == val)
                            {
                                mCheckedListBox.SetItemChecked(i, true);
                                break;
                            }
                        }
                    }
                    this.Text = SelectedCheck.Count == 0 ? "" : "已选择" + SelectedCheck.Count.ToString() + "项";
                    txtstr = this.Text;

                }
            }));
        }

        private void GetSelected(ItemCheckEventArgs e)
        {
            string s = string.Empty;
            string ss = string.Empty;

            string txt = mCheckedListBox.GetItemText(mCheckedListBox.Items[e.Index]);
            string val = (DataSource.Select(DisplayMember + "='" + txt + "'"))[0][ValueMember].ToString();

            if (e.NewValue == CheckState.Checked)
            {

                if (!SelectedCheck.ContainsKey(val))
                {
                    SelectedCheck.Add(val, txt);
                }

            }
            else
            {
                if (SelectedCheck.ContainsKey(val))
                {
                    SelectedCheck.Remove(val);
                }
            }

            this.Invoke(new Action(() =>
            {
                this.Text = SelectedCheck.Count == 0 ? "" : "已选择" + SelectedCheck.Count.ToString() + "项";
                txtstr = this.Text;
            }));

            if (ComboBoxListEx_Selected != null)
            {
                ComboBoxListEx_Selected(this, e);
            }
        }

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

        /// <summary>
        /// 全选或者取消全选
        /// </summary>
        /// <param name="Checked"></param>
        public void CancelChecked(bool Checked)
        {
            for (int i = 0, cnt = mCheckedListBox.Items.Count; i < cnt; i++)
            {
                mCheckedListBox.SetItemChecked(i, Checked);
            }
        }
    }
}