using Commun.Client.WinForms.Services;

namespace Commun.Client.WinForms;

static class Program
{
    public static ProtocolConnectionService ConnectionService { get; private set; } = null!;

    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        ConnectionService = new ProtocolConnectionService();
        Application.Run(new Forms.MainForm());
    }
}
