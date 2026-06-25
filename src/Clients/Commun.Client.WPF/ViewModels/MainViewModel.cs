using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Commun.Protocols;

namespace Commun.Client.WPF.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private string _statusText = "就绪";
    private string _connectionStatus = "未连接";

    public string StatusText { get => _statusText; set { _statusText = value; OnPropertyChanged(); } }
    public string ConnectionStatus { get => _connectionStatus; set { _connectionStatus = value; OnPropertyChanged(); } }

    public ObservableCollection<ConnectionItem> Connections { get; } = new();
    public ObservableCollection<LogEntry> LogEntries { get; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

public class ConnectionItem : INotifyPropertyChanged
{
    public string Id { get; set; } = "";
    public string Protocol { get; set; } = "";
    public string Address { get; set; } = "";
    private bool _isConnected;
    public bool IsConnected { get => _isConnected; set { _isConnected = value; OnPropertyChanged(); } }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

public class LogEntry
{
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string Level { get; set; } = "INFO";
    public string Message { get; set; } = "";
    public string Source { get; set; } = "";
}

public class ProtocolTabViewModel : INotifyPropertyChanged
{
    private string _host = "127.0.0.1";
    private int _port = 502;
    private bool _isConnected;
    private string _sendData = "";
    private string _logText = "";

    public string Host { get => _host; set { _host = value; OnPropertyChanged(); } }
    public int Port { get => _port; set { _port = value; OnPropertyChanged(); } }
    public bool IsConnected { get => _isConnected; set { _isConnected = value; OnPropertyChanged(); } }
    public string SendData { get => _sendData; set { _sendData = value; OnPropertyChanged(); } }
    public string LogText { get => _logText; set { _logText = value; OnPropertyChanged(); } }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public void AppendLog(string message)
    {
        LogText += $"[{DateTime.Now:HH:mm:ss}] {message}\n";
    }
}
