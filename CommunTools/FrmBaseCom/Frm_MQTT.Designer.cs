
namespace CommunTools
{
    partial class Frm_MQTT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MQTT));
            this.btnConnect = new ZCS_FormUI.Controls.UCBtnExt();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.White;
            this.btnConnect.BtnBackColor = System.Drawing.Color.White;
            this.btnConnect.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConnect.BtnForeColor = System.Drawing.Color.White;
            this.btnConnect.BtnText = "测试";
            this.btnConnect.ConerRadius = 5;
            this.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConnect.EnabledMouseEffect = false;
            this.btnConnect.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnConnect.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnConnect.IsRadius = true;
            this.btnConnect.IsShowRect = true;
            this.btnConnect.IsShowTips = false;
            this.btnConnect.Location = new System.Drawing.Point(161, 123);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(0);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(58)))));
            this.btnConnect.RectWidth = 1;
            this.btnConnect.Size = new System.Drawing.Size(69, 33);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.TabStop = false;
            this.btnConnect.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnConnect.TipsText = "";
            this.btnConnect.BtnClick += new System.EventHandler(this.btnConnect_BtnClick);
            // 
            // Frm_MQTT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CaptionBackColorTop = System.Drawing.Color.AntiqueWhite;
            this.CaptionFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(392, 269);
            this.CloseBoxSize = new System.Drawing.Size(32, 24);
            this.Controls.Add(this.btnConnect);
            this.EffectBack = System.Drawing.Color.Silver;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaxSize = new System.Drawing.Size(32, 24);
            this.MiniSize = new System.Drawing.Size(32, 24);
            this.Name = "Frm_MQTT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MQTT通讯";
            this.ResumeLayout(false);

        }

        #endregion

        private ZCS_FormUI.Controls.UCBtnExt btnConnect;
    }
}