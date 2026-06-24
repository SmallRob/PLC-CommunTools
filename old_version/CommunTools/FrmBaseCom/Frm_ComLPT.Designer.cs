
namespace CommunTools
{
    partial class Frm_ComLPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ComLPT));
            this.btnOut = new ZCS_FormUI.Controls.UCBtnExt();
            this.groupBoxEx1 = new ZCS_FormUI.Controls.GroupBoxEx();
            this.btnInp = new ZCS_FormUI.Controls.UCBtnExt();
            this.richTextBox_Send = new System.Windows.Forms.RichTextBox();
            this.groupBoxEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOut
            // 
            this.btnOut.BackColor = System.Drawing.Color.White;
            this.btnOut.BtnBackColor = System.Drawing.Color.White;
            this.btnOut.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOut.BtnForeColor = System.Drawing.Color.White;
            this.btnOut.BtnText = "写入";
            this.btnOut.ConerRadius = 5;
            this.btnOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOut.EnabledMouseEffect = false;
            this.btnOut.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnOut.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnOut.IsRadius = true;
            this.btnOut.IsShowRect = true;
            this.btnOut.IsShowTips = false;
            this.btnOut.Location = new System.Drawing.Point(201, 55);
            this.btnOut.Margin = new System.Windows.Forms.Padding(0);
            this.btnOut.Name = "btnOut";
            this.btnOut.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(58)))));
            this.btnOut.RectWidth = 1;
            this.btnOut.Size = new System.Drawing.Size(69, 33);
            this.btnOut.TabIndex = 0;
            this.btnOut.TabStop = false;
            this.btnOut.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnOut.TipsText = "";
            this.btnOut.BtnClick += new System.EventHandler(this.btnOut_BtnClick);
            // 
            // groupBoxEx1
            // 
            this.groupBoxEx1.Controls.Add(this.richTextBox_Send);
            this.groupBoxEx1.Location = new System.Drawing.Point(7, 118);
            this.groupBoxEx1.Name = "groupBoxEx1";
            this.groupBoxEx1.Size = new System.Drawing.Size(571, 244);
            this.groupBoxEx1.TabIndex = 50;
            this.groupBoxEx1.TabStop = false;
            this.groupBoxEx1.Text = "通讯结果";
            // 
            // btnInp
            // 
            this.btnInp.BackColor = System.Drawing.Color.White;
            this.btnInp.BtnBackColor = System.Drawing.Color.White;
            this.btnInp.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInp.BtnForeColor = System.Drawing.Color.White;
            this.btnInp.BtnText = "读取";
            this.btnInp.ConerRadius = 5;
            this.btnInp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInp.EnabledMouseEffect = false;
            this.btnInp.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnInp.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnInp.IsRadius = true;
            this.btnInp.IsShowRect = true;
            this.btnInp.IsShowTips = false;
            this.btnInp.Location = new System.Drawing.Point(89, 55);
            this.btnInp.Margin = new System.Windows.Forms.Padding(0);
            this.btnInp.Name = "btnInp";
            this.btnInp.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(58)))));
            this.btnInp.RectWidth = 1;
            this.btnInp.Size = new System.Drawing.Size(69, 33);
            this.btnInp.TabIndex = 2;
            this.btnInp.TabStop = false;
            this.btnInp.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnInp.TipsText = "";
            this.btnInp.BtnClick += new System.EventHandler(this.btnInp_BtnClick);
            // 
            // richTextBox_Send
            // 
            this.richTextBox_Send.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox_Send.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_Send.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Send.Location = new System.Drawing.Point(3, 19);
            this.richTextBox_Send.Name = "richTextBox_Send";
            this.richTextBox_Send.Size = new System.Drawing.Size(565, 222);
            this.richTextBox_Send.TabIndex = 1;
            this.richTextBox_Send.Text = "";
            // 
            // Frm_ComLPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CaptionBackColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.CaptionFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(585, 369);
            this.CloseBoxSize = new System.Drawing.Size(32, 24);
            this.Controls.Add(this.btnInp);
            this.Controls.Add(this.groupBoxEx1);
            this.Controls.Add(this.btnOut);
            this.EffectBack = System.Drawing.Color.Silver;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaxSize = new System.Drawing.Size(32, 24);
            this.MiniSize = new System.Drawing.Size(32, 24);
            this.Name = "Frm_ComLPT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LPT并口通讯";
            this.groupBoxEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ZCS_FormUI.Controls.UCBtnExt btnOut;
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx1;
        private ZCS_FormUI.Controls.UCBtnExt btnInp;
        private System.Windows.Forms.RichTextBox richTextBox_Send;
    }
}