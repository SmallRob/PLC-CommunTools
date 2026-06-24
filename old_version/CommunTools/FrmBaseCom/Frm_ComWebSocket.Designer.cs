
namespace CommunTools
{
    partial class Frm_ComWebSocket
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ComWebSocket));
            this.groupBoxEx1 = new ZCS_FormUI.Controls.GroupBoxEx();
            this.ckbTLS = new ZCS_FormUI.Controls.UCCheckBox();
            this.btnStart = new ZCS_FormUI.Controls.UCBtnExt();
            this.skinLabel1 = new Com_CSSkin.SkinControl.SkinLabel();
            this.txtTCPIP = new ZCS_FormUI.Controls.UCTextBoxEx();
            this.groupBoxEx3 = new ZCS_FormUI.Controls.GroupBoxEx();
            this.txtShowMsg = new System.Windows.Forms.RichTextBox();
            this.lblrecestatus = new Com_CSSkin.SkinControl.SkinLabel();
            this.skinPanel1 = new Com_CSSkin.SkinControl.SkinPanel();
            this.rdbFile = new Com_CSSkin.SkinControl.SkinRadioButton();
            this.rdbDirct = new Com_CSSkin.SkinControl.SkinRadioButton();
            this.ucCheckBox1 = new ZCS_FormUI.Controls.UCCheckBox();
            this.btnSend = new ZCS_FormUI.Controls.UCBtnExt();
            this.btnScan = new ZCS_FormUI.Controls.UCBtnExt();
            this.groupBoxEx4 = new ZCS_FormUI.Controls.GroupBoxEx();
            this.richTextBox_Send = new System.Windows.Forms.RichTextBox();
            this.txtFileDic = new ZCS_FormUI.Controls.UCTextBoxEx();
            this.ckbFile = new ZCS_FormUI.Controls.UCCheckBox();
            this.skinLabel4 = new Com_CSSkin.SkinControl.SkinLabel();
            this.labComInfo = new Com_CSSkin.SkinControl.SkinLabel();
            this.btnSendFile = new ZCS_FormUI.Controls.UCBtnExt();
            this.groupBoxEx1.SuspendLayout();
            this.groupBoxEx3.SuspendLayout();
            this.skinPanel1.SuspendLayout();
            this.groupBoxEx4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxEx1
            // 
            this.groupBoxEx1.Controls.Add(this.ckbTLS);
            this.groupBoxEx1.Controls.Add(this.btnStart);
            this.groupBoxEx1.Controls.Add(this.skinLabel1);
            this.groupBoxEx1.Controls.Add(this.txtTCPIP);
            this.groupBoxEx1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxEx1.Location = new System.Drawing.Point(10, 31);
            this.groupBoxEx1.Name = "groupBoxEx1";
            this.groupBoxEx1.Size = new System.Drawing.Size(394, 127);
            this.groupBoxEx1.TabIndex = 1;
            this.groupBoxEx1.TabStop = false;
            this.groupBoxEx1.Text = "连接到WebSocket服务主机";
            // 
            // ckbTLS
            // 
            this.ckbTLS.BackColor = System.Drawing.Color.Transparent;
            this.ckbTLS.Checked = false;
            this.ckbTLS.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbTLS.Location = new System.Drawing.Point(71, 74);
            this.ckbTLS.Name = "ckbTLS";
            this.ckbTLS.Padding = new System.Windows.Forms.Padding(1);
            this.ckbTLS.Size = new System.Drawing.Size(108, 26);
            this.ckbTLS.TabIndex = 42;
            this.ckbTLS.TextValue = "TLS/SSL服务";
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.White;
            this.btnStart.BtnBackColor = System.Drawing.Color.White;
            this.btnStart.BtnFont = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.BtnForeColor = System.Drawing.Color.White;
            this.btnStart.BtnText = "链接服务";
            this.btnStart.ConerRadius = 5;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.EnabledMouseEffect = false;
            this.btnStart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnStart.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnStart.IsRadius = true;
            this.btnStart.IsShowRect = true;
            this.btnStart.IsShowTips = false;
            this.btnStart.Location = new System.Drawing.Point(256, 72);
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
            // skinLabel1
            // 
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(17, 28);
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
            this.txtTCPIP.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTCPIP.InputText = "";
            this.txtTCPIP.InputType = ZCS_FormUI.TextInputType.NotControl;
            this.txtTCPIP.IsFocusColor = true;
            this.txtTCPIP.IsRadius = true;
            this.txtTCPIP.IsShowClearBtn = true;
            this.txtTCPIP.IsShowKeyboard = false;
            this.txtTCPIP.IsShowRect = true;
            this.txtTCPIP.IsShowSearchBtn = false;
            this.txtTCPIP.KeyBoardType = ZCS_FormUI.KeyBoardType.UCKeyBorderAll_EN;
            this.txtTCPIP.Location = new System.Drawing.Point(71, 27);
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
            this.txtTCPIP.PromptFont = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTCPIP.PromptText = "";
            this.txtTCPIP.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtTCPIP.RectWidth = 1;
            this.txtTCPIP.RegexPattern = "";
            this.txtTCPIP.Size = new System.Drawing.Size(291, 26);
            this.txtTCPIP.TabIndex = 0;
            // 
            // groupBoxEx3
            // 
            this.groupBoxEx3.Controls.Add(this.txtShowMsg);
            this.groupBoxEx3.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxEx3.Location = new System.Drawing.Point(413, 31);
            this.groupBoxEx3.Name = "groupBoxEx3";
            this.groupBoxEx3.Size = new System.Drawing.Size(375, 225);
            this.groupBoxEx3.TabIndex = 7;
            this.groupBoxEx3.TabStop = false;
            this.groupBoxEx3.Text = "输出消息";
            // 
            // txtShowMsg
            // 
            this.txtShowMsg.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtShowMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtShowMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtShowMsg.Location = new System.Drawing.Point(3, 22);
            this.txtShowMsg.Name = "txtShowMsg";
            this.txtShowMsg.Size = new System.Drawing.Size(369, 200);
            this.txtShowMsg.TabIndex = 0;
            this.txtShowMsg.Text = "";
            // 
            // lblrecestatus
            // 
            this.lblrecestatus.AutoSize = true;
            this.lblrecestatus.BackColor = System.Drawing.Color.Transparent;
            this.lblrecestatus.BorderColor = System.Drawing.Color.White;
            this.lblrecestatus.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblrecestatus.ForeColor = System.Drawing.Color.BlueViolet;
            this.lblrecestatus.Location = new System.Drawing.Point(414, 262);
            this.lblrecestatus.Name = "lblrecestatus";
            this.lblrecestatus.Size = new System.Drawing.Size(183, 20);
            this.lblrecestatus.TabIndex = 39;
            this.lblrecestatus.Text = "已接收数据：接收包/总字节";
            // 
            // skinPanel1
            // 
            this.skinPanel1.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel1.Controls.Add(this.rdbFile);
            this.skinPanel1.Controls.Add(this.rdbDirct);
            this.skinPanel1.ControlState = Com_CSSkin.SkinClass.ControlState.Normal;
            this.skinPanel1.DownBack = null;
            this.skinPanel1.Location = new System.Drawing.Point(530, 312);
            this.skinPanel1.MouseBack = null;
            this.skinPanel1.Name = "skinPanel1";
            this.skinPanel1.NormlBack = null;
            this.skinPanel1.Size = new System.Drawing.Size(170, 25);
            this.skinPanel1.TabIndex = 42;
            // 
            // rdbFile
            // 
            this.rdbFile.AutoSize = true;
            this.rdbFile.BackColor = System.Drawing.Color.Transparent;
            this.rdbFile.BaseColor = System.Drawing.Color.DimGray;
            this.rdbFile.ControlState = Com_CSSkin.SkinClass.ControlState.Normal;
            this.rdbFile.DownBack = null;
            this.rdbFile.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.rdbDirct.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdbDirct.Location = new System.Drawing.Point(83, 3);
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
            this.ucCheckBox1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucCheckBox1.Location = new System.Drawing.Point(418, 340);
            this.ucCheckBox1.Name = "ucCheckBox1";
            this.ucCheckBox1.Padding = new System.Windows.Forms.Padding(1);
            this.ucCheckBox1.Size = new System.Drawing.Size(108, 26);
            this.ucCheckBox1.TabIndex = 41;
            this.ucCheckBox1.TextValue = "文件轮询发送";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.White;
            this.btnSend.BtnBackColor = System.Drawing.Color.White;
            this.btnSend.BtnFont = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.BtnForeColor = System.Drawing.Color.White;
            this.btnSend.BtnText = "发送";
            this.btnSend.ConerRadius = 5;
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.EnabledMouseEffect = false;
            this.btnSend.FillColor = System.Drawing.Color.Green;
            this.btnSend.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnSend.IsRadius = true;
            this.btnSend.IsShowRect = true;
            this.btnSend.IsShowTips = false;
            this.btnSend.Location = new System.Drawing.Point(445, 410);
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
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.Color.White;
            this.btnScan.BtnBackColor = System.Drawing.Color.White;
            this.btnScan.BtnFont = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScan.BtnForeColor = System.Drawing.Color.White;
            this.btnScan.BtnText = "浏览...";
            this.btnScan.ConerRadius = 5;
            this.btnScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScan.EnabledMouseEffect = false;
            this.btnScan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnScan.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScan.IsRadius = true;
            this.btnScan.IsShowRect = true;
            this.btnScan.IsShowTips = false;
            this.btnScan.Location = new System.Drawing.Point(671, 372);
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
            this.groupBoxEx4.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxEx4.Location = new System.Drawing.Point(10, 164);
            this.groupBoxEx4.Name = "groupBoxEx4";
            this.groupBoxEx4.Size = new System.Drawing.Size(394, 281);
            this.groupBoxEx4.TabIndex = 35;
            this.groupBoxEx4.TabStop = false;
            this.groupBoxEx4.Text = "待发送信息";
            // 
            // richTextBox_Send
            // 
            this.richTextBox_Send.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox_Send.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_Send.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Send.ForeColor = System.Drawing.Color.Black;
            this.richTextBox_Send.Location = new System.Drawing.Point(3, 22);
            this.richTextBox_Send.Name = "richTextBox_Send";
            this.richTextBox_Send.Size = new System.Drawing.Size(388, 256);
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
            this.txtFileDic.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFileDic.InputText = "";
            this.txtFileDic.InputType = ZCS_FormUI.TextInputType.NotControl;
            this.txtFileDic.IsFocusColor = true;
            this.txtFileDic.IsRadius = true;
            this.txtFileDic.IsShowClearBtn = true;
            this.txtFileDic.IsShowKeyboard = false;
            this.txtFileDic.IsShowRect = true;
            this.txtFileDic.IsShowSearchBtn = false;
            this.txtFileDic.KeyBoardType = ZCS_FormUI.KeyBoardType.UCKeyBorderAll_EN;
            this.txtFileDic.Location = new System.Drawing.Point(445, 372);
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
            this.txtFileDic.PromptFont = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
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
            this.ckbFile.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbFile.Location = new System.Drawing.Point(418, 311);
            this.ckbFile.Name = "ckbFile";
            this.ckbFile.Padding = new System.Windows.Forms.Padding(1);
            this.ckbFile.Size = new System.Drawing.Size(113, 26);
            this.ckbFile.TabIndex = 34;
            this.ckbFile.TextValue = "发送文件内容";
            // 
            // skinLabel4
            // 
            this.skinLabel4.AutoSize = true;
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel4.Location = new System.Drawing.Point(421, 288);
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
            this.labComInfo.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labComInfo.ForeColor = System.Drawing.Color.BlueViolet;
            this.labComInfo.Location = new System.Drawing.Point(18, 450);
            this.labComInfo.Name = "labComInfo";
            this.labComInfo.Size = new System.Drawing.Size(65, 20);
            this.labComInfo.TabIndex = 31;
            this.labComInfo.Text = "提示消息";
            // 
            // btnSendFile
            // 
            this.btnSendFile.BackColor = System.Drawing.Color.White;
            this.btnSendFile.BtnBackColor = System.Drawing.Color.White;
            this.btnSendFile.BtnFont = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSendFile.BtnForeColor = System.Drawing.Color.White;
            this.btnSendFile.BtnText = "发送文件";
            this.btnSendFile.ConerRadius = 5;
            this.btnSendFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendFile.EnabledMouseEffect = false;
            this.btnSendFile.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnSendFile.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnSendFile.IsRadius = true;
            this.btnSendFile.IsShowRect = true;
            this.btnSendFile.IsShowTips = false;
            this.btnSendFile.Location = new System.Drawing.Point(566, 410);
            this.btnSendFile.Margin = new System.Windows.Forms.Padding(0);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(58)))));
            this.btnSendFile.RectWidth = 1;
            this.btnSendFile.Size = new System.Drawing.Size(86, 33);
            this.btnSendFile.TabIndex = 38;
            this.btnSendFile.TabStop = false;
            this.btnSendFile.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnSendFile.TipsText = "";
            // 
            // Frm_ComWebSocket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CaptionBackColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.CaptionFont = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(796, 480);
            this.CloseBoxSize = new System.Drawing.Size(32, 24);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.lblrecestatus);
            this.Controls.Add(this.skinPanel1);
            this.Controls.Add(this.ucCheckBox1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.groupBoxEx4);
            this.Controls.Add(this.txtFileDic);
            this.Controls.Add(this.ckbFile);
            this.Controls.Add(this.skinLabel4);
            this.Controls.Add(this.labComInfo);
            this.Controls.Add(this.groupBoxEx3);
            this.Controls.Add(this.groupBoxEx1);
            this.EffectBack = System.Drawing.Color.Silver;
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(796, 480);
            this.MaxSize = new System.Drawing.Size(32, 24);
            this.MinimumSize = new System.Drawing.Size(796, 480);
            this.MiniSize = new System.Drawing.Size(32, 24);
            this.Name = "Frm_ComWebSocket";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebSocket客户端";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_ComWebSocket_FormClosed);
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
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx1;
        private ZCS_FormUI.Controls.UCBtnExt btnStart;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel1;
        private ZCS_FormUI.Controls.UCTextBoxEx txtTCPIP;
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx3;
        private System.Windows.Forms.RichTextBox txtShowMsg;
        private Com_CSSkin.SkinControl.SkinPanel skinPanel1;
        private Com_CSSkin.SkinControl.SkinRadioButton rdbFile;
        private Com_CSSkin.SkinControl.SkinRadioButton rdbDirct;
        private ZCS_FormUI.Controls.UCCheckBox ucCheckBox1;
        private ZCS_FormUI.Controls.UCBtnExt btnSend;
        private ZCS_FormUI.Controls.UCBtnExt btnScan;
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx4;
        private System.Windows.Forms.RichTextBox richTextBox_Send;
        private ZCS_FormUI.Controls.UCTextBoxEx txtFileDic;
        private ZCS_FormUI.Controls.UCCheckBox ckbFile;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel4;
        private Com_CSSkin.SkinControl.SkinLabel labComInfo;
        private Com_CSSkin.SkinControl.SkinLabel lblrecestatus;
        private ZCS_FormUI.Controls.UCBtnExt btnSendFile;
        private ZCS_FormUI.Controls.UCCheckBox ckbTLS;
    }
}