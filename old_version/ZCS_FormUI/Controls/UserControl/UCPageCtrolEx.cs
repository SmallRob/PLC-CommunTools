using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ZCS_Common;
using ZCS_FormUI.Function;

/// <summary>
/// 分页控件
/// add by 何秀奇 at 2021.4.15
/// </summary>
namespace ZCS_FormUI.Controls
{
    [ToolboxItem(true)]
    public partial class UCPageCtrolEx : UserControl
    {
        public UCPageCtrolEx()
        {
            InitializeComponent();
            txtPage.txtInput.KeyDown += txtInput_KeyDown;
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnToPage_BtnClick(null, null);
                txtPage.InputText = "";
            }
        }

        private int _pageSize = 20;     //每页显示行数
        private int _RecordCount = 0;   //总记录数
        private int _pageCount = 0;     //页数＝总记录数/每页显示行数
        private int _PageIndex = 0;     //当前页号

        private int _startPage = 0;       //当前记录行

        private List<object> dataSource;  //数据源

        /// <summary>
        /// 每页显示行数,默认20，可以设置
        /// </summary>
        [DefaultValue("PageSize"), Category("自定义属性"), Description("每页显示行数,默认20，可以设置")]
        public int PageSize
        {
            set => this._pageSize = value;
            get => this._pageSize;

            /*
        get
        {
            return this._pageSize;
        }
        set
        {
            this._pageSize = value;
            //ResetPageCount();
            List<object> s = GetCurrentSource();
        }
        */
        }

        /// <summary>
        /// 每页显示行数,默认20，可以设置
        /// </summary>
        [DefaultValue("PageCount"), Category("自定义属性"), Description("总分页数")]
        public int PageCount
        {
            set
            {
                this._pageCount = value;
                txtPage.MaxValue = value;
            }
            get => this._pageCount;
        }

        /// <summary>
        /// 当前记录页数
        /// </summary>
        [DefaultValue("StartPage"), Category("自定义属性"), Description("当前记录页数")]
        public int StartPage
        {
            set => this._startPage = value;
            get => this._startPage;
        }

        /// <summary>
        /// 总记录数,必须设置
        /// </summary>
        [DefaultValue("RecordCount"), Category("自定义属性"), Description("总记录数,必须设置")]
        public int RecordCount
        {
            set => this._RecordCount = value;
            get => this._RecordCount;
        }

        /// <summary>
        /// 当前页号，初始时需要设置
        /// </summary>
        [DefaultValue("PageIndex"), Category("自定义属性"), Description("当前页号，初始时需要设置")]
        public int PageIndex
        {
            set => this._PageIndex = value;
            get => this._PageIndex;
        }

        /// <summary>
        /// 关联的数据源
        /// </summary>
        /// <value>The data source.</value>
        [DefaultValue("DataSource"), Category("自定义属性"), Description("需要分页的数据源")]
        public List<object> DataSource
        {
            set => this.dataSource = value;
            get => this.dataSource;
        }


        [Description("更改分页索引事件"), Category("自定义事件")]
        public event EventHandler PageIndexChanged;

        /// <summary>
        /// 重置总页数
        /// </summary>
        private void ResetPageCount()
        {
            if (PageSize > 0)
            {
                if (this.DataSource != null)
                {
                    PageCount = DataSource.Count / PageSize + (DataSource.Count % PageSize > 0 ? 1 : 0);
                }
            }
            txtPage.MaxValue = PageCount;
            txtPage.MinValue = 1;
            ReloadPage();
        }

        /// <summary>
        /// Reloads the page.
        /// </summary>
        private void ReloadPage()
        {
            try
            {
                ControlHelper.FreezeControl(tableLayoutPanel1, true);
                List<int> lst = new List<int>();

                if (PageCount <= 9)
                {
                    for (int i = 1; i <= PageCount; i++)
                    {
                        lst.Add(i);
                    }
                }
                else
                {
                    if (this.PageIndex <= 6)
                    {
                        for (int i = 1; i <= 7; i++)
                        {
                            lst.Add(i);
                        }
                        lst.Add(-1);
                        lst.Add(PageCount);
                    }
                    else if (this.PageIndex > PageCount - 6)
                    {
                        lst.Add(1);
                        lst.Add(-1);
                        for (int i = PageCount - 6; i <= PageCount; i++)
                        {
                            lst.Add(i);
                        }
                    }
                    else
                    {
                        lst.Add(1);
                        lst.Add(-1);
                        int begin = PageIndex - 2;
                        int end = PageIndex + 2;
                        if (end > PageCount)
                        {
                            end = PageCount;
                            begin = end - 4;
                            if (PageIndex - begin < 2)
                            {
                                begin = begin - 1;
                            }
                        }
                        else if (end + 1 == PageCount)
                        {
                            end = PageCount;
                        }
                        for (int i = begin; i <= end; i++)
                        {
                            lst.Add(i);
                        }
                        if (end != PageCount)
                        {
                            lst.Add(-1);
                            lst.Add(PageCount);
                        }
                    }
                }

                for (int i = 0; i < 9; i++)
                {
                    UCBtnExt c = (UCBtnExt)this.tableLayoutPanel1.Controls.Find("p" + (i + 1), false)[0];
                    if (i >= lst.Count)
                    {
                        c.Visible = false;
                    }
                    else
                    {
                        if (lst[i] == -1)
                        {
                            c.BtnText = "...";
                            c.Enabled = false;
                        }
                        else
                        {
                            c.BtnText = lst[i].ToString();
                            c.Enabled = true;
                        }
                        c.Visible = true;
                        if (lst[i] == PageIndex)
                        {
                            c.RectColor = Color.FromArgb(255, 77, 59);
                        }
                        else
                        {
                            c.RectColor = Color.FromArgb(223, 223, 223);
                        }
                    }
                }
                ShowBtn(PageIndex > 1, PageIndex < PageCount);
            }
            finally
            {
                ControlHelper.FreezeControl(tableLayoutPanel1, false);
            }
        }

        /// <summary>
        /// 控制按钮显示
        /// </summary>
        /// <param name="blnLeftBtn">是否显示上一页，第一页</param>
        /// <param name="blnRightBtn">是否显示下一页，最后一页</param>
        protected void ShowBtn(bool blnLeftBtn, bool blnRightBtn)
        {
            btnFirst.Enabled = btnPrevious.Enabled = blnLeftBtn;
            btnNext.Enabled = btnEnd.Enabled = blnRightBtn;
        }

        /// <summary>
        /// 获取当前页数据
        /// </summary>
        /// <returns>List&lt;System.Object&gt;.</returns>
        public virtual List<object> GetCurrentSource()
        {
            if (DataSource == null || DataSource.Count <= 0)
            {
                return null;
            }

            //int intShowCount = _pageSize;
            //if (intShowCount + _startPage > DataSource.Count)
            //{
            //    intShowCount = DataSource.Count - _startPage;
            //}

            //object[] objs = new object[intShowCount];
            //DataSource.CopyTo(_startPage, objs, 0, intShowCount);
            List<object> lst = DataSource; //objs.ToList();

            bool blnLeft = false;
            bool blnRight = false;
            if (lst.Count > 0)
            {
                if (DataSource.IndexOf(lst[0]) > 0)
                {
                    blnLeft = true;
                }
                else
                {
                    blnLeft = false;
                }
                if (DataSource.IndexOf(lst[lst.Count - 1]) >= DataSource.Count - 1)
                {
                    blnRight = false;
                }
                else
                {
                    blnRight = true;
                }
            }
            ShowBtn(blnLeft, blnRight);
            return lst;
        }

        private void PageChangeEvent(object sender, EventArgs e)
        {
            try
            {
                if (PageIndexChanged != null)
                {
                    this.PageIndexChanged(sender, e);
                }
            }
            catch (Exception)
            {

            }
        }


        //分页功能实现
        public void InitDataTable(List<object> ds, int RowCount)
        {
            //dataSource = ds;
            _RecordCount = RowCount;

            if (PageSize <= 0)
            {
                PageSize = 20;
            }

            ReloadPage();

            if (PageIndex == 0)
            {
                PageIndex = 1;    //当前页数从1开始
                StartPage = 0;
            }
            else
            {
                StartPage = (PageIndex - 1) * PageSize;
            }
        }

        /// <summary>
        /// 第一页
        /// </summary>
        public void FirstPage()
        {
            if (PageIndex == 1)
            {
                return;
            }

            PageIndex = 1;
            ReloadPage();

            StartPage = (PageIndex - 1) * PageSize;
            PageChangeEvent(null, new EventArgs());
        }

        /// <summary>
        /// 上一页
        /// </summary>
        public void PreviousPage()
        {
            if (PageIndex <= 1)
            {
                return;
            }
            PageIndex--;
            ReloadPage();

            StartPage = (PageIndex - 1) * PageSize;
            PageChangeEvent(null, new EventArgs());
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public void NextPage()
        {
            if (PageIndex >= PageCount)
            {
                return;
            }
            PageIndex++;
            ReloadPage();

            StartPage = (PageIndex - 1) * PageSize;
            PageChangeEvent(null, new EventArgs());
        }

        /// <summary>
        /// 最后一页
        /// </summary>
        public void EndPage()
        {
            if (PageIndex == PageCount)
            {
                return;
            }

            PageIndex = PageCount;
            ReloadPage();

            StartPage = (PageIndex - 1) * PageSize;
            PageChangeEvent(null, new EventArgs());
        }


        #region 按钮事件
        private void page_BtnClick(object sender, EventArgs e)
        {
            PageIndex = (sender as UCBtnExt).BtnText.ToInt();
            ReloadPage();

            dataSource = GetCurrentSource();

            if (PageIndexChanged != null)
            {
                this.PageIndexChanged(sender, e);
            }
        }

        private void btnPrevious_BtnClick(object sender, EventArgs e)
        {
            PreviousPage();
        }

        private void btnFirst_BtnClick(object sender, EventArgs e)
        {
            FirstPage();
        }

        private void btnNext_BtnClick(object sender, EventArgs e)
        {
            NextPage();
        }

        private void btnEnd_BtnClick(object sender, EventArgs e)
        {
            EndPage();
        }

        private void btnToPage_BtnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPage.InputText))
            {
                PageIndex = txtPage.InputText.ToInt();
                StartPage = (PageIndex - 1) * PageSize;
                ReloadPage();

                PageChangeEvent(null, new EventArgs());
            }
        }
        #endregion
    }
}
