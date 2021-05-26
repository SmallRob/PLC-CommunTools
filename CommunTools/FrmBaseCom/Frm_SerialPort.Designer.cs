
namespace CommunTools
{
    partial class Frm_SerialPort
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SerialPort));
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
            this.richTextBox_Receive = new System.Windows.Forms.RichTextBox();
            this.labComInfo = new Com_CSSkin.SkinControl.SkinLabel();
            this.btnRefresh = new ZCS_FormUI.Controls.UCBtnExt();
            this.skinLabel3 = new Com_CSSkin.SkinControl.SkinLabel();
            this.btnOpenPort = new ZCS_FormUI.Controls.UCBtnExt();
            this.txtSendTime = new ZCS_FormUI.Controls.UCTextBoxEx();
            this.ckbTimeSend = new ZCS_FormUI.Controls.UCCheckBox();
            this.ucTextBoxEx2 = new ZCS_FormUI.Controls.UCTextBoxEx();
            this.ucCheckBox2 = new ZCS_FormUI.Controls.UCCheckBox();
            this.groupBoxEx4 = new ZCS_FormUI.Controls.GroupBoxEx();
            this.richTextBox_Send = new System.Windows.Forms.RichTextBox();
            this.btnScan = new ZCS_FormUI.Controls.UCBtnExt();
            this.btnHAX = new ZCS_FormUI.Controls.UCCheckBox();
            this.btnSend = new ZCS_FormUI.Controls.UCBtnExt();
            this.btnClean = new ZCS_FormUI.Controls.UCBtnExt();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.lblrecestatus = new Com_CSSkin.SkinControl.SkinLabel();
            this.lblSendStatus = new Com_CSSkin.SkinControl.SkinLabel();
            this.timerSend = new System.Windows.Forms.Timer(this.components);
            this.groupBoxEx2.SuspendLayout();
            this.groupBoxEx3.SuspendLayout();
            this.groupBoxEx4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxEx2
            // 
            this.groupBoxEx2.Controls.Add(this.btnOpenPort);
            this.groupBoxEx2.Controls.Add(this.cmbDataBits);
            this.groupBoxEx2.Controls.Add(this.cmbStopBits);
            this.groupBoxEx2.Controls.Add(this.btnRefresh);
            this.groupBoxEx2.Controls.Add(this.cmbBandRate);
            this.groupBoxEx2.Controls.Add(this.cmbComLst);
            this.groupBoxEx2.Controls.Add(this.cmbPortParity);
            this.groupBoxEx2.Controls.Add(this.skinLabel8);
            this.groupBoxEx2.Controls.Add(this.skinLabel6);
            this.groupBoxEx2.Controls.Add(this.skinLabel5);
            this.groupBoxEx2.Controls.Add(this.skinLabel4);
            this.groupBoxEx2.Controls.Add(this.skinLabel7);
            this.groupBoxEx2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxEx2.Location = new System.Drawing.Point(9, 31);
            this.groupBoxEx2.Name = "groupBoxEx2";
            this.groupBoxEx2.Size = new System.Drawing.Size(519, 253);
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
            this.cmbDataBits.Location = new System.Drawing.Point(110, 165);
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
            this.cmbStopBits.Location = new System.Drawing.Point(110, 210);
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
            this.skinLabel8.Location = new System.Drawing.Point(28, 168);
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
            this.skinLabel6.Location = new System.Drawing.Point(28, 213);
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
            this.groupBoxEx3.Controls.Add(this.richTextBox_Receive);
            this.groupBoxEx3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxEx3.Location = new System.Drawing.Point(537, 31);
            this.groupBoxEx3.Name = "groupBoxEx3";
            this.groupBoxEx3.Size = new System.Drawing.Size(356, 253);
            this.groupBoxEx3.TabIndex = 7;
            this.groupBoxEx3.TabStop = false;
            this.groupBoxEx3.Text = "已接收信息";
            // 
            // richTextBox_Receive
            // 
            this.richTextBox_Receive.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox_Receive.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_Receive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Receive.Location = new System.Drawing.Point(3, 22);
            this.richTextBox_Receive.Name = "richTextBox_Receive";
            this.richTextBox_Receive.Size = new System.Drawing.Size(350, 228);
            this.richTextBox_Receive.TabIndex = 0;
            this.richTextBox_Receive.Text = "";
            // 
            // labComInfo
            // 
            this.labComInfo.AutoSize = true;
            this.labComInfo.BackColor = System.Drawing.Color.Transparent;
            this.labComInfo.BorderColor = System.Drawing.Color.White;
            this.labComInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labComInfo.ForeColor = System.Drawing.Color.BlueViolet;
            this.labComInfo.Location = new System.Drawing.Point(17, 551);
            this.labComInfo.Name = "labComInfo";
            this.labComInfo.Size = new System.Drawing.Size(85, 20);
            this.labComInfo.TabIndex = 10;
            this.labComInfo.Text = "COM口信息";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.BtnBackColor = System.Drawing.Color.White;
            this.btnRefresh.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.BtnForeColor = System.Drawing.Color.White;
            this.btnRefresh.BtnText = "刷新串口";
            this.btnRefresh.ConerRadius = 5;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.EnabledMouseEffect = false;
            this.btnRefresh.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnRefresh.IsRadius = true;
            this.btnRefresh.IsShowRect = true;
            this.btnRefresh.IsShowTips = false;
            this.btnRefresh.Location = new System.Drawing.Point(297, 85);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(58)))));
            this.btnRefresh.RectWidth = 1;
            this.btnRefresh.Size = new System.Drawing.Size(86, 33);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnRefresh.TipsText = "";
            this.btnRefresh.BtnClick += new System.EventHandler(this.btnRefresh_BtnClick);
            // 
            // skinLabel3
            // 
            this.skinLabel3.AutoSize = true;
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel3.Location = new System.Drawing.Point(551, 316);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(79, 20);
            this.skinLabel3.TabIndex = 4;
            this.skinLabel3.Text = "发送数据：";
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.BackColor = System.Drawing.Color.White;
            this.btnOpenPort.BtnBackColor = System.Drawing.Color.White;
            this.btnOpenPort.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenPort.BtnForeColor = System.Drawing.Color.White;
            this.btnOpenPort.BtnText = "打开串口";
            this.btnOpenPort.ConerRadius = 5;
            this.btnOpenPort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenPort.EnabledMouseEffect = false;
            this.btnOpenPort.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnOpenPort.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnOpenPort.IsRadius = true;
            this.btnOpenPort.IsShowRect = true;
            this.btnOpenPort.IsShowTips = false;
            this.btnOpenPort.Location = new System.Drawing.Point(297, 30);
            this.btnOpenPort.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(58)))));
            this.btnOpenPort.RectWidth = 1;
            this.btnOpenPort.Size = new System.Drawing.Size(86, 33);
            this.btnOpenPort.TabIndex = 2;
            this.btnOpenPort.TabStop = false;
            this.btnOpenPort.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnOpenPort.TipsText = "";
            this.btnOpenPort.BtnClick += new System.EventHandler(this.btnOpenServer_BtnClick);
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
            this.txtSendTime.Location = new System.Drawing.Point(659, 347);
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
            this.txtSendTime.TabIndex = 7;
            // 
            // ckbTimeSend
            // 
            this.ckbTimeSend.BackColor = System.Drawing.Color.Transparent;
            this.ckbTimeSend.Checked = false;
            this.ckbTimeSend.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbTimeSend.Location = new System.Drawing.Point(544, 347);
            this.ckbTimeSend.Name = "ckbTimeSend";
            this.ckbTimeSend.Padding = new System.Windows.Forms.Padding(1);
            this.ckbTimeSend.Size = new System.Drawing.Size(108, 26);
            this.ckbTimeSend.TabIndex = 15;
            this.ckbTimeSend.TextValue = "定时发送(ms)";
            // 
            // ucTextBoxEx2
            // 
            this.ucTextBoxEx2.BackColor = System.Drawing.Color.Transparent;
            this.ucTextBoxEx2.ConerRadius = 5;
            this.ucTextBoxEx2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ucTextBoxEx2.DecLength = 2;
            this.ucTextBoxEx2.FillColor = System.Drawing.Color.Empty;
            this.ucTextBoxEx2.FocusBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucTextBoxEx2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucTextBoxEx2.InputText = "";
            this.ucTextBoxEx2.InputType = ZCS_FormUI.TextInputType.NotControl;
            this.ucTextBoxEx2.IsFocusColor = true;
            this.ucTextBoxEx2.IsRadius = true;
            this.ucTextBoxEx2.IsShowClearBtn = true;
            this.ucTextBoxEx2.IsShowKeyboard = false;
            this.ucTextBoxEx2.IsShowRect = true;
            this.ucTextBoxEx2.IsShowSearchBtn = false;
            this.ucTextBoxEx2.KeyBoardType = ZCS_FormUI.KeyBoardType.UCKeyBorderAll_EN;
            this.ucTextBoxEx2.Location = new System.Drawing.Point(571, 413);
            this.ucTextBoxEx2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucTextBoxEx2.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.ucTextBoxEx2.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.ucTextBoxEx2.Name = "ucTextBoxEx2";
            this.ucTextBoxEx2.Padding = new System.Windows.Forms.Padding(5);
            this.ucTextBoxEx2.PasswordChar = '\0';
            this.ucTextBoxEx2.PromptColor = System.Drawing.Color.Gray;
            this.ucTextBoxEx2.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTextBoxEx2.PromptText = "";
            this.ucTextBoxEx2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucTextBoxEx2.RectWidth = 1;
            this.ucTextBoxEx2.RegexPattern = "";
            this.ucTextBoxEx2.Size = new System.Drawing.Size(301, 26);
            this.ucTextBoxEx2.TabIndex = 16;
            // 
            // ucCheckBox2
            // 
            this.ucCheckBox2.BackColor = System.Drawing.Color.Transparent;
            this.ucCheckBox2.Checked = false;
            this.ucCheckBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucCheckBox2.Location = new System.Drawing.Point(544, 379);
            this.ucCheckBox2.Name = "ucCheckBox2";
            this.ucCheckBox2.Padding = new System.Windows.Forms.Padding(1);
            this.ucCheckBox2.Size = new System.Drawing.Size(108, 26);
            this.ucCheckBox2.TabIndex = 17;
            this.ucCheckBox2.TextValue = "发送文件";
            // 
            // groupBoxEx4
            // 
            this.groupBoxEx4.Controls.Add(this.richTextBox_Send);
            this.groupBoxEx4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxEx4.Location = new System.Drawing.Point(9, 294);
            this.groupBoxEx4.Name = "groupBoxEx4";
            this.groupBoxEx4.Size = new System.Drawing.Size(518, 253);
            this.groupBoxEx4.TabIndex = 18;
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
            this.richTextBox_Send.Size = new System.Drawing.Size(512, 228);
            this.richTextBox_Send.TabIndex = 0;
            this.richTextBox_Send.Text = "";
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
            this.btnScan.Location = new System.Drawing.Point(659, 381);
            this.btnScan.Margin = new System.Windows.Forms.Padding(0);
            this.btnScan.Name = "btnScan";
            this.btnScan.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(58)))));
            this.btnScan.RectWidth = 1;
            this.btnScan.Size = new System.Drawing.Size(70, 24);
            this.btnScan.TabIndex = 2;
            this.btnScan.TabStop = false;
            this.btnScan.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnScan.TipsText = "";
            // 
            // btnHAX
            // 
            this.btnHAX.BackColor = System.Drawing.Color.Transparent;
            this.btnHAX.Checked = false;
            this.btnHAX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnHAX.Location = new System.Drawing.Point(764, 347);
            this.btnHAX.Name = "btnHAX";
            this.btnHAX.Padding = new System.Windows.Forms.Padding(1);
            this.btnHAX.Size = new System.Drawing.Size(108, 26);
            this.btnHAX.TabIndex = 19;
            this.btnHAX.TextValue = "HAX格式";
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
            this.btnSend.Location = new System.Drawing.Point(571, 460);
            this.btnSend.Margin = new System.Windows.Forms.Padding(0);
            this.btnSend.Name = "btnSend";
            this.btnSend.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnSend.RectWidth = 1;
            this.btnSend.Size = new System.Drawing.Size(86, 33);
            this.btnSend.TabIndex = 20;
            this.btnSend.TabStop = false;
            this.btnSend.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnSend.TipsText = "";
            this.btnSend.BtnClick += new System.EventHandler(this.btnSend_BtnClick);
            // 
            // btnClean
            // 
            this.btnClean.BackColor = System.Drawing.Color.White;
            this.btnClean.BtnBackColor = System.Drawing.Color.White;
            this.btnClean.BtnFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClean.BtnForeColor = System.Drawing.Color.White;
            this.btnClean.BtnText = "清空统计数";
            this.btnClean.ConerRadius = 5;
            this.btnClean.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClean.EnabledMouseEffect = false;
            this.btnClean.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnClean.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnClean.IsRadius = true;
            this.btnClean.IsShowRect = true;
            this.btnClean.IsShowTips = false;
            this.btnClean.Location = new System.Drawing.Point(692, 460);
            this.btnClean.Margin = new System.Windows.Forms.Padding(0);
            this.btnClean.Name = "btnClean";
            this.btnClean.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(58)))));
            this.btnClean.RectWidth = 1;
            this.btnClean.Size = new System.Drawing.Size(86, 33);
            this.btnClean.TabIndex = 21;
            this.btnClean.TabStop = false;
            this.btnClean.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnClean.TipsText = "";
            this.btnClean.BtnClick += new System.EventHandler(this.btnClean_BtnClick);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // lblrecestatus
            // 
            this.lblrecestatus.AutoSize = true;
            this.lblrecestatus.BackColor = System.Drawing.Color.Transparent;
            this.lblrecestatus.BorderColor = System.Drawing.Color.White;
            this.lblrecestatus.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblrecestatus.ForeColor = System.Drawing.Color.BlueViolet;
            this.lblrecestatus.Location = new System.Drawing.Point(551, 551);
            this.lblrecestatus.Name = "lblrecestatus";
            this.lblrecestatus.Size = new System.Drawing.Size(101, 20);
            this.lblrecestatus.TabIndex = 22;
            this.lblrecestatus.Text = "已接收数据：0";
            // 
            // lblSendStatus
            // 
            this.lblSendStatus.AutoSize = true;
            this.lblSendStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblSendStatus.BorderColor = System.Drawing.Color.White;
            this.lblSendStatus.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSendStatus.ForeColor = System.Drawing.Color.BlueViolet;
            this.lblSendStatus.Location = new System.Drawing.Point(551, 524);
            this.lblSendStatus.Name = "lblSendStatus";
            this.lblSendStatus.Size = new System.Drawing.Size(101, 20);
            this.lblSendStatus.TabIndex = 23;
            this.lblSendStatus.Text = "已发送数据：0";
            // 
            // timerSend
            // 
            this.timerSend.Enabled = true;
            this.timerSend.Interval = 1000;
            this.timerSend.Tick += new System.EventHandler(this.timerSend_Tick);
            // 
            // Frm_SerialPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CaptionBackColorTop = System.Drawing.Color.AntiqueWhite;
            this.CaptionFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(905, 576);
            this.CloseBoxSize = new System.Drawing.Size(32, 24);
            this.Controls.Add(this.lblSendStatus);
            this.Controls.Add(this.lblrecestatus);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnHAX);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.groupBoxEx4);
            this.Controls.Add(this.ucTextBoxEx2);
            this.Controls.Add(this.ucCheckBox2);
            this.Controls.Add(this.txtSendTime);
            this.Controls.Add(this.ckbTimeSend);
            this.Controls.Add(this.skinLabel3);
            this.Controls.Add(this.labComInfo);
            this.Controls.Add(this.groupBoxEx3);
            this.Controls.Add(this.groupBoxEx2);
            this.EffectBack = System.Drawing.Color.Silver;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(905, 576);
            this.MaxSize = new System.Drawing.Size(32, 24);
            this.MinimumSize = new System.Drawing.Size(905, 576);
            this.MiniSize = new System.Drawing.Size(32, 24);
            this.Name = "Frm_SerialPort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "串口通讯发送端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_SerialServer_FormClosing);
            this.Load += new System.EventHandler(this.Frm_SerialPort_Load);
            this.groupBoxEx2.ResumeLayout(false);
            this.groupBoxEx2.PerformLayout();
            this.groupBoxEx3.ResumeLayout(false);
            this.groupBoxEx4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx2;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel8;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel6;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel5;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel4;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel7;
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx3;
        private Com_CSSkin.SkinControl.SkinLabel labComInfo;
        private Com_CSSkin.SkinControl.SkinComboBox cmbPortParity;
        private System.Windows.Forms.RichTextBox richTextBox_Receive;
        private Com_CSSkin.SkinControl.SkinComboBox cmbComLst;
        private Com_CSSkin.SkinControl.SkinComboBox cmbDataBits;
        private Com_CSSkin.SkinControl.SkinComboBox cmbStopBits;
        private Com_CSSkin.SkinControl.SkinComboBox cmbBandRate;
        private ZCS_FormUI.Controls.UCBtnExt btnOpenPort;
        private ZCS_FormUI.Controls.UCBtnExt btnRefresh;
        private Com_CSSkin.SkinControl.SkinLabel skinLabel3;
        private ZCS_FormUI.Controls.UCCheckBox ckbTimeSend;
        private ZCS_FormUI.Controls.UCTextBoxEx txtSendTime;
        private ZCS_FormUI.Controls.UCTextBoxEx ucTextBoxEx2;
        private ZCS_FormUI.Controls.UCCheckBox ucCheckBox2;
        private ZCS_FormUI.Controls.GroupBoxEx groupBoxEx4;
        private System.Windows.Forms.RichTextBox richTextBox_Send;
        private ZCS_FormUI.Controls.UCBtnExt btnScan;
        private ZCS_FormUI.Controls.UCCheckBox btnHAX;
        private ZCS_FormUI.Controls.UCBtnExt btnSend;
        private ZCS_FormUI.Controls.UCBtnExt btnClean;
        private System.IO.Ports.SerialPort serialPort1;
        private Com_CSSkin.SkinControl.SkinLabel lblrecestatus;
        private Com_CSSkin.SkinControl.SkinLabel lblSendStatus;
        private System.Windows.Forms.Timer timerSend;
    }
}