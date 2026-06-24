using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;

namespace ZCS_FormUI.Controls
{
    public partial class DataGridViewEx : DataGridView
    {
        private CheckedListBox mCheckedListBox;
        private ToolStripDropDown mPopup;
        private Button btnSaveWidth;


        //ds为列是否显示
        private DataSet ds;
        //dswidth为列宽
        private DataSet dswidth;
        //dsindex为列顺序
        private DataSet dsindex;
        public DataGridViewEx dgvTotalRow;
        private int heightx = 0;
        private int locationY = 0;


        #region 属性

        private Form _myForm;
        /// <summary>
        /// 如果手动添加列需设置属性才能实现列显示隐藏功能
        /// </summary>
        [Category("Custom"), Description("如果手动添加列需设置属性才能实现列显示隐藏功能")]
        public Form MyForm
        {
            get { return _myForm == null ? this.FindForm() : _myForm; }
            set { _myForm = value; }
        }

        private string _username = "SystemUserName";

        /// <summary>
        /// 用户名，如不设置，则为系统用户“SysUserName”
        /// </summary>
        [Category("Custom"), Description("用户名，如不设置，则为系统用户“SysUserName”")]
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        private string _userid = "";
        /// <summary>
        /// 用户名，必须设置
        /// </summary>
        [Category("Custom"), Description("用户ID，请在代码中设置")]
        public string UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }

        private bool _columnVisibleEnableSave = true;
        /// <summary>
        /// 设置是否启用是否设置保存显示column功能
        /// </summary>
        [Category("Custom"), Description("设置是否启用是否设置保存显示column功能")]
        public bool ColumnVisibleEnableSave
        {
            get { return _columnVisibleEnableSave; }
            set { _columnVisibleEnableSave = value; }
        }

        /// <summary>
        /// 设置是否启用是否设置保存column  Width功能
        /// </summary>
        private bool _columnWidthSave = true;
        [Category("Custom"), Description("设置是否启用是否设置保存column  Width功能")]
        public bool ColumnWidthSave
        {
            get { return _columnWidthSave; }
            set { _columnWidthSave = value; }
        }

        /// <summary>
        /// 设置是否启用是否设置保存column  DisplayIndex功能
        /// </summary>
        private bool _columnDisplayIndexSave = false;
        [Category("Custom"), Description("设置是否启用是否设置保存column  DisplayIndex功能")]
        public bool ColumnDisplayIndexSave
        {
            get { return _columnDisplayIndexSave; }
            set { _columnDisplayIndexSave = value; }
        }

        private bool _showdgvTotalRow;
        /// <summary>
        /// 汇总行是否显示
        /// </summary>
        [Category("Custom"), Description("汇总行是否显示！")]
        public bool ShowdgvTotalRow
        {
            get { return _showdgvTotalRow; }
            set { _showdgvTotalRow = value; }
        }

        private int showTotal = 0;
        /// <summary>
        /// 合记标题显示在第几列
        /// </summary>
        [Category("Custom"), Description("合记标题显示在第几列！")]
        public int ShowTotal
        {
            get
            {
                return this.showTotal;
            }
            set
            {
                this.showTotal = value;
            }
        }


        private IList<string> sumColumnList;
        /// <summary>
        /// 汇总列！IList<string>
        /// </summary>
        [Category("Custom"), Browsable(false), Description("汇总列！IList<string>")]
        public IList<string> SumColumnList
        {
            get
            {
                return this.sumColumnList;
            }
            set
            {
                this.sumColumnList = value;
            }
        }

        private string sumField = "";
        /// <summary>
        /// 汇总列！用“，”分隔！
        /// </summary>
        [Category("Custom"), Description("汇总列！用“，”分隔！")]
        public string SumField
        {
            get
            {
                this.sumColumnList = this.sumField.Split(',');
                return this.sumField;
            }
            set
            {
                this.sumField = value;
                this.sumColumnList = value.Split(',');
            }
        }

        #endregion

        public DataGridViewEx()
        {
            //设计添加控时，修改默认值为DisableResizing
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            InitializeComponent();
            
         
        }

        public DataGridViewEx(IContainer container)
        {
            container.Add(this);
            //设计添加控时，修改默认值为DisableResizing
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            InitializeComponent();
            //InitDgvTotalRow();
            Init(); 
        }

        private void Init()
        {
            //this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //this.BackgroundColor = System.Drawing.SystemColors.Control;
            this.AllowUserToOrderColumns = true;
            //表头居中对其
            System.Windows.Forms.DataGridViewCellStyle DGVCSColumn = new System.Windows.Forms.DataGridViewCellStyle();
            DGVCSColumn.BackColor = System.Drawing.SystemColors.Control;
            DGVCSColumn.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            DGVCSColumn.ForeColor = System.Drawing.Color.FromArgb(114, 58, 0); ;
            DGVCSColumn.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            DGVCSColumn.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            DGVCSColumn.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            DGVCSColumn.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnHeadersDefaultCellStyle = DGVCSColumn;


            //行字体颜色
            System.Windows.Forms.DataGridViewCellStyle DGVCSRows = new System.Windows.Forms.DataGridViewCellStyle();
            DGVCSRows.Font = new System.Drawing.Font("宋体", 10F);
            DGVCSRows.ForeColor = Color.FromArgb(51, 51, 51);
            this.RowsDefaultCellStyle = DGVCSRows;
            //行边框颜色
            this.GridColor = Color.FromArgb(242, 233, 223);

            //this.RowHeadersVisible = false;
            //默认创建ColumnHeadersHeightSizeMode值代码为autosize，会覆盖此属性，所以将代码置于控件初始化前解决默认值
            //this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.RowTemplate.Height = 27;
            this.ColumnHeadersHeight = 31;

            this.BackgroundColor = Color.FromArgb(250, 245, 241);
            this.BorderStyle = BorderStyle.None;
            //隔行变色
            System.Windows.Forms.DataGridViewCellStyle DGVCS2 = new System.Windows.Forms.DataGridViewCellStyle();
            DGVCS2.BackColor = Color.FromArgb(246, 236, 230);
            this.AlternatingRowsDefaultCellStyle = DGVCS2;


            mCheckedListBox = new CheckedListBox();
            mCheckedListBox.CheckOnClick = true;
            mCheckedListBox.ItemCheck += new ItemCheckEventHandler(mCheckedListBox_ItemCheck);

            ToolStripControlHost mControlHost = new ToolStripControlHost(mCheckedListBox);
            mControlHost.Padding = Padding.Empty;
            mControlHost.Margin = Padding.Empty;
            mControlHost.AutoSize = false;

            btnSaveWidth = new Button();
            btnSaveWidth.Text = "保存设置";
            btnSaveWidth.AutoSize = true;
            //btnSaveWidth.FlatStyle = FlatStyle.Flat;
            //btnSaveWidth.FlatAppearance.BorderSize = 0;
            btnSaveWidth.BackColor = Color.White;

            btnSaveWidth.Click += new EventHandler(btnSaveWidth_Click);

            ToolStripControlHost mControlBtn = new ToolStripControlHost(btnSaveWidth);
            mControlBtn.Padding = new Padding(0);
            mControlBtn.Margin = new Padding(1);
            mControlBtn.AutoSize = true;

            mPopup = new ToolStripDropDown();
            mPopup.Padding = Padding.Empty;
            mPopup.Items.Add(mControlBtn);
            mPopup.Items.Add(mControlHost);

            this.CellMouseClick += new DataGridViewCellMouseEventHandler(DataGridViewEx_CellMouseClick);
            this.ColumnAdded += new DataGridViewColumnEventHandler(DataGridViewEx_ColumnAdded);

            this.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridViewEx_CellPainting);

            this.Scroll += new ScrollEventHandler(DataGridViewEx_Scroll);
            this.ColumnWidthChanged += new DataGridViewColumnEventHandler(DataGridViewEx_ColumnWidthChanged);
            this.Layout += new LayoutEventHandler(DataGridViewEx_Layout);
            this.Resize += new EventHandler(DataGridViewEx_Resize);
            this.LocationChanged += new EventHandler(DataGridViewEx_LocationChanged);

            this.CellFormatting += new DataGridViewCellFormattingEventHandler(DataGridViewEx_CellFormatting);

            this.ColumnStateChanged += new DataGridViewColumnStateChangedEventHandler(DataGridViewEx_ColumnStateChanged);
            //this.ColumnDisplayIndexChanged += new DataGridViewColumnEventHandler(DataGridViewEx_ColumnDisplayIndexChanged);

            this.DataBindingComplete -= new DataGridViewBindingCompleteEventHandler(DataGridViewEx_DataBindingComplete);
            this.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(DataGridViewEx_DataBindingComplete);

            this.RowsAdded += new DataGridViewRowsAddedEventHandler(DataGridViewEx_RowsAdded);
            this.RowsRemoved += new DataGridViewRowsRemovedEventHandler(DataGridViewEx_RowsRemoved);
            this.CellValueChanged += new DataGridViewCellEventHandler(DataGridViewEx_CellValueChanged);
            this.RowHeadersWidthChanged += new EventHandler(DataGridViewEx_RowHeadersWidthChanged);
        }



        void DataGridViewEx_LocationChanged(object sender, EventArgs e)
        {
            if (ShowdgvTotalRow && this.heightx != 0)
            {
                if (this.Location.Y > locationY)
                {
                    this.Height = this.Height - (this.Location.Y - locationY);
                }
                else
                {
                    this.Height = this.Height + (locationY - this.Location.Y);
                }
                locationY = this.Location.Y;
            }
        }

        void DataGridViewEx_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ShowdgvTotalRow && this.dgvTotalRow != null)
            {
                GetSumTotal();
                this.SetScroll();
            }
        }

        void DataGridViewEx_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (ShowdgvTotalRow)
            {
                GetSumTotal();
                this.SetScroll();
            }
        }

        void DataGridViewEx_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (ShowdgvTotalRow)
            {
                if (this.AllowUserToAddRows)
                {
                    if (e.RowIndex > 0)
                    {
                        if (this.dgvTotalRow != null)
                        {
                            GetSumTotal();
                            this.SetScroll();
                        }
                        else
                        {
                            ShowDgvTotal();
                        }
                    }
                }
                else
                {
                    if (this.dgvTotalRow != null)
                    {
                        GetSumTotal();
                        this.SetScroll();
                    }
                    else
                    {
                        ShowDgvTotal();
                    }
                }
            }

        }




        /// <summary>
        /// 加载汇总行
        /// </summary>
        private void InitDgvTotalRow()
        {
            if (this.dgvTotalRow != null)
            {
                return;
            }
            this.dgvTotalRow = new DataGridViewEx();

            this.dgvTotalRow.Hide();
            this.dgvTotalRow.ScrollBars = ScrollBars.None;
            this.dgvTotalRow.ColumnHeadersVisible = false;
            this.dgvTotalRow.AllowUserToAddRows = false;
            this.dgvTotalRow.AllowUserToDeleteRows = false;
            this.dgvTotalRow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTotalRow.Name = "dgvTotalRow";
            this.dgvTotalRow.RowTemplate.Height = 23;
            this.dgvTotalRow.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            //System.Windows.Forms.DataGridViewCellStyle DGVCSColumn = new System.Windows.Forms.DataGridViewCellStyle();
            //DGVCSColumn.BackColor = System.Drawing.SystemColors.Control;
            //DGVCSColumn.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //DGVCSColumn.ForeColor = System.Drawing.Color.FromArgb(114, 58, 0); ;
            //DGVCSColumn.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            //DGVCSColumn.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            //DGVCSColumn.WrapMode = System.Windows.Forms.DataGridViewTriState.False;

            //this.dgvTotalRow.DefaultCellStyle = DGVCSColumn;
            this.dgvTotalRow.DefaultCellStyle.ForeColor = this.BackgroundColor;
            this.dgvTotalRow.DefaultCellStyle.ForeColor = this.ForeColor;

            this.dgvTotalRow.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            this.dgvTotalRow.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            this.dgvTotalRow.AdvancedCellBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

            //行字体颜色
            System.Windows.Forms.DataGridViewCellStyle DGVCSRows = new System.Windows.Forms.DataGridViewCellStyle();
            DGVCSRows.Font = new Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            DGVCSRows.ForeColor = Color.FromArgb(51, 51, 51);
            DGVCSRows.BackColor = Color.FromArgb(246, 236, 230);
            this.dgvTotalRow.RowsDefaultCellStyle = DGVCSRows;
            this.dgvTotalRow.AllowUserToResizeColumns = false;

            //this.dgvTotalRow.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvTotalRow.BackgroundColor = Color.FromArgb(250, 245, 241);
            this.dgvTotalRow.BorderStyle = BorderStyle.None;

            //this.dgvTotalRow.GridColor = Color.FromArgb(242, 233, 223);
            this.dgvTotalRow.GridColor = Color.FromArgb(213, 223, 219);

            this.dgvTotalRow.RowHeadersVisible = false;
            this.dgvTotalRow.Enabled = false;
            ((System.ComponentModel.ISupportInitialize)(dgvTotalRow)).EndInit();


            this.dgvTotalRow.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dgvTotalRow_RowPostPaint);
        }

        void dgvTotalRow_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex >= this.dgvTotalRow.FirstDisplayedScrollingRowIndex)
            {
                using (SolidBrush b = new SolidBrush(dgvTotalRow.RowHeadersDefaultCellStyle.ForeColor))
                {
                    e.Graphics.DrawString("合计：", e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + 5);
                    
                }
            }
        }

        #region 事件



        //保存列显示顺序
        void DataGridViewEx_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (ShowdgvTotalRow && this.dgvTotalRow != null)
            {
                for (int i = 0; i < this.ColumnCount; i++)
                {
                    this.dgvTotalRow.Columns[i].DisplayIndex = this.Columns[i].DisplayIndex;
                }
                this.SetScroll();
            }

            if (ColumnDisplayIndexSave)
            {
                DataTable dtindex = new DataTable();
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    dtindex.Columns.Add(this.Columns[i].Name);
                }
                //行数据
                object[] rowindex = new object[this.Columns.Count];

                for (int k = 0; k < this.Columns.Count; k++)
                {
                    rowindex[k] = this.Columns[k].DisplayIndex;
                }
                dtindex.Rows.Add(rowindex);

                string tagstr = MyForm == null ? "" : MyForm.Tag == null ? "" : MyForm.Tag.ToString();
                string myformname = MyForm == null ? "" : MyForm.GetType().FullName;

                //没有判断多个传参中包含，字符导致无法正常保存 hxq注释
                //string nodenameindex = (myformname + tagstr + this.Name + "Index").Replace(".", "");
                string nodenameindex = (myformname + tagstr + this.Name + "Index").Replace(".", "").Replace(",", "");

                //保存列位置
                if (!string.IsNullOrEmpty(UserID))
                {
                    DataGridViewConfig.WriteDGVConfig(UserName, nodenameindex, dtindex, UserID);
                    dsindex = DataGridViewConfig.ReadDGVConfig(UserName, nodenameindex, UserID);
                }
            }
        }


        void DataGridViewEx_RowHeadersWidthChanged(object sender, EventArgs e)
        {
            if (ShowdgvTotalRow && this.dgvTotalRow != null)
            {
                this.dgvTotalRow.RowHeadersWidth = this.RowHeadersWidth;
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    this.dgvTotalRow.Columns[i].Width = this.Columns[i].Width;
                }
                this.dgvTotalRow.HorizontalScrollingOffset = this.HorizontalScrollingOffset;

                this.SetScroll();
            }
        }

        void DataGridViewEx_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (ShowdgvTotalRow && this.dgvTotalRow != null)
            {
                this.dgvTotalRow.RowHeadersWidth = this.RowHeadersWidth;
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    this.dgvTotalRow.Columns[i].Width = this.Columns[i].Width;
                }
                this.dgvTotalRow.HorizontalScrollingOffset = this.HorizontalScrollingOffset;

                this.SetScroll();
            }

        }

        void DataGridViewEx_Scroll(object sender, ScrollEventArgs e)
        {
            if (ShowdgvTotalRow && this.dgvTotalRow != null)
            {
                if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                {
                    this.dgvTotalRow.HorizontalScrollingOffset = e.NewValue;
                }
                if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    this.VerticalScrollBar.BringToFront();
                }
                this.SetScroll();
            }
        }

        void DataGridViewEx_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ShowDgvTotal();

            ShowColumnDisplayIndex();
        }

        /// <summary>
        /// 按配置顺序显示列
        /// </summary>
        [Description("按配置顺序显示列")]
        public void ShowColumnDisplayIndex()
        {
            #region 列显示顺序
            if (ColumnDisplayIndexSave)
            {
                if (dsindex == null || dsindex.Tables.Count <= 0 || dsindex.Tables[0].Rows.Count <= 0)
                {

                    string tagstr = MyForm == null ? "" : MyForm.Tag == null ? "" : MyForm.Tag.ToString();
                    string myformname = MyForm == null ? "" : MyForm.GetType().FullName;

                    //没有判断多个传参中包含，字符导致无法正常保存 hxq注释
                    //string nodename = (myformname + tagstr + this.Name + "Index").Replace(".", "");
                    string nodename = (myformname + tagstr + this.Name + "Index").Replace(".", "").Replace(",", "");
                    if (!string.IsNullOrEmpty(UserID))
                        dsindex = DataGridViewConfig.ReadDGVConfig(UserName, nodename,UserID);

                }
                if (dsindex != null)
                {
                    foreach (DataRow dr in dsindex.Tables[0].Rows)
                    {
                        foreach (DataColumn col in dsindex.Tables[0].Columns)
                        {

                            if (this.Columns.Contains(col.ColumnName))
                            {
                                //if (int.Parse(dr[col.ColumnName].ToString()) < this.Columns.Count)
                                //{
                                    try
                                    {
                                        this.Columns[col.ColumnName].DisplayIndex = int.Parse(dr[col.ColumnName].ToString());
                                    }
                                    catch
                                    {
                                        break;
                                    }
                                //}
                            }
                        }
                    }
                }
            }

            if (ColumnVisibleEnableSave)
            {
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    string tagstr = MyForm == null ? "" : MyForm.Tag == null ? "" : MyForm.Tag.ToString();
                    string myformname = MyForm == null ? "" : MyForm.GetType().FullName;

                    //没有判断多个传参中包含，字符导致某些情况下无法正常保存 hxq注释
                    //string nodename = (myformname + tagstr + this.Name).Replace(".", "");
                    string nodename = (myformname + tagstr + this.Name).Replace(".", "").Replace(",", "");
                    if (!string.IsNullOrEmpty(UserID))
                        ds = DataGridViewConfig.ReadDGVConfig(UserName, nodename, UserID);
                }
                if (ds != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {

                            if (this.Columns.Contains(col.ColumnName))
                            {
                                //this.Invoke(new Action(delegate
                                //{
                                    this.Columns[col.ColumnName].Visible = bool.Parse(dr[col.ColumnName].ToString());
                                //}));
                            }
                        }
                    }
                }

            }
            #endregion
        }


        void DataGridViewEx_Resize(object sender, EventArgs e)
        {
            if (ShowdgvTotalRow)
            {
                this.SetScroll();
            }
        }

        void DataGridViewEx_Layout(object sender, LayoutEventArgs e)
        {
            if (ShowdgvTotalRow)
            {
                this.SetScroll();
            }
        }


        void DataGridViewEx_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            if (ShowdgvTotalRow && this.dgvTotalRow != null && dgvTotalRow.Columns.Count > 0)
            {
                for (int i = 0; i < this.ColumnCount; i++)
                {
                    this.dgvTotalRow.Columns[i].Visible = this.Columns[i].Visible;
                }
                this.SetScroll();
            }

            #region 设置列宽
            if (ColumnWidthSave)
            {
                if (dswidth == null || dswidth.Tables.Count <= 0 || dswidth.Tables[0].Rows.Count <= 0)
                {

                    string tagstr = MyForm == null ? "" : MyForm.Tag == null ? "" : MyForm.Tag.ToString();
                    string myformname = MyForm == null ? "" : MyForm.GetType().FullName;

                    //没有判断多个传参中包含，字符导致无法正常保存 hxq注释
                    //string nodename = (myformname + tagstr + this.Name + "Width").Replace(".", "");
                    string nodename = (myformname + tagstr + this.Name + "Width").Replace(".", "").Replace(",", "");
                    if (!string.IsNullOrEmpty(UserID))
                        dswidth = DataGridViewConfig.ReadDGVConfig(UserName, nodename,UserID);

                }
                if (dswidth != null)
                {
                    this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    foreach (DataRow dr in dswidth.Tables[0].Rows)
                    {
                        foreach (DataColumn col in dswidth.Tables[0].Columns)
                        {
                            if (e.Column.Name == col.ColumnName)
                            {
                                e.Column.Width = int.Parse(dr[col.ColumnName].ToString());
                            }
                        }
                    }
                }


            }
            #endregion
        }


        /// <summary>
        /// 列添加事件,设置列是否显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DataGridViewEx_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (ColumnVisibleEnableSave)
            {
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    string tagstr = MyForm == null ? "" : MyForm.Tag == null ? "" : MyForm.Tag.ToString();
                    string myformname = MyForm == null ? "" : MyForm.GetType().FullName;

                    //没有判断多个传参中包含，字符导致某些情况下无法正常保存 hxq注释
                    //string nodename = (myformname + tagstr + this.Name).Replace(".", "");
                    string nodename = (myformname + tagstr + this.Name).Replace(".", "").Replace(",", "");
                    if (!string.IsNullOrEmpty(UserID))
                        ds = DataGridViewConfig.ReadDGVConfig(UserName, nodename,UserID);
                }
                if (ds != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            if (e.Column.Name == col.ColumnName)
                            {
                                e.Column.Visible = bool.Parse(dr[col.ColumnName].ToString());
                            }
                        }
                    }
                }

            }

            if (ColumnWidthSave)
            {
                if (dswidth == null || dswidth.Tables.Count <= 0 || dswidth.Tables[0].Rows.Count <= 0)
                {
                    string tagstr = MyForm == null ? "" : MyForm.Tag == null ? "" : MyForm.Tag.ToString();
                    string myformname = MyForm == null ? "" : MyForm.GetType().FullName;

                    //没有判断多个传参中包含，字符导致某些情况下无法正常保存 hxq注释
                    //string nodename = (myformname + tagstr + this.Name + "Width").Replace(".", "");
                    string nodename = (myformname + tagstr + this.Name + "Width").Replace(".", "").Replace(",", "");
                    if (!string.IsNullOrEmpty(UserID))
                        dswidth = DataGridViewConfig.ReadDGVConfig(UserName, nodename,UserID);
                }
                if (dswidth != null)
                {
                    this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    foreach (DataRow dr in dswidth.Tables[0].Rows)
                    {
                        foreach (DataColumn col in dswidth.Tables[0].Columns)
                        {
                            if (e.Column.Name == col.ColumnName)
                            {
                                e.Column.Width = int.Parse(dr[col.ColumnName].ToString());
                            }
                        }
                    }
                }

            }

        }

        /// <summary>
        /// Cell鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DataGridViewEx_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ColumnVisibleEnableSave)
            {
                if (e.Button == MouseButtons.Right && e.RowIndex == -1 && e.ColumnIndex == -1)
                {
                    mCheckedListBox.Items.Clear();
                    foreach (DataGridViewColumn c in this.Columns)
                    {
                        mCheckedListBox.Items.Add(c.HeaderText, c.Visible);
                    }
                    int PreferredHeight = (mCheckedListBox.Items.Count * 20) + 7;
                    mCheckedListBox.Height = (PreferredHeight < 200) ?
                                PreferredHeight : 200;
                    mPopup.Show(this.PointToScreen(new Point(e.X, e.Y)));
                }
            }
        }

        /// <summary>
        /// CheckedListBox_ItemCheck事件,保存设置是否显示列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void mCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ColumnVisibleEnableSave)
            {
                this.Columns[e.Index].Visible = (e.NewValue == CheckState.Checked);

                DataTable dt = new DataTable();
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    dt.Columns.Add(this.Columns[i].Name);
                }
                //行数据
                object[] row = new object[this.Columns.Count];
                for (int k = 0; k < this.Columns.Count; k++)
                {
                    row[k] = this.Columns[k].Visible.ToString();
                }
                dt.Rows.Add(row);

                string tagstr = MyForm == null ? "" : MyForm.Tag == null ? "" : MyForm.Tag.ToString();
                string myformname = MyForm == null ? "" : MyForm.GetType().FullName;

                //没有判断多个传参中包含，字符导致某些情况下无法正常保存 hxq注释
                //string nodename = (myformname + tagstr + this.Name).Replace(".", "");
                string nodename = (myformname + tagstr + this.Name).Replace(".", "").Replace(",", "");
                if (!string.IsNullOrEmpty(UserID))
                {
                    DataGridViewConfig.WriteDGVConfig(UserName, nodename, dt, UserID);
                    ds = DataGridViewConfig.ReadDGVConfig(UserName, nodename, UserID);
                }

            }
        }


        /// <summary>
        /// 表头checkbox的单元格改变事件
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        internal void OnCheckBoxCellCheckedChange(int columnIndex, int rowIndex, bool value)
        {
            bool existsChecked = false, existsNoChecked = false;
            DataGridViewCheckBoxCellEx cellEx;
            foreach (DataGridViewRow row in this.Rows)
            {
                cellEx = row.Cells[columnIndex] as DataGridViewCheckBoxCellEx;
                if (cellEx == null) return;
                existsChecked |= cellEx.Checked;
                existsNoChecked |= !cellEx.Checked;
            }

            DataGridViewCheckBoxColumnHeaderCellEx headerCellEx =
                this.Columns[columnIndex].HeaderCell as DataGridViewCheckBoxColumnHeaderCellEx;

            if (headerCellEx == null) return;

            CheckState oldState = headerCellEx.CheckedAllState;

            if (existsChecked)
                headerCellEx.CheckedAllState = existsNoChecked ? CheckState.Indeterminate : CheckState.Checked;
            else
                headerCellEx.CheckedAllState = CheckState.Unchecked;

            if (oldState != headerCellEx.CheckedAllState)
                this.InvalidateColumn(columnIndex);
            //MessageBox.Show(this.);
        }

        /// <summary>
        /// 全选中/取消全选中
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="isCheckedAll"></param>
        internal void OnCheckAllCheckedChange(int columnIndex, bool isCheckedAll)
        {
            DataGridViewCheckBoxCellEx cellEx;
            foreach (DataGridViewRow row in this.Rows)
            {
                cellEx = row.Cells[columnIndex] as DataGridViewCheckBoxCellEx;
                if (cellEx == null) continue;
                cellEx.Checked = isCheckedAll;
            }
        }


        /// <summary>
        /// 保列宽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSaveWidth_Click(object sender, EventArgs e)
        {
            #region 保存列宽
            if (ColumnWidthSave)
            {

                DataTable dt = new DataTable();
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    dt.Columns.Add(this.Columns[i].Name);
                }
                //行数据
                object[] row = new object[this.Columns.Count];

                for (int k = 0; k < this.Columns.Count; k++)
                {
                    row[k] = this.Columns[k].Width.ToString();
                }
                dt.Rows.Add(row);

                string tagstr = MyForm == null ? "" : MyForm.Tag == null ? "" : MyForm.Tag.ToString();
                string myformname = MyForm == null ? "" : MyForm.GetType().FullName;

                //没有判断多个传参中包含，字符导致某些情况下无法正常保存 hxq注释
                //string nodename = (myformname + tagstr + this.Name + "Width").Replace(".", "");
                string nodename = (myformname + tagstr + this.Name + "Width").Replace(".", "").Replace(",", "");

                //保存列宽
                if (!string.IsNullOrEmpty(UserID))
                {
                    DataGridViewConfig.WriteDGVConfig(UserName, nodename, dt, UserID);
                    dswidth = DataGridViewConfig.ReadDGVConfig(UserName, nodename, UserID);
                }
            }
            #endregion

            #region 保存显示顺序
            if (ColumnDisplayIndexSave)
            {
                DataTable dtindex = new DataTable();
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    dtindex.Columns.Add(this.Columns[i].Name);
                }
                //行数据
                object[] rowindex = new object[this.Columns.Count];

                for (int k = 0; k < this.Columns.Count; k++)
                {
                    rowindex[k] = this.Columns[k].DisplayIndex;
                }
                dtindex.Rows.Add(rowindex);

                string tagstr = MyForm == null ? "" : MyForm.Tag == null ? "" : MyForm.Tag.ToString();
                string myformname = MyForm == null ? "" : MyForm.GetType().FullName;

                //没有判断多个传参中包含，字符导致某些情况下无法正常保存 hxq注释
                //string nodenameindex = (myformname + tagstr + this.Name + "Index").Replace(".", "");
                string nodenameindex = (myformname + tagstr + this.Name + "Index").Replace(".", "").Replace(",", "");

                //保存列位置
                if (!string.IsNullOrEmpty(UserID))
                {
                    DataGridViewConfig.WriteDGVConfig(UserName, nodenameindex, dtindex, UserID);
                    dsindex = DataGridViewConfig.ReadDGVConfig(UserName, nodenameindex, UserID);
                }
            }
            #endregion

            MessageBox.Show("保存成功！");


            if (ShowdgvTotalRow && this.dgvTotalRow != null)
            {
                for (int i = 0; i < this.ColumnCount; i++)
                {
                    this.dgvTotalRow.Columns[i].DisplayIndex = this.Columns[i].DisplayIndex;
                }
                this.SetScroll();
            }


        }

        void DataGridViewEx_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is string)
                e.Value = e.Value.ToString().Trim();
            if (e.Value is Image)
                e.Value = e.Value.ToString().Trim();
        }

        void dataGridViewEx_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                bool mouseOver = e.CellBounds.Contains(this.PointToClient(Cursor.Position));
                Color color1 = Color.FromArgb(254, 238, 215);
                Color color2 = Color.FromArgb(255, 242, 225);
                Color color3 = Color.FromArgb(255, 177, 0);
                LinearGradientBrush brush = new LinearGradientBrush(
                    e.CellBounds,
                    color2,
                    mouseOver ? color3 : color1,
                    LinearGradientMode.Vertical);
                using (brush)
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                    Rectangle border = e.CellBounds;
                    border.Width -= 1;
                    //边框颜色
                    Color color4 = Color.FromArgb(242, 233, 223);
                    Pen pen = new Pen(color4, 1);
                    e.Graphics.DrawRectangle(pen, border);
                }
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }

        }

        #endregion

        #region private 方法   针对汇总
        /// <summary>
        ///显示汇合行
        /// </summary>
        private void ShowDgvTotal()
        {
            if (ShowdgvTotalRow)
            {
                InitDgvTotalRow();

                if (heightx != this.Height)
                {
                    if (heightx == 0)
                    {
                        if (this.HorizontalScrollBar.Visible)
                        {

                            heightx = this.Height;
                        }
                        else
                        {
                            heightx = this.Height;
                        }
                        Point p = this.Location;
                        locationY = this.Location.Y;
                        int w = this.Width;
                        this.Dock = DockStyle.Top;
                        this.Location = p;
                        this.Width = w;
                        this.Height = heightx;
                    }
                    else
                    {
                        if (heightx < this.Height && !this.VerticalScrollBar.Visible)
                        {
                            heightx = this.Height;
                        }
                        else if (this.HorizontalScrollBar.Visible && !this.VerticalScrollBar.Visible)
                        {
                            heightx = this.Height;
                            
                        }
                        else if(this.HorizontalScrollBar.Visible && this.VerticalScrollBar.Visible)
                        {
                            heightx = heightx - 25;
                        }
                        Point p = this.Location;
                        locationY = this.Location.Y;
                        int w = this.Width;
                        this.Dock = DockStyle.Top;
                        this.Location = p;
                        this.Width = w;
                        this.Height = heightx;
                    }
                }

                this.dgvTotalRow.ScrollBars = ScrollBars.None;
                this.dgvTotalRow.ColumnHeadersVisible = false;
                this.dgvTotalRow.AllowUserToAddRows = false;
                this.dgvTotalRow.AllowUserToDeleteRows = false;
                DataGridViewColumn[] columnArray = new DataGridViewColumn[this.ColumnCount];
                DataTable table = new DataTable();
                DataRow row = table.NewRow();
                for (int i = 0; i < this.ColumnCount; i++)
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.Name = this.Columns[i].Name;
                    columnArray[i] = column;
                    DataColumn column2 = new DataColumn(this.Columns[i].Name);
                    table.Columns.Add(column2);
                    row[this.Columns[i].Name] = "";
                }
                table.Rows.Add(row);
                this.dgvTotalRow.DataSource = table;
                if (this.dgvTotalRow.RowCount > 0)
                {
                    this.dgvTotalRow.Rows[0].Selected = false;
                }




                //this.Height = this.Height - 25;
                this.dgvTotalRow.Size = new System.Drawing.Size(this.Width - this.VerticalScrollBar.Width, 25);

                this.dgvTotalRow.Parent = this.Parent;

                //this.FindForm().Controls.Add(dgvTotalRow);

                this.dgvTotalRow.BringToFront();


                this.dgvTotalRow.Show();

                this.SetLastRowColor();
                this.GetSumTotal();
                this.SetScroll();

            }
        }

        /// <summary>
        /// 确定位置
        /// </summary>
        private void SetScroll()
        {
            if (this.dgvTotalRow == null) { return; }
            this.dgvTotalRow.RowHeadersVisible = this.RowHeadersVisible;
            
            this.dgvTotalRow.Width = this.Width - this.VerticalScrollBar.Width;
            this.dgvTotalRow.RowHeadersDefaultCellStyle.Padding = new Padding(this.dgvTotalRow.RowHeadersWidth);
            this.dgvTotalRow.Refresh();

            this.VerticalScrollBar.Visible = this.Height < 50 ? true : this.VerticalScrollBar.Visible;
            if (this.VerticalScrollBar.Visible && !this.HorizontalScrollBar.Visible)
            {
                this.AllowUserToAddRows = true;
                //this.Height = heightx + 25;
                this.dgvTotalRow.Location = new Point(this.Location.X, this.Location.Y + this.Height-25);
            }
            else if (!this.VerticalScrollBar.Visible && this.HorizontalScrollBar.Visible)
            {
                this.AllowUserToAddRows = false;
                //this.dgvTotalRow.Location = new Point(this.Location.X, this.Location.Y + this.Height);
                this.dgvTotalRow.Location = new Point(this.Location.X, this.Location.Y + this.Height - this.HorizontalScrollBar.Height - 25);

            }
            else if (this.VerticalScrollBar.Visible && this.HorizontalScrollBar.Visible)
            {
                this.dgvTotalRow.Location = new Point(this.Location.X, this.Location.Y + this.Height - this.HorizontalScrollBar.Height - 25);
            }
            else
            {
                this.dgvTotalRow.Location = new Point(this.Location.X, this.Location.Y + this.Height-25);
            }
        }


        private void SetLastRowColor()
        {
            if (this.RowCount > 0)
            {
                //this.Rows[this.RowCount - 1].DefaultCellStyle.SelectionBackColor = this.BackgroundColor;
                //this.Rows[this.RowCount - 1].DefaultCellStyle.SelectionForeColor = this.ForeColor;
                //this.Rows[this.RowCount - 1].DefaultCellStyle.BackColor = this.BackgroundColor;
                //this.Rows[this.RowCount - 1].DefaultCellStyle.ForeColor = this.ForeColor;
            }
        }

        /// <summary>
        /// 获取汇总
        /// </summary>
        private void GetSumTotal()
        {
            decimal num = 0M;
            if (this.SumColumnList != null && this.dgvTotalRow != null && this.dgvTotalRow.Rows.Count > 0)
            {
                foreach (string str in this.SumColumnList)
                {
                    num = 0M;
                    foreach (DataGridViewRow row in (System.Collections.IEnumerable)this.Rows)
                    {
                        if (row.Visible)
                        {
                            num += ((row.Cells[str].Value != null) && (row.Cells[str].Value != DBNull.Value)) ? Convert.ToDecimal(row.Cells[str].Value) : 0M;
                        }
                    }
                    this.dgvTotalRow.Rows[0].Cells[str].Value = num;
                }
                if ((this.ShowTotal >= 0) && (this.ShowTotal < this.dgvTotalRow.ColumnCount))
                {
                    if (!this.RowHeadersVisible)
                    {
                        this.dgvTotalRow.Rows[0].Cells[this.ShowTotal].Value = "合计：";
                    }
                }
                if (this.dgvTotalRow.RowCount > 0)
                {
                    this.dgvTotalRow.Rows[0].Selected = false;
                }
                if (this.dgvTotalRow.Columns.Count > 0)
                {
                    for (int i = 0; i < this.ColumnCount; i++)
                    {
                        this.dgvTotalRow.Columns[i].Width = this.Columns[i].Width;
                        this.dgvTotalRow.Columns[i].Visible = this.Columns[i].Visible;
                        this.dgvTotalRow.Columns[i].DisplayIndex = this.Columns[i].DisplayIndex;
                    }
                    this.dgvTotalRow.HorizontalScrollingOffset = this.HorizontalScrollingOffset;
                }
            }
        }
        #endregion

    }
}
