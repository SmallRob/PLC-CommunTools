namespace ZCS_FormUI.Forms
{
    partial class TipMessage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TipMessage));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.tmrWait = new System.Windows.Forms.Timer(this.components);
            this.imgListInfo = new System.Windows.Forms.ImageList(this.components);
            this.groupBoxEx1 = new ZCS_FormUI.Controls.GroupBoxEx();
            this.panelEx1 = new System.Windows.Forms.Panel();
            this.picInfo = new System.Windows.Forms.PictureBox();
            this.lblTxt = new System.Windows.Forms.Label();
            this.groupBoxEx1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // tmrWait
            // 
            this.tmrWait.Enabled = true;
            this.tmrWait.Interval = 2600;
            this.tmrWait.Tick += new System.EventHandler(this.tmrWait_Tick);
            // 
            // imgListInfo
            // 
            this.imgListInfo.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListInfo.ImageStream")));
            this.imgListInfo.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListInfo.Images.SetKeyName(0, "emblem_ok.png");
            this.imgListInfo.Images.SetKeyName(1, "information.png");
            this.imgListInfo.Images.SetKeyName(2, "error.png");
            this.imgListInfo.Images.SetKeyName(3, "info.png");
            // 
            // groupBoxEx1
            // 
            this.groupBoxEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxEx1.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxEx1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.groupBoxEx1.Controls.Add(this.panelEx1);
            this.groupBoxEx1.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBoxEx1.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxEx1.ForeColor = System.Drawing.Color.Black;
            this.groupBoxEx1.Location = new System.Drawing.Point(1, -6);
            this.groupBoxEx1.Name = "groupBoxEx1";
            this.groupBoxEx1.Size = new System.Drawing.Size(260, 59);
            this.groupBoxEx1.TabIndex = 3;
            this.groupBoxEx1.TabStop = false;
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.BackColor = System.Drawing.Color.MistyRose;
            this.panelEx1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelEx1.Controls.Add(this.picInfo);
            this.panelEx1.Controls.Add(this.lblTxt);
            this.panelEx1.ForeColor = System.Drawing.Color.Transparent;
            this.panelEx1.Location = new System.Drawing.Point(2, 9);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(256, 46);
            this.panelEx1.TabIndex = 3;
            // 
            // picInfo
            // 
            this.picInfo.Image = ((System.Drawing.Image)(resources.GetObject("picInfo.Image")));
            this.picInfo.Location = new System.Drawing.Point(2, 1);
            this.picInfo.Name = "picInfo";
            this.picInfo.Size = new System.Drawing.Size(44, 44);
            this.picInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picInfo.TabIndex = 2;
            this.picInfo.TabStop = false;
            // 
            // lblTxt
            // 
            this.lblTxt.AutoSize = true;
            this.lblTxt.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(81)))), ((int)(((byte)(29)))));
            this.lblTxt.Location = new System.Drawing.Point(52, 16);
            this.lblTxt.Name = "lblTxt";
            this.lblTxt.Size = new System.Drawing.Size(181, 14);
            this.lblTxt.TabIndex = 1;
            this.lblTxt.Text = "正在加载数据，请稍等...";
            // 
            // TipMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Olive;
            this.ClientSize = new System.Drawing.Size(263, 56);
            this.Controls.Add(this.groupBoxEx1);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(263, 56);
            this.Name = "TipMessage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请等待...";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.Waiting_Deactivate);
            this.Load += new System.EventHandler(this.Waiting_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Waiting_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Waiting_MouseDown);
            this.groupBoxEx1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer tmrWait;
        private System.Windows.Forms.ImageList imgListInfo;
        private System.Windows.Forms.Panel panelEx1;
        private System.Windows.Forms.PictureBox picInfo;
        private System.Windows.Forms.Label lblTxt;
    }
}