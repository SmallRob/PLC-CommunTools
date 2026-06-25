using Commun.Protocols;

namespace Commun.Client.WinForms.Forms;

public class TcpClientForm : Form
{
    private TextBox txtHost = null!, txtPort = null!, txtSend = null!, txtLog = null!;
    private Button btnConnect = null!, btnDisconnect = null!, btnSend = null!, btnClear = null!;
    private Label lblStatus = null!;
    private string _connectionId = "tcp_client";

    public TcpClientForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text = "TCP 客户端";
        Size = new Size(680, 520);

        var lblHost = new Label { Text = "主机:", Location = new Point(15, 20), AutoSize = true };
        txtHost = new TextBox { Text = "127.0.0.1", Location = new Point(60, 17), Width = 150 };

        var lblPort = new Label { Text = "端口:", Location = new Point(220, 20), AutoSize = true };
        txtPort = new TextBox { Text = "502", Location = new Point(265, 17), Width = 60 };

        btnConnect = new Button { Text = "连接", Location = new Point(340, 15), Width = 70 };
        btnDisconnect = new Button { Text = "断开", Location = new Point(415, 15), Width = 70, Enabled = false };

        lblStatus = new Label { Text = "未连接", Location = new Point(500, 20), AutoSize = true, ForeColor = Color.Gray };

        var lblSend = new Label { Text = "发送数据:", Location = new Point(15, 55), AutoSize = true };
        txtSend = new TextBox { Location = new Point(15, 75), Width = 550, Height = 60, Multiline = true };

        btnSend = new Button { Text = "发送", Location = new Point(575, 75), Width = 70, Height = 30, Enabled = false };
        btnClear = new Button { Text = "清空日志", Location = new Point(575, 110), Width = 70 };

        var chkHex = new CheckBox { Text = "HEX 发送", Location = new Point(15, 145), AutoSize = true };
        var chkHexRecv = new CheckBox { Text = "HEX 接收", Location = new Point(120, 145), AutoSize = true };

        txtLog = new TextBox
        {
            Location = new Point(15, 170),
            Width = 630,
            Height = 300,
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            ReadOnly = true,
            BackColor = Color.Black,
            ForeColor = Color.Lime
        };

        Controls.AddRange(new Control[] { lblHost, txtHost, lblPort, txtPort, btnConnect, btnDisconnect, lblStatus, lblSend, txtSend, btnSend, btnClear, chkHex, chkHexRecv, txtLog });

        btnConnect.Click += async (s, e) => await ConnectAsync();
        btnDisconnect.Click += async (s, e) => await DisconnectAsync();
        btnSend.Click += async (s, e) => await SendAsync(chkHex.Checked);
        btnClear.Click += (s, e) => txtLog.Clear();
    }

    private async Task ConnectAsync()
    {
        try
        {
            var config = new ProtocolConfig
            {
                Address = txtHost.Text,
                Port = int.Parse(txtPort.Text),
                Timeout = TimeSpan.FromSeconds(10)
            };

            var connected = await Program.ConnectionService.ConnectAsync(_connectionId, "tcp", config);

            if (connected)
            {
                lblStatus.Text = "已连接";
                lblStatus.ForeColor = Color.Green;
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                btnSend.Enabled = true;
                AppendLog($"已连接到 {txtHost.Text}:{txtPort.Text}");

                var protocol = Program.ConnectionService.GetConnection(_connectionId);
                if (protocol != null)
                {
                    protocol.DataReceived += (s, e) =>
                    {
                        Invoke(() => AppendLog($"[接收] {BitConverter.ToString(e.Data)}"));
                    };
                }
            }
            else
            {
                AppendLog("连接失败");
            }
        }
        catch (Exception ex)
        {
            AppendLog($"连接错误: {ex.Message}");
        }
    }

    private async Task DisconnectAsync()
    {
        await Program.ConnectionService.DisconnectAsync(_connectionId);
        lblStatus.Text = "未连接";
        lblStatus.ForeColor = Color.Gray;
        btnConnect.Enabled = true;
        btnDisconnect.Enabled = false;
        btnSend.Enabled = false;
        AppendLog("已断开连接");
    }

    private async Task SendAsync(bool hex)
    {
        var protocol = Program.ConnectionService.GetConnection(_connectionId);
        if (protocol == null) return;

        try
        {
            byte[] data;
            if (hex)
            {
                var hexStr = txtSend.Text.Replace(" ", "");
                data = Enumerable.Range(0, hexStr.Length / 2)
                    .Select(i => Convert.ToByte(hexStr.Substring(i * 2, 2), 16))
                    .ToArray();
            }
            else
            {
                data = System.Text.Encoding.UTF8.GetBytes(txtSend.Text);
            }

            var request = new ProtocolRequest { Data = data };
            var response = await protocol.SendAsync(request);

            if (response.Success)
            {
                AppendLog($"[发送] {(hex ? BitConverter.ToString(data) : txtSend.Text)}");
            }
            else
            {
                AppendLog($"发送失败: {response.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            AppendLog($"发送错误: {ex.Message}");
        }
    }

    private void AppendLog(string message)
    {
        txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
    }
}
