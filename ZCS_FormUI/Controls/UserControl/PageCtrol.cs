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
    [ToolboxItem(true)]
    public partial class PageCtrol : UCPagerControlBase
    {
        public PageCtrol()
        {
            InitializeComponent();
        }
        int _pageSize = 20;     //每页显示行数

        int _RecordCount = 0;         //总记录数

        int pageCount = 0;    //页数＝总记录数/每页显示行数
         
        int _PageIndex = 0;   //当前页号

        int nCurrent = 0;      //当前记录行

        //DataTable _dtInfo = new DataTable();  //存取查询数据结果
        DataTable _bindData = null;

        private DataGridView _dgv = null;

        /// <summary>
        /// 要分页的DataGridView,必填
        /// </summary>
        [DefaultValue("Dgv"), Category("自定义属性"), Description("要分页的DataGridView,必填")]
        public DataGridView Dgv
        {
            set { this._dgv = value; }
            get { return this._dgv; }
        }

        /// <summary>
        /// 每页显示行数,默认20，可以设置
        /// </summary>
        [DefaultValue("PageSize"), Category("自定义属性"), Description("每页显示行数,默认20，可以设置")]
        public int PageSize
        {
            set { this._pageSize = value; }
            get { return this._pageSize; }
        }

        /// <summary>
        /// 总记录数,必须设置
        /// </summary>
        [DefaultValue("RecordCount"), Category("自定义属性"), Description("总记录数,必须设置")]
        public int RecordCount
        {
            set { this._RecordCount = value; }
            get { return this._RecordCount; }
        }

        /// <summary>
        /// 当前页号，初始时需要设置
        /// </summary>
        [DefaultValue("PageIndex"), Category("自定义属性"), Description("当前页号，初始时需要设置")]
        public int PageIndex
        {
            set { this._PageIndex = value; }
            get { return this._PageIndex; }
        }

        [DefaultValue("PageIndex"), Category("自定义属性"), Description("要绑定到DGV的数据DataTable，必须设置")]
        public DataTable BindData
        {
            set { this._bindData = value; }
            get { return this._bindData; }
        }

        [Description("更改页面索引事件"), Category("自定义事件")]
        public event EventHandler PageIndexChanged;


        private void CustomEvent(object sender, EventArgs e)
        {
            try
            {
                this.PageIndexChanged(sender, e);
            }
            catch (Exception)
            {
                //MessageBox.Show("未找到PageIndexChanged事件【此功能暂未开放】！");
            }
        }

        //显示数据方法
        private void LoadData()
        {
            Dgv.DataBindingComplete -= new DataGridViewBindingCompleteEventHandler(Dgv_DataBindingComplete);
            Dgv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(Dgv_DataBindingComplete);
            int nStartPos = 0;   //当前页面开始记录行
            int nEndPos = 0;     //当前页面结束记录行

            //判断查询结果是否为空
            if (BindData == null || BindData.Rows.Count == 0)
            {
                Dgv.DataSource = null;
                tslbCountPage.Text = "0";
                return;
            }
            else
            {
                if (PageIndex == 1)
                {
                    bindingNavigatorMoveFirstPage.Enabled = false;
                    bindingNavigatorMovePreviousPage.Enabled = false;
                }
                else
                {
                    bindingNavigatorMoveFirstPage.Enabled = true;
                    bindingNavigatorMovePreviousPage.Enabled = true;
                }

                if (PageIndex == pageCount)
                {
                    nEndPos = RecordCount;
                    bindingNavigatorMoveLastPage.Enabled = false;
                    bindingNavigatorMoveNextPage.Enabled = false;
                }
                else
                {
                    bindingNavigatorMoveLastPage.Enabled = true;
                    bindingNavigatorMoveNextPage.Enabled = true;
                    nEndPos = PageSize * PageIndex;
                }

                nStartPos = nCurrent;

                tslbCountPage.Text = pageCount.ToString();             //界面显示总页数
                tslbPositionPage.Text = Convert.ToString(PageIndex);//当前页数
                tstxtCurrentPage.Text = Convert.ToString(PageIndex);//跳转到页数的显示

                //bdsInfo.DataSource = BindData;
                //bdnInfo.BindingSource = bdsInfo;
                //Dgv.DataSource = bdsInfo;
                Dgv.DataSource = BindData;
                Dgv.ClearSelection();

                foreach (DataGridViewColumn col in Dgv.Columns)
                {
                    if (col.Visible)
                    {
                        Dgv.CurrentCell = Dgv.Rows[0].Cells[col.Name];
                        break;
                    }
                }
                Dgv.Rows[0].Selected = true;
                Dgv.FirstDisplayedScrollingRowIndex = 0;

                //计算行标头宽度
                if (RecordCount.ToString().Length >= 3)
                {
                    Dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                    Dgv.RowHeadersWidth = 41 + (RecordCount.ToString().Length - 2) * 8;
                }
                //DataGridViewCellStyle dgvcs = new DataGridViewCellStyle();
                //dgvcs.WrapMode = DataGridViewTriState.True;
                //dgvcs.Alignment = DataGridViewContentAlignment.TopCenter;
                //dgvcs.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                //Dgv.TopLeftHeaderCell.Style = dgvcs;
                Dgv.TopLeftHeaderCell.Value = "序号";
                //Dgv.TopLeftHeaderCell.Value = "合计\r\n[" + RecordCount.ToString() + "]";

            }

        }

        /// <summary>
        /// 显示序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (Dgv.DataSource != null)
            {
                int nStartPos = 0;
                int nEndPos = 0;
                if (pageCount == 1)
                {
                    nStartPos = 0;
                    if (PageSize >= RecordCount)
                    {
                        nEndPos = RecordCount;
                    }
                    else
                    {
                        nEndPos = PageSize;
                    }
                }
                else if (PageIndex == pageCount)
                {
                    nStartPos = PageSize * (PageIndex-1);
                    nEndPos = RecordCount;
                }
                else
                {
                    nStartPos = PageSize * (PageIndex-1);
                    nEndPos = PageSize * PageIndex;
                }

                Dgv.RowHeadersWidth = 55;
                int n = 0;
                for (int i = nStartPos; i < nEndPos; i++)
                {
                    int j = i + 1;
                    Dgv.Rows[n].HeaderCell.Value = j.ToString();
                    n++;
                }
            }
        }

        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "上一页")
            {
                PageIndex--;
                if (PageIndex <= 0)
                {
                    MessageBox.Show("已经是第一页，请点击“下一页”查看！");
                    PageIndex++;
                    return;
                }
                else
                {
                    nCurrent = PageSize * (PageIndex - 1);
                }
                this.CustomEvent(sender,e);
            }

            if (e.ClickedItem.Text == "下一页")
            {
                PageIndex++;
                if (PageIndex > pageCount)
                {
                    MessageBox.Show("已经是最后一页，请点击“上一页”查看！");
                    PageIndex--;
                    return;
                }
                else
                {
                    nCurrent = PageSize * (PageIndex - 1);
                }
                this.CustomEvent(sender,e);
            }

            if (e.ClickedItem.Text == "首页")
            {
                PageIndex = 1;
                nCurrent = 0;
                this.CustomEvent(sender,e);
            }

            if (e.ClickedItem.Text == "尾页")
            {
                PageIndex = pageCount;
                nCurrent = PageSize * (PageIndex - 1);
                this.CustomEvent(sender,e);
            }
        }

        private void tsbtnGo_Click(object sender, EventArgs e)
        {
            if (tstxtCurrentPage.Text.Trim() != "")
            {
                PageIndex = Convert.ToInt32(tstxtCurrentPage.Text.Trim());
                //若输入页号大于最大显示页号，则跳转至最大页
                if (PageIndex > pageCount)
                {
                    PageIndex = pageCount;
                    nCurrent = PageSize * (PageIndex - 1);
                }
                //若输入页号小于1，则跳转至第一页
                else if (PageIndex < 1)
                {
                    PageIndex = 1;
                    nCurrent = 0;
                    this.CustomEvent(sender,e);
                }
                //跳转至输入页号
                else
                {
                    nCurrent = PageSize * (PageIndex - 1);            //当前行数定位
                }
                //调用加载数据方法
                this.CustomEvent(sender,e);
            }
        }

        private void tstxtCurrentPage_TextChanged(object sender, EventArgs e)
        {
            bool IsNum = true;
            foreach (char c in tstxtCurrentPage.Text.Trim())
            {
                if (!char.IsNumber(c)) { IsNum = false; break; }
            }
            if (IsNum == false)
            {
                tstxtCurrentPage.Text = PageIndex.ToString();
            }
            else if (string.IsNullOrEmpty(tstxtCurrentPage.Text.Trim()) || tstxtCurrentPage.Text.Trim() == "0" || int.Parse(tstxtCurrentPage.Text.Trim()) > pageCount)
            {
                tstxtCurrentPage.Text = PageIndex.ToString();
            }
        }

        private void tstxtCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                tsbtnGo_Click(sender, e);
            }
        }

        private void tsPageRowCount_TextChanged(object sender, EventArgs e)
        {
            bool IsNum = true;
            //输入字符限制
            foreach (char c in tsPageRowCount.Text.Trim())
            {
                if (!char.IsNumber(c)) { IsNum = false; break; }
            }
            if (IsNum == false)
            {
                tsPageRowCount.Text = PageSize.ToString();
            }

            //判断输入的每页显示条数是否为空或是否为0，输入长度是否大于4位等情况

            if (tsPageRowCount.Text.Trim() == "" || Convert.ToUInt32(tsPageRowCount.Text.Trim()) == 0 || tsPageRowCount.Text.Trim().Length > 4)
            {
                if (MessageBox.Show("每页最多支持9999条记录！\r\n是否置为9999？", "提示信息", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    tsPageRowCount.Text = PageSize.ToString();
                }
                else
                {
                    tsPageRowCount.Text = "9999";
                }
            }
            else
            {
                PageSize = int.Parse(tsPageRowCount.Text.Trim());
            }

            if (RecordCount / PageSize < PageIndex)
            {
                PageIndex = 1;
            }

            //规避了特殊情况后直接调用显示数据方法
            if (tslbPositionPage.Text != "0")
            {
                this.CustomEvent(sender, e);
            }
        }

        //分页功能实现
        public void InitDataTable(DataGridView dgv,DataTable dt,int RowCount)
        {
            _dgv = dgv;
            _bindData = dt;
            _RecordCount = RowCount;

            if (PageSize == 0)
            {
                PageSize = 20;
            }

            tsPageRowCount.Text = PageSize.ToString();    //界面显示的“每页记录数”赋值

            tslbRowCount.Text = RecordCount.ToString();
            pageCount = (RecordCount / PageSize);    //采用整除计算页数

            //判断整除后是否有余数，有则对页数进行+1
            if ((RecordCount % PageSize) > 0) pageCount++;
            if (PageIndex == 0)
            {
                PageIndex = 1;    //当前页数从1开始
                nCurrent = 0;       //当前记录数从0开始
            }
            else if (PageIndex == 1)
            {
                nCurrent = PageSize;
            }
            else
            {
                nCurrent = PageSize * (PageIndex - 1);
            }
            //调用显示数据方法
            //this.PageIndexChanged(this, new EventArgs());
            LoadData();
        }
    }
}
