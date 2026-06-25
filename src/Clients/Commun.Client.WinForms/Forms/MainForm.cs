using Commun.Client.WinForms.Forms;

namespace Commun.Client.WinForms.Forms;

public partial class MainForm : Form
{
    private readonly Dictionary<string, Form> _openForms = new();

    public MainForm()
    {
        SetupMenu();
    }

    private void SetupMenu()
    {
        Text = "PLC-CommunTools - 工业通讯调试工具";
        Size = new Size(1024, 768);
        StartPosition = FormStartPosition.CenterScreen;
        IsMdiContainer = true;

        var menuStrip = new MenuStrip();
        MainMenuStrip = menuStrip;
        Controls.Add(menuStrip);

        var commMenu = new ToolStripMenuItem("通讯调试");
        commMenu.DropDownItems.Add("TCP 客户端", null, (s, e) => OpenForm<TcpClientForm>("tcp"));
        commMenu.DropDownItems.Add("TCP 服务端", null, (s, e) => OpenForm<TcpServerForm>("tcpserver"));
        commMenu.DropDownItems.Add("串口通讯", null, (s, e) => OpenForm<SerialPortForm>("serial"));
        commMenu.DropDownItems.Add("Modbus TCP", null, (s, e) => OpenForm<ModbusForm>("modbus"));
        commMenu.DropDownItems.Add("MQTT 客户端", null, (s, e) => OpenForm<MqttForm>("mqtt"));
        commMenu.DropDownItems.Add("OPC UA 客户端", null, (s, e) => OpenForm<OpcUaForm>("opcua"));
        commMenu.DropDownItems.Add(new ToolStripSeparator());
        commMenu.DropDownItems.Add("退出", null, (s, e) => Close());
        menuStrip.Items.Add(commMenu);

        var viewMenu = new ToolStripMenuItem("视图");
        viewMenu.DropDownItems.Add("连接状态", null, (s, e) => ShowConnectionStatus());
        menuStrip.Items.Add(viewMenu);

        var helpMenu = new ToolStripMenuItem("帮助");
        helpMenu.DropDownItems.Add("关于", null, (s, e) => ShowAbout());
        menuStrip.Items.Add(helpMenu);

        var statusStrip = new StatusStrip();
        statusStrip.Items.Add("就绪");
        Controls.Add(statusStrip);
    }

    private void OpenForm<T>(string key) where T : Form, new()
    {
        if (_openForms.TryGetValue(key, out var existing))
        {
            existing.BringToFront();
            return;
        }

        var form = new T { MdiParent = this };
        form.FormClosed += (s, e) => _openForms.Remove(key);
        _openForms[key] = form;
        form.Show();
    }

    private void ShowConnectionStatus()
    {
        var connections = Program.ConnectionService.Connections;
        var message = connections.Count == 0
            ? "当前无活跃连接"
            : $"活跃连接数: {connections.Count}\n" + string.Join("\n", connections.Keys);
        MessageBox.Show(message, "连接状态", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ShowAbout()
    {
        MessageBox.Show(
            "PLC-CommunTools v2.0\n工业通讯调试工具\n\n支持协议: Modbus, MQTT, Serial, OPC UA",
            "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    protected override async void OnFormClosing(FormClosingEventArgs e)
    {
        await Program.ConnectionService.DisconnectAllAsync();
        base.OnFormClosing(e);
    }
}
