using Xunit;

namespace Commun.Client.CLI.Tests;

public class CommandTests
{
    [Fact]
    public void ConnectCommand_HasCorrectName()
    {
        var command = new Commun.Client.CLI.Commands.ConnectCommand();
        Assert.Equal("connect", command.Name);
    }

    [Fact]
    public void ReadCommand_HasCorrectName()
    {
        var command = new Commun.Client.CLI.Commands.ReadCommand();
        Assert.Equal("read", command.Name);
    }

    [Fact]
    public void WriteCommand_HasCorrectName()
    {
        var command = new Commun.Client.CLI.Commands.WriteCommand();
        Assert.Equal("write", command.Name);
    }

    [Fact]
    public void MonitorCommand_HasCorrectName()
    {
        var command = new Commun.Client.CLI.Commands.MonitorCommand();
        Assert.Equal("monitor", command.Name);
    }
}
