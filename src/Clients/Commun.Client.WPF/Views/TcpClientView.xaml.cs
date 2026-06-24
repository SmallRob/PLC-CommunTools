using System.Windows;
using System.Windows.Controls;
using Commun.Protocols;

namespace Commun.Client.WPF.Views;

public partial class TcpClientView : UserControl
{
    private IProtocol? _protocol;
    private string _connectionId = "wpf_tcp";

    public TcpClientView()
    {
        InitializeComponent();
    }

    private async void BtnConnect_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var config = new ProtocolConfig
            {
                Address = TxtHost.Text,
                Port = int.Parse(TxtPort.Text),
                Timeout = TimeSpan.FromSeconds(10)
            };

            _protocol = ProtocolFactory.CreateProtocol("tcp");
            var connected = await _protocol.ConnectAsync(config);

            if (connected)
            {
                BtnConnect.IsEnabled = false;
                BtnDisconnect.IsEnabled = true;
                BtnSend.IsEnabled = true;
                AppendLog($"已连接到 {TxtHost.Text}:{TxtPort.Text}");

                _protocol.DataReceived += (s, e) =>
                    Dispatcher.Invoke(() => AppendLog($"[接收] {BitConverter.ToString(e.Data)}"));
            }
            else
            {
                AppendLog("连接失败");
            }
        }
        catch (Exception ex)
        {
            AppendLog($"错误: {ex.Message}");
        }
    }

    private async void BtnDisconnect_Click(object sender, RoutedEventArgs e)
    {
        if (_protocol != null)
        {
            await _protocol.DisconnectAsync();
            _protocol.Dispose();
            _protocol = null;
        }
        BtnConnect.IsEnabled = true;
        BtnDisconnect.IsEnabled = false;
        BtnSend.IsEnabled = false;
        AppendLog("已断开连接");
    }

    private async void BtnSend_Click(object sender, RoutedEventArgs e)
    {
        if (_protocol == null) return;
        try
        {
            byte[] data;
            if (ChkHex.IsChecked == true)
            {
                var hex = TxtSend.Text.Replace(" ", "");
                data = Enumerable.Range(0, hex.Length / 2)
                    .Select(i => Convert.ToByte(hex.Substring(i * 2, 2), 16)).ToArray();
            }
            else
            {
                data = System.Text.Encoding.UTF8.GetBytes(TxtSend.Text);
            }

            var response = await _protocol.SendAsync(new ProtocolRequest { Data = data });
            AppendLog(response.Success ? $"[发送] {BitConverter.ToString(data)}" : $"发送失败: {response.ErrorMessage}");
        }
        catch (Exception ex) { AppendLog($"错误: {ex.Message}"); }
    }

    private void AppendLog(string msg)
    {
        TxtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\n");
        TxtLog.ScrollToEnd();
    }
}

public static class ProtocolFactory
{
    public static IProtocol CreateProtocol(string type)
    {
        return type.ToLower() switch
        {
            "tcp" => new Commun.Modbus.ModbusProtocol(), // Using Modbus as TCP placeholder
            "modbus" => new Commun.Modbus.ModbusProtocol(),
            "mqtt" => new Commun.MQTT.MqttProtocol(),
            "serial" => new Commun.Serial.SerialProtocol(),
            "opcua" => new Commun.OPCUA.OpcUaProtocol(),
            _ => throw new ArgumentException($"Unknown protocol: {type}")
        };
    }
}
