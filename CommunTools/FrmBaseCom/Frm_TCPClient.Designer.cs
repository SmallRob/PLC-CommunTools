
namespace CommunTools
{
    partial class Frm_TCPClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_TCPClient));
            this.btnClean = new ZCS_FormUI.Controls.UCBtnExt();
            this.groupBoxEx1 = new ZCS_FormUI.Controls.GroupBoxEx();
            this.btnStart = new ZCS_FormUI.Controls.UCBtnExt();
            this.skinLabel2 = new Com_CSSkin.SkinControl.SkinLabel();
            this.txtPort = new ZCS_FormUI.Controls.UCTextBoxEx();
            this.skinLabel1 = new Com_CSSkin.SkinControl.SkinLabel();
            this.txtTCPIP = new ZCS_FormUI.Controls.UCTextBoxEx();
            this.groupBoxEx3 = new ZCS_FormUI.Controls.GroupBoxEx();
            this.ckbRevHEX = new ZCS_FormUI.Controls.UCCheckBox();
            this.txtShowMsg = new System.Windows.Forms.RichTextBox();
            this.skinPanel1 = new Com_CSSkin.SkinControl.SkinPanel();
            this.rdbFile = new Com_CSSkin.SkinControl.SkinRadioButton();
            this.rdbDirct = new Com_CSSkin.SkinControl.SkinRadioButton();
            this.ucCheckBox1 = new ZCS_FormUI.Controls.UCCheckBox();
            this.lblSendStatus = new Com_CSSkin.SkinControl.SkinLabel();
            this.lblrecestatus = new Com_CSSkin.SkinControl.SkinLabel();
            this.ucBtnExt1 = new ZCS_FormUI.Controls.UCBtnExt();
            this.btnSend = new ZCS_FormUI.Controls.UCBtnExt();
            this.ckbHEX = new ZCS_FormUI.Controls.UCCheckBox();
            this.btnScan = new ZCS_FormUI.Controls.UCBtnExt();
            this.groupBoxEx4 = new ZCS_FormUI.Controls.GroupBoxEx();
            this.richTextBox_Send = new System.Windows.Forms.RichTextBox();
            this.txtFileDic = new ZCS_FormUI.Controls.UCTextBoxEx();
            this.ckbFile = new ZCS_FormUI.Controls.UCCheckBox();
            this.txtSendTime = new ZCS_FormUI.Controls.UCTextBoxEx();
            this.ckbTimeSend = new ZCS_FormUI.Controls.UCCheckBox();
            this.skinLabel4 = new Com_CSSkin.SkinControl.SkinLabel();
            this.labComInfo = new Com_CSSkin.SkinControl.SkinLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBoxEx1.SuspendLayout();
            this.groupBoxEx3.SuspendLayout();
            this.skinPanel1.SuspendLayout();
            this.groupBoxEx4.SuspendLayout();
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
            this.btnClean.BtnClick += new System.EventHandler(this.btnClean_BtnClick);
            // 
            // groupBoxEx1
            // 
            this.groupBoxEx1.Controls.Add(this.btnStart);
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
            this.groupBoxEx1.Text = "连接到TCP主机";
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.White;
            this.btnStart.BtnBackColor = System.Drawing.Color.White;
            this.btnStart.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.BtnForeColor = System.Drawing.Color.White;
            this.btnStart.BtnText = "连接";
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
            this.btnStart.BtnClick += new System.EventHandler(this.btnStart_BtnClick);
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
            // groupBoxEx3
            // 
            this.groupBoxEx3.Controls.Add(this.ckbRevHEX);
            this.groupBoxEx3.Controls.Add(this.txtShowMsg);
            this.groupBoxEx3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxEx3.Location = new System.Drawing.Point(413, 31);
            this.groupBoxEx3.Name = "groupBoxEx3";
            this.groupBoxEx3.Size = new System.Drawing.Size(375, 156);
            this.groupBoxEx3.TabIndex = 7;
            this.groupBoxEx3.TabStop = false;
            this.groupBoxEx3.Text = "接收信息";
            // 
            // ckbRevHEX
            // 
            this.ckbRevHEX.BackColor = System.Drawing.Color.Transparent;
            this.ckbRevHEX.Checked = false;
            this.ckbRevHEX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbRevHEX.Location = new System.Drawing.Point(236, -4);
            this.ckbRevHEX.Name = "ckbRevHEX";
            this.ckbRevHEX.Padding = new System.Windows.Forms.Padding(1);
            this.ckbRevHEX.Size = new System.Drawing.Size(108, 26);
            this.ckbRevHEX.TabIndex = 37;
            this.ckbRevHEX.TextValue = "HEX格式显示";
            // 
            // txtShowMsg
            // 
            this.txtShowMsg.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtShowMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtShowMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtShowMsg.Location = new System.Drawing.Point(3, 22);
            this.txtShowMsg.Name = "txtShowMsg";
            this.txtShowMsg.Size = new System.Drawing.Size(369, 131);
            this.txtShowMsg.TabIndex = 0;
            this.txtShowMsg.Text = "";
            // 
            // skinPanel1
            // 
            this.skinPanel1.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel1.Controls.Add(this.rdbFile);
            this.skinPanel1.Controls.Add(this.rdbDirct);
            this.skinPanel1.ControlState = Com_CSSkin.SkinClass.ControlState.Normal;
            this.skinPanel1.DownBack = null;
            this.skinPanel1.Location = new System.Drawing.Point(530, 281);
            this.skinPanel1.MouseBack = null;
            this.skinPanel1.Name = "skinPanel1";
            this.skinPanel1.NormlBack = null;
            this.skinPanel1.Size = new System.Drawing.Size(78, 54);
            this.skinPanel1.TabIndex = 42;
            // 
            // rdbFile
            // 
            this.rdbFile.AutoSize = true;
            this.rdbFile.BackColor = System.Drawing.Color.Transparent;
            this.rdbFile.BaseColor = System.Drawing.Color.DimGray;
            this.rdbFile.ControlState = Com_CSSkin.SkinClass.ControlState.Normal;
            this.rdbFile.DownBack = null;
            this.rdbFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdbFile.Location = new System.Drawing.Point(3, 3);
            this.rdbFile.MouseBack = null;
            this.rdbFile.Name = "rdbFile";
            this.rdbFile.NormlBack = null;
            this.rdbFile.SelectedDownBack = null;
            this.rdbFile.SelectedMouseBack = null;
            this.rdbFile.SelectedNormlBack = null;
            this.rdbFile.Size = new System.Drawing.Size(74, 21);
            this.rdbFile.TabIndex = 25;
            this.rdbFile.TabStop = true;
            this.rdbFile.Text = "文本文件";
            this.rdbFile.UseVisualStyleBackColor = false;
            // 
            // rdbDirct
            // 
            this.rdbDirct.AutoSize = true;
            this.rdbDirct.BackColor = System.Drawing.Color.Transparent;
            this.rdbDirct.BaseColor = System.Drawing.Color.DimGray;
            this.rdbDirct.ControlState = Com_CSSkin.SkinClass.ControlState.Normal;
            this.rdbDirct.DownBack = null;
            this.rdbDirct.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdbDirct.Location = new System.Drawing.Point(3, 30);
            this.rdbDirct.MouseBack = null;
            this.rdbDirct.Name = "rdbDirct";
            this.rdbDirct.NormlBack = null;
            this.rdbDirct.SelectedDownBack = null;
            this.rdbDirct.SelectedMouseBack = null;
            this.rdbDirct.SelectedNormlBack = null;
            this.rdbDirct.Size = new System.Drawing.Size(62, 21);
            this.rdbDirct.TabIndex = 26;
            this.rdbDirct.TabStop = true;
            this.rdbDirct.Text = "文件夹";
            this.rdbDirct.UseVisualStyleBackColor = false;
            // 
            // ucCheckBox1
            // 
            this.ucCheckBox1.BackColor = System.Drawing.Color.Transparent;
            this.ucCheckBox1.Checked = false;
            this.ucCheckBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucCheckBox1.Location = new System.Drawing.Point(633, 280);
            this.ucCheckBox1.Name = "ucCheckBox1";
            this.ucCheckBox1.Padding = new System.Windows.Forms.Padding(1);
            this.ucCheckBox1.Size = new System.Drawing.Size(108, 26);
            this.ucCheckBox1.TabIndex = 41;
            this.ucCheckBox1.TextValue = "文件轮询发送";
            // 
            // lblSendStatus
            // 
            this.lblSendStatus.AutoSize = true;
            this.lblSendStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblSendStatus.BorderColor = System.Drawing.Color.White;
            this.lblSendStatus.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSendStatus.ForeColor = System.Drawing.Color.BlueViolet;
            this.lblSendStatus.Location = new System.Drawing.Point(425, 425);
            this.lblSendStatus.Name = "lblSendStatus";
            this.lblSendStatus.Size = new System.Drawing.Size(101, 20);
            this.lblSendStatus.TabIndex = 40;
            this.lblSendStatus.Text = "已发送数据：0";
            // 
            // lblrecestatus
            // 
            this.lblrecestatus.AutoSize = true;
            this.lblrecestatus.BackColor = System.Drawing.Color.Transparent;
            this.lblrecestatus.BorderColor = System.Drawing.Color.White;
            this.lblrecestatus.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblrecestatus.ForeColor = System.Drawing.Color.BlueViolet;
            this.lblrecestatus.Location = new System.Drawing.Point(425, 189);
            this.lblrecestatus.Name = "lblrecestatus";
            this.lblrecestatus.Size = new System.Drawing.Size(183, 20);
            this.lblrecestatus.TabIndex = 39;
            this.lblrecestatus.Text = "已接收数据：接收包/总字节";
            // 
            // ucBtnExt1
            // 
            this.ucBtnExt1.BackColor = System.Drawing.Color.White;
            this.ucBtnExt1.BtnBackColor = System.Drawing.Color.White;
            this.ucBtnExt1.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucBtnExt1.BtnForeColor = System.Drawing.Color.White;
            this.ucBtnExt1.BtnText = "清空统计数";
            this.ucBtnExt1.ConerRadius = 5;
            this.ucBtnExt1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExt1.EnabledMouseEffect = false;
            this.ucBtnExt1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ucBtnExt1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnExt1.IsRadius = true;
            this.ucBtnExt1.IsShowRect = true;
            this.ucBtnExt1.IsShowTips = false;
            this.ucBtnExt1.Location = new System.Drawing.Point(566, 379);
            this.ucBtnExt1.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnExt1.Name = "ucBtnExt1";
            this.ucBtnExt1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(58)))));
            this.ucBtnExt1.RectWidth = 1;
            this.ucBtnExt1.Size = new System.Drawing.Size(86, 33);
            this.ucBtnExt1.TabIndex = 38;
            this.ucBtnExt1.TabStop = false;
            this.ucBtnExt1.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnExt1.TipsText = "";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.White;
            this.btnSend.BtnBackColor = System.Drawing.Color.White;
            this.btnSend.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.BtnForeColor = System.Drawing.Color.White;
            this.btnSend.BtnText = "发送";
            this.btnSend.ConerRadius = 5;
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.EnabledMouseEffect = false;
            this.btnSend.FillColor = System.Drawing.Color.Green;
            this.btnSend.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnSend.IsRadius = true;
            this.btnSend.IsShowRect = true;
            this.btnSend.IsShowTips = false;
            this.btnSend.Location = new System.Drawing.Point(445, 379);
            this.btnSend.Margin = new System.Windows.Forms.Padding(0);
            this.btnSend.Name = "btnSend";
            this.btnSend.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnSend.RectWidth = 1;
            this.btnSend.Size = new System.Drawing.Size(86, 33);
            this.btnSend.TabIndex = 37;
            this.btnSend.TabStop = false;
            this.btnSend.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnSend.TipsText = "";
            this.btnSend.BtnClick += new System.EventHandler(this.btnSend_BtnClick);
            // 
            // ckbHEX
            // 
            this.ckbHEX.BackColor = System.Drawing.Color.Transparent;
            this.ckbHEX.Checked = false;
            this.ckbHEX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbHEX.Location = new System.Drawing.Point(633, 247);
            this.ckbHEX.Name = "ckbHEX";
            this.ckbHEX.Padding = new System.Windows.Forms.Padding(1);
            this.ckbHEX.Size = new System.Drawing.Size(108, 26);
            this.ckbHEX.TabIndex = 36;
            this.ckbHEX.TextValue = "HEX格式发送";
            // 
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.Color.White;
            this.btnScan.BtnBackColor = System.Drawing.Color.White;
            this.btnScan.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScan.BtnForeColor = System.Drawing.Color.White;
            this.btnScan.BtnText = "浏览...";
            this.btnScan.ConerRadius = 5;
            this.btnScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScan.EnabledMouseEffect = false;
            this.btnScan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnScan.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScan.IsRadius = true;
            this.btnScan.IsShowRect = true;
            this.btnScan.IsShowTips = false;
            this.btnScan.Location = new System.Drawing.Point(671, 341);
            this.btnScan.Margin = new System.Windows.Forms.Padding(0);
            this.btnScan.Name = "btnScan";
            this.btnScan.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(58)))));
            this.btnScan.RectWidth = 1;
            this.btnScan.Size = new System.Drawing.Size(70, 24);
            this.btnScan.TabIndex = 28;
            this.btnScan.TabStop = false;
            this.btnScan.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnScan.TipsText = "";
            // 
            // groupBoxEx4
            // 
            this.groupBoxEx4.Controls.Add(this.richTextBox_Send);
            this.groupBoxEx4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxEx4.Location = new System.Drawing.Point(10, 193);
            this.groupBoxEx4.Name = "groupBoxEx4";
            this.groupBoxEx4.Size = new System.Drawing.Size(394, 253);
            this.groupBoxEx4.TabIndex = 35;
            this.groupBoxEx4.TabStop = false;
            this.groupBoxEx4.Text = "待发送信息";
            // 
            // richTextBox_Send
            // 
            this.richTextBox_Send.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox_Send.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_Send.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Send.Location = new System.Drawing.Point(3, 22);
            this.richTextBox_Send.Name = "richTextBox_Send";
            this.richTextBox_Send.Size = new System.Drawing.Size(388, 228);
            this.richTextBox_Send.TabIndex = 0;
            this.richTextBox_Send.Text = "";
            // 
            // txtFileDic
            // 
            this.txtFileDic.BackColor = System.Drawing.Color.Transparent;
            this.txtFileDic.ConerRadius = 5;
            this.txtFileDic.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFileDic.DecLength = 2;
            this.txtFileDic.FillColor = System.Drawing.Color.Empty;
            this.txtFileDic.FocusBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.txtFileDic.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFileDic.InputText = "";
            this.txtFileDic.InputType = ZCS_FormUI.TextInputType.NotControl;
            this.txtFileDic.IsFocusColor = true;
            this.txtFileDic.IsRadius = true;
            this.txtFileDic.IsShowClearBtn = true;
            this.txtFileDic.IsShowKeyboard = false;
            this.txtFileDic.IsShowRect = true;
            this.txtFileDic.IsShowSearchBtn = false;
            this.txtFileDic.KeyBoardType = ZCS_FormUI.KeyBoardType.UCKeyBorderAll_EN;
            this.txtFileDic.Location = new System.Drawing.Point(445, 341);
            this.txtFileDic.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFileDic.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtFileDic.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.txtFileDic.Name = "txtFileDic";
            this.txtFileDic.Padding = new System.Windows.Forms.Padding(5);
            this.txtFileDic.PasswordChar = '\0';
            this.txtFileDic.PromptColor = System.Drawing.Color.Gray;
            this.txtFileDic.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtFileDic.PromptText = "";
            this.txtFileDic.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtFileDic.RectWidth = 1;
            this.txtFileDic.RegexPattern = "";
            this.txtFileDic.Size = new System.Drawing.Size(207, 26);
            this.txtFileDic.TabIndex = 33;
            // 
            // ckbFile
            // 
            this.ckbFile.BackColor = System.Drawing.Color.Transparent;
            this.ckbFile.Checked = false;
            this.ckbFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbFile.Location = new System.Drawing.Point(418, 280);
            this.ckbFile.Name = "ckbFile";
            this.ckbFile.Padding = new System.Windows.Forms.Padding(1);
            this.ckbFile.Size = new System.Drawing.Size(113, 26);
            this.ckbFile.TabIndex = 34;
            this.ckbFile.TextValue = "发送文件内容";
            // 
            // txtSendTime
            // 
            this.txtSendTime.BackColor = System.Drawing.Color.Transparent;
            this.txtSendTime.ConerRadius = 5;
            this.txtSendTime.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSendTime.DecLength = 2;
            this.txtSendTime.FillColor = System.Drawing.Color.Empty;
            this.txtSendTime.FocusBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.txtSendTime.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSendTime.InputText = "";
            this.txtSendTime.InputType = ZCS_FormUI.TextInputType.NotControl;
            this.txtSendTime.IsFocusColor = true;
            this.txtSendTime.IsRadius = true;
            this.txtSendTime.IsShowClearBtn = true;
            this.txtSendTime.IsShowKeyboard = false;
            this.txtSendTime.IsShowRect = true;
            this.txtSendTime.IsShowSearchBtn = false;
            this.txtSendTime.KeyBoardType = ZCS_FormUI.KeyBoardType.UCKeyBorderAll_EN;
            this.txtSendTime.Location = new System.Drawing.Point(533, 247);
            this.txtSendTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSendTime.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtSendTime.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.txtSendTime.Name = "txtSendTime";
            this.txtSendTime.Padding = new System.Windows.Forms.Padding(5);
            this.txtSendTime.PasswordChar = '\0';
            this.txtSendTime.PromptColor = System.Drawing.Color.Gray;
            this.txtSendTime.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtSendTime.PromptText = "";
            this.txtSendTime.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtSendTime.RectWidth = 1;
            this.txtSendTime.RegexPattern = "";
            this.txtSendTime.Size = new System.Drawing.Size(70, 26);
            this.txtSendTime.TabIndex = 30;
            // 
            // ckbTimeSend
            // 
            this.ckbTimeSend.BackColor = System.Drawing.Color.Transparent;
            this.ckbTimeSend.Checked = false;
            this.ckbTimeSend.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbTimeSend.Location = new System.Drawing.Point(418, 247);
            this.ckbTimeSend.Name = "ckbTimeSend";
            this.ckbTimeSend.Padding = new System.Windows.Forms.Padding(1);
            this.ckbTimeSend.Size = new System.Drawing.Size(108, 26);
            this.ckbTimeSend.TabIndex = 32;
            this.ckbTimeSend.TextValue = "定时发送(ms)";
            // 
            // skinLabel4
            // 
            this.skinLabel4.AutoSize = true;
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel4.Location = new System.Drawing.Point(425, 216);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(79, 20);
            this.skinLabel4.TabIndex = 29;
            this.skinLabel4.Text = "发送数据：";
            // 
            // labComInfo
            // 
            this.labComInfo.AutoSize = true;
            this.labComInfo.BackColor = System.Drawing.Color.Transparent;
            this.labComInfo.BorderColor = System.Drawing.Color.White;
            this.labComInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labComInfo.ForeColor = System.Drawing.Color.BlueViolet;
            this.labComInfo.Location = new System.Drawing.Point(18, 450);
            this.labComInfo.Name = "labComInfo";
            this.labComInfo.Size = new System.Drawing.Size(65, 20);
            this.labComInfo.TabIndex = 31;
            this.labComInfo.Text = "提示消息";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Frm_TCPClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CaptionBackColorTop = System.Drawing.Color.AntiqueWhite;
            this.CaptionFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(796, 480);
            this.CloseBoxSize = new System.Drawing.Size(32, 24);
            this.Controls.Add(this.skinPanel1);
            this.Controls.Add(this.ucCheckBox1);
            this.Controls.Add(this.lblSendStatus);
            this.Controls.Add(this.lblrecestatus);
            this.Controls.Add(this.ucBtnExt1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.ckbHEX);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.groupBoxEx4);
            this.Controls.Add(this.txtFileDic);
            this.Controls.Add(this.ckbFile);
            this.Controls.Add(this.txtSendTime);
            this.Controls.Add(this.ckbTimeSend);
            this.Controls.Add(this.skinLabel4);
            this.Controls.Add(this.labComInfo);
            this.Controls.Add(this.groupBoxEx3);
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
            this.Name = "Frm_TCPClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCP客户端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_SerialServer_FormClosing);
            this.Load += new System.EventHandler(this.Frm_SerialServer_Load);
            this.groupBoxEx1.ResumeLayout(false);
            this.groupBoxEx1.PerformLayout();
            this.groupBoxEx3.ResumeLayout(false);
            this.skinPanel1.ResumeLayout(false);
            this.skinPanel1.PerformLayout();
            this.groupBoxEx4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZCS_FormUI.Controls.UCBtnExt btnClean;
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx1;
        private ZCS_FormUI.Controls.UCBtnExt btnStart;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel2;
        private ZCS_FormUI.Controls.UCTextBoxEx txtPort;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel1;
        private ZCS_FormUI.Controls.UCTextBoxEx txtTCPIP;
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx3;
        private System.Windows.Forms.RichTextBox txtShowMsg;
        private Com_CSSkin.SkinControl.SkinPanel skinPanel1;
        private Com_CSSkin.SkinControl.SkinRadioButton rdbFile;
        private Com_CSSkin.SkinControl.SkinRadioButton rdbDirct;
        private ZCS_FormUI.Controls.UCCheckBox ucCheckBox1;
        private Com_CSSkin.SkinControl.SkinLabel lblSendStatus;
        private Com_CSSkin.SkinControl.SkinLabel lblrecestatus;
        private ZCS_FormUI.Controls.UCBtnExt ucBtnExt1;
        private ZCS_FormUI.Controls.UCBtnExt btnSend;
        private ZCS_FormUI.Controls.UCCheckBox ckbHEX;
        private ZCS_FormUI.Controls.UCBtnExt btnScan;
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx4;
        private System.Windows.Forms.RichTextBox richTextBox_Send;
        private ZCS_FormUI.Controls.UCTextBoxEx txtFileDic;
        private ZCS_FormUI.Controls.UCCheckBox ckbFile;
        private ZCS_FormUI.Controls.UCTextBoxEx txtSendTime;
        private ZCS_FormUI.Controls.UCCheckBox ckbTimeSend;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel4;
        private Com_CSSkin.SkinControl.SkinLabel labComInfo;
        private ZCS_FormUI.Controls.UCCheckBox ckbRevHEX;
        private System.Windows.Forms.Timer timer1;
    }
}