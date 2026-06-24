
namespace CommunTools
{
    partial class FrmMenu
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenu));
            this.menuSystem = new Com_CSSkin.SkinControl.SkinContextMenuStrip();
            this.tsmShow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.notifySystem = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuSystem.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuSystem
            // 
            this.menuSystem.Arrow = System.Drawing.Color.Black;
            this.menuSystem.Back = System.Drawing.Color.White;
            this.menuSystem.BackRadius = 4;
            this.menuSystem.Base = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.menuSystem.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.menuSystem.Fore = System.Drawing.Color.Black;
            this.menuSystem.HoverFore = System.Drawing.Color.White;
            this.menuSystem.ItemAnamorphosis = true;
            this.menuSystem.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.menuSystem.ItemBorderShow = true;
            this.menuSystem.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.menuSystem.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.menuSystem.ItemRadius = 4;
            this.menuSystem.ItemRadiusStyle = Com_CSSkin.SkinClass.RoundStyle.All;
            this.menuSystem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShow,
            this.toolStripSeparator1,
            this.tsmExit});
            this.menuSystem.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.RadiusStyle = Com_CSSkin.SkinClass.RoundStyle.All;
            this.menuSystem.Size = new System.Drawing.Size(111, 62);
            this.menuSystem.SkinAllColor = true;
            this.menuSystem.TitleAnamorphosis = true;
            this.menuSystem.TitleColor = System.Drawing.Color.RoyalBlue;
            this.menuSystem.TitleRadius = 4;
            this.menuSystem.TitleRadiusStyle = Com_CSSkin.SkinClass.RoundStyle.All;
            this.menuSystem.Opening += new System.ComponentModel.CancelEventHandler(this.menuSystem_Opening);
            // 
            // tsmShow
            // 
            this.tsmShow.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmShow.Image = global::CommunTools.Properties.Resources.shichang;
            this.tsmShow.Name = "tsmShow";
            this.tsmShow.Size = new System.Drawing.Size(110, 26);
            this.tsmShow.Text = "显示";
            this.tsmShow.Click += new System.EventHandler(this.tsmShow_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(107, 6);
            // 
            // tsmExit
            // 
            this.tsmExit.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmExit.Image = global::CommunTools.Properties.Resources.close;
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(110, 26);
            this.tsmExit.Text = "退出";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // notifySystem
            // 
            this.notifySystem.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifySystem.BalloonTipText = "可以双击托盘图标还原工具界面";
            this.notifySystem.BalloonTipTitle = "界面已最小化";
            this.notifySystem.ContextMenuStrip = this.menuSystem;
            this.notifySystem.Icon = ((System.Drawing.Icon)(resources.GetObject("notifySystem.Icon")));
            this.notifySystem.Text = "通讯工具";
            this.notifySystem.Visible = true;
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.CaptionBackColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.CaptionFont = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(385, 435);
            this.CloseBoxSize = new System.Drawing.Size(32, 24);
            this.EffectBack = System.Drawing.Color.LightGray;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaxSize = new System.Drawing.Size(32, 24);
            this.MinimumSize = new System.Drawing.Size(385, 435);
            this.MiniSize = new System.Drawing.Size(32, 24);
            this.Name = "FrmMenu";
            this.ShowInTaskbar = false;
            this.ShowSystemMenu = true;
            this.Text = "硬件及协议通讯工具";
            this.Load += new System.EventHandler(this.FrmMenu_Load);
            this.Resize += new System.EventHandler(this.FrmMenu_Resize);
            this.menuSystem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Com_CSSkin.SkinControl.SkinContextMenuStrip menuSystem;
        private System.Windows.Forms.ToolStripMenuItem tsmShow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        public System.Windows.Forms.NotifyIcon notifySystem;
    }
}

