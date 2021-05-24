namespace ZCS_FormUI.Controls
{
    partial class FuncItemBox
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
            this.pnlBox = new Com_CSSkin.SkinControl.SkinPanel();
            this.lblFuncItem = new Com_CSSkin.SkinControl.SkinLabel();
            this.lblFuncMark = new Com_CSSkin.SkinControl.SkinLabel();
            this.pnlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBox
            // 
            this.pnlBox.BackColor = System.Drawing.Color.DodgerBlue;
            this.pnlBox.BorderColor = System.Drawing.Color.Silver;
            this.pnlBox.Controls.Add(this.lblFuncItem);
            this.pnlBox.Controls.Add(this.lblFuncMark);
            this.pnlBox.ControlState = Com_CSSkin.SkinClass.ControlState.Normal;
            this.pnlBox.DownBack = null;
            this.pnlBox.ForeColor = System.Drawing.Color.Transparent;
            this.pnlBox.Location = new System.Drawing.Point(3, 3);
            this.pnlBox.MouseBack = null;
            this.pnlBox.Name = "pnlBox";
            this.pnlBox.NormlBack = null;
            this.pnlBox.Radius = 6;
            this.pnlBox.RoundStyle = Com_CSSkin.SkinClass.RoundStyle.All;
            this.pnlBox.Size = new System.Drawing.Size(130, 40);
            this.pnlBox.TabIndex = 3;
            // 
            // lblFuncItem
            // 
            this.lblFuncItem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFuncItem.AutoSize = true;
            this.lblFuncItem.BackColor = System.Drawing.Color.Transparent;
            this.lblFuncItem.BorderColor = System.Drawing.Color.DarkGray;
            this.lblFuncItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFuncItem.ForeColor = System.Drawing.Color.Navy;
            this.lblFuncItem.Location = new System.Drawing.Point(9, 4);
            this.lblFuncItem.Name = "lblFuncItem";
            this.lblFuncItem.Size = new System.Drawing.Size(99, 19);
            this.lblFuncItem.TabIndex = 1;
            this.lblFuncItem.Text = "1234567890";
            // 
            // lblFuncMark
            // 
            this.lblFuncMark.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFuncMark.ArtTextStyle = Com_CSSkin.SkinControl.ArtTextStyle.Relievo;
            this.lblFuncMark.AutoSize = true;
            this.lblFuncMark.BackColor = System.Drawing.Color.Transparent;
            this.lblFuncMark.BorderColor = System.Drawing.Color.Yellow;
            this.lblFuncMark.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFuncMark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblFuncMark.Location = new System.Drawing.Point(29, 20);
            this.lblFuncMark.Name = "lblFuncMark";
            this.lblFuncMark.Size = new System.Drawing.Size(62, 17);
            this.lblFuncMark.TabIndex = 2;
            this.lblFuncMark.Text = "999999 X";
            // 
            // FuncItemBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlBox);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Name = "FuncItemBox";
            this.Size = new System.Drawing.Size(136, 45);
            this.pnlBox.ResumeLayout(false);
            this.pnlBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public Com_CSSkin.SkinControl.SkinLabel lblFuncMark;
        public Com_CSSkin.SkinControl.SkinPanel pnlBox;
        public Com_CSSkin.SkinControl.SkinLabel lblFuncItem;
    }
}
