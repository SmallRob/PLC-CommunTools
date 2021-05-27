
namespace CommunTools
{
    partial class Frm_SerialServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SerialServer));
            this.btnClean = new ZCS_FormUI.Controls.UCBtnExt();
            this.groupBoxEx1 = new ZCS_FormUI.Controls.GroupBoxEx();
            this.btnStart = new ZCS_FormUI.Controls.UCBtnExt();
            this.lblCount = new Com_CSSkin.SkinControl.SkinLabel();
            this.skinLabel3 = new Com_CSSkin.SkinControl.SkinLabel();
            this.skinLabel2 = new Com_CSSkin.SkinControl.SkinLabel();
            this.txtPort = new ZCS_FormUI.Controls.UCTextBoxEx();
            this.skinLabel1 = new Com_CSSkin.SkinControl.SkinLabel();
            this.txtTCPIP = new ZCS_FormUI.Controls.UCTextBoxEx();
            this.groupBoxEx2 = new ZCS_FormUI.Controls.GroupBoxEx();
            this.cmbDataBits = new Com_CSSkin.SkinControl.SkinComboBox();
            this.cmbStopBits = new Com_CSSkin.SkinControl.SkinComboBox();
            this.cmbBandRate = new Com_CSSkin.SkinControl.SkinComboBox();
            this.cmbComLst = new Com_CSSkin.SkinControl.SkinComboBox();
            this.cmbPortParity = new Com_CSSkin.SkinControl.SkinComboBox();
            this.skinLabel8 = new Com_CSSkin.SkinControl.SkinLabel();
            this.skinLabel6 = new Com_CSSkin.SkinControl.SkinLabel();
            this.skinLabel5 = new Com_CSSkin.SkinControl.SkinLabel();
            this.skinLabel4 = new Com_CSSkin.SkinControl.SkinLabel();
            this.skinLabel7 = new Com_CSSkin.SkinControl.SkinLabel();
            this.groupBoxEx3 = new ZCS_FormUI.Controls.GroupBoxEx();
            this.txtShowMsg = new System.Windows.Forms.RichTextBox();
            this.labComInfo = new Com_CSSkin.SkinControl.SkinLabel();
            this.labLinkClient = new Com_CSSkin.SkinControl.SkinLabel();
            this.groupBoxEx1.SuspendLayout();
            this.groupBoxEx2.SuspendLayout();
            this.groupBoxEx3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClean
            // 
            this.btnClean.BackColor = System.Drawing.Color.White;
            this.btnClean.BtnBackColor = System.Drawing.Color.White;
            this.btnClean.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClean.BtnForeColor = System.Drawing.Color.White;
            this.btnClean.BtnText = "清除统计";
            this.btnClean.ConerRadius = 5;
            this.btnClean.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClean.EnabledMouseEffect = false;
            this.btnClean.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnClean.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnClean.IsRadius = true;
            this.btnClean.IsShowRect = true;
            this.btnClean.IsShowTips = false;
            this.btnClean.Location = new System.Drawing.Point(282, 69);
            this.btnClean.Margin = new System.Windows.Forms.Padding(0);
            this.btnClean.Name = "btnClean";
            this.btnClean.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(58)))));
            this.btnClean.RectWidth = 1;
            this.btnClean.Size = new System.Drawing.Size(86, 33);
            this.btnClean.TabIndex = 0;
            this.btnClean.TabStop = false;
            this.btnClean.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnClean.TipsText = "";
            // 
            // groupBoxEx1
            // 
            this.groupBoxEx1.Controls.Add(this.btnStart);
            this.groupBoxEx1.Controls.Add(this.lblCount);
            this.groupBoxEx1.Controls.Add(this.skinLabel3);
            this.groupBoxEx1.Controls.Add(this.btnClean);
            this.groupBoxEx1.Controls.Add(this.skinLabel2);
            this.groupBoxEx1.Controls.Add(this.txtPort);
            this.groupBoxEx1.Controls.Add(this.skinLabel1);
            this.groupBoxEx1.Controls.Add(this.txtTCPIP);
            this.groupBoxEx1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxEx1.Location = new System.Drawing.Point(10, 31);
            this.groupBoxEx1.Name = "groupBoxEx1";
            this.groupBoxEx1.Size = new System.Drawing.Size(394, 156);
            this.groupBoxEx1.TabIndex = 1;
            this.groupBoxEx1.TabStop = false;
            this.groupBoxEx1.Text = "TCP转发服务";
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.White;
            this.btnStart.BtnBackColor = System.Drawing.Color.White;
            this.btnStart.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.BtnForeColor = System.Drawing.Color.White;
            this.btnStart.BtnText = "开启服务";
            this.btnStart.ConerRadius = 5;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.EnabledMouseEffect = false;
            this.btnStart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnStart.IsRadius = true;
            this.btnStart.IsShowRect = true;
            this.btnStart.IsShowTips = false;
            this.btnStart.Location = new System.Drawing.Point(282, 22);
            this.btnStart.Margin = new System.Windows.Forms.Padding(0);
            this.btnStart.Name = "btnStart";
            this.btnStart.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(58)))));
            this.btnStart.RectWidth = 1;
            this.btnStart.Size = new System.Drawing.Size(86, 33);
            this.btnStart.TabIndex = 2;
            this.btnStart.TabStop = false;
            this.btnStart.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnStart.TipsText = "";
            this.btnStart.BtnClick += new System.EventHandler(this.btnOpenServer_BtnClick);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCount.BorderColor = System.Drawing.Color.White;
            this.lblCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCount.Location = new System.Drawing.Point(141, 121);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(99, 20);
            this.lblCount.TabIndex = 5;
            this.lblCount.Text = "接收包/总字节";
            // 
            // skinLabel3
            // 
            this.skinLabel3.AutoSize = true;
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel3.Location = new System.Drawing.Point(28, 121);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(107, 20);
            this.skinLabel3.TabIndex = 4;
            this.skinLabel3.Text = "接收数据统计：";
            // 
            // skinLabel2
            // 
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(28, 76);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(51, 20);
            this.skinLabel2.TabIndex = 3;
            this.skinLabel2.Text = "Port：";
            // 
            // txtPort
            // 
            this.txtPort.BackColor = System.Drawing.Color.Transparent;
            this.txtPort.ConerRadius = 5;
            this.txtPort.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPort.DecLength = 2;
            this.txtPort.FillColor = System.Drawing.Color.Empty;
            this.txtPort.FocusBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.txtPort.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPort.InputText = "";
            this.txtPort.InputType = ZCS_FormUI.TextInputType.NotControl;
            this.txtPort.IsFocusColor = true;
            this.txtPort.IsRadius = true;
            this.txtPort.IsShowClearBtn = true;
            this.txtPort.IsShowKeyboard = false;
            this.txtPort.IsShowRect = true;
            this.txtPort.IsShowSearchBtn = false;
            this.txtPort.KeyBoardType = ZCS_FormUI.KeyBoardType.UCKeyBorderAll_EN;
            this.txtPort.Location = new System.Drawing.Point(110, 76);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPort.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtPort.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.txtPort.Name = "txtPort";
            this.txtPort.Padding = new System.Windows.Forms.Padding(5);
            this.txtPort.PasswordChar = '\0';
            this.txtPort.PromptColor = System.Drawing.Color.Gray;
            this.txtPort.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPort.PromptText = "";
            this.txtPort.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtPort.RectWidth = 1;
            this.txtPort.RegexPattern = "";
            this.txtPort.Size = new System.Drawing.Size(85, 26);
            this.txtPort.TabIndex = 2;
            // 
            // skinLabel1
            // 
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(28, 27);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(36, 20);
            this.skinLabel1.TabIndex = 1;
            this.skinLabel1.Text = "IP：";
            // 
            // txtTCPIP
            // 
            this.txtTCPIP.BackColor = System.Drawing.Color.Transparent;
            this.txtTCPIP.ConerRadius = 5;
            this.txtTCPIP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTCPIP.DecLength = 2;
            this.txtTCPIP.FillColor = System.Drawing.Color.Empty;
            this.txtTCPIP.FocusBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.txtTCPIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTCPIP.InputText = "";
            this.txtTCPIP.InputType = ZCS_FormUI.TextInputType.NotControl;
            this.txtTCPIP.IsFocusColor = true;
            this.txtTCPIP.IsRadius = true;
            this.txtTCPIP.IsShowClearBtn = true;
            this.txtTCPIP.IsShowKeyboard = false;
            this.txtTCPIP.IsShowRect = true;
            this.txtTCPIP.IsShowSearchBtn = false;
            this.txtTCPIP.KeyBoardType = ZCS_FormUI.KeyBoardType.UCKeyBorderAll_EN;
            this.txtTCPIP.Location = new System.Drawing.Point(110, 27);
            this.txtTCPIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTCPIP.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtTCPIP.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.txtTCPIP.Name = "txtTCPIP";
            this.txtTCPIP.Padding = new System.Windows.Forms.Padding(5);
            this.txtTCPIP.PasswordChar = '\0';
            this.txtTCPIP.PromptColor = System.Drawing.Color.Gray;
            this.txtTCPIP.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTCPIP.PromptText = "";
            this.txtTCPIP.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtTCPIP.RectWidth = 1;
            this.txtTCPIP.RegexPattern = "";
            this.txtTCPIP.Size = new System.Drawing.Size(140, 26);
            this.txtTCPIP.TabIndex = 0;
            // 
            // groupBoxEx2
            // 
            this.groupBoxEx2.Controls.Add(this.cmbDataBits);
            this.groupBoxEx2.Controls.Add(this.cmbStopBits);
            this.groupBoxEx2.Controls.Add(this.cmbBandRate);
            this.groupBoxEx2.Controls.Add(this.cmbComLst);
            this.groupBoxEx2.Controls.Add(this.cmbPortParity);
            this.groupBoxEx2.Controls.Add(this.skinLabel8);
            this.groupBoxEx2.Controls.Add(this.skinLabel6);
            this.groupBoxEx2.Controls.Add(this.skinLabel5);
            this.groupBoxEx2.Controls.Add(this.skinLabel4);
            this.groupBoxEx2.Controls.Add(this.skinLabel7);
            this.groupBoxEx2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxEx2.Location = new System.Drawing.Point(10, 193);
            this.groupBoxEx2.Name = "groupBoxEx2";
            this.groupBoxEx2.Size = new System.Drawing.Size(394, 257);
            this.groupBoxEx2.TabIndex = 6;
            this.groupBoxEx2.TabStop = false;
            this.groupBoxEx2.Text = "COM口设置";
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.BorderColor = System.Drawing.Color.Gainsboro;
            this.cmbDataBits.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbDataBits.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Location = new System.Drawing.Point(110, 210);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(140, 27);
            this.cmbDataBits.TabIndex = 14;
            this.cmbDataBits.WaterText = "";
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.BorderColor = System.Drawing.Color.Gainsboro;
            this.cmbStopBits.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbStopBits.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Location = new System.Drawing.Point(110, 164);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(140, 27);
            this.cmbStopBits.TabIndex = 13;
            this.cmbStopBits.WaterText = "";
            // 
            // cmbBandRate
            // 
            this.cmbBandRate.BorderColor = System.Drawing.Color.Gainsboro;
            this.cmbBandRate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbBandRate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBandRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBandRate.FormattingEnabled = true;
            this.cmbBandRate.Location = new System.Drawing.Point(110, 119);
            this.cmbBandRate.Name = "cmbBandRate";
            this.cmbBandRate.Size = new System.Drawing.Size(140, 27);
            this.cmbBandRate.TabIndex = 12;
            this.cmbBandRate.WaterText = "";
            // 
            // cmbComLst
            // 
            this.cmbComLst.BorderColor = System.Drawing.Color.Gainsboro;
            this.cmbComLst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbComLst.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbComLst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComLst.FormattingEnabled = true;
            this.cmbComLst.Location = new System.Drawing.Point(110, 27);
            this.cmbComLst.Name = "cmbComLst";
            this.cmbComLst.Size = new System.Drawing.Size(140, 27);
            this.cmbComLst.TabIndex = 11;
            this.cmbComLst.WaterText = "";
            // 
            // cmbPortParity
            // 
            this.cmbPortParity.BorderColor = System.Drawing.Color.Gainsboro;
            this.cmbPortParity.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbPortParity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPortParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortParity.FormattingEnabled = true;
            this.cmbPortParity.Location = new System.Drawing.Point(110, 73);
            this.cmbPortParity.Name = "cmbPortParity";
            this.cmbPortParity.Size = new System.Drawing.Size(140, 27);
            this.cmbPortParity.TabIndex = 10;
            this.cmbPortParity.WaterText = "";
            // 
            // skinLabel8
            // 
            this.skinLabel8.AutoSize = true;
            this.skinLabel8.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel8.BorderColor = System.Drawing.Color.White;
            this.skinLabel8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel8.Location = new System.Drawing.Point(28, 213);
            this.skinLabel8.Name = "skinLabel8";
            this.skinLabel8.Size = new System.Drawing.Size(65, 20);
            this.skinLabel8.TabIndex = 9;
            this.skinLabel8.Text = "数据位：";
            // 
            // skinLabel6
            // 
            this.skinLabel6.AutoSize = true;
            this.skinLabel6.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel6.BorderColor = System.Drawing.Color.White;
            this.skinLabel6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel6.Location = new System.Drawing.Point(28, 167);
            this.skinLabel6.Name = "skinLabel6";
            this.skinLabel6.Size = new System.Drawing.Size(65, 20);
            this.skinLabel6.TabIndex = 7;
            this.skinLabel6.Text = "停止位：";
            // 
            // skinLabel5
            // 
            this.skinLabel5.AutoSize = true;
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel5.Location = new System.Drawing.Point(28, 122);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(65, 20);
            this.skinLabel5.TabIndex = 5;
            this.skinLabel5.Text = "波特率：";
            // 
            // skinLabel4
            // 
            this.skinLabel4.AutoSize = true;
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel4.Location = new System.Drawing.Point(28, 76);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(79, 20);
            this.skinLabel4.TabIndex = 3;
            this.skinLabel4.Text = "奇偶校验：";
            // 
            // skinLabel7
            // 
            this.skinLabel7.AutoSize = true;
            this.skinLabel7.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel7.BorderColor = System.Drawing.Color.White;
            this.skinLabel7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel7.Location = new System.Drawing.Point(28, 30);
            this.skinLabel7.Name = "skinLabel7";
            this.skinLabel7.Size = new System.Drawing.Size(57, 20);
            this.skinLabel7.TabIndex = 1;
            this.skinLabel7.Text = "COM：";
            // 
            // groupBoxEx3
            // 
            this.groupBoxEx3.Controls.Add(this.txtShowMsg);
            this.groupBoxEx3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxEx3.Location = new System.Drawing.Point(413, 31);
            this.groupBoxEx3.Name = "groupBoxEx3";
            this.groupBoxEx3.Size = new System.Drawing.Size(375, 419);
            this.groupBoxEx3.TabIndex = 7;
            this.groupBoxEx3.TabStop = false;
            this.groupBoxEx3.Text = "接收信息";
            // 
            // txtShowMsg
            // 
            this.txtShowMsg.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtShowMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtShowMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtShowMsg.Location = new System.Drawing.Point(3, 22);
            this.txtShowMsg.Name = "txtShowMsg";
            this.txtShowMsg.Size = new System.Drawing.Size(369, 394);
            this.txtShowMsg.TabIndex = 0;
            this.txtShowMsg.Text = "";
            // 
            // labComInfo
            // 
            this.labComInfo.AutoSize = true;
            this.labComInfo.BackColor = System.Drawing.Color.Transparent;
            this.labComInfo.BorderColor = System.Drawing.Color.White;
            this.labComInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labComInfo.ForeColor = System.Drawing.Color.BlueViolet;
            this.labComInfo.Location = new System.Drawing.Point(32, 455);
            this.labComInfo.Name = "labComInfo";
            this.labComInfo.Size = new System.Drawing.Size(85, 20);
            this.labComInfo.TabIndex = 10;
            this.labComInfo.Text = "COM口信息";
            // 
            // labLinkClient
            // 
            this.labLinkClient.ArtTextStyle = Com_CSSkin.SkinControl.ArtTextStyle.Forme;
            this.labLinkClient.AutoSize = true;
            this.labLinkClient.BackColor = System.Drawing.Color.Transparent;
            this.labLinkClient.BorderColor = System.Drawing.Color.Silver;
            this.labLinkClient.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labLinkClient.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labLinkClient.Location = new System.Drawing.Point(129, 4);
            this.labLinkClient.Name = "labLinkClient";
            this.labLinkClient.Size = new System.Drawing.Size(86, 20);
            this.labLinkClient.TabIndex = 11;
            this.labLinkClient.Text = "- Client信息";
            // 
            // Frm_SerialServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CaptionBackColorTop = System.Drawing.Color.AntiqueWhite;
            this.CaptionFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(796, 480);
            this.CloseBoxSize = new System.Drawing.Size(32, 24);
            this.Controls.Add(this.labLinkClient);
            this.Controls.Add(this.labComInfo);
            this.Controls.Add(this.groupBoxEx3);
            this.Controls.Add(this.groupBoxEx2);
            this.Controls.Add(this.groupBoxEx1);
            this.EffectBack = System.Drawing.Color.Silver;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(796, 480);
            this.MaxSize = new System.Drawing.Size(32, 24);
            this.MinimumSize = new System.Drawing.Size(796, 480);
            this.MiniSize = new System.Drawing.Size(32, 24);
            this.Name = "Frm_SerialServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "串口通讯转发端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_SerialServer_FormClosing);
            this.Load += new System.EventHandler(this.Frm_SerialServer_Load);
            this.groupBoxEx1.ResumeLayout(false);
            this.groupBoxEx1.PerformLayout();
            this.groupBoxEx2.ResumeLayout(false);
            this.groupBoxEx2.PerformLayout();
            this.groupBoxEx3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZCS_FormUI.Controls.UCBtnExt btnClean;
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx1;
        private ZCS_FormUI.Controls.UCBtnExt btnStart;
        private Com_CSSkin.SkinControl.SkinLabel lblCount;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel3;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel2;
        private ZCS_FormUI.Controls.UCTextBoxEx txtPort;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel1;
        private ZCS_FormUI.Controls.UCTextBoxEx txtTCPIP;
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx2;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel8;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel6;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel5;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel4;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel7;
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx3;
        private Com_CSSkin.SkinControl.SkinLabel labComInfo;
        private Com_CSSkin.SkinControl.SkinComboBox cmbPortParity;
        private Com_CSSkin.SkinControl.SkinLabel labLinkClient;
        private System.Windows.Forms.RichTextBox txtShowMsg;
        private Com_CSSkin.SkinControl.SkinComboBox cmbComLst;
        private Com_CSSkin.SkinControl.SkinComboBox cmbDataBits;
        private Com_CSSkin.SkinControl.SkinComboBox cmbStopBits;
        private Com_CSSkin.SkinControl.SkinComboBox cmbBandRate;
    }
}