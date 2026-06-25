using System.Windows;
using System.Windows.Controls;
using Commun.Modbus;
using Commun.Protocols;

namespace Commun.Client.WPF.Views;

public partial class ModbusView : UserControl
{
    private ModbusProtocol? _protocol;

    public ModbusView() { InitializeComponent(); }

    private async void BtnConnect_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            _protocol = new ModbusProtocol();
            var config = new ProtocolConfig { Address = TxtHost.Text, Port = int.Parse(TxtPort.Text), Timeout = TimeSpan.FromSeconds(10) };
            var ok = await _protocol.ConnectAsync(config);
            AppendLog(ok ? "Modbus 连接成功" : "Modbus 连接失败");
        }
        catch (Exception ex) { AppendLog($"错误: {ex.Message}"); }
    }

    private async void BtnRead_Click(object sender, RoutedEventArgs e)
    {
        if (_protocol == null) { AppendLog("请先连接"); return; }
        var request = new ProtocolRequest { CommandType = "ReadHoldingRegisters", Register = int.Parse(TxtRegister.Text), Length = int.Parse(TxtValue.Text) };
        var response = await _protocol.SendAsync(request);
        AppendLog(response.Success ? $"读取成功: {BitConverter.ToString(response.Data ?? Array.Empty<byte>())}" : $"读取失败: {response.ErrorMessage}");
    }

    private async void BtnWrite_Click(object sender, RoutedEventArgs e)
    {
        if (_protocol == null) { AppendLog("请先连接"); return; }
        var val = ushort.Parse(TxtValue.Text);
        var request = new ProtocolRequest { CommandType = "WriteSingleRegister", Register = int.Parse(TxtRegister.Text), Data = BitConverter.GetBytes(val) };
        var response = await _protocol.SendAsync(request);
        AppendLog(response.Success ? "写入成功" : $"写入失败: {response.ErrorMessage}");
    }

    private void AppendLog(string msg) => TxtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\n");
}
