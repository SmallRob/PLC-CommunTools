using System.CommandLine;
using Commun.Client.CLI.Commands;

var rootCommand = new RootCommand("PLC-CommunTools CLI - Device communication tool");

rootCommand.Add(new ConnectCommand());
rootCommand.Add(new ReadCommand());
rootCommand.Add(new WriteCommand());
rootCommand.Add(new MonitorCommand());

var parseResult = rootCommand.Parse(args);
return await parseResult.InvokeAsync();
