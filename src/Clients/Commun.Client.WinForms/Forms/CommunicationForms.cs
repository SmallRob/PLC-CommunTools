namespace Commun.Client.WinForms.Forms;

public class TcpServerForm : Form
{
    private TextBox txtPort = null!, txtLog = null!;
    private Button btnStart = null!, btnStop = null!;
    private Label lblStatus = null!;

    public TcpServerForm()
    {
        Text = "TCP 服务端";
        Size = new Size(600, 450);

        var lblPort = new Label { Text = "监听端口:", Location = new Point(15, 20), AutoSize = true };
        txtPort = new TextBox { Text = "502", Location = new Point(80, 17), Width = 80 };
        btnStart = new Button { Text = "启动", Location = new Point(170, 15), Width = 70 };
        btnStop = new Button { Text = "停止", Location = new Point(245, 15), Width = 70, Enabled = false };
        lblStatus = new Label { Text = "未启动", Location = new Point(330, 20), AutoSize = true };

        txtLog = new TextBox { Location = new Point(15, 50), Width = 550, Height = 350, Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = true, BackColor = Color.Black, ForeColor = Color.Cyan };

        Controls.AddRange(new Control[] { lblPort, txtPort, btnStart, btnStop, lblStatus, txtLog });

        btnStart.Click += (s, e) => { lblStatus.Text = "运行中"; lblStatus.ForeColor = Color.Green; btnStart.Enabled = false; btnStop.Enabled = true; AppendLog("服务端已启动"); };
        btnStop.Click += (s, e) => { lblStatus.Text = "已停止"; lblStatus.ForeColor = Color.Gray; btnStart.Enabled = true; btnStop.Enabled = false; AppendLog("服务端已停止"); };
    }

    private void AppendLog(string msg) => txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
}

public class SerialPortForm : Form
{
    private ComboBox cmbPorts = null!;
    private TextBox txtLog = null!;
    private Button btnOpen = null!, btnClose = null!, btnSend = null!;

    public SerialPortForm()
    {
        Text = "串口通讯";
        Size = new Size(600, 450);

        var lblPort = new Label { Text = "串口:", Location = new Point(15, 20), AutoSize = true };
        cmbPorts = new ComboBox { Location = new Point(55, 17), Width = 100, DropDownStyle = ComboBoxStyle.DropDownList };
        cmbPorts.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
        if (cmbPorts.Items.Count > 0) cmbPorts.SelectedIndex = 0;

        btnOpen = new Button { Text = "打开", Location = new Point(165, 15), Width = 60 };
        btnClose = new Button { Text = "关闭", Location = new Point(230, 15), Width = 60, Enabled = false };
        btnSend = new Button { Text = "发送", Location = new Point(300, 15), Width = 60, Enabled = false };

        txtLog = new TextBox { Location = new Point(15, 50), Width = 550, Height = 350, Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = true, BackColor = Color.Black, ForeColor = Color.Yellow };

        Controls.AddRange(new Control[] { lblPort, cmbPorts, btnOpen, btnClose, btnSend, txtLog });

        btnOpen.Click += (s, e) => { btnOpen.Enabled = false; btnClose.Enabled = true; btnSend.Enabled = true; AppendLog("串口已打开"); };
        btnClose.Click += (s, e) => { btnOpen.Enabled = true; btnClose.Enabled = false; btnSend.Enabled = false; AppendLog("串口已关闭"); };
    }

    private void AppendLog(string msg) => txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
}

public class ModbusForm : Form
{
    private TextBox txtHost = null!, txtPort = null!, txtRegister = null!, txtValue = null!, txtResult = null!;
    private ComboBox cmbFunction = null!;
    private Button btnConnect = null!, btnRead = null!, btnWrite = null!;

    public ModbusForm()
    {
        Text = "Modbus TCP";
        Size = new Size(650, 500);

        var grpConn = new GroupBox { Text = "连接", Location = new Point(10, 10), Size = new Size(620, 55) };
        var lblHost = new Label { Text = "主机:", Location = new Point(10, 22), AutoSize = true };
        txtHost = new TextBox { Text = "192.168.1.100", Location = new Point(45, 19), Width = 130 };
        var lblPort = new Label { Text = "端口:", Location = new Point(185, 22), AutoSize = true };
        txtPort = new TextBox { Text = "502", Location = new Point(220, 19), Width = 50 };
        btnConnect = new Button { Text = "连接", Location = new Point(280, 17), Width = 60 };
        grpConn.Controls.AddRange(new Control[] { lblHost, txtHost, lblPort, txtPort, btnConnect });

        var grpRW = new GroupBox { Text = "读写", Location = new Point(10, 75), Size = new Size(620, 80) };
        var lblFunc = new Label { Text = "功能码:", Location = new Point(10, 25), AutoSize = true };
        cmbFunction = new ComboBox { Location = new Point(60, 22), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
        cmbFunction.Items.AddRange(new object[] { "读保持寄存器 (03)", "读输入寄存器 (04)", "写单个寄存器 (06)", "写多个寄存器 (10)" });
        cmbFunction.SelectedIndex = 0;

        var lblReg = new Label { Text = "起始地址:", Location = new Point(10, 52), AutoSize = true };
        txtRegister = new TextBox { Text = "0", Location = new Point(70, 49), Width = 60 };
        var lblVal = new Label { Text = "值/长度:", Location = new Point(140, 52), AutoSize = true };
        txtValue = new TextBox { Text = "1", Location = new Point(200, 49), Width = 60 };
        btnRead = new Button { Text = "读取", Location = new Point(280, 47), Width = 60 };
        btnWrite = new Button { Text = "写入", Location = new Point(345, 47), Width = 60 };
        grpRW.Controls.AddRange(new Control[] { lblFunc, cmbFunction, lblReg, txtRegister, lblVal, txtValue, btnRead, btnWrite });

        txtResult = new TextBox { Location = new Point(10, 165), Width = 620, Height = 290, Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = true, BackColor = Color.Black, ForeColor = Color.Lime };

        Controls.AddRange(new Control[] { grpConn, grpRW, txtResult });

        btnConnect.Click += async (s, e) =>
        {
            try
            {
                var config = new Protocols.ProtocolConfig { Address = txtHost.Text, Port = int.Parse(txtPort.Text), Timeout = TimeSpan.FromSeconds(10) };
                var ok = await Program.ConnectionService.ConnectAsync("modbus", "modbus", config);
                AppendResult(ok ? "Modbus 连接成功" : "Modbus 连接失败");
            }
            catch (Exception ex) { AppendResult($"错误: {ex.Message}"); }
        };

        btnRead.Click += async (s, e) =>
        {
            var protocol = Program.ConnectionService.GetConnection("modbus") as Commun.Modbus.ModbusProtocol;
            if (protocol == null) { AppendResult("请先连接"); return; }
            try
            {
                var request = new Protocols.ProtocolRequest { CommandType = "ReadHoldingRegisters", Register = int.Parse(txtRegister.Text), Length = int.Parse(txtValue.Text) };
                var response = await protocol.SendAsync(request);
                AppendResult(response.Success ? $"读取成功: {BitConverter.ToString(response.Data ?? Array.Empty<byte>())}" : $"读取失败: {response.ErrorMessage}");
            }
            catch (Exception ex) { AppendResult($"错误: {ex.Message}"); }
        };

        btnWrite.Click += async (s, e) =>
        {
            var protocol = Program.ConnectionService.GetConnection("modbus") as Commun.Modbus.ModbusProtocol;
            if (protocol == null) { AppendResult("请先连接"); return; }
            try
            {
                var val = ushort.Parse(txtValue.Text);
                var request = new Protocols.ProtocolRequest { CommandType = "WriteSingleRegister", Register = int.Parse(txtRegister.Text), Data = BitConverter.GetBytes(val) };
                var response = await protocol.SendAsync(request);
                AppendResult(response.Success ? "写入成功" : $"写入失败: {response.ErrorMessage}");
            }
            catch (Exception ex) { AppendResult($"错误: {ex.Message}"); }
        };
    }

    private void AppendResult(string msg) => txtResult.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
}

public class MqttForm : Form
{
    private TextBox txtBroker = null!, txtPort = null!, txtTopic = null!, txtMessage = null!, txtLog = null!;
    private Button btnConnect = null!, btnPublish = null!, btnSubscribe = null!;

    public MqttForm()
    {
        Text = "MQTT 客户端";
        Size = new Size(600, 500);

        var grpConn = new GroupBox { Text = "连接", Location = new Point(10, 10), Size = new Size(570, 55) };
        var lblBroker = new Label { Text = "代理:", Location = new Point(10, 22), AutoSize = true };
        txtBroker = new TextBox { Text = "localhost", Location = new Point(45, 19), Width = 120 };
        var lblPort = new Label { Text = "端口:", Location = new Point(175, 22), AutoSize = true };
        txtPort = new TextBox { Text = "1883", Location = new Point(210, 19), Width = 50 };
        btnConnect = new Button { Text = "连接", Location = new Point(270, 17), Width = 60 };
        grpConn.Controls.AddRange(new Control[] { lblBroker, txtBroker, lblPort, txtPort, btnConnect });

        var grpPub = new GroupBox { Text = "发布/订阅", Location = new Point(10, 75), Size = new Size(570, 80) };
        var lblTopic = new Label { Text = "主题:", Location = new Point(10, 22), AutoSize = true };
        txtTopic = new TextBox { Text = "test/topic", Location = new Point(45, 19), Width = 200 };
        var lblMsg = new Label { Text = "消息:", Location = new Point(10, 50), AutoSize = true };
        txtMessage = new TextBox { Text = "Hello MQTT", Location = new Point(45, 47), Width = 200 };
        btnPublish = new Button { Text = "发布", Location = new Point(255, 45), Width = 60 };
        btnSubscribe = new Button { Text = "订阅", Location = new Point(320, 45), Width = 60 };
        grpPub.Controls.AddRange(new Control[] { lblTopic, txtTopic, lblMsg, txtMessage, btnPublish, btnSubscribe });

        txtLog = new TextBox { Location = new Point(10, 165), Width = 570, Height = 290, Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = true, BackColor = Color.Black, ForeColor = Color.Cyan };

        Controls.AddRange(new Control[] { grpConn, grpPub, txtLog });

        btnConnect.Click += async (s, e) =>
        {
            try
            {
                var config = new Protocols.ProtocolConfig { Address = txtBroker.Text, Port = int.Parse(txtPort.Text), Timeout = TimeSpan.FromSeconds(10) };
                var ok = await Program.ConnectionService.ConnectAsync("mqtt", "mqtt", config);
                AppendLog(ok ? "MQTT 连接成功" : "MQTT 连接失败");
            }
            catch (Exception ex) { AppendLog($"错误: {ex.Message}"); }
        };

        btnPublish.Click += async (s, e) =>
        {
            var protocol = Program.ConnectionService.GetConnection("mqtt") as Commun.MQTT.MqttProtocol;
            if (protocol == null) { AppendLog("请先连接"); return; }
            try
            {
                var request = new Protocols.ProtocolRequest { CommandType = "Publish", Data = System.Text.Encoding.UTF8.GetBytes(txtMessage.Text), Parameters = new Dictionary<string, object> { { "topic", txtTopic.Text } } };
                var response = await protocol.SendAsync(request);
                AppendLog(response.Success ? $"已发布到 {txtTopic.Text}" : $"发布失败: {response.ErrorMessage}");
            }
            catch (Exception ex) { AppendLog($"错误: {ex.Message}"); }
        };
    }

    private void AppendLog(string msg) => txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
}

public class OpcUaForm : Form
{
    private TextBox txtEndpoint = null!, txtNodeId = null!, txtValue = null!, txtLog = null!;
    private Button btnConnect = null!, btnRead = null!, btnWrite = null!;

    public OpcUaForm()
    {
        Text = "OPC UA 客户端";
        Size = new Size(650, 500);

        var grpConn = new GroupBox { Text = "连接", Location = new Point(10, 10), Size = new Size(620, 55) };
        var lblEndpoint = new Label { Text = "端点:", Location = new Point(10, 22), AutoSize = true };
        txtEndpoint = new TextBox { Text = "opc.tcp://localhost:4840", Location = new Point(50, 19), Width = 300 };
        btnConnect = new Button { Text = "连接", Location = new Point(360, 17), Width = 60 };
        grpConn.Controls.AddRange(new Control[] { lblEndpoint, txtEndpoint, btnConnect });

        var grpRW = new GroupBox { Text = "读写", Location = new Point(10, 75), Size = new Size(620, 80) };
        var lblNode = new Label { Text = "节点ID:", Location = new Point(10, 25), AutoSize = true };
        txtNodeId = new TextBox { Text = "ns=2;s=Demo.Static.Scalar.Int32", Location = new Point(65, 22), Width = 280 };
        var lblVal = new Label { Text = "值:", Location = new Point(10, 52), AutoSize = true };
        txtValue = new TextBox { Location = new Point(35, 49), Width = 100 };
        btnRead = new Button { Text = "读取", Location = new Point(355, 47), Width = 60 };
        btnWrite = new Button { Text = "写入", Location = new Point(420, 47), Width = 60 };
        grpRW.Controls.AddRange(new Control[] { lblNode, txtNodeId, lblVal, txtValue, btnRead, btnWrite });

        txtLog = new TextBox { Location = new Point(10, 165), Width = 620, Height = 290, Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = true, BackColor = Color.Black, ForeColor = Color.Lime };

        Controls.AddRange(new Control[] { grpConn, grpRW, txtLog });

        btnConnect.Click += async (s, e) =>
        {
            try
            {
                var uri = new Uri(txtEndpoint.Text);
                var config = new Protocols.ProtocolConfig { Address = uri.Host, Port = uri.Port, Timeout = TimeSpan.FromSeconds(30) };
                var ok = await Program.ConnectionService.ConnectAsync("opcua", "opcua", config);
                AppendLog(ok ? "OPC UA 连接成功" : "OPC UA 连接失败");
            }
            catch (Exception ex) { AppendLog($"错误: {ex.Message}"); }
        };

        btnRead.Click += async (s, e) =>
        {
            var protocol = Program.ConnectionService.GetConnection("opcua") as Commun.OPCUA.OpcUaProtocol;
            if (protocol == null) { AppendLog("请先连接"); return; }
            var result = await protocol.ReadNodeAsync(txtNodeId.Text);
            AppendLog(result.Success ? $"读取成功: {BitConverter.ToString(result.Data ?? Array.Empty<byte>())}" : $"读取失败: {result.ErrorMessage}");
        };

        btnWrite.Click += async (s, e) =>
        {
            var protocol = Program.ConnectionService.GetConnection("opcua") as Commun.OPCUA.OpcUaProtocol;
            if (protocol == null) { AppendLog("请先连接"); return; }
            if (int.TryParse(txtValue.Text, out var val))
            {
                var result = await protocol.WriteNodeAsync(txtNodeId.Text, val);
                AppendLog(result.Success ? "写入成功" : $"写入失败: {result.ErrorMessage}");
            }
        };
    }

    private void AppendLog(string msg) => txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
}
