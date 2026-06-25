namespace ZCS_FormUI.Controls
{
    partial class PageCtrol
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageCtrol));
            this.bdnInfo = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstPage = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousPage = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tslbPositionPage = new System.Windows.Forms.ToolStripLabel();
            this.tslbCountPage = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextPage = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastPage = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tslbRowCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsPageRowCount = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.tstxtCurrentPage = new System.Windows.Forms.ToolStripTextBox();
            this.tsbtnGo = new System.Windows.Forms.ToolStripButton();
            this.bdsInfo = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bdnInfo)).BeginInit();
            this.bdnInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // bdnInfo
            // 
            this.bdnInfo.AddNewItem = null;
            this.bdnInfo.CountItem = this.bindingNavigatorCountItem;
            this.bdnInfo.CountItemFormat = "/ ";
            this.bdnInfo.DeleteItem = null;
            this.bdnInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bdnInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstPage,
            this.bindingNavigatorMovePreviousPage,
            this.bindingNavigatorSeparator,
            this.tslbPositionPage,
            this.bindingNavigatorCountItem,
            this.tslbCountPage,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextPage,
            this.bindingNavigatorMoveLastPage,
            this.bindingNavigatorSeparator2,
            this.toolStripLabel2,
            this.tslbRowCount,
            this.toolStripLabel1,
            this.tsPageRowCount,
            this.toolStripLabel3,
            this.toolStripSeparator2,
            this.toolStripLabel5,
            this.tstxtCurrentPage,
            this.tsbtnGo});
            this.bdnInfo.Location = new System.Drawing.Point(0, 0);
            this.bdnInfo.MoveFirstItem = null;
            this.bdnInfo.MoveLastItem = null;
            this.bdnInfo.MoveNextItem = null;
            this.bdnInfo.MovePreviousItem = null;
            this.bdnInfo.Name = "bdnInfo";
            this.bdnInfo.PositionItem = null;
            this.bdnInfo.Size = new System.Drawing.Size(627, 25);
            this.bdnInfo.TabIndex = 8;
            this.bdnInfo.Text = "bindingNavigator1";
            this.bdnInfo.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.bdnInfo_ItemClicked);
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(17, 22);
            this.bindingNavigatorCountItem.Text = "/ ";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
            // 
            // bindingNavigatorMoveFirstPage
            // 
            this.bindingNavigatorMoveFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstPage.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstPage.Image")));
            this.bindingNavigatorMoveFirstPage.ImageTransparentColor = System.Drawing.Color.White;
            this.bindingNavigatorMoveFirstPage.Name = "bindingNavigatorMoveFirstPage";
            this.bindingNavigatorMoveFirstPage.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstPage.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstPage.Text = "首页";
            this.bindingNavigatorMoveFirstPage.ToolTipText = "首页";
            // 
            // bindingNavigatorMovePreviousPage
            // 
            this.bindingNavigatorMovePreviousPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousPage.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousPage.Image")));
            this.bindingNavigatorMovePreviousPage.ImageTransparentColor = System.Drawing.Color.White;
            this.bindingNavigatorMovePreviousPage.Name = "bindingNavigatorMovePreviousPage";
            this.bindingNavigatorMovePreviousPage.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousPage.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousPage.Text = "上一页";
            this.bindingNavigatorMovePreviousPage.ToolTipText = "上一页";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // tslbPositionPage
            // 
            this.tslbPositionPage.Name = "tslbPositionPage";
            this.tslbPositionPage.Size = new System.Drawing.Size(15, 22);
            this.tslbPositionPage.Text = "0";
            // 
            // tslbCountPage
            // 
            this.tslbCountPage.Name = "tslbCountPage";
            this.tslbCountPage.Size = new System.Drawing.Size(15, 22);
            this.tslbCountPage.Text = "0";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextPage
            // 
            this.bindingNavigatorMoveNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextPage.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextPage.Image")));
            this.bindingNavigatorMoveNextPage.ImageTransparentColor = System.Drawing.Color.White;
            this.bindingNavigatorMoveNextPage.Name = "bindingNavigatorMoveNextPage";
            this.bindingNavigatorMoveNextPage.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextPage.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextPage.Text = "下一页";
            this.bindingNavigatorMoveNextPage.ToolTipText = "下一页";
            // 
            // bindingNavigatorMoveLastPage
            // 
            this.bindingNavigatorMoveLastPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastPage.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastPage.Image")));
            this.bindingNavigatorMoveLastPage.ImageTransparentColor = System.Drawing.Color.White;
            this.bindingNavigatorMoveLastPage.Name = "bindingNavigatorMoveLastPage";
            this.bindingNavigatorMoveLastPage.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastPage.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastPage.Text = "尾页";
            this.bindingNavigatorMoveLastPage.ToolTipText = "尾页";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(20, 22);
            this.toolStripLabel2.Text = "共";
            // 
            // tslbRowCount
            // 
            this.tslbRowCount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.tslbRowCount.ForeColor = System.Drawing.Color.Green;
            this.tslbRowCount.Name = "tslbRowCount";
            this.tslbRowCount.Size = new System.Drawing.Size(15, 22);
            this.tslbRowCount.Text = "0";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(73, 22);
            this.toolStripLabel1.Text = "条记录/每页";
            // 
            // tsPageRowCount
            // 
            this.tsPageRowCount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.tsPageRowCount.ForeColor = System.Drawing.Color.Green;
            this.tsPageRowCount.Name = "tsPageRowCount";
            this.tsPageRowCount.Size = new System.Drawing.Size(50, 25);
            this.tsPageRowCount.Text = "20";
            this.tsPageRowCount.TextChanged += new System.EventHandler(this.tsPageRowCount_TextChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel3.Text = "条记录";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel5.Text = "跳转:";
            // 
            // tstxtCurrentPage
            // 
            this.tstxtCurrentPage.Name = "tstxtCurrentPage";
            this.tstxtCurrentPage.Size = new System.Drawing.Size(50, 25);
            this.tstxtCurrentPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tstxtCurrentPage_KeyPress);
            this.tstxtCurrentPage.TextChanged += new System.EventHandler(this.tstxtCurrentPage_TextChanged);
            // 
            // tsbtnGo
            // 
            this.tsbtnGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnGo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnGo.Image")));
            this.tsbtnGo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnGo.Name = "tsbtnGo";
            this.tsbtnGo.Size = new System.Drawing.Size(29, 22);
            this.tsbtnGo.Text = "Go";
            this.tsbtnGo.Click += new System.EventHandler(this.tsbtnGo_Click);
            // 
            // PageCtrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bdnInfo);
            this.Name = "PageCtrol";
            this.Size = new System.Drawing.Size(627, 25);
            ((System.ComponentModel.ISupportInitialize)(this.bdnInfo)).EndInit();
            this.bdnInfo.ResumeLayout(false);
            this.bdnInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator bdnInfo;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstPage;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousPage;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripLabel tslbPositionPage;
        private System.Windows.Forms.ToolStripLabel tslbCountPage;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextPage;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastPage;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel tslbRowCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tsPageRowCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripTextBox tstxtCurrentPage;
        private System.Windows.Forms.ToolStripButton tsbtnGo;
        private System.Windows.Forms.BindingSource bdsInfo;
    }
}
