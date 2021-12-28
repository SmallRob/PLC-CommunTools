
namespace CommunTools
{
    partial class Frm_Twain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Twain));
            this.btnStartScan = new ZCS_FormUI.Controls.UCBtnExt();
            this.ckbSeries = new ZCS_FormUI.Controls.UCCheckBox();
            this.groupBoxEx1 = new ZCS_FormUI.Controls.GroupBoxEx();
            this.SuspendLayout();
            // 
            // btnStartScan
            // 
            this.btnStartScan.BackColor = System.Drawing.Color.White;
            this.btnStartScan.BtnBackColor = System.Drawing.Color.White;
            this.btnStartScan.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStartScan.BtnForeColor = System.Drawing.Color.White;
            this.btnStartScan.BtnText = "开始扫描";
            this.btnStartScan.ConerRadius = 5;
            this.btnStartScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartScan.EnabledMouseEffect = false;
            this.btnStartScan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnStartScan.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnStartScan.IsRadius = true;
            this.btnStartScan.IsShowRect = true;
            this.btnStartScan.IsShowTips = false;
            this.btnStartScan.Location = new System.Drawing.Point(201, 55);
            this.btnStartScan.Margin = new System.Windows.Forms.Padding(0);
            this.btnStartScan.Name = "btnStartScan";
            this.btnStartScan.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(58)))));
            this.btnStartScan.RectWidth = 1;
            this.btnStartScan.Size = new System.Drawing.Size(69, 33);
            this.btnStartScan.TabIndex = 0;
            this.btnStartScan.TabStop = false;
            this.btnStartScan.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnStartScan.TipsText = "";
            this.btnStartScan.BtnClick += new System.EventHandler(this.btnStartScan_BtnClick);
            // 
            // ckbSeries
            // 
            this.ckbSeries.BackColor = System.Drawing.Color.Transparent;
            this.ckbSeries.Checked = false;
            this.ckbSeries.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbSeries.ForeColor = System.Drawing.Color.Black;
            this.ckbSeries.Location = new System.Drawing.Point(71, 60);
            this.ckbSeries.Name = "ckbSeries";
            this.ckbSeries.Padding = new System.Windows.Forms.Padding(1);
            this.ckbSeries.Size = new System.Drawing.Size(96, 24);
            this.ckbSeries.TabIndex = 49;
            this.ckbSeries.TextValue = "连续扫描";
            // 
            // groupBoxEx1
            // 
            this.groupBoxEx1.Location = new System.Drawing.Point(7, 118);
            this.groupBoxEx1.Name = "groupBoxEx1";
            this.groupBoxEx1.Size = new System.Drawing.Size(571, 244);
            this.groupBoxEx1.TabIndex = 50;
            this.groupBoxEx1.TabStop = false;
            this.groupBoxEx1.Text = "扫描结果";
            // 
            // Frm_Twain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CaptionBackColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.CaptionFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(585, 369);
            this.CloseBoxSize = new System.Drawing.Size(32, 24);
            this.Controls.Add(this.groupBoxEx1);
            this.Controls.Add(this.ckbSeries);
            this.Controls.Add(this.btnStartScan);
            this.EffectBack = System.Drawing.Color.Silver;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaxSize = new System.Drawing.Size(32, 24);
            this.MiniSize = new System.Drawing.Size(32, 24);
            this.Name = "Frm_Twain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Twain扫描协议";
            this.ResumeLayout(false);

        }

        #endregion

        private ZCS_FormUI.Controls.UCBtnExt btnStartScan;
        private ZCS_FormUI.Controls.UCCheckBox ckbSeries;
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx1;
    }
}