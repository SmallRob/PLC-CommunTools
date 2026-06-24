using System.Windows;
using System.Windows.Controls;
using Commun.Client.WPF.ViewModels;

namespace Commun.Client.WPF.Views;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }

    private void AddTab(string header, UIElement content)
    {
        var tabItem = new TabItem
        {
            Header = header,
            Content = content,
            Style = (Style)FindResource("MetroTabItem")
        };
        MainTabControl.Items.Add(tabItem);
        MainTabControl.SelectedItem = tabItem;
    }

    private void TcpClient_Click(object sender, RoutedEventArgs e)
        => AddTab("TCP 客户端", new TcpClientView());

    private void Serial_Click(object sender, RoutedEventArgs e)
        => AddTab("串口通讯", new SerialView());

    private void Modbus_Click(object sender, RoutedEventArgs e)
        => AddTab("Modbus TCP", new ModbusView());

    private void Mqtt_Click(object sender, RoutedEventArgs e)
        => AddTab("MQTT", new MqttView());

    private void OpcUa_Click(object sender, RoutedEventArgs e)
        => AddTab("OPC UA", new OpcUaView());

    private void ConnectionStatus_Click(object sender, RoutedEventArgs e)
        => MessageBox.Show($"活跃连接数: {_viewModel.Connections.Count}", "连接状态");

    private void About_Click(object sender, RoutedEventArgs e)
        => MessageBox.Show("PLC-CommunTools v2.0\n工业通讯调试工具\n\n支持: Modbus, MQTT, Serial, OPC UA", "关于");

    private void Exit_Click(object sender, RoutedEventArgs e) => Close();
}
