namespace ZCS_FormUI.Controls
{
    partial class TreeViewCheckEx
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeViewCheckEx));
            this.ctlStateImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ctlStateImageList
            // 
            this.ctlStateImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ctlStateImageList.ImageStream")));
            this.ctlStateImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ctlStateImageList.Images.SetKeyName(0, "StateNone16.png");
            this.ctlStateImageList.Images.SetKeyName(1, "StateUnchecked16.png");
            this.ctlStateImageList.Images.SetKeyName(2, "StateChecked16.png");
            this.ctlStateImageList.Images.SetKeyName(3, "StateIndeterminate16.png");
            // 
            // TreeViewCheckEx
            // 
            this.LineColor = System.Drawing.Color.Black;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList ctlStateImageList;

    }
}
